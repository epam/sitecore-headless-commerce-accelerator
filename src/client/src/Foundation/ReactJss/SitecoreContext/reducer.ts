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

import { Action, LoadingStatus } from 'models';
import { Sitecore } from '../headlessDefinitions';

import { reducerActionTypes } from './constants';

export const initialState: Sitecore<{}, {}> = {
  context: {},
  route: {},
  status: LoadingStatus.NotLoaded,
};

export default (state: Sitecore<{}, {}> = initialState, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.GET_SITECORE_CONTEXT_FAILURE:
    case reducerActionTypes.GET_SITECORE_CONTEXT_REQUEST:
    case reducerActionTypes.GET_SITECORE_CONTEXT_SUCCESS: {
      return { ...state, ...action.payload };
    }
    default: {
      return state;
    }
  }
};
