//    Copyright 2019 EPAM Systems, Inc.
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
      reducerActionTypes.GET_SITECORE_CONTEXT_FAILURE,
      reducerActionTypes.GET_SITECORE_CONTEXT_REQUEST,
      reducerActionTypes.GET_SITECORE_CONTEXT_SUCCESS,
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
});
