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

namespace HCA.Foundation.Commerce
{
    public static class Constants
    {
        public enum ItemType
        {
            Unknown,

            Category,

            Product,

            Variant
        }

        public const string CommerceRoutePrefix = "apix/client/commerce";

        public const string CommerceCustomerIdPrefix = "Entity-Customer-";

        public static class Login
        {
            public const string CommerceUserSource = "CommerceUser";
        }

        public static class StorefrontSettings
        {
            public const string FederatedPaymentOptionTitle = "Federated";
        }
    }
}