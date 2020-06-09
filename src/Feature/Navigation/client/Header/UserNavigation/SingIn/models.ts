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

import * as JSS from 'Foundation/ReactJss/client';

import {
  AuthenticationProcessState,
  GlobalAuthenticationState,
} from 'Feature/Account/client/Integration/Authentication';
import { FormValues } from 'Foundation/ReactJss/client/Form';

export interface SingInDispatchProps {
  Authentication: (email: string, password: string, returnUrl: string) => void;
  ResetState: () => void;
}

export interface SingInStateProps {
  authenticationProcess: AuthenticationProcessState;
  returnUrl: string;
}

export interface SingInProps extends SingInDispatchProps, SingInStateProps {}

export interface SingInValues extends FormValues {
  email: string;
  password: string;
}

export interface AppState extends GlobalAuthenticationState, JSS.RoutingState {}
