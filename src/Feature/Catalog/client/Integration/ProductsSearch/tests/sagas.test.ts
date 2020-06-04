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

import { call, fork, put, select } from 'redux-saga/effects';

import { LoadingStatus } from 'Foundation/Integration/client';

import { ProductSearch } from 'Feature/Catalog/client/Integration/api';

import * as actions from '../actions';
import * as sagas from '../sagas';
import * as selectors from '../selectors';

describe('ProductsSearch sagas', () => {
  describe('fetchProductsSearch', () => {
    const fakeParams = {
      pg: 0,
    };

    const gen = sagas.fetchProductsSearch(fakeParams);

    test('should put productsSearchRequest', () => {
      const expected = put(actions.ProductsSearchRequest(fakeParams));
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should call api', () => {
      const expected = call(ProductSearch.searchProducts, fakeParams);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should select stored items', () => {
      const expected = select(selectors.productSearchItems);
      const actual = gen.next({
        data: {
          facets: [],
          products: [],
          totalItemCount: 0,
          totalPageCount: 0,
        },
      });

      expect(actual.value).toEqual(expected);
    });

    test('should put productsSearchSuccess', () => {
      const expected = put(actions.ProductsSearchSuccess([], [], fakeParams.pg, 0, 0));

      const actual = gen.next([]);

      expect(actual.value).toEqual(expected);
    });

    test('should put productsSearchFailure, due to error from api', () => {
      const errorGen = sagas.fetchProductsSearch(fakeParams);

      errorGen.next();
      errorGen.next();

      const fakeErrorMessage = 'fakeErrorMessage';

      const expected = put(actions.ProductsSearchFailure(fakeErrorMessage));
      const actual = errorGen.next({
        error: {
          message: fakeErrorMessage,
        },
      });

      expect(actual.value).toEqual(expected);
    });

    test('should handle unexpected error and put productsSearchFailure', () => {
      const errorGen = sagas.fetchProductsSearch(fakeParams);

      errorGen.next();
      errorGen.next();

      // tslint:disable-next-line:quotemark
      const expected = put(actions.ProductsSearchFailure("Cannot read property 'data' of undefined"));
      const actual = errorGen.next();

      expect(actual.value).toEqual(expected);
    });
  });

  describe('initialSearch', () => {
    const fakeCategoryId = 'fakeCategoryId';
    const fakeSearch = '';
    const gen = sagas.initialSearch({ payload: { categoryId: fakeCategoryId, search: fakeSearch }, type: 'faketype' });

    test('should select status', () => {
      const expected = select(selectors.productsSearchStatus);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should fork fetchProductsSearch', () => {
      const expected = fork(sagas.fetchProductsSearch, { cci: fakeCategoryId, pg: 0, ps: '12' });
      const actual = gen.next(LoadingStatus.NotLoaded);
      expect(actual.value).toEqual(expected);
    });
  });

  describe('loadMoreProducts', () => {
    const gen = sagas.loadMoreProducts();

    // tslint:disable-next-line:no-identical-functions
    test('should select productSearchParams', () => {
      const expected = select(selectors.productSearchParams);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should fork fetchProductsSearch', () => {
      const expected = fork(sagas.fetchProductsSearch, { pg: 0 });
      const actual = gen.next({});
      expect(actual.value).toEqual(expected);
    });
  });

  describe('applyFacet', () => {
    test('should fork handleFacetChange with fn', () => {
      const fakeName = 'fakeName';
      const fakeValue = 'fakeValue';
      const fakeSearch = 'fakeSearch';

      const gen = sagas.applyFacet({
        payload: {
          name: fakeName,
          search: fakeSearch,
          value: fakeValue,
        },
        type: 'type',
      });

      const expected = fork(sagas.handleFacetChange, 'fakeName%3DfakeValue');

      const actual = gen.next();

      expect(actual.value).toEqual(expected);
    });
  });

  describe('discardFacet', () => {
    test('should fork handleFacetChange with fn', () => {
      const fakeName = 'fakeName';
      const fakeValue = 'fakeValue';
      const fakeSearch = 'fakeSearch';

      const gen = sagas.discardFacet({
        payload: {
          name: fakeName,
          search: fakeSearch,
          value: fakeValue,
        },
        type: 'type',
      });

      const expected = fork(sagas.handleFacetChange, '');

      const actual = gen.next();

      expect(actual.value).toEqual(expected);
    });
  });
});
