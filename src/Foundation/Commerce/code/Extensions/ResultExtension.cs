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

namespace Wooli.Foundation.Commerce.Extensions
{
    using System;

    using Base.Models;
    using Base.Models.Result;

    using Sitecore.Commerce.Services;
    using Sitecore.Diagnostics;

    public static class ResultExtension
    {
        [Obsolete("This method is obsolete. Use SetErrors(IList<string> errors) Result class method instead.")]
        public static void SetErrors<T>(this Result<T> result, ServiceProviderResult providerResult)
            where T : class
        {
            Assert.ArgumentNotNull(result, nameof(result));
            Assert.ArgumentNotNull(providerResult, nameof(providerResult));

            result.Success = providerResult.Success;
            if (providerResult.SystemMessages.Count <= 0)
            {
                return;
            }

            foreach (var systemMessage in providerResult.SystemMessages)
            {
                var message = !string.IsNullOrEmpty(systemMessage.Message) ? systemMessage.Message : null;
                result.SetError(message);
            }
        }
    }
}