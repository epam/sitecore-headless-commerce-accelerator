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

import { SagaIterator } from 'redux-saga';
import { call, put, race, select, take, takeEvery, takeLatest } from 'redux-saga/effects';
import * as Commerce from 'Foundation/Commerce';
import { eventHub, events } from 'Foundation/EventHub';
import { Action, LoadingStatus, Result } from 'Foundation/Integration';
import * as DataModels from 'services/checkout/models/generated';
import * as actions from './actions';
import { actionTypes } from './actionTypes';
import * as ShoppingCart from './api';
import {
  RemoveCartLinePayload,
  ShoppingCartData,
  ShoppingCartState,
  UpdateCartItemRequestPayload,
} from './models';

import { shoppingCart, shoppingCartData } from './selectors';

import * as Order from 'services/order';

export function* loadCart(): SagaIterator {
  const { status: cartLoadingStatus }: ShoppingCartState = yield select(shoppingCart);

  if (cartLoadingStatus !== LoadingStatus.Loading) {
    try {
      yield put(actions.GetCartRequest());

      const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.getShoppingCart);

      if (error) {
        return yield put(actions.GetCartFailure(error.message || 'Error Occured'));
      }

      yield put(actions.GetCartSuccess(data));
    } catch (e) {
      yield put(actions.GetCartFailure(e.message));
    }
  }
}

export function* addToCart(requestData: Action<DataModels.AddCartLineRequest>): SagaIterator {
  const addToCartModel = requestData.payload;
  yield put(actions.AddToCartRequest());

  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.addCartItemAsync, addToCartModel);

  if (error) {
    yield put(actions.AddToCartFailure(error.message || 'can not add to cart'));
  } else {
    const cartLine = data.cartLines.find((line) => line.product.productId === addToCartModel.productId);
    eventHub.publish(events.CART.CARTLINE_ADDED, {
      brand: cartLine.product.brand,
      id: cartLine.product.productId,
      name: cartLine.product.displayName,
      price: cartLine.variant.adjustedPrice,
      quantity: addToCartModel.quantity,
      variant: cartLine.variant.displayName,
    });
    yield put(actions.AddToCartSuccess(data));
  }
}

export function* updateCartLine(requestData: Action<DataModels.UpdateCartLineRequest>): SagaIterator {
  const updateCartLineModel = requestData.payload;
  yield put(actions.UpdateCartLineRequest());

  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.updateCartItemAsync, updateCartLineModel);

  if (error) {
    yield put(actions.UpdateCartLineFailure(error.message || 'can not update cart line'));
  } else {
    const cartLine = data.cartLines.find((line) => line.product.productId === updateCartLineModel.productId);
    eventHub.publish(events.CART.CARTLINE_UPDATED, {
      brand: cartLine.product.brand,
      id: cartLine.product.productId,
      name: cartLine.product.displayName,
      price: cartLine.variant.adjustedPrice,
      quantity: updateCartLineModel.quantity,
      variant: cartLine.variant.displayName,
    });
    yield put(actions.UpdateCartLineSuccess(data));
  }
}

export function* removeCartLine(action: Action<RemoveCartLinePayload>) {
  const { name, productId, quantity, variantId, variantName } = action.payload;
  yield put(actions.RemoveCartLineRequest());

  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.removeCartItem, { productId, variantId });

  if (error) {
    yield put(actions.RemoveCartLineFailure(error.message || 'can not remove cart line'));
  } else {
    eventHub.publish(events.CART.CARTLINE_REMOVED, {
      id: productId,
      name,
      quantity,
      variant: variantName,
    });
    yield put(actions.RemoveCartLineSuccess(data));
  }
}

export function* updateCartItem(requestData: Action<UpdateCartItemRequestPayload>) {
  const { productId, variantId, quantity } = requestData.payload;
  const cartData: ShoppingCartData = yield select(shoppingCartData);
  const cartLines = (cartData && cartData.cartLines) || [];
  const cartItem = cartLines.find(
    ({ product, variant }) => product.productId === productId && variant.variantId === variantId,
  );

  let updateError;

  if (cartItem) {
    if (quantity > 0) {
      yield put(actions.UpdateCartLine({ productId, variantId, quantity }));

      const [, error] = yield race([
        take(actionTypes.UPDATE_CART_LINE_SUCCESS),
        take(actionTypes.UPDATE_CART_LINE_FAILURE),
      ]);
      updateError = error;
    } else {
      yield put(
        actions.RemoveCartLine({
          id: cartData.id,
          price: cartData.price,
          product: cartItem.product,
          quantity,
          variant: cartItem.variant,
        }),
      );
      const [, error] = yield race([
        take(actionTypes.REMOVE_CART_LINE_SUCCESS),
        take(actionTypes.REMOVE_CART_LINE_FAILURE),
      ]);
      updateError = error;
    }
  } else {
    yield put(actions.AddToCart({ productId, variantId, quantity }));
    const [, error] = yield race([take(actionTypes.ADD_TO_CART_SUCCESS), take(actionTypes.ADD_TO_CART_FAILURE)]);
    updateError = error;
  }

  if (updateError) {
    yield put(actions.updateCartItemFailure({ productId, variantId, error: updateError }));
  } else {
    yield put(actions.updateCartItemSuccess({ productId, variantId }));
  }
}

function* watch(): SagaIterator {
  yield takeEvery(actionTypes.LOAD_CART, loadCart);
  yield takeLatest(Order.actionTypes.GET_ORDER_SUCCESS, loadCart);
  yield takeEvery(actionTypes.ADD_TO_CART, addToCart);
  yield takeEvery(actionTypes.UPDATE_CART_LINE, updateCartLine);
  yield takeEvery(actionTypes.REMOVE_CART_LINE, removeCartLine);
  yield takeEvery(actionTypes.UPDATE_CART_ITEM_REQUEST, updateCartItem);
}

export default [watch()];
