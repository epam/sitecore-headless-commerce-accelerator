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

import * as JSS from 'Foundation/ReactJss';

import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import { LoadingStatus } from 'Foundation/Integration';

import * as Account from 'Feature/Account/Integration/Account';

import Component from './Component';
import { AppState, SignUpDispatchProps, SignUpStateProps } from './models';

const mapStateToProps = (state: AppState): SignUpStateProps => {
  const commerceUser = Account.commerceUser(state);
  const signUpState = Account.signUp(state);
  const returnUrl = JSS.routingLocationPathname(state);

  return {
    accountValidation: signUpState.accountValidation,
    commerceUser,
    createAccount: signUpState.create,
    loading: signUpState.create.status === LoadingStatus.Loading,
    returnUrl,
  };
};

const mapDispatchToProps = (dispatch: Dispatch): SignUpDispatchProps =>
  bindActionCreators(
    {
      AccountValidation: Account.AccountValidation,
      CreateAccount: Account.CreateAccount,
      ResetValidation: Account.ResetValidation,
    },
    dispatch,
  );

export const SignUpForm = compose(JSS.rendering, connect(mapStateToProps, mapDispatchToProps))(Component);
