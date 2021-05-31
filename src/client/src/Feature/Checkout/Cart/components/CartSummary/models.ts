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

import * as JSS from 'Foundation/ReactJss';

import { ProductColors } from 'Foundation/Commerce';

import * as ShoppingCart from 'services/shoppingCart';

export interface CartSummaryOwnProps {
  cartLines: ShoppingCart.ShoppingCartLine[];
  productColors: ProductColors;
  fallbackImageUrl: string;
}
export interface CartSummaryStateProps {
  isLoading: boolean;
}
export interface CartSummaryDispatchProps {
  UpdateCartLine: (model: ShoppingCart.CartItemDto) => void;
  RemoveCartLine: (model: ShoppingCart.ShoppingCartLine) => void;
}

export interface CartSummaryProps extends CartSummaryOwnProps, CartSummaryStateProps, CartSummaryDispatchProps {}
export interface CartSummaryState extends JSS.SafePureComponentState {}

export interface AppState extends ShoppingCart.GlobalShoppingCartState {}
