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

namespace HCA.Foundation.ReactJss.Serialization.ItemSerializers
{
    using System.IO;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.LayoutService.Serialization;
    using Sitecore.LayoutService.Serialization.ItemSerializers;
    using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;

    public class EnhancedItemSerializer : DefaultItemSerializer
    {
        public EnhancedItemSerializer(IGetFieldSerializerPipeline getFieldSerializerPipeline)
            : base(getFieldSerializerPipeline)
        {
        }

        public override string Serialize(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            using (var stringWriter = new StringWriter())
            {
                using (var writer = new EnhancedJsonTextWriter(stringWriter))
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("id");
                    writer.WriteValue(item.ID.Guid.ToString("D", null));

                    var itemFields = this.GetItemFields(item);
                    var options = new SerializationOptions();
                    foreach (var itemField in itemFields)
                    {
                        this.SerializeField(itemField, writer, options, 0);
                    }

                    writer.WriteEndObject();
                }

                return stringWriter.ToString();
            }
        }
    }
}