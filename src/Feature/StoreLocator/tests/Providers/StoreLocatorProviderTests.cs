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

namespace HCA.Feature.StoreLocator.Tests.Providers
{
    using Foundation.Base.Providers.SiteDataSources;

    using Glass.Mapper.Sc;

    using Models;

    using NSubstitute;

    using Sitecore.Data.Items;
    using Sitecore.FakeDb;

    using StoreLocator.Providers;

    using Xunit;

    public class StoreLocatorProviderTests
    {
        private readonly IDataSourcesProvider datasourceProvider;
        private readonly ISitecoreService sitecoreService;

        private readonly IStoreLocatorProvider storeLocatorProvider;

        private DbItem storeLocator;
        private DbItem storeLocatorFolder;
        private DbItem dataSourceRoot;

        public StoreLocatorProviderTests()
        {
            this.datasourceProvider = Substitute.For<IDataSourcesProvider>();
            this.sitecoreService = Substitute.For<ISitecoreService>();

            this.storeLocatorProvider = new StoreLocatorProvider(this.datasourceProvider, this.sitecoreService);

            this.InitFakeDbContent();
        }

        [Fact]
        public void GetStoreLocator_ShouldReturnStoreLocator()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(db);

                // act
                var result = this.storeLocatorProvider.GetLocator();

                // assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetStoreLocator_ShouldCallGetDataSourcesRootFolderMethod()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(db);

                // act
               this.storeLocatorProvider.GetLocator();

                // assert
                this.datasourceProvider
                    .Received(1)
                    .GetDataSourcesRootFolder();
            }
        }

        [Fact]
        public void GetStoreLocator_ShouldCallGetFirstChildOrDefaultMethodWithRootFolderData()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(db);

                var rootDsFolder = db.GetItem(this.dataSourceRoot.ID);

                // act
                this.storeLocatorProvider.GetLocator();

                // assert
                this.datasourceProvider
                    .Received(1)
                    .GetFirstChildOrDefault(
                        Arg.Is(rootDsFolder),
                        Arg.Is(StoreLocatorFolder.TemplateId));
            }
        }

        [Fact]
        public void GetStoreLocator_ShouldCallGetFirstChildOrDefaultMethodWithStoreLocatorFolderData()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(db);

                var storeLocatorDsFolder = db.GetItem(this.storeLocatorFolder.ID);

                // act
                this.storeLocatorProvider.GetLocator();

                // assert
                this.datasourceProvider
                    .Received(1)
                    .GetFirstChildOrDefault(
                        Arg.Is(storeLocatorDsFolder),
                        Arg.Is(StoreLocator.TemplateId));
            }
        }

        [Fact]
        public void StoreLocatorIsNotExists_ShouldReturnNull()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(
                    db,
                    locatorExists: false);

                // act
                var result = this.storeLocatorProvider.GetLocator();

                // assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void StoreLocatorFolderIsNotExists_ShouldReturnNull()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(
                    db,
                    locatorFolderExists: false,
                    locatorExists: false);

                // act
                var result = this.storeLocatorProvider.GetLocator();

                // assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void RootDatasourceFolderIsNotExists_ShouldReturnNull()
        {
            //arrange
            using (var db = new Db { dataSourceRoot })
            {
                this.PrepareTestData(
                    db,
                    rootDatasourceFolderExists: false,
                    locatorFolderExists: false,
                    locatorExists: false);

                // act
                var result = this.storeLocatorProvider.GetLocator();

                // assert
                Assert.Null(result);
            }
        }

        private void PrepareTestData(
            Db db,
            bool rootDatasourceFolderExists = true, 
            bool locatorFolderExists = true,
            bool locatorExists = true)
        {
            var rootDsFolder = db.GetItem(this.dataSourceRoot.ID);
            var storeLocatorDsFolder = db.GetItem(this.storeLocatorFolder.ID);
            var storeLocatorDs = db.GetItem(this.storeLocator.ID);

            this.datasourceProvider
                .GetDataSourcesRootFolder()
                .Returns(rootDatasourceFolderExists ? rootDsFolder : default(Item));

            this.datasourceProvider
                .GetFirstChildOrDefault(Arg.Any<Item>(), Arg.Any<string>())
                .Returns(
                    locatorFolderExists ? storeLocatorDsFolder : default(Item),
                    locatorExists ? storeLocatorDs : default(Item));

            this.sitecoreService
                .GetItem<StoreLocator>(Arg.Any<Item>())
                .Returns(x => new StoreLocator());
        }

        private void InitFakeDbContent()
        {
            this.storeLocator = new DbItem("Store Locator");

            this.storeLocatorFolder = new DbItem("Store Locator Folder")
            {
                this.storeLocator
            };

            this.dataSourceRoot = new DbItem("Data Folder")
            {
                this.storeLocatorFolder
            };
        }
    }
}
