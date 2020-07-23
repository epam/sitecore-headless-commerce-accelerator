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
import { call, put, takeEvery, takeLatest } from 'redux-saga/effects';

import * as Commerce from 'Foundation/Commerce';

import * as DataModels from 'Feature/Checkout/dataModel.Generated';
import { Action, Result } from 'Foundation/Integration';

import { Promotions, ShoppingCart } from 'Feature/Checkout/Integration/api';

import * as actions from './actions';
import { actionTypes } from './actionTypes';
import { GerPromotionPayload, RemoveCartLinePayload } from './models';

import * as Order from '../Order/actionTypes';

export function* loadCart() {
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

export function* addToCart(requestData: Action<DataModels.AddCartLineRequest>): SagaIterator {
  const addToCartModel = requestData.payload;
  yield put(actions.AddToCartRequest());

  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.addCartItemAsync, addToCartModel);

  if (error) {
    yield put(actions.AddToCartFailure(error.message || 'can not add to cart'));
  } else {
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
    yield put(actions.UpdateCartLineSuccess(data));
  }
}

export function* removeCartLine(action: Action<RemoveCartLinePayload>) {
  const { productId, variantId } = action.payload;
  yield put(actions.RemoveCartLineRequest());

  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.removeCartItem, productId, variantId);

  if (error) {
    yield put(actions.RemoveCartLineFailure(error.message || 'can not remove cart line'));
  } else {
    yield put(actions.RemoveCartLineSuccess(data));
  }
}

export function* addPromoCode(requestData: Action<DataModels.PromoCodeRequest>): SagaIterator {
  const addPromoCodeModel = requestData.payload;
  yield put(actions.AddPromoCodeRequest());
  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.addPromoCode, addPromoCodeModel);

  if (error) {
    yield put(actions.AddPromoCodeFailure(error.message || 'can not add promo code'));
  } else {
    yield put(actions.AddPromoCodeSuccess(data));
  }
}

export function* removePromoCode(requestData: Action<DataModels.PromoCodeRequest>): SagaIterator {
  const { payload } = requestData;

  yield put(actions.RemovePromoCodeRequest());
  const { data, error }: Result<Commerce.Cart> = yield call(ShoppingCart.removePromoCode, payload);

  if (error) {
    yield put(actions.RemovePromoCodeFailure(error.message || 'can not remove promo code'));
  } else {
    yield put(actions.RemovePromoCodeSuccess(data));
  }
}

export function* getFreeShippingSubtotal(requestData: Action<GerPromotionPayload>): SagaIterator {
  const { callback } = requestData.payload;
  const { data, error }: Result<Commerce.FreeShippingResult> = yield call(Promotions.getFreeShippingSubtotal);

  if (!error) {
    callback(data);
  }
}

function* watch(): SagaIterator {
  yield takeLatest(actionTypes.LOAD_CART, loadCart);
  yield takeLatest(Order.actionTypes.GET_ORDER_SUCCESS, loadCart);
  yield takeEvery(actionTypes.ADD_TO_CART, addToCart);
  yield takeEvery(actionTypes.UPDATE_CART_LINE, updateCartLine);
  yield takeEvery(actionTypes.REMOVE_CART_LINE, removeCartLine);
  yield takeEvery(actionTypes.ADD_PROMO_CODE, addPromoCode);
  yield takeEvery(actionTypes.REMOVE_PROMO_CODE, removePromoCode);
  yield takeEvery(actionTypes.GET_FREE_SHIPPING_SUBTOTAL, getFreeShippingSubtotal);
}

export default [watch()];
