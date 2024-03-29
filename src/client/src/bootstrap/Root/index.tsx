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

import { ApolloProvider } from '@apollo/client';
import { Provider } from 'react-redux';

import { SitecoreContext } from '@sitecore-jss/sitecore-jss-react';

import SitecoreContextFactory from 'Foundation/ReactJss/SitecoreContextFactory';

import { RootProps } from '../models';
import componentFactory from './../componentFactory';
import ActionTypes from './actionTypes';

import { eventHub, events } from 'services/eventHub';

import { subscribeAnalyticsEvents } from 'services/analytics';

subscribeAnalyticsEvents();

class Root extends React.Component<RootProps> {
  public constructor(props: RootProps) {
    super(props);
  }

  public componentDidMount() {
    eventHub.publish(events.ANALYTICS.INITIALIZE);
    this.props.store.dispatch({ type: ActionTypes.INITIALIZATION_COMPLETE });
  }

  public render() {
    return (
      <ApolloProvider client={this.props.graphQLClient}>
        <Provider store={this.props.store}>
          <SitecoreContext componentFactory={componentFactory} contextFactory={SitecoreContextFactory}>
            {this.props.children}
          </SitecoreContext>
        </Provider>
      </ApolloProvider>
    );
  }
}

export default Root;
