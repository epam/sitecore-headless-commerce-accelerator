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

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Shipping;

    using Utils;

    using ShippingMethod = Models.Entities.Shipping.ShippingMethod;
    using ShippingOption = Models.Entities.Shipping.ShippingOption;

    public class ShippingProfile : Profile
    {
        public ShippingProfile()
        {
            this.CreateMap<Sitecore.Commerce.Entities.Shipping.ShippingMethod, ShippingMethod>()
                .ForMember(dest => dest.LineIds, opt => opt.Ignore())
                .ForMember(dest => dest.PartyId, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingPreferenceType, opt => opt.Ignore());

            // TODO: Get Rid of reference to Sitecore.Commerce.Engine.Connect
            this.CreateMap<ShippingMethod, CommerceShippingInfo>()
                .ForMember(
                    dest => dest.ShippingOptionType,
                    opt => opt.MapFrom(src => src.ShippingPreferenceType))
                .ForMember(dest => dest.ShippingMethodID, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ElectronicDeliveryEmail, opt => opt.Ignore())
                .ForMember(dest => dest.ElectronicDeliveryEmailContent, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingProviderID, opt => opt.Ignore());


            this.CreateMap<Sitecore.Commerce.Entities.Shipping.ShippingOption, ShippingOption>();

            this.CreateMap<string, ShippingOptionType>()
                .ConvertUsing(ConnectOptionTypeHelper.ToShippingOptionType);
        }
    }
}