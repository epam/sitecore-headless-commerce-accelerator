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

namespace HCA.Feature.Catalog.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Foundation.Commerce.Models.Entities.Search;

    using Models.Requests.Search.DTO;

    [ExcludeFromCodeCoverage]
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            this.CreateMap<FacetDto, Facet>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}