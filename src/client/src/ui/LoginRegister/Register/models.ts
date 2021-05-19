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

import * as Commerce from 'Foundation/Commerce';
import * as JSS from 'Foundation/ReactJss';

import * as Account from 'services/account';

import { CreateAccountRequest } from 'Feature/Account/dataModel.Generated';

export interface SignUpOwnProps extends JSS.Rendering<JSS.BaseDataSourceItem> {}

export interface SignUpStateProps {
  accountValidation: Account.AccountValidationState;
  createAccount: Account.CreateState;
  commerceUser: Commerce.User;
  loading: boolean;
  returnUrl: string;
}

export interface SignUpDispatchProps {
  AccountValidation: (email: string) => void;
  CreateAccount: (createAccountDto: CreateAccountRequest, returnUrl?: string) => void;
  ResetValidation: () => void;
}

export interface SignUpProps extends SignUpOwnProps, SignUpDispatchProps, SignUpStateProps {}

export interface AppState extends Account.GlobalAccountState, Account.AppState, JSS.RoutingState {}
