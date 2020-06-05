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

import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';
import { LoadingStatus } from 'Foundation/Integration/client';

import * as actions from '../actions';
import { actionTypes } from '../actionTypes';

describe('ShoppingCart actions', () => {
  const fakeCartData: ShoppingCart.ShoppingCartData = {
    addresses: [],
    adjustments: [],
    cartLines: [],
    email: null,
    id: 'id',
    payment: [],
    price: null,
    shipping: []
  };

  test('should return action for LOAD_CART', () => {
    const actual = actions.LoadCart();
    expect(actual).toEqual({
      type: actionTypes.LOAD_CART,
    });
  });

  test('should return action for CART_GET_REQUEST', () => {
    const actual = actions.GetCartRequest();
    expect(actual).toEqual({
      payload: {
        status: LoadingStatus.Loading,
      },
      type: actionTypes.CART_GET_REQUEST,
    });
  });

  test('should return action for CART_GET_FAILURE', () => {
    const fakeError = 'fakeError';
    const actual = actions.GetCartFailure(fakeError);
    expect(actual).toEqual({
      payload: {
        error: fakeError,
        status: LoadingStatus.Failure,
      },
      type: actionTypes.CART_GET_FAILURE,
    });
  });

  test('should return action for CART_GET_SUCCESS', () => {
    const actual = actions.GetCartSuccess(fakeCartData);
    expect(actual).toEqual({
      payload: {
        data: fakeCartData,
        status: LoadingStatus.Loaded,
      },
      type: actionTypes.CART_GET_SUCCESS,
    });
  });

  test('should return action for ADD_TO_CART', () => {
    const fakeModel = {
      productId: 'productId',
      quantity: 0,
      variantId: 'variantId',
    };

    const actual = actions.AddToCart(fakeModel);
    expect(actual).toEqual({
      payload: {
        ...fakeModel,
      },
      type: actionTypes.ADD_TO_CART,
    });
  });

  test('should return action for ADD_TO_CART_REQUEST', () => {
    const actual = actions.AddToCartRequest();
    expect(actual).toEqual({
      payload: {
        status: LoadingStatus.Loading,
      },
      type: actionTypes.ADD_TO_CART_REQUEST,
    });
  });
  test('should return action for ADD_TO_CART_FAILURE', () => {
    const fakeError = 'fakeError';
    const actual = actions.AddToCartFailure(fakeError);
    expect(actual).toEqual({
      payload: {
        error: fakeError,
        status: LoadingStatus.Failure,
      },
      type: actionTypes.ADD_TO_CART_FAILURE,
    });
  });

  test('should return action for ADD_TO_CART_SUCCESS', () => {
    const actual = actions.AddToCartSuccess(fakeCartData);
    expect(actual).toEqual({
      payload: {
        cartUpdated: true,
        data: fakeCartData,
        status: LoadingStatus.Loaded,
      },
      type: actionTypes.ADD_TO_CART_SUCCESS,
    });
  });

  test('should return action for UPDATE_CART_LINE', () => {
    const fakeModel = {
      productId: 'productId',
      quantity: 0,
      variantId: 'variantId',
    };

    const actual = actions.UpdateCartLine(fakeModel);
    expect(actual).toEqual({
      payload: {
        ...fakeModel,
      },
      type: actionTypes.UPDATE_CART_LINE,
    });
  });

  test('should return action for UPDATE_CART_LINE_REQUEST', () => {
    const actual = actions.UpdateCartLineRequest();
    expect(actual).toEqual({
      payload: {
        status: LoadingStatus.Loading,
      },
      type: actionTypes.UPDATE_CART_LINE_REQUEST,
    });
  });
  test('should return action for UPDATE_CART_LINE_FAILURE', () => {
    const fakeError = 'fakeError';
    const actual = actions.UpdateCartLineFailure(fakeError);
    expect(actual).toEqual({
      payload: {
        error: fakeError,
        status: LoadingStatus.Failure,
      },
      type: actionTypes.UPDATE_CART_LINE_FAILURE,
    });
  });

  test('should return action for UPDATE_CART_LINE_SUCCESS', () => {
    const actual = actions.UpdateCartLineSuccess(fakeCartData);
    expect(actual).toEqual({
      payload: {
        cartUpdated: true,
        data: fakeCartData,
        status: LoadingStatus.Loaded,
      },
      type: actionTypes.UPDATE_CART_LINE_SUCCESS,
    });
  });
});
