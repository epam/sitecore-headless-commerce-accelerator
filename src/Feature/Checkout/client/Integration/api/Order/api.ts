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

import axios from 'axios';

import * as Commerce from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import { GetOrderResponse, GetOrdersResponse } from './models';

const routeBase = '/apix/client/commerce/order';

export const getOrder = async (trackingNumber: string): Promise<Result<Commerce.OrderModel>> => {
  try {
    const response = await axios.get<GetOrderResponse>(`${routeBase}/get/${trackingNumber}`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getOrders = async (page: number, count: number): Promise<Result<Commerce.OrderHistoryResultModel>> => {
  try {
    const response = await axios.get<GetOrdersResponse>(`${routeBase}/get`, {
      params: {
        count,
        page
      }
    });

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};
