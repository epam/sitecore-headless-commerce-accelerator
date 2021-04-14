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

import { RouterProps } from 'react-router';

import { GlobalAccountState, ResetPasswordState } from 'Feature/Account/Integration/Account';

export interface PasswordResetOwnProps extends RouterProps {}

export interface PasswordResetFormStateProps {
  resetPasswordState: ResetPasswordState;
}

export interface PasswordResetFormDispatchProps {
  recoverPassword: (userName: string, newPassword: string, token: string) => void;
}

export interface PasswordResetFormProps
  extends PasswordResetOwnProps,
    PasswordResetFormStateProps,
    PasswordResetFormDispatchProps {}

export interface AppState extends GlobalAccountState {}
