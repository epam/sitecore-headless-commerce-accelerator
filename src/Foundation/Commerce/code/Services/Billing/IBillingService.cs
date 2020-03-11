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

namespace Wooli.Foundation.Commerce.Services.Billing
{
    using Models;
    using Models.Entities;

    /// <summary>
    /// Performs main operations with billing and payment options
    /// </summary>
    public interface IBillingService
    {
        /// <summary>
        /// Allows to get billing options
        /// </summary>
        /// <returns>Billing options result</returns>
        Result<BillingInfo> GetBillingOptions();

        /// <summary>
        /// Sets payment options
        /// </summary>
        /// <param name="billingAddress">Billing address</param>
        /// <param name="federatedPayment">Contains payment information</param>
        /// <returns></returns>
        Result<VoidResult> SetPaymentOptions(Address billingAddress, FederatedPaymentInfo federatedPayment);
    }
}