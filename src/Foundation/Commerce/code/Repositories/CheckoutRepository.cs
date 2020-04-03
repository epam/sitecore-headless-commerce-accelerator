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

namespace Wooli.Foundation.Commerce.Repositories
{
    using System;
    using System.Linq;

    using Base.Models;

    using Connect.Context;
    using Connect.Managers;

    using Context;

    using DependencyInjection;

    using Extensions;

    using ModelInitializers;

    using ModelMappers;

    using Models.Checkout;

    using Services.Catalog;

    using Sitecore.Diagnostics;

    [Service(typeof(ICheckoutRepository), Lifetime = Lifetime.Singleton)]
    public class CheckoutRepository : BaseCheckoutRepository, ICheckoutRepository
    {
        public CheckoutRepository(
            IOrderManager orderManager,
            ICartManager cartManager,
            ICatalogService catalogService,
            IAccountManager accountManager,
            ICartModelBuilder cartModelBuilder,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
            : base(
                cartManager,
                catalogService,
                accountManager,
                cartModelBuilder,
                entityMapper,
                storefrontContext,
                visitorContext)
        {
            this.OrderManager = orderManager;
        }

        protected IOrderManager OrderManager { get; }

        public virtual Result<SubmitOrderModel> SubmitOrder()
        {
            var result = new Result<SubmitOrderModel>();
            var model = new SubmitOrderModel();
            try
            {
                var currentCart = this.CartManager.GetCurrentCart(
                    this.StorefrontContext.ShopName,
                    this.VisitorContext.ContactId);
                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                var managerResponse = this.OrderManager.SubmitVisitorOrder(currentCart.Result);
                if (managerResponse.ServiceProviderResult.Success
                    || !managerResponse.ServiceProviderResult.SystemMessages.Any())
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
                result.SetErrors(nameof(this.SubmitOrder), ex);
            }

            return result;
        }
    }
}