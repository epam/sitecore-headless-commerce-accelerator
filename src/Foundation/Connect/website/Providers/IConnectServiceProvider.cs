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
    using HCA.Foundation.ConnectBase.Providers;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Catalog;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Commerce.Services.Shipping;

    public interface IConnectServiceProvider
    {
        CartServiceProvider GetCartServiceProvider();

        CatalogServiceProvider GetCatalogServiceProvider();

        CartServiceProviderBase GetCommerceCartServiceProvider();

        CustomerServiceProvider GetCustomerServiceProvider();

        InventoryServiceProvider GetInventoryServiceProvider();

        OrderServiceProvider GetOrderServiceProvider();

        PaymentServiceProviderBase GetPaymentServiceProvider();

        PricingServiceProviderBase GetPricingServiceProvider();

        ShippingServiceProvider GetShippingServiceProvider();

    }
}