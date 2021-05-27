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

import { connectRouter } from 'connected-react-router';
import { History } from 'history';
import { combineReducers } from 'redux';

import * as StoreLocator from 'services/storeLocator';
import * as Wishlist from 'services/wishlist';

import { SitecoreContext } from 'Foundation/ReactJss';

import * as Account from 'services/account';
import * as Authentication from 'services/authentication';
import * as Checkout from 'services/checkout';
import * as HamburgerMenu from 'services/navigationMenu';
import * as Order from 'services/order';
import * as ProductVariant from 'services/productVariant';
import * as Search from 'services/search';
import * as ShoppingCart from 'services/shoppingCart';

export const makeRootReducer = (history: History) =>
  combineReducers({
    account: Account.rootReducer,
    authentication: Authentication.rootReducer,
    checkout: Checkout.rootReducer,
    hamburgerMenu: HamburgerMenu.reducer,
    order: Order.rootReducer,
    router: connectRouter(history),
    search: Search.reducer,
    selectedProductVariant: ProductVariant.reducer,
    shoppingCart: ShoppingCart.reducer,
    sitecore: SitecoreContext.reducer,
    storeLocator: StoreLocator.reducer,
    viewBag: (state = {}) => state,
    wishlist: Wishlist.rootReducer,
  });

export default makeRootReducer;
