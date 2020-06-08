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

namespace HCA.Foundation.Commerce.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Providers.StorefrontSettings;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Diagnostics;

    using FederatedPaymentInfo = Models.Entities.Payment.FederatedPaymentInfo;
    using PaymentMethod = Sitecore.Commerce.Entities.Payments.PaymentMethod;
    using PaymentOption = Sitecore.Commerce.Entities.Payments.PaymentOption;

    [ExcludeFromCodeCoverage]
    public class PaymentProfile : Profile
    {
        public PaymentProfile(IStorefrontSettingsProvider storefrontSettingsProvider)
        {
            Assert.ArgumentNotNull(storefrontSettingsProvider, nameof(storefrontSettingsProvider));

            this.CreateMap<FederatedPaymentInfo, Sitecore.Commerce.Entities.Carts.FederatedPaymentInfo>()
                .ForMember(
                    dest => dest.PaymentMethodID,
                    opt => opt.MapFrom(
                        src => storefrontSettingsProvider.GetPaymentOptionId(
                            Constants.StorefrontSettings.FederatedPaymentOptionTitle)))
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorizationResult, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
                .ForMember(dest => dest.LineIDs, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentProviderID, opt => opt.Ignore());

            this.CreateMap<PaymentOption, Models.Entities.Payment.PaymentOption>();

            this.CreateMap<PaymentMethod, Models.Entities.Payment.PaymentMethod>();

            this.CreateMap<PaymentInfo, FederatedPaymentInfo>()
                .ForMember(dest => dest.CardToken, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}