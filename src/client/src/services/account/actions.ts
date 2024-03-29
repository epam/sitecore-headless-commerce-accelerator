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
import { LoadingStatus } from 'models';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';
import * as DataModel from './models/generated';

export const CreateAccount: actionCreators.CreateAccount = (
  createAccountRequest: DataModel.CreateAccountRequest,
  returnUrl?: string,
) => ({
  payload: {
    request: createAccountRequest,
    returnUrl,
  },
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

export const UpdateAccount: actionCreators.UpdateAccount = (
  firstName: string,
  lastName: string,
  dateOfBirth?: string,
  phoneNumber?: string,
) => ({
  payload: {
    firstName,
    lastName,
    dateOfBirth,
    phoneNumber,
  },
  type: sagaActionTypes.UPDATE,
});

export const DeleteAccount: actionCreators.DeleteAccount = () => ({
  type: sagaActionTypes.DELETE,
});

export const RequestPasswordReset: actionCreators.RequestPasswordReset = (email: string) => ({
  payload: {
    email,
  },
  type: sagaActionTypes.REQUEST_PASSWORD_RESET,
});

export const ResetPassword: actionCreators.ResetPassword = (userName: string, newPassword: string, token: string) => ({
  payload: {
    newPassword,
    token,
    userName,
  },
  type: sagaActionTypes.PASSWORD_RESET,
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

export const AccountValidationSuccess: actionCreators.EmailValidationSuccess = (
  invalid: boolean,
  inUse: boolean,
  errorMessage: string = '',
) => ({
  payload: {
    errorMessage,
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

export const UpdateAddressSuccess: actionCreators.UpdateAddressSuccess = (addressList: Commerce.Address[]) => ({
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

export const AddAddressSuccess: actionCreators.AddAddressSuccess = (addressList: Commerce.Address[]) => ({
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

export const RemoveAddressSuccess: actionCreators.RemoveAddressSuccess = (addressList: Commerce.Address[]) => ({
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

export const RequestPasswordResetRequest: actionCreators.RequestPasswordResetRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.REQUEST_PASSWORD_RESET_REQUEST,
});

export const RequestPasswordResetFailure: actionCreators.RequestPasswordResetFailure = (
  error: string,
  stack?: string,
) => ({
  payload: {
    error,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.REQUEST_PASSWORD_RESET_FAILURE,
});

export const RequestPasswordResetSuccess: actionCreators.RequestPasswordResetSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.REQUEST_PASSWORD_RESET_SUCCESS,
});

export const ResetPasswordRequest: actionCreators.ResetPasswordRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.PASSWORD_RESET_REQUEST,
});

export const ResetPasswordFailure: actionCreators.ResetPasswordFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.PASSWORD_RESET_FAILURE,
});

export const ResetPasswordSuccess: actionCreators.ResetPasswordSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.PASSWORD_RESET_SUCCESS,
});

export const DeleteAccountRequest: actionCreators.DeleteAccountRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.DELETE_REQUEST,
});

export const DeleteAccountFailure: actionCreators.DeleteAccountFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.DELETE_FAILURE,
});

export const DeleteAccountSuccess: actionCreators.DeleteAccountSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.DELETE_SUCCESS,
});

export const AddImage: actionCreators.AddImage = (image: File) => ({
  payload: {
    image,
  },
  type: sagaActionTypes.IMAGE_ADD,
});

export const AddImageRequest: actionCreators.AddImageRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.IMAGE_ADD_REQUEST,
});

export const AddImageFailure: actionCreators.AddImageFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.IMAGE_ADD_FAILURE,
});

export const AddImageSuccess: actionCreators.AddImageSuccess = (imageUrl: string) => ({
  payload: {
    imageUrl,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.IMAGE_ADD_SUCCESS,
});

export const RemoveImage: actionCreators.RemoveImage = () => ({
  payload: {},
  type: sagaActionTypes.IMAGE_REMOVE,
});

export const RemoveImageRequest: actionCreators.RemoveImageRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.IMAGE_REMOVE_REQUEST,
});

export const RemoveImageFailure: actionCreators.RemoveImageFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.IMAGE_REMOVE_FAILURE,
});

export const RemoveImageSuccess: actionCreators.RemoveImageSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.IMAGE_REMOVE_SUCCESS,
});

export const GetCardList: actionCreators.GetCardList = () => ({
  type: sagaActionTypes.CARD_GET_LIST,
});

export const GetCardListRequest: actionCreators.GetCardListRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.CARD_GET_LIST_REQUEST,
});

export const GetCardListFailure: actionCreators.GetCardListFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CARD_GET_LIST_FAILURE,
});

export const GetCardListSuccess: actionCreators.GetCardListSuccess = (cardList: Commerce.Card[]) => ({
  payload: {
    items: cardList.reduce((acc, currentItem) => {
      acc[currentItem.id] = currentItem;
      return acc;
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.CARD_GET_LIST_SUCCESS,
});

export const UpdateCardData: actionCreators.UpdateCard = (card: Commerce.Card) => ({
  payload: card,
  type: sagaActionTypes.CARD_UPDATE,
});

export const UpdateCardRequest: actionCreators.UpdateCardRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.CARD_UPDATE_REQUEST,
});

export const UpdateCardFailure: actionCreators.UpdateCardFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CARD_UPDATE_FAILURE,
});

export const UpdateCardSuccess: actionCreators.UpdateCardSuccess = (cardList: Commerce.Card[]) => ({
  payload: {
    items: cardList.reduce((acc, currentItem) => {
      acc[currentItem.id] = currentItem;
      return acc;
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.CARD_UPDATE_SUCCESS,
});
export const AddCard: actionCreators.AddCard = (card: Commerce.Card) => ({
  payload: card,
  type: sagaActionTypes.CARD_ADD,
});

export const AddCardRequest: actionCreators.AddCardRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.CARD_ADD_REQUEST,
});

export const AddCardFailure: actionCreators.AddAddressFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CARD_ADD_FAILURE,
});

export const AddCardSuccess: actionCreators.AddCardSuccess = (cardList: Commerce.Card[]) => ({
  payload: {
    items: cardList.reduce((acc, currentItem) => {
      acc[currentItem.id] = currentItem;
      return acc;
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.CARD_ADD_SUCCESS,
});

export const RemoveCard: actionCreators.RemoveCard = (id: string) => ({
  payload: id,
  type: sagaActionTypes.CARD_REMOVE,
});

export const RemoveCardRequest: actionCreators.RemoveAddressRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.ADDRESS_ADD_REQUEST,
});

export const RemoveCardFailure: actionCreators.RemoveCardFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.CARD_ADD_FAILURE,
});

export const RemoveCardSuccess: actionCreators.RemoveCardSuccess = (cardList: Commerce.Card[]) => ({
  payload: {
    items: cardList.reduce((acc, currentItem) => {
      acc[currentItem.id] = currentItem;
      return acc;
    }, {}),
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.CARD_ADD_SUCCESS,
});
