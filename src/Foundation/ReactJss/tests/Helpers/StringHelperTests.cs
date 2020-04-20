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

namespace HCA.Foundation.ReactJss.Tests.Helpers
{
    using ReactJss.Helpers;

    using Xunit;

    public class StringHelperTests
    {
        [Theory]
        [InlineData("OneWord", "oneWord")]
        [InlineData("Two Words", "twoWords")]
        [InlineData("Long long long text", "longLongLongText")]
        [InlineData("Long long long text with Dot.", "longLongLongTextWithDot")]
        [InlineData("Text with CAPS.", "textWithCAPS")]
        [InlineData("CAPS First Text", "capsFirstText")]
        [InlineData("Text with particialCAPS", "textWithParticialCAPS")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ConvertToCamelCase_InputString_ReturnsCorrectValue(string str, string expected)
        {
            var result = StringHelper.ConvertToCamelCase(str);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("lower")]
        [InlineData("12345")]
        public void IsLower_LowerString_ReturnsTrue(string str)
        {
            var result = str.IsLower();
            Assert.True(result);
        }

        [Theory]
        [InlineData("notLower")]
        [InlineData("UPPER")]
        public void IsLower_NotLowerString_ReturnsFalse(string str)
        {
            var result = str.IsLower();
            Assert.False(result);
        }

        [Theory]
        [InlineData("notUpper")]
        [InlineData("lower")]
        public void IsUpper_NotUpperString_ReturnsFalse(string str)
        {
            var result = str.IsUpper();
            Assert.False(result);
        }

        [Theory]
        [InlineData("UPPER")]
        [InlineData("12345")]
        public void IsUpper_UpperString_ReturnsTrue(string str)
        {
            var result = str.IsUpper();
            Assert.True(result);
        }
    }
}