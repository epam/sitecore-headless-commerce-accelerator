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

export interface StartAuthenticationPayload {
  email: string;
  password: string;
  returnUrl?: string;
}

export interface AuthProcessState {
  hasValidCredentials: boolean;
  status: LoadingStatus;
}

export interface AuthenticationState {
  authenticated: boolean;
  authProcess: AuthProcessState;
}

export interface GlobalAuthenticationState {
  authentication: AuthenticationState;
}

export interface AuthProcessSuccessPayload {
  hasValidCredentials: boolean;
}

export interface SetAuthenticatedPayload {
  authenticated: boolean;
}
