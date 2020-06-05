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
import * as Jss from 'Foundation/ReactJss/client';

import * as Account from 'Feature/Account/client/Integration/Account';

export interface ChangeUserInformationFormOwnProps extends Jss.Rendering<Jss.BaseDataSourceItem> {}

export interface ChangeUserInformationFormStateProps {
  commerceUser: Commerce.User;
  updateStatus: LoadingStatus;
}

export interface ChangeUserInformationFormDispatchProps {
  UpdateAccount: (firstName: string, lastName: string) => void;
}

export interface ChangeUserInformationFormProps
  extends ChangeUserInformationFormOwnProps,
    ChangeUserInformationFormStateProps,
    ChangeUserInformationFormDispatchProps {}

export interface ChangeUserInformationFormOwnState extends Jss.SafePureComponentState {}

export interface AppState extends Account.GlobalAccountState, Account.AppState {}
