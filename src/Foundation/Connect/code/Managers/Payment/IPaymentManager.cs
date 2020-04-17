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

namespace HCA.Foundation.Connect.Managers.Payment
{
    using Models.Payment;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services.Payments;

    /// <summary>
    /// Performs operations related to payment
    /// </summary>
    public interface IPaymentManager
    {
        /// <summary>
        /// Gets client token for payment by card
        /// </summary>
        /// <returns>Payment client token result</returns>
        PaymentClientTokenResult GetPaymentClientToken();

        /// <summary>
        /// Gets payment methods
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="paymentOption">Payment option</param>
        /// <returns>Get payment methods result</returns>
        GetPaymentMethodsResult GetPaymentMethods(Cart cart, PaymentOption paymentOption);

        /// <summary>
        /// Gets payment options
        /// </summary>
        /// <param name="shopName">Shop name</param>
        /// <param name="cart">Cart</param>
        /// <returns>Get payment options result</returns>
        GetPaymentOptionsResult GetPaymentOptions(string shopName, Cart cart);
    }
}