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

namespace Wooli.Foundation.Commerce.Mappers.Search
{
    using AutoMapper;

    using Connect.Models.Search;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Connect = Connect.Models;
    using Facet = Models.Entities.Search.Facet;
    using FacetValue = Models.Entities.Search.FacetValue;

    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            this.CreateMap<FacetValue, Connect.Search.FacetValue>()
                .ForMember(dest => dest.AggregateCount, opt => opt.Ignore());
            
            this.CreateMap<SearchResultsV2<Connect.Product>, Product>();

            this.CreateMap<QueryFacet, Facet>();

            this.CreateMap<Connect.Product, Product>()
                .ForCtorParam("sellableItem", opt => opt.MapFrom(src => src.Item));

            this.CreateMap<Connect.Search.SearchResultsV2<Connect.Product>, ProductSearchResults>()
                .ForMember(dest => dest.Facets, opt => opt.MapFrom(src => src.QueryFacets))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Results));


            this.CreateMap<Facet, Connect.Search.Facet>()
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore());
        }
    }
}