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

import { Order, ProductColorsContext } from 'Foundation/Commerce/client';

import * as Orders from 'Feature/Checkout/client/Integration/Order';

export interface OrderConfirmationOwnProps extends JSS.RenderingWithContext<JSS.BaseDataSourceItem, OrderConfirmationContext> {}

export interface OrderConfirmationStateProps {
  currentOrder: Order;
  trackingNumber: string;
}

export interface OrderConfirmationDispatchProps {
  GetOrder: (trackingNumber: string) => void;
}

export interface OrderConfirmationProps extends OrderConfirmationOwnProps, OrderConfirmationStateProps, OrderConfirmationDispatchProps {}

export interface OrderConfirmationState extends JSS.SafePureComponentState {}

export interface AppState extends Orders.GlobalCurrentOrderState, JSS.RoutingState {}

export interface OrderConfirmationContext extends ProductColorsContext, JSS.ImageFallbackContext {}