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
import { Status } from 'Foundation/Integration';
import { SitecoreState } from 'Foundation/ReactJss';

import * as DataModel from '../../dataModel.Generated';

export interface AccountValidationState extends Status {
  email: string;
  invalid: boolean;
  inUse: boolean;
}

export interface CreateState extends Status {
  accountInfo?: Commerce.User;
}

export interface ChangePasswordState extends Status {}

export interface SignUpState {
  accountValidation: AccountValidationState;
  create: CreateState;
}

export interface SavedAddressListState extends Status {
  items: {
    [key: string]: Commerce.Address;
  };
}

export interface UpdateAccountState extends Status {}

export interface RequestPasswordResetState extends Status {
  email: string;
}

export interface ResetPasswordState extends Status {
  userName: string;
  newPassword: string;
  token: string;
}

export interface AccountState {
  signUp: SignUpState;
  changePassword: ChangePasswordState;
  savedAddressList: SavedAddressListState;
  update: UpdateAccountState;
  requestPasswordReset: RequestPasswordResetState;
  resetPassword: ResetPasswordState;
}

export interface GlobalAccountState {
  account: AccountState;
}

export interface AppState extends SitecoreState<Commerce.UserContext> {}

export interface CreateAccountPayload {
  returnUrl?: string;
  request: DataModel.CreateAccountRequest;
}

export interface ValidateEmailPayload extends DataModel.ValidateEmailRequest {}
export interface ValidateAccountResultPayload extends Status, Partial<DataModel.ValidateEmailRequest> {}
export interface ChangePasswordPayload {
  oldPassword: string;
  newPassword: string;
}

export interface AddressPayload extends Status {
  items: {
    [key: string]: Commerce.Address;
  };
}

export interface UpdateAccountPayload {
  firstName: string;
  lastName: string;
}

export interface RequestPasswordResetPayload {
  email: string;
}

export interface ResetPasswordPayload {
  userName: string;
  newPassword: string;
  token: string;
}
