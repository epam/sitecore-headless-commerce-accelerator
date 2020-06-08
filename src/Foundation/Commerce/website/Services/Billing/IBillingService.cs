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

namespace HCA.Foundation.Commerce.Services.Billing
{
    using Base.Models.Result;

    using Models.Entities.Addresses;
    using Models.Entities.Billing;
    using Models.Entities.Payment;

    /// <summary>
    /// Performs main operations with billing and payment options
    /// </summary>
    public interface IBillingService
    {
        /// <summary>
        /// Allows to get billing info
        /// </summary>
        /// <returns>Billing options result</returns>
        Result<BillingInfo> GetBillingInfo();

        /// <summary>
        /// Sets payment info
        /// </summary>
        /// <param name="billingAddress">Billing address</param>
        /// <param name="federatedPayment">Contains payment information</param>
        /// <returns>Void result</returns>
        Result<VoidResult> SetPaymentInfo(Address billingAddress, FederatedPaymentInfo federatedPayment);
    }
}