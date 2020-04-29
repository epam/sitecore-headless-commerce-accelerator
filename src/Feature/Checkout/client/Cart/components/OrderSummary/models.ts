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

import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

import * as DataModels from 'Feature/Checkout/client/dataModel.Generated';

export interface OrderSummaryOwnProps {
  price: ShoppingCart.ShoppingCartPrice;
  rendering: any;
}
export interface OrderSummaryStateProps {
  isLoading: boolean;
  isFailure: boolean;
  adjustments: string[];
}
export interface OrderSummaryDispatchProps {
  AddPromoCode: (model: DataModels.PromoCodeRequest) => void;
}

export interface OrderSummaryProps extends OrderSummaryOwnProps, OrderSummaryStateProps, OrderSummaryDispatchProps {}

export interface OrderSummaryState extends JSS.SafePureComponentState {}

export interface AppState extends ShoppingCart.GlobalShoppingCartState {}
