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

namespace HCA.Foundation.Connect
{
    public static class Constants
    {
        public const string CommerceUsersDomainName = "CommerceUsers";

        public const string DefaultCartName = "Default";

        public static class Search
        {
            public static class ItemType
            {
                public const string Product = "SellableItem";

                public const string Category = "Category";
            }
        }

        public static class Pricing
        {
            public static class PricingTypes
            {
                public const string List = "List";

                public const string Adjusted = "Adjusted";

                public const string LowestPricedVariant = "LowestPricedVariant";

                public const string LowestPricedVariantListPrice = "LowestPricedVariantListPrice";

                public const string HighestPricedVariant = "HighestPricedVariant";
            }
        }
    }
}