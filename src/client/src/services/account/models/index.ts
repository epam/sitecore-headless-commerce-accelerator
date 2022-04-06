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

import * as Commerce from 'services/commerce';
import { Status, VoidResult, OkJsonResultModel } from 'models';
import { SitecoreState } from 'Foundation/ReactJss';

import * as DataModel from './generated/dataModel.Generated';

export interface AccountValidationState extends Status {
  email: string;
  invalid: boolean;
  inUse: boolean;
  errorMessage: string;
}

export interface CreateState extends Status {
  accountInfo?: Commerce.User;
}

export interface DeleteState extends Status {
  accountInfo?: Commerce.User;
}

export interface ChangePasswordState extends Status {}

export interface SignUpState {
  accountValidation: AccountValidationState;
  create: CreateState;
  delete: DeleteState;
}

export interface SavedAddressListState extends Status {
  items: {
    [key: string]: Commerce.Address;
  };
}

export interface SavedCardListState extends Status {
  items: {
    [key: string]: Commerce.Card;
  };
}

export interface UpdateAccountState extends Status {}

export interface DeleteAccountState extends Status {}

export interface RequestPasswordResetState extends Status {
  email: string;
}

export interface ResetPasswordState extends Status {
  userName: string;
  newPassword: string;
  token: string;
}

export interface ImageState {
  addAccountImage: AddImageState;
  removeAccountImage: RemoveImageState;
}

export interface AddImageState extends Status {
  imageUrl: string;
}

export interface RemoveImageState extends Status {}

export interface AccountState {
  signUp: SignUpState;
  changePassword: ChangePasswordState;
  savedAddressList: SavedAddressListState;
  savedCardList: SavedCardListState;
  update: UpdateAccountState;
  delete: DeleteAccountState;
  requestPasswordReset: RequestPasswordResetState;
  resetPassword: ResetPasswordState;
  accountImage: ImageState;
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

export interface CardPayload extends Status {
  items: {
    [key: string]: Commerce.Card;
  };
}

export interface UpdateAccountPayload {
  firstName: string;
  lastName: string;
  dateOfBirth?: string;
  phoneNumber?: string;
}

export interface AddImagePayload {
  image: File;
}

export interface RemoveImagePayload {}

export interface RequestPasswordResetPayload {
  email: string;
}

export interface ResetPasswordPayload {
  userName: string;
  newPassword: string;
  token: string;
}

export interface CreateAccountResponse extends OkJsonResultModel<Commerce.User> {}

export interface AddImageResponse {
  data: {
    imageUrl: string;
  };
}
export interface RemoveImageResponse extends OkJsonResultModel<VoidResult> {}

export interface EmailValidationResponse extends OkJsonResultModel<Commerce.ValidateEmail> {}

export interface ChangePasswordResponse extends OkJsonResultModel<VoidResult> {}

export interface UpdateAccountResponse extends OkJsonResultModel<VoidResult> {}

export interface DeleteAccountResponse extends OkJsonResultModel<VoidResult> {}

export interface AddressResponse extends OkJsonResultModel<Commerce.Address[]> {}

export interface CardResponse extends OkJsonResultModel<Commerce.Card[]> {}

export interface ConfirmPasswordRecoveryResponse extends OkJsonResultModel<{ IsEmailValid: boolean }> {}

export interface RecoverPasswordResponse extends OkJsonResultModel<VoidResult> {}
