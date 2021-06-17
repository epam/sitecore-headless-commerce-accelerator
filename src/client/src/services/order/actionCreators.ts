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

import * as Commerce from 'services/commerce';
import { Action, StatusPayload } from 'models';

import * as Models from './models';

export type GetOrder = (trackingNumber: string) => Action<Models.OrderRequestPayload>;

export type GetOrderRequest = (trackingNumber: string) => Action<StatusPayload>;

export type GetOrderSuccess = (value: Commerce.Order) => Action<Models.OrderSuccessPayload>;

export type GetOrderHistory = () => Action<Models.OrderHistoryRequestPayload>;

export type GetOrderHistoryRequest = () => Action<StatusPayload>;

export type GetOrderHistorySuccess = (
  orders: Commerce.Order[],
  currentPageNumber: number,
  isLastPage: boolean
) => Action<Models.OrderHistorySuccessPayload>;
