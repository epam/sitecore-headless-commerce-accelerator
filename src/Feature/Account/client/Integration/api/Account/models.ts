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

import * as Extensions from 'Foundation/Base/client';
import * as Commerce from 'Foundation/Commerce/client';

export interface CreateAccountResponse extends Extensions.OkJsonResultModel<Commerce.CreateAccountResultModel> {}

export interface AccountValidationResponse extends Extensions.OkJsonResultModel<Commerce.ValidateAccountResultModel> {}

export interface ChangePasswordResponse extends Extensions.OkJsonResultModel<Commerce.ChangePasswordResultModel> {}

export interface UpdateAccountResponse extends Extensions.OkJsonResultModel<Commerce.CommerceUserModel> {}
export interface AddressResponse extends Extensions.OkJsonResultModel<Commerce.AddressModel[]> {}
