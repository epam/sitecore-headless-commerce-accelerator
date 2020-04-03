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

namespace Wooli.Foundation.Connect.Managers.Payment
{
    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Mappers;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Payments;
    using Sitecore.Diagnostics;

    using GetPaymentMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Payments.GetPaymentMethodsRequest;

    [Service(typeof(IPaymentManagerV2), Lifetime = Lifetime.Singleton)]
    public class PaymentManagerV2 : BaseManager, IPaymentManagerV2
    {
        private readonly PaymentServiceProvider paymentServiceProvider;
        private readonly IConnectEntityMapper connectMapper;

        public PaymentManagerV2(IConnectServiceProvider connectServiceProvider, IConnectEntityMapper connectMapper, ILogService<CommonLog> logService) : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(connectMapper, nameof(connectMapper));

            this.paymentServiceProvider = connectServiceProvider.GetPaymentServiceProvider();
            this.connectMapper = connectMapper;
        }

        public Models.Payment.PaymentClientTokenResult GetPaymentClientToken()
        {
            return this.Execute(
                new ServiceProviderRequest(),
                req =>
                {
                    var serviceProviderResult =
                        this.paymentServiceProvider.RunPipeline<ServiceProviderRequest, PaymentClientTokenResult>(
                            "commerce.payments.getClientToken",
                            req);

                    return this.connectMapper
                        .Map<PaymentClientTokenResult, Models.Payment.PaymentClientTokenResult>(serviceProviderResult);
                });
        }

        public GetPaymentMethodsResult GetPaymentMethods(Cart cart, PaymentOption paymentOption)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(paymentOption, nameof(paymentOption));

            var request = new GetPaymentMethodsRequest(cart as CommerceCart, paymentOption);

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