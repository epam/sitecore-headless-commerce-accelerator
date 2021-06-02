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

import { keyMirrorReducer, keyMirrorSaga } from './../utils';

export const SITECORE_CONTEXT_NAMESPACE = 'SITECORE';

export const sagaActionTypes = keyMirrorSaga(
  {
    CHANGE_ROUTE: null,
    INITIALIZATION_COMPLETE: null,
  },
  SITECORE_CONTEXT_NAMESPACE,
);

export const reducerActionTypes = keyMirrorReducer(
  {
    GET_SITECORE_CONTEXT_FAILURE: null,
    GET_SITECORE_CONTEXT_REQUEST: null,
    GET_SITECORE_CONTEXT_SUCCESS: null,
  },
  SITECORE_CONTEXT_NAMESPACE,
);

export const DEFAULT_LANGUAGE = 'en';
export const NOT_FOUND_ROUTE = '/notfound';
export const SERVER_ERROR_ROUTE = '/error';
export const SITECORE_ROUTES = ['/:sitecoreRoute*'];
