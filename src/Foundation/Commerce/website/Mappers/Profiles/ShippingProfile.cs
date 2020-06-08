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
    using System.Globalization;

    using AutoMapper;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;

    using Utils;

    [ExcludeFromCodeCoverage]
    public class ShippingProfile : Profile
    {
        public ShippingProfile()
        {
            this.CreateMap<ShippingMethod, Models.Entities.Shipping.ShippingMethod>()
                .ForMember(dest => dest.LineIds, opt => opt.Ignore())
                .ForMember(dest => dest.PartyId, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingPreferenceType, opt => opt.Ignore());

            this.CreateMap<Models.Entities.Shipping.ShippingMethod, ShippingInfo>()
                .ForMember(dest => dest.ShippingMethodID, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.ShippingProviderID, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<Models.Entities.Shipping.ShippingMethod, CommerceShippingInfo>()
                .ForMember(
                    dest => dest.ShippingOptionType,
                    opt => opt.MapFrom(src => src.ShippingPreferenceType))
                .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ElectronicDeliveryEmail, opt => opt.Ignore())
                .ForMember(dest => dest.ElectronicDeliveryEmailContent, opt => opt.Ignore())
                .IncludeBase<Models.Entities.Shipping.ShippingMethod, ShippingInfo>()
                .ReverseMap();

            this.CreateMap<ShippingOption, Models.Entities.Shipping.ShippingOption>();

            this.CreateMap<string, ShippingOptionType>()
                .ConvertUsing(ConnectOptionTypeHelper.ToShippingOptionType);

            this.CreateMap<ShippingOptionType, string>()
                .ConstructUsing(src => src.Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}