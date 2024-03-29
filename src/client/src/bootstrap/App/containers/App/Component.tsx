//    Copyright 2021 EPAM Systems, Inc.
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
import { ToastContainer } from 'react-toastify';

import * as JSS from 'Foundation/ReactJss';
import { SITECORE_ROUTES } from 'Foundation/ReactJss/SitecoreContext';
import SitecoreContextFactory from 'Foundation/ReactJss/SitecoreContextFactory';
import { NotFound } from 'ui/NotFound';

import { AppProps } from './../../../models';
import { LoadingBar, ServerError } from '../../components';
import { CookieBanner } from '../../../../ui/CookieBanner';

import { Default } from 'layouts';

import 'react-toastify/dist/ReactToastify.min.css';

const { SitecoreContext } = JSS;

export default class AppComponent extends JSS.SafePureComponent<AppProps, {}> {
  public componentDidMount() {
    const { InitAuthentication } = this.props;
    InitAuthentication();
  }

  public safeRender() {
    const sitecoreContext = {
      ...this.props.sitecoreContext,
      routeFields: this.props.routeFields,
    };

    SitecoreContextFactory.setSitecoreContext(sitecoreContext);

    return (
      <>
        <LoadingBar loading={this.props.isLoading} />
        <CookieBanner isLoaded={this.props.isLoaded} />
        <Switch>
          <Route
            exact={true}
            path={SitecoreContext.NOT_FOUND_ROUTE}
            render={(routeProps) => <NotFound {...routeProps} {...this.props} />}
          />
          <Route exact={true} path={SitecoreContext.SERVER_ERROR_ROUTE} component={ServerError} />
          {SITECORE_ROUTES.map((path, index) => (
            <Route key={index} path={path} render={(routeProps) => <Default {...routeProps} {...this.props} />} />
          ))}
          <Route render={(routeProps) => <NotFound {...routeProps} {...this.props} />} />
        </Switch>
        <ToastContainer hideProgressBar={true} closeOnClick={false} newestOnTop={true} />
      </>
    );
  }
}
