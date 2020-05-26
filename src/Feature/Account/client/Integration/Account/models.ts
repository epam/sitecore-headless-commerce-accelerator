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

import * as Commerce from 'Foundation/Commerce/client';
import { Status } from 'Foundation/Integration/client';
import { SitecoreState } from 'Foundation/ReactJss/client';

export interface AccountValidationState extends Status {
  email: string;
  invalid: boolean;
  inUse: boolean;
}

export interface CreateState extends Status {
  accountInfo?: Commerce.CommerceUserModel;
}

export interface ChangePasswordState extends Status {}

export interface SignUpState {
  accountValidation: AccountValidationState;
  create: CreateState;
}

export interface SavedAddressListState extends Status {
  items: {
    [key: string]: Commerce.AddressModel;
  };
}

export interface UpdateAccountState extends Status {}

export interface AccountState {
  signUp: SignUpState;
  changePassword: ChangePasswordState;
  savedAddressList: SavedAddressListState;
  update: UpdateAccountState;
}

export interface GlobalAccountState {
  account: AccountState;
}

export interface AppState extends SitecoreState<Commerce.CommerceUserContext> {}

export interface CreateAccountPayload extends Commerce.CreateAccountRequest {}
export interface ValidateEmailPayload extends Commerce.ValidateEmailRequest {}
export interface ValidateAccountResultPayload extends Status, Partial<Commerce.ValidateEmailRequest> {}
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
