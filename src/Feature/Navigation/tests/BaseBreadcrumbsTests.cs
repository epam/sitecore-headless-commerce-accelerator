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

namespace HCA.Feature.Navigation.Tests
{
    using Foundation.Base.Context;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Data;
    using Sitecore.FakeDb;

    public class BaseBreadcrumbTests
    {
        protected const string StartItemPath = "/sitecore/content/HCA/Home";
        protected const string ShopPageId = Constants.ShopPageId;
        protected const string CategoryTemplateId = Constants.CategoryTemplateId;
        protected const string ProductTemplateId = "{1DAEFF25-B075-4C13-B41A-72B553B22542}";
        protected const string CategoryId = "{28A8C141-F104-4433-B50E-DBFF74B4FEF9}";
        protected const string ProductId = "{4BA60D18-6973-4A69-9D14-BA602BED362A}";
        protected const string CategoryName = "Desktops";
        protected const string ProductName = "SPARK DESKTOP";
        protected readonly IFixture Fixture;

        protected readonly ISitecoreContext SitecoreContext;
        protected readonly Db SitecoreTree;

        public BaseBreadcrumbTests()
        {
            this.Fixture = new Fixture();
            this.SitecoreTree = this.CreateSitecoreTree();
            this.SitecoreContext = Substitute.For<ISitecoreContext>();

            this.SitecoreContext.Database.Returns(x => this.SitecoreTree.Database);
        }

        protected Db CreateSitecoreTree()
        {
            var database = new Db
            {
                new DbItem("HCA")
                {
                    new DbItem("Home", new ID("{A91CCFEC-F221-4BAC-AFAA-BC653B06C69E}"))
                    {
                        new DbItem("Account", new ID("{141D9A06-EEE9-4EF4-B2FC-1F84891326DD}"))
                        {
                            new DbItem("Login-Register", new ID("{E6C4FD6E-523A-4266-90EE-569C289B4832}"))
                        },
                        new DbItem("Product", new ID("{56F3C5B3-CC57-41C4-8895-EC5AF747D209}"))
                        {
                            new DbItem("*", new ID("{7463C80F-69A4-4DDF-8BD3-A4155D69CB9B}"))
                        },
                        new DbItem("Shop", new ID("{66FB80E3-B83A-4CEC-B367-85DD3CC63BA4}"))
                        {
                            new DbItem("*", new ID(CategoryId), new ID(CategoryTemplateId))
                            {
                                new DbItem("*", new ID(ProductId), new ID(ProductTemplateId))
                                {
                                    { "__Display name", ProductName }
                                },
                                { "__Display name", CategoryName }
                            }
                        }
                    }
                }
            };

            return database;
        }
    }
}