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

namespace Wooli.Feature.Catalog.Mappers
{
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Web;

    using AutoMapper;

    using Foundation.Commerce.Models.Entities.Search;
    using Foundation.DependencyInjection;

    using Models.Requests;

    using Mapper = Foundation.Base.Mappers.Mapper;

    [Service(typeof(ICatalogEntityMapper), Lifetime = Lifetime.Transient)]
    public sealed class CatalogEntityMapper : Mapper, ICatalogEntityMapper
    {
        public CatalogEntityMapper()
        {
            var configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ProductsSearchRequest, ProductsSearchOptions>()
                        .ForMember(dest => dest.SearchKeyword, opt => opt.MapFrom(src => src.SearchKeyword))
                        .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.Page))
                        .ForMember(
                            dest => dest.FacetValues,
                            opt => opt.MapFrom(
                                src => !string.IsNullOrEmpty(src.FacetValues)
                                    ? HttpUtility.ParseQueryString(src.FacetValues)
                                    : new NameValueCollection()))
                        .ForMember(dest => dest.SortField, opt => opt.MapFrom(src => src.SortField))
                        .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                        .ForMember(dest => dest.SortDirection, opt => opt.MapFrom(src => src.SortDirection))
                        .ForMember(
                            dest => dest.CurrentCatalogItemId,
                            opt => opt.MapFrom(src => src.CurrentCatalogItemId))
                        .ForMember(dest => dest.CurrentItemId, opt => opt.MapFrom(src => src.CurrentItemId));
                });

            this.InnerMapper = new AutoMapper.Mapper(configuration);
        }
    }
}