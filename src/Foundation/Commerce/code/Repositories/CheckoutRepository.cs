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

using System;
using System.Linq;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Entities.Orders;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Commerce.Services.Orders;
using Sitecore.Diagnostics;
using Wooli.Foundation.Commerce.Context;
using Wooli.Foundation.Commerce.ModelInitilizers;
using Wooli.Foundation.Commerce.ModelMappers;
using Wooli.Foundation.Commerce.Models;
using Wooli.Foundation.Commerce.Models.Checkout;
using Wooli.Foundation.Connect.Managers;
using Wooli.Foundation.DependencyInjection;

namespace Wooli.Foundation.Commerce.Repositories
{
    [Service(typeof(ICheckoutRepository), Lifetime = Lifetime.Singleton)]
    public class CheckoutRepository : BaseCheckoutRepository, ICheckoutRepository
    {
        public CheckoutRepository(
            IOrderManager orderManager,
            ICartManager cartManager,
            ICatalogRepository catalogRepository,
            IAccountManager accountManager,
            ICartModelBuilder cartModelBuilder,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext,
                visitorContext)
        {
            OrderManager = orderManager;
        }

        protected IOrderManager OrderManager { get; }

        public virtual Result<SubmitOrderModel> SubmitOrder()
        {
            var result = new Result<SubmitOrderModel>();
            var model = new SubmitOrderModel();
            try
            {
                ManagerResponse<CartResult, Cart> currentCart =
                    CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);
                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                ManagerResponse<SubmitVisitorOrderResult, Order> managerResponse =
                    OrderManager.SubmitVisitorOrder(currentCart.Result);
                if (managerResponse.ServiceProviderResult.Success ||
                    !managerResponse.ServiceProviderResult.SystemMessages.Any())
                {
                    model.Temp = managerResponse.Result;
                    model.ConfirmationId = managerResponse.Result.TrackingNumber;
                    result.SetResult(model);

                    return result;
                }

                result.SetErrors(managerResponse.ServiceProviderResult);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(SubmitOrder), ex);
            }

            return result;
        }
    }
}