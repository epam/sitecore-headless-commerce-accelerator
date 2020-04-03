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

namespace Wooli.Foundation.Commerce.Mappers.Profiles
{
    using AutoMapper;

    using Connect.Utils;

    using Models.Entities.Payment;

    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            // TODO: Create SharedSettingsProvider instead of CommerceRequestUtils 
            this.CreateMap<FederatedPaymentInfo, Sitecore.Commerce.Entities.Carts.FederatedPaymentInfo>()
                .ForMember(
                    dest => dest.PaymentMethodID,
                    opt => opt.MapFrom(src => CommerceRequestUtils.GetPaymentOptionId("Federated")))
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorizationResult, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
                .ForMember(dest => dest.LineIDs, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentProviderID, opt => opt.Ignore());

            this.CreateMap<Sitecore.Commerce.Entities.Payments.PaymentOption, PaymentOption>();

            this.CreateMap<Sitecore.Commerce.Entities.Payments.PaymentMethod, PaymentMethod>();
        }
    }
}