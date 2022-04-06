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
import { get } from 'lodash';

import * as Commerce from 'services/commerce';
import { Result, VoidResult } from 'models';

import {
  AddressResponse,
  CardResponse,
  ChangePasswordResponse,
  ConfirmPasswordRecoveryResponse,
  CreateAccountResponse,
  EmailValidationResponse,
  RecoverPasswordResponse,
  UpdateAccountResponse,
  DeleteAccountResponse,
  AddImageResponse,
  RemoveImageResponse,
} from './models';
import * as DataModel from './models/generated';

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
        return { data: { errorMessage: get(error, ['response', 'data', 'error']), invalid: true, inUse: false } };
      }

      return { error };
    });
};

export const changePassword = async (changePasswordRequest: DataModel.ChangePasswordRequest): Promise<Result<any>> => {
  return axios
    .put<ChangePasswordResponse>(`${routeBase}/password`, changePasswordRequest)
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      if (error.response.status === 400) {
        return { data: { errorMessage: get(error, ['response', 'data', 'error']) } };
      }
      return { data: { errorMessage: 'Change password failed' } };
    });
};

export const getCardList = async (): Promise<Result<Commerce.Card[]>> => {
  return axios
    .get<CardResponse>(`${routeBase}/paymentcards`)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const updateCard = async (cardRequest: DataModel.CardRequest): Promise<Result<Commerce.Card[]>> => {
  return null;
};
export const removeCard = async (externalId: string): Promise<Result<Commerce.Card[]>> => {
  return null;
};

export const addCard = async (cardRequest: DataModel.CardRequest): Promise<Result<Commerce.Card[]>> => {
  return null;
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
): Promise<{ data: boolean } | { data: { errorMessage: string } } | { error: AxiosError }> => {
  return axios
    .put<UpdateAccountResponse>(`${routeBase}/account`, updateAccountRequest)
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      if (error.response.status === 400) {
        return { data: { errorMessage: get(error, ['response', 'data', 'error']) } };
      }
      return { error };
    });
};

export const deleteAccountInfo = async (
  deleteAccountRequest: DataModel.DeleteAccountRequest,
): Promise<Result<VoidResult>> => {
  return axios
    .delete<DeleteAccountResponse>(`${routeBase}/account`, { data: deleteAccountRequest })
    .then((response: AxiosResponse<DeleteAccountResponse>) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const confirmPasswordRecovery = async (email: string): Promise<Result<boolean>> => {
  return axios
    .post<ConfirmPasswordRecoveryResponse>(`${routeBase}/Password`, { email })
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const recoverPassword = async (
  recoverPasswordRequest: DataModel.RecoverPasswordRequest,
): Promise<Result<boolean>> => {
  return axios
    .put<RecoverPasswordResponse>(`${routeBase}/RecoverPassword`, recoverPasswordRequest)
    .then((response) => {
      return { data: response.data.status === 'ok' };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const addImage = async (addImageRequest: DataModel.AddImageRequest): Promise<Result<any>> => {
  const newImage = addImageRequest.image;
  const formData = new FormData();
  formData.append('newImage', newImage, newImage.name);

  return axios
    .post<AddImageResponse>(`${routeBase}/userimage`, formData)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};

export const removeImage = async (removeImageRequest: DataModel.RemoveImageRequest): Promise<Result<VoidResult>> => {
  return axios
    .delete<RemoveImageResponse>(`${routeBase}/userimage`)
    .then((response) => {
      return { data: response.data.data };
    })
    .catch((error: AxiosError) => {
      return { error };
    });
};
