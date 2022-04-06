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

namespace HCA.Foundation.Base.Extensions
{
    using System;
    using System.Globalization;

    using Sitecore;

    public static class CustomPropertyExtensions
    {
        public static string GetCustomProperty(string property)
        {
            string customProperty;
            try
            {
                customProperty = Context.User.Profile.GetCustomProperty(property);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ex.Message),
                    ex);
            }

            return customProperty;
        }
    }
}