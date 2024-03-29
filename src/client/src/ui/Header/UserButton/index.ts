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

import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import * as Authentication from 'services/authentication';

import * as JSS from 'Foundation/ReactJss';

import { UserButtonComponent } from './Component';
import { AppState } from './models';

const mapStateToProps = (state: AppState) => {
  const commerceUser = JSS.sitecoreContext(state).commerceUser;
  const url = JSS.routingLocationPathname(state);
  const returnUrl = '/';

  return {
    commerceUser,
    returnUrl,
    url,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      logout: Authentication.Logout,
    },
    dispatch,
  );

export const UserButton = connect(mapStateToProps, mapDispatchToProps)(UserButtonComponent);
