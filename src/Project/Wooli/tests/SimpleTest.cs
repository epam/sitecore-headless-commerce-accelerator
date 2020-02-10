//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Project.Wooli.Tests
{
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Xunit;

    public class SimpleTest
    {
        [Fact]
        public void CreatingHierarchyOfItems()
        {
            using (
                var db = new Db
                {
                    new DbItem("Articles")
                    {
                        new DbItem("Getting Started"),
                        new DbItem("Troubleshooting")
                    }
                })
            {
                var articles = db.GetItem("/sitecore/content/Articles");

                Assert.NotNull(articles.Children["Getting Started"]);
                Assert.NotNull(articles.Children["Troubleshooting"]);
            }
        }

        [Fact]
        public void CreatingSimpleItem()
        {
            using (var db = new Db {new DbItem("Home") {{"Title", "Welcome!"}}})
            {
                var home = db.GetItem("/sitecore/content/home");
                Assert.Equal("Welcome!", home["Title"]);
            }
        }
    }
}