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
    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Entities.Shipping;
    using Utils;
    using CountryRegionModel = Models.Region.CountryRegionModel;
    using SubdivisionModel = Models.Region.SubdivisionModel;

    [Service(typeof(IEntityMapper))]
    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper innerMapper;

        public EntityMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
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

                cfg.CreateMap<string, ShippingOptionType>()
                    .ConvertUsing(ConnectOptionTypeHelper.ToShippingOptionType);

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
                            src.ExternalId.Replace(Constants.CommerceCustomerIdPrefix, string.Empty)))
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customers.FirstOrDefault()))
                    .ReverseMap();

                cfg.CreateMap<ICountryRegionModel, CountryRegionModel>();
                cfg.CreateMap<ISubdivisionModel, SubdivisionModel>();
            });

            innerMapper = new Mapper(config);
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

        public ShippingOptionModel MapToShippingOptionModel(ShippingOption shippingOption)
        {
            return innerMapper.Map<ShippingOptionModel>(shippingOption);
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