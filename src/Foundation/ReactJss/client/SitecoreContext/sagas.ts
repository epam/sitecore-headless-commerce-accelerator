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

import { isExperienceEditorActive } from '@sitecore-jss/sitecore-jss-react';
import { push } from 'connected-react-router';
import queryString from 'query-string';
import { apply, fork, put, takeEvery } from 'redux-saga/effects';

import * as Extensions from 'Foundation/Extensions/client';
import { Action } from 'Foundation/Integration/client';

import dataProvider from '../dataProvider';
import { ChangeRoutePayload, SitecoreState } from '../models';

import * as actions from './actions';
import * as constants from './constants';

const tryParseUrl = (urlString: string): { pathname: string; params: { [key: string]: string } } => {
  try {
    const [pathname, search] = urlString.split('?');
    const queryParams = Extensions.tryParseUrlSearch(search);

    return {
      params: queryParams,
      pathname,
    };
  } catch (error) {
    console.error(`Unable to parse '${urlString}' url. Error: ${error}`, error);
  }

  return {
    params: {},
    pathname: urlString,
  };
};

export function* getRoute(payload: ChangeRoutePayload) {
  try {
    // sometimes newUrl can be undefined, due to incorrect configuration of NavigationLink in runtime,
    // in this case we want to prevent it by redirecting to home page
    let newUrl = payload.newRoute;
    newUrl = newUrl || '/';

    yield put(actions.GetSitecoreContextRequest());

    // TODO: cancel all request, running on current page
    if (window) {
      window.stop();
    }

    const search = queryString.parse(location.search) as { sc_itemid: string };
    const parsedUrlData = tryParseUrl(newUrl);

    // TODO: implement functionality language selection
    const data: SitecoreState = yield apply(dataProvider, dataProvider.getRouteData, [
      // sc_itemid means we're in Experience Editor
      search.sc_itemid || parsedUrlData.pathname,
      constants.DEFAULT_LANGUAGE,
      {
        querystringParams: parsedUrlData.params,
      },
    ]);

    yield put(actions.SetLoadedUrl(newUrl));

    if (payload.shouldPushNewRoute) {
      yield put(push(newUrl));
    }
    yield put(actions.GetSitecoreContextSuccess(data.sitecore));

    // reset scroll to the begin
    if (window) {
      window.scrollTo(0, 0);
    }
  } catch (err) {
    yield put(actions.GetSitecoreContextFailure(err.message));
    if (err.response) {
      if (err.response.status === 404) {
        yield put(push(constants.NOT_FOUND_ROUTE));
      } else if (err.response.status === 500) {
        yield put(push(constants.SERVER_ERROR_ROUTE));
      }
    }
  }
}

export function* changeRoute(action: Action<ChangeRoutePayload>) {
  const { newRoute } = action.payload;

  if (isExperienceEditorActive()) {
    window.location.assign(newRoute);
    return;
  }

  if (newRoute === constants.NOT_FOUND_ROUTE || newRoute === constants.SERVER_ERROR_ROUTE) {
    return;
  }

  yield fork(getRoute, action.payload);
}

function* watch() {
  yield takeEvery(constants.sagaActionTypes.CHANGE_ROUTE, changeRoute);
}

export default [watch()];
