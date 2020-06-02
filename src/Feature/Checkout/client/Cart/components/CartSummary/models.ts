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

import * as JSS from 'Foundation/ReactJss/client';

import { ProductColors } from 'Foundation/Commerce/client';

import { ShoppingCart as ShoppingCartApi } from 'Feature/Checkout/client/Integration/api';
import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

export interface CartSummaryOwnProps {
  cartLines: ShoppingCart.ShoppingCartLine[];
  productColors: ProductColors;
  fallbackImageUrl: string;
}
export interface CartSummaryStateProps {
  isLoading: boolean;
}
export interface CartSummaryDispatchProps {
  UpdateCartLine: (model: ShoppingCartApi.CartItemDto) => void;
}

export interface CartSummaryProps extends CartSummaryOwnProps, CartSummaryStateProps, CartSummaryDispatchProps {}
export interface CartSummaryState extends JSS.SafePureComponentState {}

export interface AppState extends ShoppingCart.GlobalShoppingCartState {}
