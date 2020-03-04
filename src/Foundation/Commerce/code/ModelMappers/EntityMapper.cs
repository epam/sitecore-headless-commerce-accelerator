//    Copyright 2019 EPAM Systems, Inc.
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
    using Connect.Models;
    using DependencyInjection;
    using Models;
    using Models.Checkout;
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
    using CountryRegionModel = Models.Region.CountryRegionModel;
    using ShippingMethod = Sitecore.Commerce.Entities.Shipping.ShippingMethod;
    using SubdivisionModel = Models.Region.SubdivisionModel;

    [Service(typeof(IEntityMapper))]
    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper innerMapper;
        private readonly ICatalogRepository catalogRepository;

        public EntityMapper(ICatalogRepository catalogRepository)
        {
            Assert.ArgumentNotNull(catalogRepository, nameof(catalogRepository));
            this.catalogRepository = catalogRepository;

            var config = new MapperConfiguration(cfg =>
            {
                #region Cart

                cfg.CreateMap<CartResult, Result<Models.Entities.Cart>>()
                    .ConstructUsing(source => new Result<Models.Entities.Cart>(this.innerMapper.Map<Models.Entities.Cart>(source.Cart), source.SystemMessages.Select(_ => _.Message).ToList()))
                    .ReverseMap();

                cfg.CreateMap<Cart, Models.Entities.Cart>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                    .ForMember(dest => dest.CartLines, opt => opt.MapFrom(src => src.Lines))
                    .ReverseMap();

                cfg.CreateMap<CommerceCart, Models.Entities.Cart>()
                    .IncludeBase<Cart, Models.Entities.Cart>()
                    .ReverseMap();

                cfg.CreateMap<CartLine, Models.Entities.CartLine>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalCartLineId))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                    .ForMember(dest => dest.Product, opt => opt.Ignore())
                    .ForMember(dest => dest.Variant, opt => opt.Ignore())
                    .AfterMap(
                        (src, dest) =>
                        {
                            dest.Product = this.catalogRepository.GetProduct(src.Product.ProductId);
                            dest.Variant = dest.Product.Variants?.FirstOrDefault(x => x.ProductVariantId == (src.Product as CommerceCartProduct)?.ProductVariantId);
                        })
                    .ReverseMap();

                cfg.CreateMap<CommerceCartLine, Models.Entities.CartLine>()
                    .IncludeBase<CartLine, Models.Entities.CartLine>()
                    .ReverseMap();

                #endregion

                #region Price

                cfg.CreateMap<Total, Models.Entities.TotalPrice>()
                    .ReverseMap();

                cfg.CreateMap<CommerceTotal, Models.Entities.TotalPrice>()
                    .IncludeBase<Total, Models.Entities.TotalPrice>()
                    .ReverseMap();

                #endregion

                #region Address

                cfg.CreateMap<Party, Models.Entities.Address>()
                    .ReverseMap();

                cfg.CreateMap<CommerceParty, Models.Entities.Address>()
                    .IncludeBase<Party, Models.Entities.Address>()
                    .ReverseMap();

                cfg.CreateMap<PartyEntity, Models.Entities.Address>()
                    .ReverseMap();
                
                #endregion

                #region Shipping

                cfg.CreateMap<ShippingMethod, Models.Entities.ShippingMethod>()
                    .ReverseMap();

                cfg.CreateMap<ShippingInfo, Models.Entities.ShippingMethod>()
                    .ReverseMap();
                cfg.CreateMap<CommerceShippingInfo, Models.Entities.ShippingMethod>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShippingMethodName))
                    .ReverseMap();

                cfg.CreateMap<Models.Entities.ShippingMethod, ShippingInfoArgument>()
                    //.ForMember(dest => dest.LineIds,
                    //    opt => opt.MapFrom(src =>
                    //        src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()))
                    .ForMember(dest => dest.ShippingMethodId, opt => opt.MapFrom(src => src.ExternalId))
                    .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();

                cfg.CreateMap<ShippingOption, ShippingOptionModel>()
                    .ForMember(dest => dest.ShippingOptionType,
                        opt => opt.MapFrom(src => src.ShippingOptionType.Value));

                cfg.CreateMap<string, ShippingOptionType>()
                    .ConvertUsing(ConnectOptionTypeHelper.ToShippingOptionType);

                #endregion

                cfg.CreateMap<Party, AddressModel>()
                    .ReverseMap();

                cfg.CreateMap<PartyEntity, AddressModel>()
                    .ReverseMap();
                cfg.CreateMap<CommerceParty, AddressModel>()
                    .ReverseMap();

                cfg.CreateMap<FederatedPaymentArgs, FederatedPaymentModel>()
                    .ReverseMap();

                cfg.CreateMap<ShippingMethodModel, ShippingInfoArgument>()
                    .ForMember(dest => dest.LineIds,
                        opt => opt.MapFrom(src =>
                            src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()))
                    .ForMember(dest => dest.ShippingMethodId, opt => opt.MapFrom(src => src.ExternalId))
                    .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();

                
                cfg.CreateMap<ShippingMethod, ShippingMethodModel>()
                    .ReverseMap();

                cfg.CreateMap<ShippingOption, ShippingOptionModel>()
                    .ForMember(dest => dest.ShippingOptionType,
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

                cfg.CreateMap<CommerceUser, CommerceUserModel>()
                    .ForMember(dest => dest.ContactId,
                        opt => opt.MapFrom(src =>
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
            return innerMapper.Map<TResult>(source);
        }
        
        public PartyEntity MapToPartyEntity(AddressModel item)
        {
            return innerMapper.Map<PartyEntity>(item);
        }

        public List<PartyEntity> MapToPartyEntityList(IEnumerable<AddressModel> items)
        {
            return items?.Select(MapToPartyEntity).ToList();
        }

        public Party MapToParty(AddressModel item)
        {
            return innerMapper.Map<Party>(item);
        }

        public AddressModel MapToAddress(Party item)
        {
            return innerMapper.Map<AddressModel>(item);
        }

        public ShippingInfoArgument MapToShippingInfoArgument(ShippingMethodModel item)
        {
            return innerMapper.Map<ShippingInfoArgument>(item);
        }

        public List<ShippingInfoArgument> MapToShippingInfoArgumentList(IEnumerable<ShippingMethodModel> items)
        {
            return items?.Select(MapToShippingInfoArgument).ToList();
        }

        public FederatedPaymentArgs MapToFederatedPaymentArgs(FederatedPaymentModel model)
        {
            return innerMapper.Map<FederatedPaymentArgs>(model);
        }

        public ShippingMethodModel MapToShippingMethodModel(ShippingMethod shippingMethod)
        {
            return innerMapper.Map<ShippingMethodModel>(shippingMethod);
        }

        public ShippingOptionModel MapToShippingOptionModel(ShippingOption shipppingOption)
        {
            return innerMapper.Map<ShippingOptionModel>(shipppingOption);
        }

        public FederatedPaymentModel MapToFederatedPayment(PaymentInfo x)
        {
            return innerMapper.Map<FederatedPaymentModel>(x);
        }

        public ShippingMethodModel MapToShippingMethodModel(ShippingInfo x)
        {
            return innerMapper.Map<ShippingMethodModel>(x);
        }

        public CommerceUserModel MapToCommerceUserModel(CommerceUser x)
        {
            return innerMapper.Map<CommerceUserModel>(x);
        }

        public IEnumerable<CountryRegionModel> MapToCountryRegionModel(IEnumerable<ICountryRegionModel> model)
        {
            return innerMapper.Map<IEnumerable<CountryRegionModel>>(model);
        }
    }
}