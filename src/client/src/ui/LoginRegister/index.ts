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

import { Location, Pathname } from 'history';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators } from 'redux';

import * as Extensions from 'Foundation/Base';
import * as JSS from 'Foundation/ReactJss';
import { ChangeRoute } from 'Foundation/ReactJss/SitecoreContext';

import * as Account from 'services/account';

import { LoginRegisterFormComponent } from './Component';
import { AppState, LoginRegisterStateProps } from './models';

const mapStateToProps = (state: AppState): LoginRegisterStateProps => {
  const commerceUser = Account.commerceUser(state);
  const location: Location = state.router.location;
  const currentForm = Extensions.tryParseUrlSearch(location.search).form;
  const pathname: Pathname = location.pathname;
  return {
    commerceUser,
    currentForm,
    pathname,
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ChangeRoute,
    },
    dispatch,
  );

export const LoginRegister = compose(
  JSS.rendering,
  connect(mapStateToProps, mapDispatchToProps),
)(LoginRegisterFormComponent);
