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

import axios from 'axios';

import * as Commerce from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import {
  AccountValidationResponse,
  AddressResponse,
  ChangePasswordResponse,
  CreateAccountResponse,
  UpdateAccountResponse,
} from './models';

const routeBase = '/apix/client/commerce/account';

export const createAccount = async (
  createAccountModel: Commerce.CreateAccountModel
): Promise<Result<Commerce.CreateAccountResultModel>> => {
  try {
    const response = await axios.post<CreateAccountResponse>(`${routeBase}/create`, createAccountModel);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const accountValidation = async (
  validateAccountModel: Commerce.ValidateAccountModel
): Promise<Result<Commerce.ValidateAccountResultModel>> => {
  try {
    const response = await axios.post<AccountValidationResponse>(`${routeBase}/validate`, validateAccountModel);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const changePassword = async (
  changePasswordModel: Commerce.ChangePasswordModel
): Promise<Result<Commerce.ChangePasswordResultModel>> => {
  try {
    const response = await axios.post<ChangePasswordResponse>(`${routeBase}/change-password`, changePasswordModel);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const addAddress = async (addressModel: Commerce.AddressModel): Promise<Result<Commerce.AddressModel[]>> => {
  try {
    const response = await axios.post<AddressResponse>(`${routeBase}/address-add`, addressModel);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const getAddressList = async (): Promise<Result<Commerce.AddressModel[]>> => {
  try {
    const response = await axios.get<AddressResponse>(`${routeBase}/address-list`);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const updateAddress = async (address: Commerce.AddressModel): Promise<Result<Commerce.AddressModel[]>> => {
  try {
    const response = await axios.post<AddressResponse>(`${routeBase}/address-update`, address);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const removeAddress = async (address: Commerce.AddressModel): Promise<Result<Commerce.AddressModel[]>> => {
  try {
    const response = await axios.post<AddressResponse>(`${routeBase}/address-remove`, address);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};

export const updateAccountInfo = async (
  commerceUser: Commerce.CommerceUserModel
): Promise<Result<Commerce.CommerceUserModel>> => {
  try {
    const response = await axios.post<UpdateAccountResponse>(`${routeBase}/update`, commerceUser);

    const { data: responseData } = response;

    return { data: responseData.data };
  } catch (e) {
    return { error: e };
  }
};
