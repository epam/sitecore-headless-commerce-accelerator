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

import * as Commerce from 'Foundation/Commerce';
import { Result } from 'models';

import { GetOrderResponse, GetOrdersResponse } from './models';
import * as DataModel from 'services/checkout/models/generated';

const routeBase = '/apix/client/commerce/orders';

export const getOrder = async (trackingNumber: string): Promise<Result<Commerce.Order>> => {
  try {
    const response = await axios.get<GetOrderResponse>(`${routeBase}/${trackingNumber}`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getOrders = async (getOrdersRequest: DataModel.GetOrdersRequest): Promise<Result<Commerce.Order[]>> => {
  try {
    const response = await axios.get<GetOrdersResponse>(`${routeBase}`, {
      params: getOrdersRequest,
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
