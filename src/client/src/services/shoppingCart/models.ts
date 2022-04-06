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

import { LoadingStatus, Status } from 'models';
import * as Commerce from 'services/commerce';

export interface Adjustment extends Commerce.Adjustment {}

export interface ShoppingCartLine extends Commerce.CartLine {}

export interface ShoppingCartPrice extends Commerce.TotalPrice {}

export interface ShoppingCartData extends Commerce.Cart {
  cartLines: ShoppingCartLine[];
  price: ShoppingCartPrice;
}

export interface RequestData {
  actionType: string;
}

export interface ShoppingCartState extends Status, RequestData {
  data?: ShoppingCartData;
  cartItemsState?: {
    string?: {
      status: LoadingStatus;
      error?: string;
    };
  };
}

export interface GlobalShoppingCartState {
  shoppingCart: ShoppingCartState;
}

export interface CartSuccessPayload extends Status {
  data: ShoppingCartData;
}

export interface CartLineSuccessPayload {
  cartUpdated: boolean;
}

export interface RemoveCartLinePayload {
  name: string;
  productId: string;
  quantity: number;
  variantId: string;
  variantName: string;
}

export interface UpdateCartItemRequestPayload {
  productId: string;
  variantId: string;
  quantity: number;
}

export interface UpdateCartItemSuccessPayload {
  productId: string;
  variantId: string;
}

export interface UpdateCartItemFailurePayload {
  productId: string;
  variantId: string;
  error: string;
}

export interface Response<T> {
  data: T;
  status: string;
}

export interface GetCartResponse extends Response<Commerce.Cart> {}

export interface CartItemDto {
  productId: string;
  variantId: string;
  quantity: number;
}
