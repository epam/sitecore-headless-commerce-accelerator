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

namespace HCA.Foundation.Commerce.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Connect.Models;

    using DependencyInjection;

    using Models.Checkout;
    using Models.Entities.Addresses;
    using Models.Entities.Users;

    using Providers;

    using Services.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Diagnostics;

    using Utils;

    [Service(typeof(IEntityMapper))]
    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper innerMapper;

        public EntityMapper(ICatalogService catalogService, ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(catalogService, nameof(catalogService));
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            var config = new MapperConfiguration(
                cfg =>
                {
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
                                    src.ExternalId.Replace(Constants.CommerceCustomerIdPrefix, string.Empty)))
                        .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customers.FirstOrDefault()))
                        .ReverseMap();
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
    }
}