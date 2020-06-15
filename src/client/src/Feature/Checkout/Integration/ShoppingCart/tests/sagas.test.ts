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

import { call, put } from 'redux-saga/effects';

import { ShoppingCart } from 'Feature/Checkout/Integration/api';

import * as actions from '../actions';
import * as sagas from '../sagas';

describe('ShoppingCart sagas', () => {
  describe('loadCart', () => {
    const gen = sagas.loadCart();

    test('should put GetCartRequest', () => {
      const expected = put(actions.GetCartRequest());
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should call api', () => {
      const expected = call(ShoppingCart.getShoppingCart);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should put GetCartSuccess', () => {
      // don't need to mock a data object, a trick with any helps with this
      const expected = put(actions.GetCartSuccess({} as any));
      const actual = gen.next({ data: {} });
      expect(actual.value).toEqual(expected);
    });

    test('should put GetCartFailure', () => {
      const fakeMessage = 'fakeMessage';
      const errorGen = sagas.loadCart();

      errorGen.next();
      errorGen.next();

      const expected = put(actions.GetCartFailure(fakeMessage));

      const actual = errorGen.next({ error: { message: fakeMessage } });

      expect(actual.value).toEqual(expected);
    });

    test('should put GetCartFailure, when unexpected error is occured', () => {
      const errorGen = sagas.loadCart();

      errorGen.next();
      errorGen.next();

      // tslint:disable-next-line:quotemark
      const expected = put(actions.GetCartFailure("Cannot read property 'data' of undefined"));

      const actual = errorGen.next();

      expect(actual.value).toEqual(expected);
    });
  });
});
