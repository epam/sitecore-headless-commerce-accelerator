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

import { Action, StatusType } from 'Foundation/Integration/client';
import { AuthenticationPayload, AuthenticationProcessSuccessPayload, SetAuthenticatedPayload } from './models';

export type InitAuthentication = () => Action;
export type Authentication = (email: string, passwords: string, returnUrl?: string) => Action<AuthenticationPayload>;

export type AuthenticationRequest = StatusType;
export type AuthenticationFailure = StatusType;
export type AuthenticationSuccess = () => Action<AuthenticationProcessSuccessPayload>;
export type ResetAuthenticationProcessState = () => Action;
export type SetAuthenticated = (authenticated: boolean) => Action<SetAuthenticatedPayload>;
