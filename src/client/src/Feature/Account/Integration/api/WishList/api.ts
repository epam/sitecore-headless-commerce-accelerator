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

import axios, { AxiosError, AxiosResponse } from 'axios';

import { Result } from 'Foundation/Integration';

import { GetWishlistResponse } from './models';

import { Variant } from 'Foundation/Commerce';
import { Wishlist } from '../../Wishlist/mock';

const routeBase = '/apix/client/commerce';

export const getWishlist = async (): Promise<Result<Wishlist>> =>
  axios
    .get<GetWishlistResponse>(`${routeBase}/wishlist`)
    .then((response) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));

export const addWishlistItem = async (item: Variant): Promise<Result<Wishlist>> =>
  axios
    .post<GetWishlistResponse>(`${routeBase}/wishlist`, item)
    .then((response) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));

export const updateWishlistItem = async (item: Variant): Promise<Result<Wishlist>> =>
  axios
    .put<GetWishlistResponse>(`${routeBase}/wishlist`, item)
    .then((response) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));

export const removeWishlistItem = async (id: string): Promise<Result<Wishlist>> =>
  axios
    .delete(`${routeBase}/wishlist/${id}`)
    .then((response: AxiosResponse<GetWishlistResponse>) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));
