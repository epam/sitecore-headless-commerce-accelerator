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

namespace HCA.Foundation.Connect.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Models.Catalog;
    using Models.Search;

    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.SolrNetExtension;

    using SortDirection = Sitecore.Commerce.CustomModels.Models.SortDirection;

    [ExcludeFromCodeCoverage]
    public class SuggestionProfile : Profile
    {
        public SuggestionProfile()
        {
            this.CreateMap<SuggestionOptions, SuggestHandlerQueryOptions>()
                .ForMember(dest=> dest.Parameters, opt => opt.MapFrom(src => src));

            this.CreateMap<SuggestResult, SuggestionResult>(); 
        }
    }
}