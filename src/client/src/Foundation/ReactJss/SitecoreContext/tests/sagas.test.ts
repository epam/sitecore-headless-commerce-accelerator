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

import { push } from 'connected-react-router';
import { apply, put } from 'redux-saga/effects';

import { LoadingStatus } from 'models';

import dataProvider from '../../dataProvider';

import * as actions from '../actions';
import * as constants from '../constants';
import * as sagas from '../sagas';

describe('SitecoreContext sagas', () => {
  describe('getRoute', () => {
    const fakeNewRoute = 'fakeNewRoute';
    const fakeSitecore = {
      sitecore: {
        context: {},
        route: {},
        status: LoadingStatus.Loading
      },
    };
    const gen = sagas.getRoute({ newRoute: fakeNewRoute, shouldPushNewRoute: true });

    test('should put GetSitecoreContextRequest', () => {
      const expected = put(actions.GetSitecoreContextRequest());
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should apply call to api', () => {
      const expected = apply(dataProvider, dataProvider.getRouteData, [
        fakeNewRoute,
        constants.DEFAULT_LANGUAGE,
        { querystringParams: {} },
      ]);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should put new route', () => {
      const expected = put(push(fakeNewRoute));
      const actual = gen.next(fakeSitecore);
      expect(actual.value).toEqual(expected);
    });

    test('should put GetSitecoreContextSuccess', () => {
      const expected = put(actions.GetSitecoreContextSuccess(fakeSitecore.sitecore as any));
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });
  });
});
