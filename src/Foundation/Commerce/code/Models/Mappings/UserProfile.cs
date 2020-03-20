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

namespace Wooli.Foundation.Commerce.Models.Mappings
{
    using System.Linq;

    using AutoMapper;

    using Entities.Users;

    using Foundation.Account.Infrastructure.Pipelines.Login;
    using Sitecore.Commerce.Entities.Customers;

    using Constants = Utils.Constants;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<CommerceUser, User>()
                // It is assumed that we have only one commerce customer per commerce user,
                // so we select a first if it exists
                .ForMember(
                    dest => dest.CustomerId,
                    opt =>
                        opt.MapFrom(src => src.Customers != null ? src.Customers.FirstOrDefault() : null))
                // We extract contact id from externalId,
                // it is not returned by current implementation of Sitecore.Commerce.Core
                .ForMember(
                    dest => dest.ContactId,
                    opt =>
                        opt.MapFrom(
                            src => src.ExternalId != null
                                ? src.ExternalId.Replace(Constants.CommereceCustomerIdPrefix, string.Empty)
                                : null));
        }
    }
}