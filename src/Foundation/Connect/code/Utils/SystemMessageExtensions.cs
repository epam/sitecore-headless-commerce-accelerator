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

namespace Wooli.Foundation.Connect.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Commerce.Services;
    using Sitecore.Diagnostics;

    public static class SystemMessageExtensions
    {
        public static void LogSystemMessages(this IEnumerable<SystemMessage> messages, object owner)
        {
            var source = messages as IList<SystemMessage> ?? messages.ToList();

            if (!source.Any())
            {
                return;
            }

            foreach (var systemMessage in source)
            {
                Log.Error(systemMessage.Message, owner);
            }
        }
    }
}