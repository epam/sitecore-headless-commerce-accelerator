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

import { reducerActionTypes } from '../constants';
import reducer, { initialState } from '../reducer';

describe('ProductsSearch reducer', () => {
  test('should return initial state', () => {
    const actual = reducer(initialState, { type: 'fake' });

    expect(actual).toEqual(initialState);
  });

  test('should apply payload', () => {
    [
      reducerActionTypes.PRODUCTS_SEARCH_FAILURE,
      reducerActionTypes.PRODUCTS_SEARCH_REQUEST,
      reducerActionTypes.PRODUCTS_SEARCH_SUCCESS,
      reducerActionTypes.UPDATE_APPLIED_FACET,
    ].forEach((actionType) => {
      const fakeAction = {
        payload: {
          fakePayload: 'fakePayload',
        },
        type: actionType,
      };
      const actual = reducer(initialState, fakeAction);

      expect(actual).toEqual({
        ...initialState,
        ...fakeAction.payload,
      });
    });
  });

  test('should clear items', () => {
    // prepare state
    const fakeItems = [1, 2, 3, 4, 5];
    const stateWithItems = reducer(initialState, {
      payload: {
        items: fakeItems,
      },
      type: reducerActionTypes.PRODUCTS_SEARCH_SUCCESS,
    });

    const actual = reducer(stateWithItems, {
      type: reducerActionTypes.CLEAR_ITEMS,
    });

    expect(stateWithItems.items.length).toBe(fakeItems.length);
    expect(actual.items.length).toBe(0);
  });
});
