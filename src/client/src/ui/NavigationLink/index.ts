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
import { bindActionCreators } from 'redux';

import * as Jss from 'Foundation/ReactJss';
import Component from './Component';
import { NavigationLinkDispatchProps, NavigationLinkOwnProps } from './models';

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ChangeRoute: Jss.SitecoreContext.ChangeRoute,
    },
    dispatch,
  );

export const NavigationLink = connect<null, NavigationLinkDispatchProps, NavigationLinkOwnProps>(
  null,
  mapDispatchToProps,
)(Component);
