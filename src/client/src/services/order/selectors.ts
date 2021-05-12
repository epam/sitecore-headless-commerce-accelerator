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

import { GlobalOrderState } from './models';

const currentOrderContext = (state: GlobalOrderState) => state.order.currentOrder;

export const requestTrackingNumber = (state: GlobalOrderState) => currentOrderContext(state).requestTrackingNumber;
export const currentOrder = (state: GlobalOrderState) => currentOrderContext(state).order;
export const currentOrderTrackingNumber = (state: GlobalOrderState) => currentOrderContext(state).trackingNumber;
export const currentOrderStatus = (state: GlobalOrderState) => currentOrderContext(state).status;

const getOrderHistoryContext = (state: GlobalOrderState) => state.order.orderHistory;

export const orderHistoryIsLastPage = (state: GlobalOrderState) => getOrderHistoryContext(state).isLastPage;
export const orderHistoryCurrentPageNumber = (state: GlobalOrderState) =>
  getOrderHistoryContext(state).currentPageNumber;
export const orderHistoryList = (state: GlobalOrderState) => getOrderHistoryContext(state).orders;
export const orderHistoryStatus = (state: GlobalOrderState) => getOrderHistoryContext(state).status;
