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

namespace HCA.Foundation.ReactJss.Serialization
{
    using System.IO;

    using Helpers;

    using Newtonsoft.Json;

    public class EnhancedJsonTextWriter : JsonTextWriter
    {
        public EnhancedJsonTextWriter(TextWriter textWriter)
            : base(textWriter)
        {
        }

        public override void WritePropertyName(string name)
        {
            var newName = this.FormatPropertyName(name);
            base.WritePropertyName(newName);
        }

        public override void WritePropertyName(string name, bool escape)
        {
            var newName = this.FormatPropertyName(name);
            base.WritePropertyName(newName, escape);
        }

        private string FormatPropertyName(string originalValue)
        {
            var result = StringHelper.ConvertToCamelCase(originalValue);
            return result;
        }
    }
}