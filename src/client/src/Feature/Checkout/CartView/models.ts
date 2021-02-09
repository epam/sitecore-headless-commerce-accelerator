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

import { ProductColorsContext } from 'Foundation/Commerce';

import * as ShoppingCart from 'Feature/Checkout/Integration/ShoppingCart';

import * as Authentication from 'Feature/Account/Integration/Authentication';

export interface CartOwnProps extends JSS.RenderingWithContext<JSS.BaseDataSourceItem, CartContext> {}
export interface CartFunctionProps {
  ToggleClick: () => void;
}

export interface CartStateProps {
  isLoading: boolean;
  shoppingCartData: ShoppingCart.ShoppingCartData;
  authenticationProcess: Authentication.AuthenticationProcessState;
  logoutProcess: Authentication.LogoutProcessState;
}

export interface CartDispatchProps {
  LoadCart: () => void;
  RemoveCartLine: (model: ShoppingCart.ShoppingCartLine) => void;
}

export interface CartProps extends CartFunctionProps, CartStateProps, CartDispatchProps, CartOwnProps {}
export interface CartState extends JSS.SafePureComponentState {}
export interface AppState extends ShoppingCart.GlobalShoppingCartState, Authentication.GlobalAuthenticationState, Authentication.LogoutProcessState {}
export interface CartContext extends ProductColorsContext, JSS.ImageFallbackContext {}
