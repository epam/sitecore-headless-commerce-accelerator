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
using System.Collections.Generic;
using System.Linq;
using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Commerce.Services.Customers;
using Wooli.Foundation.Commerce.Context;
using Wooli.Foundation.Commerce.ModelInitilizers;
using Wooli.Foundation.Commerce.ModelMappers;
using Wooli.Foundation.Commerce.Models;
using Wooli.Foundation.Commerce.Models.Checkout;
using Wooli.Foundation.Connect.Managers;

namespace Wooli.Foundation.Commerce.Repositories
{
    public abstract class BaseCheckoutRepository
    {
        protected readonly ICartManager CartManager;

        protected readonly ICartModelBuilder CartModelBuilder;

        protected readonly ICatalogRepository CatalogRepository;

        protected readonly IEntityMapper EntityMapper;

        protected BaseCheckoutRepository(
            ICartManager cartManager,
            ICatalogRepository catalogRepository,
            IAccountManager accountManager,
            ICartModelBuilder cartModelBuilder,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
        {
            AccountManager = accountManager;
            StorefrontContext = storefrontContext;
            VisitorContext = visitorContext;
            CartManager = cartManager;
            CatalogRepository = catalogRepository;
            CartModelBuilder = cartModelBuilder;
            EntityMapper = entityMapper;
        }

        public IAccountManager AccountManager { get; }

        public IStorefrontContext StorefrontContext { get; }

        public IVisitorContext VisitorContext { get; }

        protected virtual void AddUserInfo<T>(BaseCheckoutModel baseCheckoutModel, Result<T> result) where T : class
        {
            ManagerResponse<GetPartiesResult, IEnumerable<Party>> currentCustomerParties =
                AccountManager.GetCurrentCustomerParties(StorefrontContext.ShopName, VisitorContext.ContactId);

            if (currentCustomerParties.ServiceProviderResult.Success && currentCustomerParties.Result != null)
            {
                baseCheckoutModel.UserAddresses = new List<AddressModel>();
                foreach (Party party in currentCustomerParties.Result)
                {
                    AddressModel address = EntityMapper.MapToAddress(party);
                    var commerceParty = party as CommerceParty;
                    address.Name = commerceParty.Name;
                    address.CountryCode = commerceParty.CountryCode;
                    baseCheckoutModel.UserAddresses.Add(address);
                }
            }
            else
            {
                result.SetErrors(currentCustomerParties.ServiceProviderResult);
            }
        }

        protected virtual Result<CartModel> GetCart(CartResult serviceProviderResult, Cart cart)
        {
            var result = new Result<CartModel>();

            try
            {
                CartModel model = CartModelBuilder.Initialize(cart);
                result.SetResult(model);

                // ToDo: investigate the sometimes issue where Success=false but no any errors and the action is success
                if (serviceProviderResult.SystemMessages.Any() || cart == null) result.SetErrors(serviceProviderResult);
            }
            catch (Exception ex)
            {
                result.SetErrors(nameof(GetCart), ex);
            }

            return result;
        }
    }
}