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

namespace HCA.Foundation.ReactJss.Helpers
{
    using System;
    using System.Linq;
    using System.Text;

    using Sitecore.Diagnostics;

    public static class StringHelper
    {
        public static string ConvertToCamelCase(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return phrase;
            }

            var splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            var isFirst = true;
            foreach (var s in splittedPhrase)
            {
                if (s.Length <= 0)
                {
                    continue;
                }

                var modifiedValue = !isFirst ? FormatFirstChar(s, x => x.ToUpper()) :
                    s.IsUpper() ? s.ToLower() : FormatFirstChar(s, x => x.ToLower());

                sb.Append(modifiedValue);

                isFirst = false;
            }

            return sb.ToString();
        }

        public static bool IsLower(this string str)
        {
            return str.All(ch => !char.IsUpper(ch));
        }

        public static bool IsUpper(this string str)
        {
            return str.All(ch => !char.IsLower(ch));
        }

        private static string FormatFirstChar(string str, Func<string, string> formatter)
        {
            Assert.ArgumentNotNullOrEmpty(str, nameof(str));
            Assert.ArgumentNotNull(formatter, nameof(formatter));

            var firstPart = formatter(str.Substring(0, 1));
            var lastPart = str.Substring(1);
            var resultString = firstPart + lastPart;

            return resultString;
        }
    }
}