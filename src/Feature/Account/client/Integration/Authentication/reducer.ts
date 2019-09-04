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

import { Action, LoadingStatus } from 'Foundation/Integration/client';

import { reducerActionTypes } from './constants';
import { AuthenticationState, AuthProcessState, SetAuthenticatedPayload } from './models';

export const initialAuthProcessState: AuthProcessState = {
  hasValidCredentials: false,
  status: LoadingStatus.NotLoaded,
};

export const initialState: AuthenticationState = {
  authProcess: initialAuthProcessState,
  authenticated: false,
};

export default (state: AuthenticationState = { ...initialState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.START_AUTHENTICATION_FAILURE:
    case reducerActionTypes.START_AUTHENTICATION_REQUEST:
    case reducerActionTypes.START_AUTHENTICATION_SUCCESS: {
      const { authProcess } = state;
      return {
        ...state,
        authProcess: {
          ...authProcess,
          ...action.payload,
        },
      };
    }

    case reducerActionTypes.SET_AUTHENTICATED: {
      const { authenticated } = (action as Action<SetAuthenticatedPayload>).payload;
      return {
        ...state,
        authenticated,
      };
    }
    default: {
      return state;
    }
  }
};
