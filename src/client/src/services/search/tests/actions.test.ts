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

import { LoadingStatus } from 'models';
import * as actions from '../actions';
import { productsSearchReducerActionTypes, productsSearchSagaActionTypes } from '../constants';

describe('Products Search actions', () => {
  // preparation
  const fakeFacetName = 'fakeFacetName';
  const fakeFacetValue = 'fakeFacetValue';
  const fakeCategoryId = 'fakeCategoryId';
  const fakeSearch = 'fakeSearch';

  test('should return action for INIT_CATEGORY_PRODUCTS', () => {
    const actual = actions.InitialSearch({ categoryId: fakeCategoryId });

    expect(actual).toEqual({
      payload: { categoryId: fakeCategoryId },
      type: productsSearchSagaActionTypes.INIT_SEARCH,
    });
  });

  test('should return action for LOAD_MORE', () => {
    const actual = actions.LoadMore();

    expect(actual).toEqual({
      type: productsSearchSagaActionTypes.LOAD_MORE,
    });
  });

  test('should return action for APPLY_FACET', () => {
    const actual = actions.ApplyFacet(fakeFacetName, fakeFacetValue, fakeSearch);

    expect(actual).toEqual({
      payload: {
        name: fakeFacetName,
        search: fakeSearch,
        value: fakeFacetValue,
      },
      type: productsSearchSagaActionTypes.APPLY_FACET,
    });
  });

  test('should return action for DISCARD_FACET', () => {
    const actual = actions.DiscardFacet(fakeFacetName, fakeFacetValue, fakeSearch);

    expect(actual).toEqual({
      payload: {
        name: fakeFacetName,
        search: fakeSearch,
        value: fakeFacetValue,
      },
      type: productsSearchSagaActionTypes.DISCARD_FACET,
    });
  });

  test('should return action for CLEAR_ITEMS', () => {
    const actual = actions.ClearItems();

    expect(actual).toEqual({
      type: productsSearchReducerActionTypes.CLEAR_ITEMS,
    });
  });

  test('should return action fro PRODUCTS_SEARCH_REQUEST', () => {
    const fakeParams = {};

    const actual = actions.ProductsSearchRequest(fakeParams);

    expect(actual).toEqual({
      payload: {
        params: fakeParams,
        status: LoadingStatus.Loading,
      },
      type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_REQUEST,
    });
  });

  test('should return action for PRODUCTS_SEARCH_FAILURE', () => {
    const fakeError = 'fakeError';

    const actual = actions.ProductsSearchFailure(fakeError);

    expect(actual).toEqual({
      payload: {
        error: fakeError,
        status: LoadingStatus.Failure,
      },
      type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_FAILURE,
    });
  });

  test('should return action for PRODUCTS_SEARCH_SUCCESS', () => {
    const fakeCurrentPageNumber = 0;
    const fakeTotalPageCount = 0;
    const fakeTotalItemCount = 0;

    const actual = actions.ProductsSearchSuccess([], [], fakeCurrentPageNumber, fakeTotalPageCount, fakeTotalItemCount);

    expect(actual).toEqual({
      payload: {
        currentPageNumber: fakeCurrentPageNumber,
        facets: [],
        items: [],
        status: LoadingStatus.Loaded,
        totalItemCount: fakeTotalItemCount,
        totalPageCount: fakeTotalPageCount,
      },
      type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_SUCCESS,
    });
  });
});
