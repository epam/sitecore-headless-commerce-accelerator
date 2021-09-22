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

namespace HCA.Feature.Navigation.Tests.Repositories
{
    using System;
    using System.Linq;

    using Foundation.Base.Context;
    using Foundation.Base.Services;

    using Navigation.Repositories.Breadcrumb;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class BreadcrumbRepositoryTests 
    {
        private readonly IBreadcrumbRepository breadcrumbRepository;
        private readonly IFixture fixture;

        public BreadcrumbRepositoryTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());
            this.breadcrumbRepository = new BreadcrumbRepository(Substitute.For<ILinkManagerService>());
        }

        [Fact]
        public void GetPageLinks_IfInputParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.breadcrumbRepository.GetPageLinks(null, this.fixture.Create<string>()));
            Assert.Throws<ArgumentNullException>(() => this.breadcrumbRepository.GetPageLinks(this.fixture.Create<Item>(), null));
        }
    }
}