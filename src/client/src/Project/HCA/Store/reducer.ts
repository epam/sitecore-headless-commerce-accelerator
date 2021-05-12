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

import { connectRouter } from 'connected-react-router';
import { History } from 'history';
import { combineReducers } from 'redux';

import { connectRouter } from 'connected-react-router';

import { SitecoreContext } from 'Foundation/ReactJss';

import * as GlobalError from 'Foundation/UI/common/components/Errors/Integration';

import * as Checkout from 'Feature/Checkout/Integration/Checkout';

import * as ProductSearch from 'Feature/Catalog/Integration/ProductsSearch';
import * as ProductSearchSuggestion from 'Feature/Catalog/Integration/ProductsSearchSuggestions';
import * as HamburgerMenu from 'Feature/Navigation/Header/NavigationMenu/Integration';
import * as Account from 'services/account';
import * as Authentication from 'services/authentication';
import * as Checkout from 'services/checkout';
import * as Order from 'services/order';
import * as ProductVariant from 'services/productVariant';
import * as ShoppingCart from 'services/shoppingCart';

export const makeRootReducer = (history: History) =>
  combineReducers({
    account: Account.rootReducer,
    authentication: Authentication.rootReducer,
    checkout: Checkout.rootReducer,
    globalError: GlobalError.reducer,
    hamburgerMenu: HamburgerMenu.reducer,
    order: Order.rootReducer,
    productSearchSuggestion: ProductSearchSuggestion.reducer,
    productsSearch: ProductSearch.reducer,
    router: connectRouter(history),
    selectedProductVariant: ProductVariant.reducer,
    shoppingCart: ShoppingCart.reducer,
    sitecore: SitecoreContext.reducer,
    viewBag: (state = {}) => state,
  });

export default makeRootReducer;
