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

namespace HCA.Foundation.Analytics.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Models.Entities.Contact;

    using Sitecore.Commerce.Entities;
    using Sitecore.XConnect.Collection.Model;

    [ExcludeFromCodeCoverage]
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            this.CreateMap<Party, PersonalInfo>();

            this.CreateMap<PersonalInfo, PersonalInformation>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}