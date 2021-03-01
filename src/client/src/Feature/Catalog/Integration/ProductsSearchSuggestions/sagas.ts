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
import { call, put, takeLatest } from 'redux-saga/effects';

import { Action, Result } from 'Foundation/Integration';

import { ProductSearch } from 'Feature/Catalog/Integration/api';
import { ProductSearchSuggestionResponse } from 'Feature/Catalog/Integration/api/ProductSearch';
import { addError } from 'Foundation/UI/common/components/Errors/Integration/actions';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import * as Models from './models';

export function* fetchProductsSearchSuggestions(params: Action<Models.SuggestionsRequestPayload>) {
  const {
    payload: { search },
  } = params;
  try {
    yield put(actions.ProductsSearchSuggestionsRequest(search));
    const { data, error }: Result<ProductSearchSuggestionResponse> = yield call(
      ProductSearch.requestSuggestions,
      search,
    );

    if (error) {
      return yield put(addError(error.message));
    }

    yield put(actions.ProductsSearchSuggestionsSuccess(data.products));
  } catch (e) {
    yield put(actions.ProductsSearchSuggestionsFailure(e.message || 'Products search failure'));
  }
}

export function* watch(): SagaIterator {
  yield takeLatest(sagaActionTypes.REQUEST_SUGGESTIONS, fetchProductsSearchSuggestions);
}

export default [watch()];
