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
import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';

export const StartAuthentication: actionCreators.StartAuthentication = (email: string, password: string) => ({
  payload: {
    email,
    password,
  },
  type: sagaActionTypes.START_AUTHENTICATION,
});

export const InitAuthentication: actionCreators.InitAuthentication = () => ({
  type: sagaActionTypes.INIT_AUTHENTICATION,
});

export const SetAuthenticated: actionCreators.SetAuthenticated = (authenticated) => ({
  payload: {
    authenticated,
  },
  type: reducerActionTypes.SET_AUTHENTICATED,
});

export const StartAuthenticationRequest: actionCreators.StartAuthenticationRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.START_AUTHENTICATION_REQUEST,
});

export const StartAuthenticationFailure: actionCreators.StartAuthenticationFailure = () => ({
  payload: {
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.START_AUTHENTICATION_FAILURE,
});

export const StartAuthenticationSuccess: actionCreators.StartAuthenticationSuccess = () => ({
  payload: {
    hasValidCredentials: true,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.START_AUTHENTICATION_SUCCESS,
});
