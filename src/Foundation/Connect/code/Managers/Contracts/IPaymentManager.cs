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

namespace Wooli.Foundation.Connect.Managers
{
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Payments;

    public interface IPaymentManager
    {
        ManagerResponse<ServiceProviderResult, string> GetPaymentClientToken();

        ManagerResponse<GetPaymentMethodsResult, IEnumerable<PaymentMethod>> GetPaymentMethods(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            PaymentOption paymentOption);

        ManagerResponse<GetPaymentOptionsResult, IEnumerable<PaymentOption>> GetPaymentOptions(
            string shopName,
            Sitecore.Commerce.Entities.Carts.Cart cart);
    }
}