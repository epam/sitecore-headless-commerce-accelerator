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

import { sitecoreContext } from 'Foundation/ReactJss';

import { AppState, GlobalAccountState } from './models';

export const commerceUser = (state: AppState) => sitecoreContext(state).commerceUser;

export const account = (state: GlobalAccountState) => state.account;
export const signUp = (state: GlobalAccountState) => account(state).signUp;
export const accountValidation = (state: GlobalAccountState) => signUp(state).accountValidation;
export const createAccount = (state: GlobalAccountState) => signUp(state).create;
export const deleteAccount = (state: GlobalAccountState) => signUp(state).delete;
export const changePassword = (state: GlobalAccountState) => account(state).changePassword;
export const savedAddressList = (state: GlobalAccountState) => account(state).savedAddressList;
export const updateStatus = (state: GlobalAccountState) => account(state).update.status;
export const requestPasswordReset = (state: GlobalAccountState) => account(state).requestPasswordReset;
export const resetPassword = (state: GlobalAccountState) => account(state).resetPassword;
