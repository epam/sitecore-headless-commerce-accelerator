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
import { Action, FailureType, StatusType } from 'Foundation/Integration/client';

import * as DataModel from '../../dataModel.Generated';

import {
  AddressPayload,
  ChangePasswordPayload,
  CreateAccountPayload,
  UpdateAccountPayload,
  ValidateAccountResultPayload,
  ValidateEmailPayload,
} from './models';

export type CreateAccount = (createAccountRequest: DataModel.CreateAccountRequest) => Action<CreateAccountPayload>;
export type UpdateAccount = (firstName: string, lastName: string) => Action<UpdateAccountPayload>;
export type EmailValidation = (email: string) => Action<ValidateEmailPayload>;
export type ResetValidation = () => Action;
export type ChangePassword = (oldPassword: string, newPassword: string) => Action<ChangePasswordPayload>;
export type VerifyCommerceUser = () => Action;
export type GetAddressList = () => Action;
export type UpdateAddress = (address: Commerce.Address) => Action<Commerce.Address>;
export type AddAddress = (address: Commerce.Address) => Action<Commerce.Address>;
export type RemoveAddress = (externalId: string) => Action<string>;

export type CreateAccountRequest = StatusType;
export type CreateAccountFailure = FailureType;
export type CreateAccountSuccess = (user: Commerce.User) => Action;

export type UpdateAccountRequest = StatusType;
export type UpdateAccountFailure = FailureType;
export type UpdateAccountSuccess = StatusType;

export type EmailValidationRequest = (email: string) => Action<ValidateAccountResultPayload>;
export type EmailValidationFailure = FailureType;
export type EmailValidationSuccess = (invalid: boolean, inUse: boolean) => Action<ValidateAccountResultPayload>;

export type ResetEmailValidation = () => Action;

export type GetAddressListRequest = StatusType;
export type GetAddressListFailure = FailureType;
export type GetAddressListSuccess = (addressList: Commerce.Address[]) => Action<AddressPayload>;
export type UpdateAddressRequest = StatusType;
export type UpdateAddressFailure = FailureType;
export type UpdateAddressSuccess = (addressList: Commerce.Address[]) => Action<AddressPayload>;
export type AddAddressRequest = StatusType;
export type AddAddressFailure = FailureType;
export type AddAddressSuccess = (addressList: Commerce.Address[]) => Action<AddressPayload>;

export type RemoveAddressRequest = StatusType;
export type RemoveAddressFailure = FailureType;
export type RemoveAddressSuccess = (addressList: Commerce.Address[]) => Action<AddressPayload>;

export type ChangePasswordRequest = StatusType;
export type ChangePasswordFailure = FailureType;
export type ChangePasswordSuccess = StatusType;
