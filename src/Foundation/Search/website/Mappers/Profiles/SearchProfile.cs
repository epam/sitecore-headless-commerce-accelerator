namespace HCA.Foundation.Search.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using AutoMapper;

    using Models.Common;
    using Models.Entities.Category;
    using Models.Entities.Product;

    using Sitecore.ContentSearch.Linq;

    using FacetValue = Models.Common.FacetValue;

    [ExcludeFromCodeCoverage]
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            this.CreateMap<FacetValue, Sitecore.ContentSearch.Linq.FacetValue>()
                .ForCtorParam("aggregate", opt => opt.MapFrom(src => src.AggregateCount));

            this.CreateMap<FacetCategory, Facet>()
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
                .ForMember(dest => dest.FoundValues, opt => opt.Ignore());

            this.CreateMap<Sitecore.ContentSearch.Linq.FacetValue, FacetValue>();

            this.CreateMap<Sitecore.ContentSearch.Linq.SearchResults<CategorySearchResultItem>, Models.Common.SearchResults<CategorySearchResultItem>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Hits.Select(h => h.Document).ToList()))
                .ForMember(dest => dest.Facets, opt => opt.Ignore())
                .ForMember(dest => dest.TotalItemCount, opt => opt.Ignore())
                .ForMember(dest => dest.TotalPageCount, opt => opt.Ignore());


            this.CreateMap<Sitecore.ContentSearch.Linq.SearchResults<ProductSearchResultItem>, Models.Common.SearchResults<ProductSearchResultItem>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Hits.Select(h => h.Document).ToList()))
                .ForMember(dest => dest.Facets, opt => opt.MapFrom(src => src.Facets != null ? src.Facets.Categories : null))
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.TotalSearchResults))
                .ForMember(dest => dest.TotalPageCount, opt => opt.Ignore());
        }
    }
}
