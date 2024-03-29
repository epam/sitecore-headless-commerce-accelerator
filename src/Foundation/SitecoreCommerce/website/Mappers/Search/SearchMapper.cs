﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.SitecoreCommerce.Mappers.Search
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Commerce.Mappers.Profiles;
    using Commerce.Providers;
    using Commerce.Providers.Currency;

    using DependencyInjection;

    using Sitecore.Diagnostics;

    using Mapper = Base.Mappers.Mapper;
    using SearchProfile = Profiles.SearchProfile;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ISearchMapper), Lifetime = Lifetime.Singleton)]
    public class SearchMapper : Mapper, ISearchMapper
    {
        public SearchMapper(ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            var configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<SearchProfile>();
                    cfg.AddProfile(new CatalogProfile(currencyProvider));
                });

            this.InnerMapper = new AutoMapper.Mapper(configuration);
        }
    }
}