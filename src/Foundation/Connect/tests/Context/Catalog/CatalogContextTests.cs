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

namespace HCA.Foundation.Connect.Tests.Context.Catalog
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture;

    using Connect.Context.Catalog;

    using Glass.Mapper.Sc;

    using Models;

    using NSubstitute;



    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class CatalogContextTests
    {
        private readonly CatalogContext catalogContext;

        private readonly IFixture fixture;

        private readonly ISitecoreService sitecoreService;

        public CatalogContextTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.sitecoreService = Substitute.For<ISitecoreService>();

            this.catalogContext = new CatalogContext(this.sitecoreService);
        }

        [Fact]
        public void CatalogItem_IfNoSelectedCatalogs_ShouldReturnNull()
        {
            // arrange
            var catalogFolder = this.fixture.Build<CommerceCatalogFolderModel>()
                .With(f => f.SelectedCatalogs, new List<string>())
                .Create();
            this.sitecoreService.GetItem<CommerceCatalogFolderModel>(Arg.Any<string>()).Returns(catalogFolder);

            // act
            var catalogItem = this.catalogContext.CatalogItem;

            // assert
            Assert.Null(catalogItem);
        }

        [Fact]
        public void CatalogItem_ShouldReturnFirstSelectedCatalogItem()
        {
            // arrange
            var selectedCatalogs = new List<string>();

            var database = Substitute.For<Database>();

            var item = this.CreateItem(database);
            var child = this.CreateItem(database);
            item.Children.Returns(
                new ChildList(
                    item,
                    new List<Item>
                    {
                        child
                    }));

            var catalogFolder = this.fixture.Build<CommerceCatalogFolderModel>()
                .With(f => f.SelectedCatalogs, selectedCatalogs)
                .With(f => f.Item, item)
                .Create();

            selectedCatalogs.Add(child.Name);

            this.sitecoreService.GetItem<CommerceCatalogFolderModel>(Arg.Any<string>()).Returns(catalogFolder);

            // act
            var catalogItem = this.catalogContext.CatalogItem;

            // assert
            Assert.Equal(child, catalogItem);
        }

        [Fact]
        public void CatalogName_IfNoSelectedCatalogs_ShouldReturnNull()
        {
            // arrange
            var catalogFolder = this.fixture.Build<CommerceCatalogFolderModel>()
                .With(f => f.SelectedCatalogs, new List<string>())
                .Create();
            this.sitecoreService.GetItem<CommerceCatalogFolderModel>(Arg.Any<string>()).Returns(catalogFolder);

            // act
            var catalogName = this.catalogContext.CatalogName;

            // assert
            Assert.Null(catalogName);
        }

        [Fact]
        public void CatalogName_ShouldReturnFirstSelectedCatalogName()
        {
            // arrange
            var catalogFolder = this.fixture.Create<CommerceCatalogFolderModel>();
            this.sitecoreService.GetItem<CommerceCatalogFolderModel>(Arg.Any<string>()).Returns(catalogFolder);

            // act
            var catalogName = this.catalogContext.CatalogName;

            // assert
            Assert.Equal(catalogFolder.SelectedCatalogs.First(), catalogName);
        }

        private Item CreateItem(Database database)
        {
            var item = Substitute.For<Item>(
                this.fixture.Create<ID>(),
                this.fixture.Create<ItemData>(),
                database);

            item.Name = this.fixture.Create<string>();

            return item;
        }
    }
}