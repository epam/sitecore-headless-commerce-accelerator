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

import { call, put, select, takeEvery } from 'redux-saga/effects';

import { Action, Result } from 'Foundation/Integration';

import { Variant } from 'Foundation/Commerce';
import * as api from '../api/Wishlist';

import { action } from './actions';
import { sagaTypes } from './constants';
import * as selector from './selectors';

import { Wishlist } from './mock';

export function* getWishlist() {
  yield put(action.GetWishlist.Request());

  const { data, error }: Result<Wishlist> = yield call(api.getWishlist);

  if (error) {
    return;
    // return yield put(action.GetWishlist.Failure(error.message, error?.stack));
  }

  yield put(action.Wishlist.Success(data.lines.map((line) => line.product.variants[0])));
}

export function* addWishlistItem(request: Action<Variant>) {
  yield put(action.AddWishlist.Request());

  const { data, error }: Result<Wishlist> = yield call(api.addWishlistItem, request.payload);

  if (error) {
    // Mock back-end logic
    const wishlist: Variant[] = yield select(selector.wishlist);
    const newWishlist: Variant[] = [...wishlist];

    const sameProduct = (item: Variant) =>
      item.productId === request.payload.productId && item.variantId === request.payload.variantId;

    if (!wishlist.find(sameProduct)) {
      newWishlist.push(request.payload);
    }

    return yield put(action.Wishlist.Success(newWishlist));

    // return yield put(action.AddWishlist.Failure(error.message, error?.stack));
  }

  yield put(action.Wishlist.Success(data.lines.map((line) => line.product.variants[0])));
}

export function* removeWishlistItem(request: Action<string>) {
  yield put(action.RemoveWishlist.Request());

  const { data, error }: Result<Wishlist> = yield call(api.removeWishlistItem, request.payload);

  if (error) {
    // Mock back-end logic
    const wishlist: Variant[] = yield select(selector.wishlist);

    return yield put(action.Wishlist.Success(wishlist.filter((product) => product.variantId !== request.payload)));

    // return yield put(action.RemoveWishlist.Failure(error.message, error?.stack));
  }

  yield put(action.Wishlist.Success(data.lines.map((line) => line.product.variants[0])));
}

function* watch() {
  yield takeEvery(sagaTypes.GET_WISHLIST, getWishlist);
  yield takeEvery(sagaTypes.ADD_WISHLIST_ITEM, addWishlistItem);
  yield takeEvery(sagaTypes.REMOVE_WISHLIST_ITEM, removeWishlistItem);
}

export default [watch()];
