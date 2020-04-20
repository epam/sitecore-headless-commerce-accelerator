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

import { LoadingStatus } from 'Foundation/Integration/client';

import * as actions from '../actions';
import { reducerActionTypes, sagaActionTypes } from '../constants';

describe('SitecoreContext', () => {
  test('should return action for INITIALIZATION_COMPLETE', () => {
    const actual = actions.InitializationComplete();
    expect(actual).toEqual({
      type: sagaActionTypes.INITIALIZATION_COMPLETE,
    });
  });

  test('should return action for CHANGE_ROUTE', () => {
    const fakeRoute = 'fakeRoute';
    const actual = actions.ChangeRoute(fakeRoute);
    expect(actual).toEqual({
      payload: { newRoute: fakeRoute, shouldPushNewRoute: true },
      type: sagaActionTypes.CHANGE_ROUTE,
    });
  });

  test('should return action for GET_SITECORECONTEXT_REQUEST', () => {
    const actual = actions.GetSitecoreContextRequest();
    expect(actual).toEqual({
      payload: {
        status: LoadingStatus.Loading,
      },
      type: reducerActionTypes.GET_SITECORE_CONTEXT_REQUEST,
    });
  });

  test('should return action for GET_SITECORECONTEXT_FAILURE', () => {
    const fakeError = 'fakeError';
    const actual = actions.GetSitecoreContextFailure(fakeError);
    expect(actual).toEqual({
      payload: {
        error: fakeError,
        status: LoadingStatus.Failure,
      },
      type: reducerActionTypes.GET_SITECORE_CONTEXT_FAILURE,
    });
  });

  test('should return action for GET_SITECORECONTEXT_SUCCESS', () => {
    const fakeSitecore = {
      context: {},
      route: {},
      status: LoadingStatus.Loaded,
    };

    const actual = actions.GetSitecoreContextSuccess(fakeSitecore);
    expect(actual).toEqual({
      payload: {
        ...fakeSitecore,
      },
      type: reducerActionTypes.GET_SITECORE_CONTEXT_SUCCESS,
    });
  });
});
