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

import { Action, LoadingStatus } from 'Foundation/Integration/client';

import { reducerActionTypes } from './constants';
import { AuthenticationProcessState, AuthenticationState, SetAuthenticatedPayload } from './models';

export const initialAuthenticationProcessState: AuthenticationProcessState = {
  hasValidCredentials: false,
  status: LoadingStatus.NotLoaded,
};

export const initialState: AuthenticationState = {
  authenticated: false,
  authenticationProcess: initialAuthenticationProcessState,
};

export default (state: AuthenticationState = { ...initialState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.AUTHENTICATION_FAILURE:
    case reducerActionTypes.AUTHENTICATION_REQUEST:
    case reducerActionTypes.AUTHENTICATION_SUCCESS: {
      const { authenticationProcess } = state;
      return {
        ...state,
        authenticationProcess: {
          ...authenticationProcess,
          ...action.payload,
        },
      };
    }

    case reducerActionTypes.RESET_AUTHENTICATION_PROCESS_STATE: {
      return {
        ...state,
        authProcess: initialAuthenticationProcessState,
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
