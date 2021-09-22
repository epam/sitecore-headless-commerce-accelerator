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

namespace HCA.Foundation.SitecoreCommerce.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using AutoMapper;

    using Commerce.Models.Entities.Catalog;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Models.Entities.Category;

    using HCA.Foundation.Commerce.Models.Entities.Search;

    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data.Items;

    using Facet = Foundation.Search.Models.Common.Facet;
    using ProductSearchOptions = Foundation.Search.Models.Entities.Product.ProductSearchOptions;
    using SortDirection = Sitecore.Commerce.CustomModels.Models.SortDirection;

    [ExcludeFromCodeCoverage]
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            this.CreateMap<CommerceQueryFacet, Facet>();

            this.CreateMap<CommerceConstants.SortDirection, SortDirection>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
            
            this.CreateMap<SearchResults<Item>, ProductSearchResults>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            
            this.CreateMap<CategorySearchResultItem, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SitecoreId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForAllOtherMembers(opt => opt.Ignore());
            
            this.CreateMap<SearchResponse, SearchResults<Item>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.ResponseItems));
            
            this.CreateMap<Sitecore.ContentSearch.Linq.SearchResults<CommerceSellableItemSearchResultItem>, Foundation.Search.Models.Common.SearchResults<CategorySearchResultItem>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Hits.Select(h => h.Document).ToList()))
                .ForMember(dest => dest.Facets, opt => opt.Ignore())
                .ForMember(dest => dest.TotalItemCount, opt => opt.Ignore())
                .ForMember(dest => dest.TotalPageCount, opt => opt.Ignore());

            this.CreateMap<ProductSearchOptions, CommerceSearchOptions>()
                .ForMember(dest => dest.FacetFields, opt => opt.MapFrom(src => src.Facets))
                .ReverseMap();
            
            this.CreateMap<Facet, CommerceQueryFacet>()
                .ForMember(dest => dest.FoundValues, opt => opt.Ignore());
        }
    }
}