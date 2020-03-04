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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import * as Account from 'Feature/Account/client/Integration/Account';

import Component from './Component';
import {
  AppState,
  ChangeUserInformationFormDispatchProps,
  ChangeUserInformationFormOwnProps,
  ChangeUserInformationFormStateProps,
} from './models';

const mapStateToProps = (state: AppState) => {
  const commerceUser = Account.commerceUser(state);
  const updateStatus = Account.updateStatus(state);
  return {
    commerceUser,
    updateStatus,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      UpdateAccount: Account.UpdateAccount,
    },
    dispatch
  );

const connectedToStore = connect<
  ChangeUserInformationFormStateProps,
  ChangeUserInformationFormDispatchProps,
  ChangeUserInformationFormOwnProps
>(
  mapStateToProps,
  mapDispatchToProps
)(Component);

export const ChangeUserInformationForm = withExperienceEditorChromes(connectedToStore);
