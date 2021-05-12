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

import { all } from 'redux-saga/effects';

import { SitecoreContext } from 'Foundation/ReactJss';

import * as StoreLocator from 'services/storeLocator';
import * as Wishlist from 'services/wishlist';

import * as Account from 'services/account';
import * as Authentication from 'services/authentication';
import * as Checkout from 'services/checkout';
import * as Order from 'services/order';
import * as ShoppingCart from 'services/shoppingCart';

import * as ProductSearch from 'Feature/Catalog/Integration/ProductsSearch';
import * as ProductSearchSuggestion from 'Feature/Catalog/Integration/ProductsSearchSuggestions';

export default function* rootSaga() {
  yield all([
    ...Account.rootSaga,
    ...Authentication.rootSaga,
    ...Wishlist.rootSaga,
    ...ProductSearch.rootSaga,
    ...ShoppingCart.rootSaga,
    ...Checkout.rootSaga,
    ...SitecoreContext.rootSaga,
    ...StoreLocator.rootSaga,
    ...Order.rootSaga,
    ...ProductSearchSuggestion.rootSaga,
  ]);
}
