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

namespace Wooli.Foundation.ReactJss.Tests.Serialization.ItemSerializers
{
    using Newtonsoft.Json;

    using NSubstitute;

    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.Globalization;
    using Sitecore.LayoutService.Serialization.FieldSerializers;
    using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;

    using Wooli.Foundation.ReactJss.Serialization.ItemSerializers;

    using Xunit;

    public class EnhancedItemSerializerTest
    {
        // [Fact]
        public void Serialize_ItemWithFields_LowerCaseJson()
        {
            var fieldSerializer = Substitute.For<IFieldSerializer>();

            var getFieldSerializerPipeline = Substitute.For<IGetFieldSerializerPipeline>();
            getFieldSerializerPipeline.GetResult(Arg.Any<GetFieldSerializerPipelineArgs>()).Returns(fieldSerializer);

            using (Db db =
                new Db { new DbItem("dataSource") { { "field", "value1" }, { "Text Field", "value2" } } }.WithLanguages(
                    Language.Parse("en")))
            {
                Item item = db.GetItem("sitecore/content/dataSource");
                var serializer = new EnhancedItemSerializer(getFieldSerializerPipeline);

                string jsonString = serializer.Serialize(db.GetItem(item.ID));

                dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);

                Assert.NotNull(jsonObject);
                Assert.Equal(item.ID.Guid.ToString("D"), jsonObject.id.ToString());
            }
        }
    }
}