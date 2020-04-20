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

namespace Wooli.Foundation.Commerce.Models.Checkout
{
    using System.Collections.Generic;

    using TypeLite;

    [TsClass]
    public class BillingModel : BaseCheckoutModel
    {
        public string PaymentClientToken { get; set; }

        public IList<PaymentMethodModel> PaymentMethods { get; set; }

        public IList<PaymentOptionModel> PaymentOptions { get; set; }
    }
}