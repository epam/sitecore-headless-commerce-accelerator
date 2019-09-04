//    Copyright 2019 EPAM Systems, Inc.
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
import * as React from 'react';
import { Route, Switch } from 'react-router-dom';

import * as Jss from 'Foundation/ReactJss/client';
import { SITECORE_ROUTES } from 'Foundation/ReactJss/client/SitecoreContext';

import { Layout, LoadingBar, NotFound, ServerError } from './../../components';

import { registerCommerceInterceptor } from 'Foundation/Commerce/client';

import { AppProps } from './../../../models';

const { SitecoreContext } = Jss;

class App extends Jss.SafePureComponent<AppProps, {}> {
  public componentDidMount() {
    const { sitecoreContext, InitAuthentication } = this.props;

    registerCommerceInterceptor(sitecoreContext);

    InitAuthentication();
  }

  public safeRender() {
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

export default withExperienceEditorChromes(App);
