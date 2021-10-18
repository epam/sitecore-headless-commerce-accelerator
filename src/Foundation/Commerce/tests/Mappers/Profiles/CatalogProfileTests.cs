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

namespace HCA.Foundation.Commerce.Tests.Mappers.Profiles
{
    using AutoMapper;

    using Commerce.Mappers.Profiles;
    using Commerce.Providers;
    using Commerce.Providers.Currency;

    using NSubstitute;

    using Xunit;

    public class CatalogProfileTests
    {
        [Fact]
        public void Configuration_ShouldBeValid()
        {
            // arrange
            var currencyProvider = Substitute.For<ICurrencyProvider>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CatalogProfile(currencyProvider)));

            // act, assert
            configuration.AssertConfigurationIsValid();
        }
    }
}