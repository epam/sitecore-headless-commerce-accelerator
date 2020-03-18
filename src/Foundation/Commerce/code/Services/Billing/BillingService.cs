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
    using System;

    using DependencyInjection;

    using Models;
    using Models.Entities;
    using Models.Entities.Addresses;
    using Models.Entities.Billing;
    using Models.Entities.Payment;

    [Service(typeof(IBillingService), Lifetime = Lifetime.Singleton)]
    public class BillingService : IBillingService
    {
        public Result<BillingInfo> GetBillingOptions()
        {
            throw new NotImplementedException();
        }

        public Result<VoidResult> SetPaymentOptions(Address billingAddress, FederatedPaymentInfo federatedPayment)
        {
            throw new NotImplementedException();
        }
    }
}