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

import { initialState as order } from 'services/order';
import { initialState as search } from 'services/search';
import { initialState as shoppingCart } from 'services/shoppingCart';

import { AppState } from '../models';

// allow any here due to complex structure of view bag and sitecore
export default (sitecore: any = {}, viewBag: any = {}): AppState => ({
  order,
  // fix to get rid of "Could not find router reducer in state tree, it must be mounted under "router""
  router: undefined,
  search,
  shoppingCart,
  sitecore,
  viewBag
});
