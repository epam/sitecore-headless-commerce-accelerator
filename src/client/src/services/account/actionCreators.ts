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
import { Action, FailureType, StatusType } from 'models';

import {
  AddImagePayload,
  AddressPayload,
  CardPayload,
  ChangePasswordPayload,
  CreateAccountPayload,
  RemoveImagePayload,
  RequestPasswordResetPayload,
  ResetPasswordPayload,
  UpdateAccountPayload,
  ValidateAccountResultPayload,
  ValidateEmailPayload,
} from './models';
import * as DataModel from './models/generated';

export type CreateAccount = (
  createAccountRequest: DataModel.CreateAccountRequest,
  returnUrl?: string,
) => Action<CreateAccountPayload>;
export type UpdateAccount = (
  firstName: string,
  lastName: string,
  dateOfBirth?: string,
  phoneNumber?: string,
) => Action<UpdateAccountPayload>;
export type DeleteAccount = () => Action;
export type EmailValidation = (email: string) => Action<ValidateEmailPayload>;
export type ResetValidation = () => Action;
export type ChangePassword = (oldPassword: string, newPassword: string) => Action<ChangePasswordPayload>;
export type VerifyCommerceUser = () => Action;

export type GetCardList = () => Action;

export type UpdateCard = (card: Commerce.Card) => Action<Commerce.Card>;
export type AddCard = (card: Commerce.Card) => Action<Commerce.Card>;
export type RemoveCard = (id: string) => Action<string>;
export type GetCardListRequest = StatusType;
export type GetCardListFailure = FailureType;
export type GetCardListSuccess = (cardList: Commerce.Card[]) => Action<CardPayload>;
export type UpdateCardRequest = StatusType;
export type UpdateCardFailure = FailureType;
export type UpdateCardSuccess = (cardList: Commerce.Card[]) => Action<CardPayload>;
export type AddCardRequest = StatusType;
export type AddCardFailure = FailureType;
export type AddCardSuccess = (cardList: Commerce.Card[]) => Action<CardPayload>;

export type RemoveCardRequest = StatusType;
export type RemoveCardFailure = FailureType;
export type RemoveCardSuccess = (cardList: Commerce.Card[]) => Action<CardPayload>;

export type GetAddressList = () => Action;
export type UpdateAddress = (address: Commerce.Address) => Action<Commerce.Address>;
export type AddAddress = (address: Commerce.Address) => Action<Commerce.Address>;
export type RemoveAddress = (externalId: string) => Action<string>;
export type RequestPasswordReset = (email: string) => Action<RequestPasswordResetPayload>;
export type ResetPassword = (userName: string, newPassword: string, token: string) => Action<ResetPasswordPayload>;
export type AddImage = (image: File) => Action<AddImagePayload>;
export type RemoveImage = () => Action<RemoveImagePayload>;

export type AddImageRequest = StatusType;
export type AddImageFailure = FailureType;
export type AddImageSuccess = (imageUrl: string) => Action;

export type RemoveImageRequest = StatusType;
export type RemoveImageFailure = FailureType;
export type RemoveImageSuccess = () => Action;

export type CreateAccountRequest = StatusType;
export type CreateAccountFailure = FailureType;
export type CreateAccountSuccess = (user: Commerce.User) => Action;

export type UpdateAccountRequest = StatusType;
export type UpdateAccountFailure = FailureType;
export type UpdateAccountSuccess = StatusType;

export type DeleteAccountRequest = StatusType;
export type DeleteAccountFailure = FailureType;
export type DeleteAccountSuccess = StatusType;

export type EmailValidationRequest = (email: string) => Action<ValidateAccountResultPayload>;
export type EmailValidationFailure = FailureType;
export type EmailValidationSuccess = (
  invalid: boolean,
  inUse: boolean,
  errorMessage?: string,
) => Action<ValidateAccountResultPayload>;

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

export type RequestPasswordResetRequest = StatusType;
export type RequestPasswordResetFailure = FailureType;
export type RequestPasswordResetSuccess = StatusType;

export type ResetPasswordRequest = StatusType;
export type ResetPasswordFailure = FailureType;
export type ResetPasswordSuccess = StatusType;
