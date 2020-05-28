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

import { renderingWithContext } from 'Foundation/ReactJss/client';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import * as Account from 'Feature/Account/client/Integration/Account';

import Component from './Component';

import { AppState, ChangePasswordDispatchProps, ChangePasswordOwnProps, ChangePasswordStateProps } from './models';

const mapStateToProps = (state: AppState) => {
  const changePasswordState = Account.changePassword(state);

  return {
    changePasswordState,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      ChangePassword: Account.ChangePassword,
      VerifyCommerceUser: Account.VerifyCommerceUser,
    },
    dispatch
  );

const connectedToStore = connect<ChangePasswordStateProps, ChangePasswordDispatchProps, ChangePasswordOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const ChangePassword = compose(connectedToStore, renderingWithContext)(Component) ;
