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

namespace HCA.Foundation.Connect.Providers
{
    using Base.Providers.Object;

    using DependencyInjection;
    using HCA.Foundation.ConnectBase.Providers;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Catalog;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Diagnostics;

    [Service(typeof(IConnectServiceProvider), Lifetime = Lifetime.Singleton)]
    public class ConnectServiceProvider : IConnectServiceProvider
    {
        private readonly IObjectProvider objectProvider;

        public ConnectServiceProvider(IObjectProvider objectProvider)
        {
            Assert.ArgumentNotNull(objectProvider, nameof(objectProvider));

            this.objectProvider = objectProvider;
        }

        public CartServiceProvider GetCartServiceProvider()
        {
            return this.GetConnectServiceProvider<CartServiceProvider>("cartServiceProvider");
        }

        public CatalogServiceProvider GetCatalogServiceProvider()
        {
            return this.GetConnectServiceProvider<CatalogServiceProvider>("catalogServiceProvider");
        }

        public CartServiceProviderBase GetCommerceCartServiceProvider()
        {
            return this.GetConnectServiceProvider<CartServiceProviderBase>("cartServiceProvider");
        }

        public CustomerServiceProvider GetCustomerServiceProvider()
        {
            return this.GetConnectServiceProvider<CustomerServiceProvider>("customerServiceProvider");
        }

        public InventoryServiceProvider GetInventoryServiceProvider()
        {
            return this.GetConnectServiceProvider<InventoryServiceProvider>("inventoryServiceProvider");
        }

        public OrderServiceProvider GetOrderServiceProvider()
        {
            return this.GetConnectServiceProvider<OrderServiceProvider>("orderServiceProvider");
        }

        public PaymentServiceProviderBase GetPaymentServiceProvider()
        {
            return this.GetConnectServiceProvider<PaymentServiceProviderBase>("paymentServiceProvider");
        }

        public PricingServiceProviderBase GetPricingServiceProvider()
        {
            return this.GetConnectServiceProvider<PricingServiceProviderBase>("pricingServiceProvider");
        }

        public ShippingServiceProvider GetShippingServiceProvider()
        {
            return this.GetConnectServiceProvider<ShippingServiceProvider>("shippingServiceProvider");
        }

        private TServiceProvider GetConnectServiceProvider<TServiceProvider>(string serviceProviderName)
            where TServiceProvider : ServiceProvider
        {
            return this.objectProvider.GetObject<TServiceProvider>(serviceProviderName);
        }
    }
}