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

import * as JSS from 'Foundation/ReactJss';
import { FormValues } from 'Foundation/ReactJss/Form';

import { AuthenticationProcessState, GlobalAuthenticationState } from 'services/authentication';
import * as DataModel from 'services/account/models/generated';

export interface LogInDispatchProps {
  Authentication: (loginRequestDto: DataModel.LoginRequest, returnUrl: string) => void;
  ResetState: () => void;
  onLoaded?: () => void;
}

export interface LogInStateProps {
  authenticationProcess: AuthenticationProcessState;
}

export interface LogInStates extends JSS.SafePureComponentState {
  isPasswordEmpty: boolean;
  isUsernameValid: boolean;
  showPassword: boolean;
}

export interface LogInProps extends LogInDispatchProps, LogInStateProps {}

export interface LogInValues extends FormValues {
  email: string;
  password: string;
}

export interface AppState extends GlobalAuthenticationState, JSS.RoutingState {}
