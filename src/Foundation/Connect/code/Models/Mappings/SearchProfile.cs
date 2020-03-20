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

namespace Wooli.Foundation.Connect.Models.Mappings
{
    using System.Collections.Generic;

    using AutoMapper;

    using Search;

    using Sitecore.Commerce.CustomModels.Models;
    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;

    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            this.CreateMap<SearchOptions, CommerceSearchOptions>()
                .ForMember(dest => dest.FacetFields, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CommerceConstants.SortDirection, SortDirection>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));

            this.CreateMap<CommerceQueryFacet, QueryFacet>()
                .ForMember(
                    dest => dest.FoundValues,
                    opt => opt.NullSubstitute(new List<Sitecore.ContentSearch.Linq.FacetValue>()));

            this.CreateMap<Sitecore.ContentSearch.Linq.FacetValue, FacetValue>();

            this.CreateMap<SearchResponse, SearchResultsV2>()
                .ForMember(dest => dest.QueryFacets, opt => opt.MapFrom(src => src.Facets))
                .ForMember(dest => dest.SearchResultItems, opt => opt.MapFrom(src => src.ResponseItems));
        }
    }
}