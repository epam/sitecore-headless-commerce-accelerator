//    Copyright 2021 EPAM Systems, Inc.
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

import { Action } from 'models';

import * as DataModels from 'services/checkout/models/generated';
import { FreeShippingResult } from 'services/commerce';

import {
  CartItemDto,
  CartLineSuccessPayload,
  CartSuccessPayload,
  RemoveCartLinePayload,
  ShoppingCartData,
  ShoppingCartLine,
  UpdateCartItemFailurePayload,
  UpdateCartItemRequestPayload,
  UpdateCartItemSuccessPayload,
} from './models';

export type LoadCart = () => Action;

export type GetCartSuccess = (cart: ShoppingCartData) => Action<CartSuccessPayload>;

export type AddToCart = (model: CartItemDto) => Action<CartItemDto>;

export type AddToCartSuccess = (cart: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type UpdateCartLine = (model: CartItemDto) => Action<CartItemDto>;

export type UpdateCartLineSuccess = (model: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type RemoveCartLine = (model: ShoppingCartLine) => Action<RemoveCartLinePayload>;

export type RemoveCartLineSuccess = (model: ShoppingCartData) => Action<CartSuccessPayload>;

export type CleanCart = () => Action;

export type CleanCartSuccess = (cart: ShoppingCartData) => Action<CartSuccessPayload>;

export type AddPromoCode = (promoCode: DataModels.PromoCodeRequest) => Action;

export type AddPromoCodeSuccess = (cart: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type GetFreeShippingSubtotal = (callback: (value: FreeShippingResult) => void) => Action;

export type RemovePromoCode = (promoCode: DataModels.PromoCodeRequest) => Action;

export type RemovePromoCodeSuccess = (cart: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type UpdateCartItemRequest = (payload: UpdateCartItemRequestPayload) => Action<UpdateCartItemRequestPayload>;
export type UpdateCartItemSuccess = (payload: UpdateCartItemSuccessPayload) => Action<UpdateCartItemSuccessPayload>;
export type UpdateCartItemFailure = (payload: UpdateCartItemFailurePayload) => Action<UpdateCartItemFailurePayload>;
