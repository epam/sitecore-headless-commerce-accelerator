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

namespace Wooli.Foundation.Connect.Mappers.Search
{
    using AutoMapper;

    using Models;

    using Sitecore.Commerce.CustomModels.Models;
    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Search.Models;

    using Mapper = Base.Mappers.Mapper;

    internal class SearchMapper : Mapper, ISearchMapper
    {
        public SearchMapper()
        {
            var configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<SearchOptions, CommerceSearchOptions>()
                        .ForMember(dest => dest.FacetFields, opt => opt.Ignore())
                        .ReverseMap();

                    cfg.CreateMap<CommerceConstants.SortDirection, SortDirection>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                        .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
                });

            this.InnerMapper = new AutoMapper.Mapper(configuration);
        }
    }
}