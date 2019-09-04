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

import { initialState as productsSearch } from 'Feature/Catalog/client/Integration/ProductsSearch';
import { initialState as currentOrder } from 'Feature/Checkout/client/Integration/Order';
import { initialState as orderHistory } from 'Feature/Checkout/client/Integration/OrderHistory';
import { initialState as shoppingCart } from 'Feature/Checkout/client/Integration/ShoppingCart';

import { AppState } from '../models';

// allow any here due to complex structure of view bag and sitecore
export default (sitecore: any = {}, viewBag: any = {}): AppState => ({
  currentOrder,
  orderHistory,
  productsSearch,
  // fix to get rid of "Could not find router reducer in state tree, it must be mounted under "router""
  router: undefined,
  shoppingCart,
  sitecore,
  viewBag
});
