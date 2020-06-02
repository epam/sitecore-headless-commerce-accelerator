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
import { LoadingStatus } from 'Foundation/Integration/client';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';

export const CreateAccount: actionCreators.CreateAccount = (createAccountRequest: Commerce.CreateAccountRequest) => ({
  payload: createAccountRequest,
  type: sagaActionTypes.CREATE,
});

export const AccountValidation: actionCreators.EmailValidation = (email: string) => ({
  payload: {
    email,
  },
  type: sagaActionTypes.ACCOUNT_VALIDATION,
});

export const ResetValidation: actionCreators.ResetValidation = () => ({
  type: sagaActionTypes.RESET_VALIDATION,
});

export const ChangePassword: actionCreators.ChangePassword = (oldPassword: string, newPassword: string) => ({
  payload: {
    newPassword,
    oldPassword,
  },
  type: sagaActionTypes.CHANGE_PASSWORD,
});

export const GetAddressList: actionCreators.GetAddressList = () => ({
  type: sagaActionTypes.ADDRESS_GET_LIST,
});

export const UpdateAddress: actionCreators.UpdateAddress = (address: Commerce.Address) => ({
  payload: address,
  type: sagaActionTypes.ADDRESS_UPDATE,
});

export const AddAddress: actionCreators.AddAddress = (address: Commerce.Address) => ({
  payload: address,
  type: sagaActionTypes.ADDRESS_ADD,
});

export const RemoveAddress: actionCreators.RemoveAddress = (externalId: string) => ({
  payload: externalId,
  type: sagaActionTypes.ADDRESS_REMOVE,
});

export const VerifyCommerceUser: actionCreators.VerifyCommerceUser = () => ({
  type: sagaActionTypes.VERIFY_COMMERCE_USER,
});

export const UpdateAccount: actionCreators.UpdateAccount = (firstName: string, lastName: string) => ({
  payload: {
    firstName,
    lastName,
  },
  type: sagaActionTypes.UPDATE,
});

export const CreateAccountRequest: actionCreators.CreateAccountRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.CREATE_REQUEST,
});

export const CreateAccountFailure: actionCreators.CreateAccountFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CREATE_FAILURE,
});

export const CreateAccountSuccess: actionCreators.CreateAccountSuccess = (user: Commerce.User) => ({
  payload: {
    status: LoadingStatus.Loaded,
    user,
  },
  type: reducerActionTypes.CREATE_SUCCESS,
});

export const AccountValidationRequest: actionCreators.EmailValidationRequest = (email: string) => ({
  payload: {
    email,
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ACCOUNT_VALIDATION_REQUEST,
});

export const AccountValidationFailure: actionCreators.EmailValidationFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.ACCOUNT_VALIDATION_FAILURE,
});

export const AccountValidationSuccess: actionCreators.EmailValidationSuccess = (invalid: boolean, inUse: boolean) => ({
  payload: {
    inUse,
    invalid,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.ACCOUNT_VALIDATION_SUCCESS,
});

export const ResetEmailValidation: actionCreators.ResetEmailValidation = () => ({
  type: reducerActionTypes.RESET_EMAIL_VALIDATION,
});

export const ChangePasswordRequest: actionCreators.ChangePasswordRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.CHANGE_PASSWORD_REQUEST,
});

export const ChangePasswordFailure: actionCreators.ChangePasswordFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CHANGE_PASSWORD_FAILURE,
});

export const ChangePasswordSuccess: actionCreators.ChangePasswordSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.CHANGE_PASSWORD_SUCCESS,
});

export const GetAddressListRequest: actionCreators.GetAddressListRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ADDRESS_GET_LIST_REQUEST,
});

export const GetAddressListFailure: actionCreators.GetAddressListFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.ADDRESS_GET_LIST_FAILURE,
});

export const GetAddressListSuccess: actionCreators.GetAddressListSuccess = (addressList: Commerce.Address[]) => ({
  payload: {
    items: addressList.reduce((acc, currentItem) => {
      acc[currentItem.externalId] = currentItem;
      return acc;
      // tslint:disable-next-line:align
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.ADDRESS_GET_LIST_SUCCESS,
});

export const UpdateAddressRequest: actionCreators.UpdateAddressRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ADDRESS_UPDATE_REQUEST,
});

export const UpdateAddressFailure: actionCreators.UpdateAddressFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.ADDRESS_UPDATE_FAILURE,
});

export const UpdateAddressSuccess: actionCreators.UpdateAddressSuccess = (addressList: Commerce.AddressModel[]) => ({
  payload: {
    items: addressList.reduce((acc, currentItem) => {
      acc[currentItem.externalId] = currentItem;
      return acc;
      // tslint:disable-next-line:align
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.ADDRESS_UPDATE_SUCCESS,
});

export const AddAddressRequest: actionCreators.AddAddressRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ADDRESS_ADD_REQUEST,
});

export const AddAddressFailure: actionCreators.AddAddressFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.ADDRESS_ADD_FAILURE,
});

export const AddAddressSuccess: actionCreators.AddAddressSuccess = (addressList: Commerce.AddressModel[]) => ({
  payload: {
    items: addressList.reduce((acc, currentItem) => {
      acc[currentItem.externalId] = currentItem;
      return acc;
      // tslint:disable-next-line:align
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.ADDRESS_ADD_SUCCESS,
});

export const RemoveAddressRequest: actionCreators.RemoveAddressRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ADDRESS_ADD_REQUEST,
});

export const RemoveAddressFailure: actionCreators.RemoveAddressFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.ADDRESS_ADD_FAILURE,
});

export const RemoveAddressSuccess: actionCreators.RemoveAddressSuccess = (addressList: Commerce.AddressModel[]) => ({
  payload: {
    items: addressList.reduce((acc, currentItem) => {
      acc[currentItem.externalId] = currentItem;
      return acc;
      // tslint:disable-next-line:align
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.ADDRESS_ADD_SUCCESS,
});

export const UpdateAccountRequest: actionCreators.UpdateAccountRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.UPDATE_REQUEST,
});

export const UpdateAccountFailure: actionCreators.UpdateAccountFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.UPDATE_FAILURE,
});

export const UpdateAccountSuccess: actionCreators.UpdateAccountSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.UPDATE_SUCCESS,
});
