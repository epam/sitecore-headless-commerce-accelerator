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

import { renderingWithContext } from 'Foundation/ReactJss';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import * as Account from 'services/account';

import Component from './Component';

import { AppState, DeleteAccountDispatchProps, DeleteAccountOwnProps, DeleteAccountStateProps } from './models';

const mapStateToProps = (state: AppState) => {
  const commerceUser = Account.commerceUser(state);
  const deleteAccountState = Account.deleteAccount(state);
  return {
    commerceUser,
    deleteAccountState,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      DeleteAccount: Account.DeleteAccount,
    },
    dispatch,
  );

const connectedToStore = connect<DeleteAccountStateProps, DeleteAccountDispatchProps, DeleteAccountOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const DeleteAccount = compose(connectedToStore, renderingWithContext)(Component);
