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

    using Models.Entities.Addresses;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;

    [ExcludeFromCodeCoverage]
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Party, Address>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.CountryCode, opt => opt.Ignore())
                .ForSourceMember(party => party.Company, opt => opt.Ignore())
                .ForSourceMember(party => party.PhoneNumber, opt => opt.Ignore())
                .ForSourceMember(party => party.Facet, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CommerceParty, Address>()
                .IncludeBase<Party, Address>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForSourceMember(dest => dest.RegionName, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<Address, Connect.Models.Party>()
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Facet, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}