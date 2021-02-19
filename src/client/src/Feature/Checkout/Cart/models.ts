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

import * as Commerce from 'Foundation/Commerce';
import * as JSS from 'Foundation/ReactJss';

import * as ShoppingCart from 'Feature/Checkout/Integration/ShoppingCart';

export interface CartDataSource extends JSS.BaseDataSourceItem {
  countries: Commerce.CountryRegion[];
}
export interface CartOwnProps extends JSS.RenderingWithContext<CartDataSource, CartContext> {}

export interface CartStateProps {
  isLoading: boolean;
  shoppingCartData: ShoppingCart.ShoppingCartData;
}

export interface CartDispatchProps {
  LoadCart: () => void;
}

export interface CartProps extends CartStateProps, CartDispatchProps, CartOwnProps {}

export interface CartState extends JSS.SafePureComponentState {
  isFirstInitLoadPage: boolean;
}

export interface AppState extends ShoppingCart.GlobalShoppingCartState {}

export interface CartContext extends Commerce.ProductColorsContext, JSS.ImageFallbackContext {}
