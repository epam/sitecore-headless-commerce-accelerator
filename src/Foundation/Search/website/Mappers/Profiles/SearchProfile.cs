namespace HCA.Foundation.Search.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using AutoMapper;

    using Models.Common;

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
        }
    }
}