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
    using System.Linq;

    using AutoMapper;

    using Base.Extensions;

    using Foundation.Account.Infrastructure.Pipelines.Login;

    using Models.Entities.Users;
    using Sitecore.Commerce.Entities.Customers;

    [ExcludeFromCodeCoverage]
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, LoginPipelineArgs>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<LoginPipelineArgs, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<CommerceUser, User>()

                // It is assumed that we have only one commerce customer per commerce user,
                // so we select a first if it exists
                .ForMember(
                    dest => dest.CustomerId,
                    opt =>
                        opt.MapFrom(src => src.Customers != null ? src.Customers.FirstOrDefault() : null))

                .ForMember(
                    dest => dest.PhoneNumber,
                    opt =>
                        opt.MapFrom(src => src.GetPropertyValue("Phone")))

                .ForMember(
                    dest => dest.ImageUrl,
                    opt =>
                        opt.MapFrom(src => ItemExtensions.GetImageUrl()))

                .ForMember(
                    dest => dest.DateOfBirth,
                    opt =>
                        opt.MapFrom(src => CustomPropertyExtensions.GetCustomProperty("BirthDate")))

                .ForMember(
                    dest => dest.ExternalId,
                    opt => opt.MapFrom(src => src.ExternalId));
        }
    }
}