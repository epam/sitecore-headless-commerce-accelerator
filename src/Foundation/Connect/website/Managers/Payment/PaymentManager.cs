//    Copyright 2021 EPAM Systems, Inc.
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
    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;
    using HCA.Foundation.ConnectBase.Entities;
    using HCA.Foundation.ConnectBase.Providers;
    using Mappers.Payment;

    using Models.Payment;

    using Providers;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Payments;
    using Sitecore.Diagnostics;

    [Service(typeof(IPaymentManager), Lifetime = Lifetime.Singleton)]
    public class PaymentManager : BaseManager, IPaymentManager
    {
        private readonly IPaymentMapper paymentMapper;

        private readonly PaymentServiceProviderBase paymentServiceProvider;

        public PaymentManager(
            IConnectServiceProvider connectServiceProvider,
            IPaymentMapper paymentMapper,
            ILogService<CommonLog> logService) : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(paymentMapper, nameof(paymentMapper));

            this.paymentServiceProvider = connectServiceProvider.GetPaymentServiceProvider();
            this.paymentMapper = paymentMapper;
        }

        public PaymentClientTokenResult GetPaymentClientToken()
        {
            var result = this.Execute(new ServiceProviderRequest(), this.paymentServiceProvider.GetPaymentClientToken);
            return this.paymentMapper.Map<ConnectBase.Pipelines.Arguments.PaymentClientTokenResult, PaymentClientTokenResult>(result);
        }

        public GetPaymentMethodsResult GetPaymentMethods(Cart cart, PaymentOption paymentOption)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(paymentOption, nameof(paymentOption));

            var request = new ConnectBase.Pipelines.Arguments.GetPaymentMethodsRequest(cart as CommerceCart, paymentOption);

            return this.Execute(request, this.paymentServiceProvider.GetPaymentMethods);
        }

        public GetPaymentOptionsResult GetPaymentOptions(string shopName, Cart cart)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));
            Assert.ArgumentNotNull(cart, nameof(cart));

            var request = new GetPaymentOptionsRequest(shopName, cart);

            return this.Execute(request, this.paymentServiceProvider.GetPaymentOptions);
        }
    }
}