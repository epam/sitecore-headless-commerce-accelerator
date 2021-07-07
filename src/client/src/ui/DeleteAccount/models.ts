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

import * as Jss from 'Foundation/ReactJss';
import * as Commerce from 'services/commerce';
import * as Account from 'services/account';

export interface DeleteAccountOwnProps extends Jss.RenderingWithContext<Jss.BaseDataSourceItem> {}

export interface DeleteAccountStateProps {
  deleteAccountState: Account.DeleteAccountState;
  commerceUser: Commerce.User;
}

export interface DeleteAccountDispatchProps {
  DeleteAccount: () => void;
}

export interface DeleteAccountProps
  extends DeleteAccountOwnProps,
    DeleteAccountStateProps,
    DeleteAccountDispatchProps {}

export interface DeleteAccountOwnState extends Jss.SafePureComponentState {}

export interface AppState extends Account.GlobalAccountState, Account.AppState {}
