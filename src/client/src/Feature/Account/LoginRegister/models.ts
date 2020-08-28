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

import * as Commerce from 'Foundation/Commerce';
import * as JSS from 'Foundation/ReactJss';

import * as Account from 'Feature/Account/Integration/Account';

export interface LoginRegisterOwnProps extends JSS.Rendering<JSS.BaseDataSourceItem> {}

export interface LoginRegisterStateProps {
  commerceUser: Commerce.User;
}

export interface LoginRegisterProps extends LoginRegisterOwnProps, LoginRegisterStateProps {}

export interface LoginRegisterOwnState extends JSS.SafePureComponentState {
  isSignUp: boolean;
}

export interface AppState extends Account.GlobalAccountState, Account.AppState, JSS.RoutingState {}
