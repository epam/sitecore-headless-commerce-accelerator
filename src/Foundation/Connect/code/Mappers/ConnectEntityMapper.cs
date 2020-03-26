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

namespace Wooli.Foundation.Connect.Mappers
{
    using System.Collections.Generic;

    using AutoMapper;

    using DependencyInjection;

    using Models;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Entities.Carts;

    using Utils;

    using Mapper = Base.Mappers.Mapper;

    [Service(typeof(IConnectEntityMapper))]
    public class ConnectEntityMapper : Mapper, IConnectEntityMapper
    {
        public ConnectEntityMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Party, CommerceParty>();
                    cfg.CreateMap<FederatedPaymentArgs, FederatedPaymentInfo>()
                        .ForMember(
                            dest => dest.PaymentMethodID,
                            opt => opt.MapFrom(src => CommerceRequestUtils.GetPaymentOptionId("Federated")));
                    cfg.CreateMap<ShippingInfoArgument, CommerceShippingInfo>()
                        .ForMember(
                            dest => dest.ShippingOptionType,
                            opt => opt.MapFrom(src => src.ShippingPreferenceType))
                        .ForMember(
                            dest => dest.LineIDs,
                            opt => opt.MapFrom(
                                src => src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()));
                    cfg.CreateMap<PaymentClientTokenResult, Models.Payment.PaymentClientTokenResult>();
                });
            this.InnerMapper = new AutoMapper.Mapper(config);
        }

        public CommerceParty MapToCommerceParty(Party item)
        {
            return this.InnerMapper.Map<CommerceParty>(item);
        }

        public IList<CommerceParty> MapToCommercePartyList(IEnumerable<Party> item)
        {
            return this.InnerMapper.Map<IList<CommerceParty>>(item);
        }

        public CommerceShippingInfo MapToCommerceShippingInfo(ShippingInfoArgument item)
        {
            return this.InnerMapper.Map<CommerceShippingInfo>(item);
        }

        public FederatedPaymentInfo MapToFederatedPaymentInfo(FederatedPaymentArgs item)
        {
            return this.InnerMapper.Map<FederatedPaymentInfo>(item);
        }
    }
}