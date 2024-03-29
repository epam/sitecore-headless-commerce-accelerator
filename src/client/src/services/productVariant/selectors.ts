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

import * as Jss from 'Foundation/ReactJss';

import { product } from 'services/catalog';

import { ProductVariantGlobalState } from './models';

export const selectedProductVariantList = (state: ProductVariantGlobalState) => state.selectedProductVariant;
export const selectedProductVariant = (state: ProductVariantGlobalState, id: string) =>
  selectedProductVariantList(state)[id];
export const category = (state: ProductVariantGlobalState) => Jss.sitecoreContext(state);

export const productId = (state: ProductVariantGlobalState) => {
  const productInStore = product(state);
  return productInStore ? productInStore.productId : '';
};
export const variants = (state: ProductVariantGlobalState) => product(state) && product(state).variants;

export const commerceUser = (state: ProductVariantGlobalState) => Jss.sitecoreContext(state).commerceUser;
