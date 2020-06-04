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

import axios, { AxiosError, AxiosResponse } from 'axios';

import * as Commerce from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import * as DataModel from 'Feature/Account/client/dataModel.Generated';
import {
  AddressResponse,
  ChangePasswordResponse,
  CreateAccountResponse,
  EmailValidationResponse,
  UpdateAccountResponse,
} from './models';

const routeBase = '/apix/client/commerce/accounts';

export const createAccount = async (
  createAccountRequest: DataModel.CreateAccountRequest,
): Promise<Result<Commerce.User>> => {
  return axios
    .post<CreateAccountResponse>(`${routeBase}/account`, createAccountRequest)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const emailValidation = async (
  validateEmailRequest: DataModel.ValidateEmailRequest,
): Promise<Result<Commerce.ValidateEmail>> => {
  return axios
    .post<EmailValidationResponse>(`${routeBase}/validate`, validateEmailRequest)
    .then((response) => {
      return { data: { invalid: false, inUse: response.data.data.inUse } };
    })
    .catch((error: AxiosError) => {
      if (error.response.status === 400) {
        return { data: { invalid: true, inUse: false } };
      }

      return { error };
    });
};

export const changePassword = async (
  changePasswordRequest: DataModel.ChangePasswordRequest,
): Promise<Result<boolean>> => {
  return axios
    .put<ChangePasswordResponse>(`${routeBase}/password`, changePasswordRequest)
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const addAddress = async (addressRequest: DataModel.AddressRequest): Promise<Result<Commerce.Address[]>> => {
  return axios
    .post<AddressResponse>(`${routeBase}/address`, addressRequest)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const getAddressList = async (): Promise<Result<Commerce.Address[]>> => {
  return axios
    .get<AddressResponse>(`${routeBase}/address`)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const updateAddress = async (addressRequest: DataModel.AddressRequest): Promise<Result<Commerce.Address[]>> => {
  return axios
    .put<AddressResponse>(`${routeBase}/address`, addressRequest)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const removeAddress = async (externalId: string): Promise<Result<Commerce.Address[]>> => {
  return axios
    .delete(`${routeBase}/address?externalid=${externalId}`)
    .then((response: AxiosResponse<AddressResponse>) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const updateAccountInfo = async (
  updateAccountRequest: DataModel.UpdateAccountRequest,
): Promise<Result<boolean>> => {
  return axios
    .put<UpdateAccountResponse>(`${routeBase}/account`, updateAccountRequest)
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};
