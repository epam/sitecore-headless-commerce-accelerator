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

import * as React from 'react';
import { Route, Switch } from 'react-router-dom';

import { registerCommerceInterceptor } from 'Foundation/Commerce/client';
import * as JSS from 'Foundation/ReactJss/client';
import { SITECORE_ROUTES } from 'Foundation/ReactJss/client/SitecoreContext';
import SitecoreContextFactory from 'Foundation/ReactJss/client/SitecoreContextFactory';

import { AppProps } from './../../../models';
import { Layout, LoadingBar, NotFound, ServerError } from './../../components';

const { SitecoreContext } = JSS;

export default class AppComponent extends JSS.SafePureComponent<AppProps, {}> {
  public componentDidMount() {
    const { sitecoreContext, InitAuthentication } = this.props;

    registerCommerceInterceptor(sitecoreContext);

    InitAuthentication();
  }

  public safeRender() {
    const sitecoreContext = {
      ...this.props.sitecoreContext,
      routeFields: this.props.routeFields
    };

    SitecoreContextFactory.setSitecoreContext(sitecoreContext);

    return (
      <>
        <LoadingBar loading={this.props.isLoading} />
        <Switch>
          <Route exact={true} path={SitecoreContext.NOT_FOUND_ROUTE} component={NotFound} />
          <Route exact={true} path={SitecoreContext.SERVER_ERROR_ROUTE} component={ServerError} />
          {SITECORE_ROUTES.map((path, index) => (
            <Route key={index} path={path} render={(routeProps) => <Layout {...routeProps} {...this.props} />} />
          ))}
          <Route component={NotFound} />
        </Switch>
      </>
    );
  }
}
