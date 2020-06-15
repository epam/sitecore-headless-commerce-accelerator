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

import { History } from 'history';
import { combineReducers } from 'redux';

import { connectRouter } from 'connected-react-router';

import { SitecoreContext } from 'Foundation/ReactJss';

import * as Account from 'Feature/Account/Integration/Account';
import * as Authentication from 'Feature/Account/Integration/Authentication';
import * as Checkout from 'Feature/Checkout/Integration/Checkout';

import * as ProductSearch from 'Feature/Catalog/Integration/ProductsSearch';
import * as ProductVariant from 'Feature/Catalog/Integration/ProductVariant';
import * as Order from 'Feature/Checkout/Integration/Order';
import * as OrderHistory from 'Feature/Checkout/Integration/OrderHistory';
import * as ShoppingCart from 'Feature/Checkout/Integration/ShoppingCart';

export const makeRootReducer = (history: History) =>
  combineReducers({
    account: Account.rootReducer,
    authentication: Authentication.rootReducer,
    checkout: Checkout.rootReducer,
    currentOrder: Order.rootReducer,
    orderHistory: OrderHistory.rootReducer,
    productsSearch: ProductSearch.reducer,
    router: connectRouter(history),
    selectedProductVariant: ProductVariant.reducer,
    shoppingCart: ShoppingCart.reducer,
    sitecore: SitecoreContext.reducer,
    viewBag: (state = {}) => state,
  });

export default makeRootReducer;
