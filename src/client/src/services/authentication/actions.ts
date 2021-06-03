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

import { LoadingStatus } from 'Foundation/Integration';
import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';
import * as DataModel from 'services/account/models/generated';

export const Authentication: actionCreators.Authentication = (
  loginRequest: DataModel.LoginRequest,
  returnUrl?: string,
) => ({
  payload: {
    request: loginRequest,
    returnUrl,
  },
  type: sagaActionTypes.AUTHENTICATION,
});

export const InitAuthentication: actionCreators.InitAuthentication = () => ({
  type: sagaActionTypes.INIT_AUTHENTICATION,
});

export const Logout: actionCreators.Logout = (returnUrl?: string) => ({
  payload: {
    returnUrl,
  },
  type: sagaActionTypes.LOGOUT,
});

export const AuthenticationRequest: actionCreators.AuthenticationRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.AUTHENTICATION_REQUEST,
});

export const AuthenticationFailure: actionCreators.AuthenticationFailure = () => ({
  payload: {
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.AUTHENTICATION_FAILURE,
});

export const AuthenticationSuccess: actionCreators.AuthenticationSuccess = () => ({
  payload: {
    hasValidCredentials: true,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.AUTHENTICATION_SUCCESS,
});

export const LogoutRequest: actionCreators.LogoutRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.LOGOUT_REQUEST,
});

export const LogoutFailure: actionCreators.LogoutFailure = () => ({
  payload: {
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.LOGOUT_FAILURE,
});

export const LogoutSuccess: actionCreators.LogoutSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.LOGOUT_SUCCESS,
});

export const ResetAuthenticationProcessState: actionCreators.ResetAuthenticationProcessState = () => ({
  type: reducerActionTypes.RESET_AUTHENTICATION_PROCESS_STATE,
});

export const SetAuthenticated: actionCreators.SetAuthenticated = (authenticated) => ({
  payload: {
    authenticated,
  },
  type: reducerActionTypes.SET_AUTHENTICATED,
});
