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
import { call, fork, put, select, takeEvery, takeLatest } from 'redux-saga/effects';

import { tryParseUrlSearch } from 'utils';
import { Product, ProductSearchResults } from 'services/commerce';
import { Action, LoadingStatus, Result } from 'models';
import { ChangeRoute } from 'Foundation/ReactJss/SitecoreContext';
import { notify } from 'services/notifications';

import * as actions from './actions';
import { requestSuggestions, searchProducts } from './api';
import {
  DEFAULT_ITEMS_PER_PAGE,
  DEFAULT_START_PAGE_NUMBER,
  FACET_PARAMETER_NAME,
  KEYWORD_PARAMETER_NAME,
  productsSearchSagaActionTypes,
  productsSearchSuggestionsSagaActionTypes,
} from './constants';
import * as Models from './models';
import * as selectors from './selectors';
import * as utils from './utils';

export function* fetchProductsSearchSuggestions(params: Action<Models.SuggestionsRequestPayload>) {
  const {
    payload: { search },
  } = params;
  try {
    yield put(actions.ProductsSearchSuggestionsRequest(search));
    const { data, error }: Result<Models.ProductSearchSuggestionResponse> = yield call(requestSuggestions, search);

    if (error) {
      notify('error', error.message);
      return;
    }

    yield put(actions.ProductsSearchSuggestionsSuccess(data.products));
  } catch (e) {
    yield put(actions.ProductsSearchSuggestionsFailure(e.message || 'Products search failure'));
  }
}

export function* fetchProductsSearch(params: Models.Params) {
  try {
    yield put(actions.ProductsSearchRequest(params));
    const { data, error }: Result<ProductSearchResults> = yield call(searchProducts, params);
    if (error) {
      notify('error', error.message);
      return;
    }

    const { facets, products, totalPageCount, totalItemCount } = data;
    const items: Product[] = yield select(selectors.productSearchItems);
    const newItems = [...items, ...products];

    yield put(actions.ProductsSearchSuccess(facets, newItems, params.pg, totalPageCount, totalItemCount));
  } catch (e) {
    yield put(actions.ProductsSearchFailure(e.message || 'Products search failure'));
  }
}

export function* initialSearch(action: Action<Models.InitSearchPayload>): SagaIterator {
  const { payload } = action;

  const status: LoadingStatus = yield select(selectors.productsSearchStatus);

  if (status === LoadingStatus.Loaded) {
    yield put(actions.ResetState());
  }
  // stop current request
  if (status === LoadingStatus.Loading) {
    window.stop();
    yield put(actions.ResetState());
  }

  const newParams: Models.Params = {
    pg: DEFAULT_START_PAGE_NUMBER,
    ps: DEFAULT_ITEMS_PER_PAGE,
  };

  const parsedSearch = tryParseUrlSearch(payload.search || '');

  if (parsedSearch[KEYWORD_PARAMETER_NAME]) {
    newParams.q = parsedSearch[KEYWORD_PARAMETER_NAME];
  }

  if (parsedSearch[FACET_PARAMETER_NAME]) {
    newParams.f = parsedSearch[FACET_PARAMETER_NAME];
  }

  if (payload.categoryId) {
    newParams.cci = payload.categoryId;
  }

  if (payload.sortingField) {
    newParams.s = payload.sortingField;
  }

  if (payload.sortingDirection) {
    newParams.sd = payload.sortingDirection;
  }

  yield fork(fetchProductsSearch, newParams);
}

export function* clearSearch() {
  yield put(actions.ResetState());
}

export function* loadMoreProducts(): SagaIterator {
  const params: Models.Params = yield select(selectors.productSearchParams);
  const { pg } = params;

  const newParams = { ...params };
  newParams.pg = typeof pg === 'number' ? pg + 1 : 0;

  yield fork(fetchProductsSearch, newParams);
}

export function* changeSortingType(action: Action<Models.ChangeSortingTypePayload>): SagaIterator {
  const params: Models.Params = yield select(selectors.productSearchParams);
  const status: LoadingStatus = yield select(selectors.productsSearchStatus);

  if (status === LoadingStatus.Loaded) {
    yield put(actions.ClearItems());
  }

  // @ts-ignore
  // const { payload } = action;
  // const newParams = { ...params };
  // newParams.s = payload.sortingField;
  // newParams.sd = payload.sortingDirection;

  // This is temporary mock sorting params, because backend doesn't support any other perams, except brand
  const newParams = { ...params };

  newParams.s = 'brand';
  newParams.sd = params.sd === '0' ? '1' : '0';

  utils.saveSortingParametersToLS(newParams.sd, newParams.s);

  yield fork(fetchProductsSearch, newParams);
}

export function* handleFacetChange(facetQueryString: string) {
  const params: Models.Params = yield select(selectors.productSearchParams);

  const newSearchQuery = [];

  if (params.q) {
    newSearchQuery.push(`${KEYWORD_PARAMETER_NAME}=${params.q}`);
  }
  if (facetQueryString) {
    newSearchQuery.push(`${FACET_PARAMETER_NAME}=${facetQueryString}`);
  }

  const newSearchQueryString = newSearchQuery.length !== 0 ? `?${newSearchQuery.join('&')}` : '';
  const { location } = window;

  yield put(ChangeRoute(`${location.pathname}${newSearchQueryString}`));
}
export function* applyFacet(action: Action<Models.FacetPayload>) {
  const { name, value, search } = action.payload;

  const parsedSearch = tryParseUrlSearch(search);

  const newFacetQuery = utils
    .facetsManager(parsedSearch[FACET_PARAMETER_NAME])
    .addFacet(name, value)
    .getFacetsQueryString();

  yield fork(handleFacetChange, newFacetQuery);
}

export function* discardFacet(action: Action<Models.FacetPayload>) {
  const { name, value, search } = action.payload;

  const parsedSearch = tryParseUrlSearch(search);

  const newFacetQuery = utils
    .facetsManager(parsedSearch[FACET_PARAMETER_NAME])
    .removeFacet(name, value)
    .getFacetsQueryString();

  yield fork(handleFacetChange, newFacetQuery);
}

export function* watch(): SagaIterator {
  yield takeEvery(productsSearchSagaActionTypes.APPLY_FACET, applyFacet);
  yield takeEvery(productsSearchSagaActionTypes.DISCARD_FACET, discardFacet);
  yield takeEvery(productsSearchSagaActionTypes.LOAD_MORE, loadMoreProducts);
  yield takeLatest(productsSearchSagaActionTypes.INIT_SEARCH, initialSearch);
  yield takeEvery(productsSearchSagaActionTypes.CLEAR_SEARCH, clearSearch);
  yield takeEvery(productsSearchSagaActionTypes.CHANGE_SORTING, changeSortingType);

  yield takeLatest(productsSearchSuggestionsSagaActionTypes.REQUEST_SUGGESTIONS, fetchProductsSearchSuggestions);
}

export default [watch()];
