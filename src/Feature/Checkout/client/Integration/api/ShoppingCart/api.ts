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

import { GetCartResponse } from './models';

import * as DataModels from 'Feature/Checkout/client/dataModel.Generated';

const routeBase = '/apix/client/commerce/carts';

export const getShoppingCart = async (): Promise<Result<Commerce.Cart>> => {
  try {
    const response = await axios.get<GetCartResponse>(`${routeBase}/cart`, null);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const addCartItemAsync = async (requestPayload: DataModels.AddCartLineRequest): Promise<Result<Commerce.Cart>> => {
  try {
    const response = await axios.post<GetCartResponse>(`${routeBase}/cartLines/`, requestPayload);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const updateCartItemAsync = async (requestPayload: DataModels.UpdateCartLineRequest): Promise<Result<Commerce.Cart>> => {
  try {
    const response = await axios.put<GetCartResponse>(`${routeBase}/cartLines/`, requestPayload);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const removeCartItem = async (requestPayload: DataModels.RemoveCartLineRequest): Promise<Result<string>> => {
  try {
    const response = await axios.delete(`${routeBase}/cartLines/`);
    return { data: response.data };
  } catch (e) {
    return { error: e };
  }
};

export const addPromoCode = async (requestPayload: DataModels.PromoCodeRequest): Promise<Result<Commerce.Cart>> => {
  try {
    const response = await axios.post<GetCartResponse>(`${routeBase}/promoCodes/`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};
