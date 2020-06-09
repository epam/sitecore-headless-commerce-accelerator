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

import * as JSS from 'Foundation/ReactJss/client';
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import * as Authentication from 'Feature/Account/client/Integration/Authentication';

import { SingInComponent } from './Component';
import { AppState } from './models';

const mapStateToProps = (state: AppState) => {
  const authenticationProcess = Authentication.authenticationProcess(state);
  const returnUrl = JSS.routingLocationPathname(state);

  return {
    authenticationProcess,
    returnUrl,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      Authentication: Authentication.Authentication,
      ResetState: Authentication.ResetAuthenticationProcessState,
    },
    dispatch,
  );

export const SingIn = connect(mapStateToProps, mapDispatchToProps)(SingInComponent);
