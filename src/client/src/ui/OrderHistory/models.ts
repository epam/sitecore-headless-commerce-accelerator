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

import * as Models from 'services/commerce';
import * as JSS from 'Foundation/ReactJss';
import { GlobalOrderState } from 'services/order';

export interface OrderHistoryOwnProps
  extends JSS.RenderingWithContext<JSS.BaseDataSourceItem, JSS.ImageFallbackContext> {}

export interface OrderHistoryStateProps {
  orders: Models.Order[];
  loaded: boolean;
  isLastPage: boolean;
}

export interface OrderHistoryDispatchProps {
  GetOrderHistory: () => void;
  LoadMore: () => void;
}

export interface OrderHistoryProps extends OrderHistoryOwnProps, OrderHistoryStateProps, OrderHistoryDispatchProps {}
export interface OrderHistoryState extends JSS.SafePureComponentState {
  isFirstLoad: boolean;
}

export interface AppState extends GlobalOrderState, JSS.RoutingState {}
