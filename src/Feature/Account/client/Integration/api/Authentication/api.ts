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

import { ValidateCredentialsResultModel } from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import { ValidateCredentialsResponse } from './models';

export const startAuthentication = async (
  email: string,
  password: string
): Promise<Result<ValidateCredentialsResultModel>> => {
  try {
    const response = await axios.post<ValidateCredentialsResponse>('/apix/client/commerce/auth/start', {
      email,
      password,
    });

    const { data: responseData } = response;

    const { data, status } = responseData;
    if (status !== 'ok') {
      return { error: new Error(status) };
    }

    return { data };
  } catch (e) {
    return { error: e };
  }
};
