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
import { withRouter } from 'react-router-dom';
import { bindActionCreators, Dispatch } from 'redux';

import { InitAuthentication } from 'Feature/Account/client/Integration/Authentication';
import { LoadingStatus } from 'Foundation/Integration/client';
import * as Jss from 'Foundation/ReactJss/client';

import { AppDispatchProps, AppState, AppStateProps } from './../../../models';

import App from './Component';

const mapStateToProps = (state: AppState): AppStateProps => {
  const sitecore = Jss.sitecore(state);
  const viewBag = state.viewBag;

  return {
    dictionary: viewBag.dictionary,
    isLoading: sitecore.status === LoadingStatus.Loading,
    language: viewBag.language || 'EN',
    rendering: sitecore.route,
    routeFields: sitecore.route && state.sitecore.route.fields,
    sitecoreContext: state.sitecore.context,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      InitAuthentication,
    },
    dispatch
  );

export default withRouter(connect<AppStateProps, AppDispatchProps>(
  mapStateToProps,
  mapDispatchToProps
)(App) as any);
