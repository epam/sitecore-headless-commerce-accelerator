//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace Wooli.Foundation.Commerce.ModelMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Base.Models;

    using Connect.Models;

    using DependencyInjection;

    using Models;
    using Models.Checkout;
    using Models.Entities.Addresses;
    using Models.Entities.Cart;
    using Models.Entities.Users;

    using Providers;

    using Repositories;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Diagnostics;

    using Utils;

    using Cart = Models.Entities.Cart.Cart;
    using CartLine = Sitecore.Commerce.Entities.Carts.CartLine;
    using CountryRegionModel = Models.Region.CountryRegionModel;
    using FederatedPaymentInfo = Sitecore.Commerce.Entities.Carts.FederatedPaymentInfo;
    using Party = Connect.Models.Party;
    using ShippingInfo = Sitecore.Commerce.Entities.Carts.ShippingInfo;
    using ShippingMethod = Sitecore.Commerce.Entities.Shipping.ShippingMethod;
    using ShippingOption = Sitecore.Commerce.Entities.Shipping.ShippingOption;
    using SubdivisionModel = Models.Region.SubdivisionModel;

    [Service(typeof(IEntityMapper))]
    public class EntityMapper : IEntityMapper
    {
        private readonly ICatalogRepository catalogRepository;
        private readonly ICurrencyProvider currencyProvider;
        private readonly IMapper innerMapper;

        public EntityMapper(ICatalogRepository catalogRepository, ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(catalogRepository, nameof(catalogRepository));
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            this.catalogRepository = catalogRepository;
            this.currencyProvider = currencyProvider;

            var config = new MapperConfiguration(
                cfg =>
                {
                    #region Cart

                    cfg.CreateMap<CartResult, Result<Cart>>()
                        .ConstructUsing(
                            source => new Result<Cart>(
                                this.innerMapper.Map<Cart>(source.Cart),
                                source.SystemMessages.Select(_ => _.Message).ToList()))
                        .ReverseMap();

                    cfg.CreateMap<Sitecore.Commerce.Entities.Carts.Cart, Cart>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                        .ForMember(dest => dest.CartLines, opt => opt.MapFrom(src => src.Lines))
                        .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Parties))
                        .ForMember(dest => dest.Adjustments, opt => opt.MapFrom(src => src.Adjustments.Select(a => a.Description).ToList()))
                        .ReverseMap();

                    cfg.CreateMap<CommerceCart, Cart>()
                        .IncludeBase<Sitecore.Commerce.Entities.Carts.Cart, Cart>()
                        .ReverseMap();

                    cfg.CreateMap<CartLine, Models.Entities.Cart.CartLine>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalCartLineId))
                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                        .ForMember(dest => dest.Product, opt => opt.Ignore())
                        .ForMember(dest => dest.Variant, opt => opt.Ignore())
                        .AfterMap(
                            (src, dest) =>
                            {
                                dest.Product = this.catalogRepository.GetProduct(src.Product.ProductId);
                                dest.Variant = dest.Product.Variants?.FirstOrDefault(
                                    x => x.ProductVariantId == (src.Product as CommerceCartProduct)?.ProductVariantId);
                            })
                        .ReverseMap();

                    cfg.CreateMap<CommerceCartLine, Models.Entities.Cart.CartLine>()
                        .IncludeBase<CartLine, Models.Entities.Cart.CartLine>()
                        .ReverseMap();

                    #endregion

                    #region Price

                    cfg.CreateMap<TotalPrice, Total>()
                        .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Total))
                        .ForMember(
                            dest => dest.TaxTotal,
                            opt => opt.MapFrom(
                                src => new TaxTotal
                                {
                                    Amount = src.TaxTotal
                                }))
                        .ForMember(src => src.Description, opt => opt.Ignore());

                    cfg.CreateMap<Total, TotalPrice>()
                        .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Amount))
                        .ForMember(dest => dest.TaxTotal, opt => opt.MapFrom(src => src.TaxTotal.Amount))
                        .ForSourceMember(src => src.Description, opt => opt.Ignore())
                        .AfterMap(
                            (src, dest) =>
                            {
                                dest.CurrencySymbol = this.currencyProvider.GetCurrencySymbolByCode(dest.CurrencyCode);
                            });

                    cfg.CreateMap<CommerceTotal, TotalPrice>()
                        .ForMember(
                            dest => dest.TotalSavings,
                            opt => opt.MapFrom(src => src.LineItemDiscountAmount + src.OrderLevelDiscountAmount))
                        .IncludeBase<Total, TotalPrice>();

                    #endregion

                    #region Address

                    cfg.CreateMap<Sitecore.Commerce.Entities.Party, Address>()
                        .ReverseMap();

                    cfg.CreateMap<CommerceParty, Address>()
                        .IncludeBase<Sitecore.Commerce.Entities.Party, Address>()
                        .ReverseMap();

                    cfg.CreateMap<Party, Address>()
                        .ReverseMap();

                    #endregion

                    #region Shipping

                    cfg.CreateMap<ShippingMethod, Models.Entities.Shipping.ShippingMethod>()
                        .ReverseMap();

                    cfg.CreateMap<ShippingInfo, Models.Entities.Shipping.ShippingMethod>()
                        .ReverseMap();
                    cfg.CreateMap<CommerceShippingInfo, Models.Entities.Shipping.ShippingMethod>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShippingMethodName))
                        .ReverseMap();

                    cfg.CreateMap<Models.Entities.Shipping.ShippingMethod, ShippingInfoArgument>()

                        //.ForMember(dest => dest.LineIds,
                        //    opt => opt.MapFrom(src =>
                        //        src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()))
                        .ForMember(dest => dest.ShippingMethodId, opt => opt.MapFrom(src => src.ExternalId))
                        .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                        .ReverseMap();

                    cfg.CreateMap<ShippingOption, ShippingOptionModel>()
                        .ForMember(
                            dest => dest.ShippingOptionType,
                            opt => opt.MapFrom(src => src.ShippingOptionType.Value));

                    cfg.CreateMap<string, ShippingOptionType>()
                        .ConvertUsing(ConnectOptionTypeHelper.ToShippingOptionType);

                    #endregion

                    #region Payment

                    cfg.CreateMap<FederatedPaymentArgs, FederatedPaymentInfo>()
                        .ReverseMap();

                    cfg.CreateMap<PaymentInfo, Models.Entities.Payment.FederatedPaymentInfo>()
                        .ReverseMap();
                    cfg.CreateMap<FederatedPaymentInfo, Models.Entities.Payment.FederatedPaymentInfo>()
                        .ReverseMap();
                    #endregion

                    cfg.CreateMap<Sitecore.Commerce.Entities.Party, AddressModel>()
                        .ReverseMap();

                    cfg.CreateMap<Party, AddressModel>()
                        .ReverseMap();
                    cfg.CreateMap<CommerceParty, AddressModel>()
                        .ReverseMap();

                    cfg.CreateMap<FederatedPaymentArgs, FederatedPaymentModel>()
                        .ReverseMap();

                    cfg.CreateMap<ShippingMethodModel, ShippingInfoArgument>()
                        .ForMember(
                            dest => dest.LineIds,
                            opt => opt.MapFrom(
                                src =>
                                    src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()))
                        .ForMember(dest => dest.ShippingMethodId, opt => opt.MapFrom(src => src.ExternalId))
                        .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                        .ReverseMap();

                    cfg.CreateMap<ShippingMethod, ShippingMethodModel>()
                        .ReverseMap();

                    cfg.CreateMap<ShippingOption, ShippingOptionModel>()
                        .ForMember(
                            dest => dest.ShippingOptionType,
                            opt => opt.MapFrom(src => src.ShippingOptionType.Value));

                    cfg.CreateMap<PaymentInfo, FederatedPaymentModel>()
                        .ReverseMap();
                    cfg.CreateMap<FederatedPaymentInfo, FederatedPaymentModel>()
                        .ReverseMap();

                    cfg.CreateMap<ShippingInfo, ShippingMethodModel>()
                        .ReverseMap();
                    cfg.CreateMap<CommerceShippingInfo, ShippingMethodModel>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShippingMethodName))
                        .ReverseMap();

                    cfg.CreateMap<CommerceUser, User>()
                        .ForMember(
                            dest => dest.ContactId,
                            opt => opt.MapFrom(
                                src =>
                                    src.ExternalId.Replace(Constants.CommereceCustomerIdPrefix, string.Empty)))
                        .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customers.FirstOrDefault()))
                        .ReverseMap();

                    cfg.CreateMap<ICountryRegionModel, CountryRegionModel>();
                    cfg.CreateMap<ISubdivisionModel, SubdivisionModel>();
                });

            this.innerMapper = new Mapper(config);
        }

        public TResult Map<TResult, TSource>(TSource source) where TSource : class
        {
            return this.innerMapper.Map<TResult>(source);
        }

        public Party MapToPartyEntity(AddressModel item)
        {
            return this.innerMapper.Map<Party>(item);
        }

        public List<Party> MapToPartyEntityList(IEnumerable<AddressModel> items)
        {
            return items?.Select(this.MapToPartyEntity).ToList();
        }

        public Sitecore.Commerce.Entities.Party MapToParty(AddressModel item)
        {
            return this.innerMapper.Map<Sitecore.Commerce.Entities.Party>(item);
        }

        public AddressModel MapToAddress(Sitecore.Commerce.Entities.Party item)
        {
            return this.innerMapper.Map<AddressModel>(item);
        }

        public ShippingInfoArgument MapToShippingInfoArgument(ShippingMethodModel item)
        {
            return this.innerMapper.Map<ShippingInfoArgument>(item);
        }

        public List<ShippingInfoArgument> MapToShippingInfoArgumentList(IEnumerable<ShippingMethodModel> items)
        {
            return items?.Select(this.MapToShippingInfoArgument).ToList();
        }

        public FederatedPaymentArgs MapToFederatedPaymentArgs(FederatedPaymentModel model)
        {
            return this.innerMapper.Map<FederatedPaymentArgs>(model);
        }

        public ShippingMethodModel MapToShippingMethodModel(ShippingMethod shippingMethod)
        {
            return this.innerMapper.Map<ShippingMethodModel>(shippingMethod);
        }

        public ShippingOptionModel MapToShippingOptionModel(ShippingOption shipppingOption)
        {
            return this.innerMapper.Map<ShippingOptionModel>(shipppingOption);
        }

        public FederatedPaymentModel MapToFederatedPayment(PaymentInfo x)
        {
            return this.innerMapper.Map<FederatedPaymentModel>(x);
        }

        public ShippingMethodModel MapToShippingMethodModel(ShippingInfo x)
        {
            return this.innerMapper.Map<ShippingMethodModel>(x);
        }

        public User MapToCommerceUserModel(CommerceUser x)
        {
            return this.innerMapper.Map<User>(x);
        }

        public IEnumerable<CountryRegionModel> MapToCountryRegionModel(IEnumerable<ICountryRegionModel> model)
        {
            return this.innerMapper.Map<IEnumerable<CountryRegionModel>>(model);
        }
    }
}