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

import { Result, VoidResult } from 'models';

import * as DataModel from 'services/account/models/generated';

export const authentication = async (
  loginRequest: DataModel.LoginRequest,
  returnUrl?: string,
): Promise<Result<VoidResult>> => {
  returnUrl = returnUrl ? `?returnUrl=${returnUrl}` : '';
  return axios
    .post<VoidResult>(`/apix/client/commerce/auth/login${returnUrl}`, loginRequest)
    .then((response) => ({ data: response.data }))
    .catch((error) => ({ error }));
};

export const logout = async (): Promise<Result<VoidResult>> =>
  axios
    .post<VoidResult>('/apix/client/commerce/auth/logout')
    .then((response) => ({ data: response.data }))
    .catch((error) => ({ error }));
