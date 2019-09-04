//    Copyright 2019 EPAM Systems, Inc.
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

import { CartItemDto, GetCartResponse } from './models';

import * as DataModels from 'Feature/Checkout/client/dataModel.Generated';

const routeBase = '/apix/client/commerce/cart';

export const getShoppingCart = async (): Promise<Result<Commerce.CartModel>> => {
  try {
    const response = await axios.get<GetCartResponse>(`${routeBase}/get`, null);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const addCartItemAsync = async (requestPayload: CartItemDto): Promise<Result<Commerce.CartModel>> => {
  try {
    const response = await axios.post<GetCartResponse>(`${routeBase}/add/`, requestPayload);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const updateCartItemAsync = async (requestPayload: CartItemDto): Promise<Result<Commerce.CartModel>> => {
  try {
    const response = await axios.post<GetCartResponse>(`${routeBase}/update/`, requestPayload);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const addPromoCode = async (requestPayload: DataModels.PromoCodeDto): Promise<Result<Commerce.CartModel>> => {
  try {
    const response = await axios.post<GetCartResponse>(`${routeBase}/addpromo/`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const removeCartItem = async (cartLine: string): Promise<Result<string>> => {
  try {
    const response = await axios.delete(`${routeBase}/remove/${cartLine}`);
    return { data: response.data };
  } catch (e) {
    return { error: e };
  }
};
