﻿//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Connect.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;
    using HCA.Foundation.ConnectBase.Entities;
    using Models;

    [ExcludeFromCodeCoverage]
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Party, CommerceParty>()
                .ForMember(dest => dest.UserProfileAddressId, opt => opt.Ignore())
                .ForMember(dest => dest.EveningPhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.FaxNumber, opt => opt.Ignore())
                .ForMember(dest => dest.RegionCode, opt => opt.Ignore())
                .ForMember(dest => dest.RegionName, opt => opt.Ignore());
        }
    }
}