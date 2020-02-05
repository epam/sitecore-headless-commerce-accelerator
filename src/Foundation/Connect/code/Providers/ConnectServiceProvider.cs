//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Connect.Providers
{
    using Contracts;
    using DependencyInjection;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Catalog;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Commerce.Services.Payments;
    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Configuration;

    [Service(typeof(IConnectServiceProvider))]
    public class ConnectServiceProvider : IConnectServiceProvider
    {
        public CommerceCartServiceProvider GetCommerceCartServiceProvider()
        {
            return new CommerceCartServiceProvider();
        }

        public CartServiceProvider GetCartServiceProvider()
        {
            return GetConnectServiceProvider<CartServiceProvider>("cartServiceProvider");
        }

        public CustomerServiceProvider GetCustomerServiceProvider()
        {
            return GetConnectServiceProvider<CustomerServiceProvider>("customerServiceProvider");
        }

        public OrderServiceProvider GetOrderServiceProvider()
        {
            return GetConnectServiceProvider<OrderServiceProvider>("orderServiceProvider");
        }

        public InventoryServiceProvider GetInventoryServiceProvider()
        {
            return GetConnectServiceProvider<InventoryServiceProvider>("inventoryServiceProvider");
        }

        public virtual PricingServiceProvider GetPricingServiceProvider()
        {
            return GetConnectServiceProvider<PricingServiceProvider>("pricingServiceProvider");
        }

        public virtual ShippingServiceProvider GetShippingServiceProvider()
        {
            return GetConnectServiceProvider<ShippingServiceProvider>("shippingServiceProvider");
        }

        public PaymentServiceProvider GetPaymentServiceProvider()
        {
            return GetConnectServiceProvider<PaymentServiceProvider>("paymentServiceProvider");
        }

        public CatalogServiceProvider GetCatalogServiceProvider()
        {
            return GetConnectServiceProvider<CatalogServiceProvider>("catalogServiceProvider");
        }

        protected virtual TServiceProvider GetConnectServiceProvider<TServiceProvider>(string serviceProviderName)
            where TServiceProvider : ServiceProvider
        {
            return (TServiceProvider) Factory.CreateObject(serviceProviderName, true);
        }
    }
}