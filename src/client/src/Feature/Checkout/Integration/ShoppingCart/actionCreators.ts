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

import { Action } from 'Foundation/Integration';

import * as ShoppingCart from 'Feature/Checkout/Integration/api/ShoppingCart';

import * as DataModels from 'Feature/Checkout/dataModel.Generated';

import {
  CartLineSuccessPayload,
  CartSuccessPayload,
  RemoveCartLinePayload,
  ShoppingCartData,
  ShoppingCartLine,
} from './models';

export type LoadCart = () => Action;

export type GetCartSuccess = (cart: ShoppingCartData) => Action<CartSuccessPayload>;

export type AddToCart = (model: ShoppingCart.CartItemDto) => Action<ShoppingCart.CartItemDto>;

export type AddToCartSuccess = (cart: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type UpdateCartLine = (model: ShoppingCart.CartItemDto) => Action<ShoppingCart.CartItemDto>;

export type UpdateCartLineSuccess = (model: ShoppingCartData) => Action<CartLineSuccessPayload>;

export type RemoveCartLine = (model: ShoppingCartLine) => Action<RemoveCartLinePayload>;

export type RemoveCartLineSuccess = (model: ShoppingCartData) => Action<CartSuccessPayload>;

export type AddPromoCode = (promoCode: DataModels.PromoCodeRequest) => Action;

export type AddPromoCodeSuccess = (cart: ShoppingCartData) => Action<CartLineSuccessPayload>;
