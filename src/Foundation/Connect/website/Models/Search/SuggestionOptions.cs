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

namespace HCA.Foundation.Connect.Models.Search
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SuggestionOptions
    {
        public string Query { get; set; }

        public string ContextFilterQuery { get; set; }

        public string Dictionary { get; set; }

        public int? Count { get; set; }

        public bool? Build { get; set; }

        public bool? Reload { get; set; }

        public bool? BuildAll { get; set; }

        public bool? ReloadAll { get; set; }
    }
}