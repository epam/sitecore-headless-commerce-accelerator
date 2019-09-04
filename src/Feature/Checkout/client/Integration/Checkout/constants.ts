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

import { keyMirrorReducer, keyMirrorSaga } from 'Foundation/ReactJss/client';

export const CHECKOUT_NAMESPACE = 'CHECKOUT';

export const sagaActionTypes = keyMirrorSaga(
  {
    GET_CHECKOUT_DATA: null,
    INIT_STEP: null,
    SUBMIT_STEP: null,
  },
  CHECKOUT_NAMESPACE
);

export const reducerActionTypes = keyMirrorReducer(
  {
    GET_CHECKOUT_DATA_FAILURE: null,
    GET_CHECKOUT_DATA_REQUEST: null,
    GET_CHECKOUT_DATA_SUCCESS: null,

    SET_CURRENT_STEP: null,
    SUBMIT_STEP_FAILURE: null,
    SUBMIT_STEP_REQUEST: null,
    SUBMIT_STEP_SUCCESS: null,

    SET_STEP_VALUES: null,
  },
  CHECKOUT_NAMESPACE
);
