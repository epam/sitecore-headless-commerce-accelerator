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

import { Variant } from 'Foundation/Commerce';

import { ProductVariantGlobalState } from 'Feature/Catalog/Integration/ProductVariant';
import { ShoppingCart } from 'Feature/Checkout/Integration/api';
import { GlobalShoppingCartState } from 'Feature/Checkout/Integration/ShoppingCart';

export interface AddToCartOwnProps extends Jss.Rendering<Jss.BaseDataSourceItem> {}
export interface AddToCartStateProps {
  isLoading: boolean;
  productId: string;
  variant: Variant;
}
export interface AddToCartDispatchProps {
  AddToCart: (model: ShoppingCart.CartItemDto) => void;
}

export interface AddToCartProps extends AddToCartOwnProps, AddToCartStateProps, AddToCartDispatchProps {}
export interface AddToCartState extends Jss.SafePureComponentState {
  quantityCount: number;
}

export interface AppState extends GlobalShoppingCartState, ProductVariantGlobalState {}
