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

using System.Collections.Generic;
using AutoMapper;
using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities.Carts;
using Wooli.Foundation.Connect.Models;
using Wooli.Foundation.Connect.Utils;
using Wooli.Foundation.DependencyInjection;

namespace Wooli.Foundation.Connect.ModelMappers
{
    [Service(typeof(IConnectEntityMapper))]
    public class ConnectEntityMapper : IConnectEntityMapper
    {
        private readonly IMapper innerMapper;

        public ConnectEntityMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PartyEntity, CommerceParty>();
                    cfg.CreateMap<FederatedPaymentArgs, FederatedPaymentInfo>()
                        .ForMember(dest => dest.PaymentMethodID,
                            opt => opt.MapFrom(src => CommerceRequestUtils.GetPaymentOptionId("Federated")));
                    cfg.CreateMap<ShippingInfoArgument, CommerceShippingInfo>()
                        .ForMember(dest => dest.ShippingOptionType,
                            opt => opt.MapFrom(src => src.ShippingPreferenceType))
                        .ForMember(dest => dest.LineIDs,
                            opt => opt.MapFrom(src =>
                                src.LineIds != null ? new List<string>(src.LineIds) : new List<string>()));
                    ;
                });
            innerMapper = new Mapper(config);
        }

        public CommerceParty MapToCommerceParty(PartyEntity item)
        {
            return innerMapper.Map<CommerceParty>(item);
        }

        public IList<CommerceParty> MapToCommercePartyList(IEnumerable<PartyEntity> item)
        {
            return innerMapper.Map<IList<CommerceParty>>(item);
        }

        public FederatedPaymentInfo MapToFederatedPaymentInfo(FederatedPaymentArgs item)
        {
            return innerMapper.Map<FederatedPaymentInfo>(item);
        }

        public CommerceShippingInfo MapToCommerceShippingInfo(ShippingInfoArgument item)
        {
            return innerMapper.Map<CommerceShippingInfo>(item);
        }
    }
}