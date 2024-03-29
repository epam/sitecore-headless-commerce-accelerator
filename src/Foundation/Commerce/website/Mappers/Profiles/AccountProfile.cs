﻿//    Copyright 2020 EPAM Systems, Inc.
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
    using System.Linq;

    using AutoMapper;

    using Models.Entities.Users;
    using Sitecore.Commerce.Entities.Customers;

    [ExcludeFromCodeCoverage]
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<CommerceUser, User>()
                .ForMember(
                    dest => dest.ExternalId,
                    opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(
                    dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.Customers.FirstOrDefault()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}