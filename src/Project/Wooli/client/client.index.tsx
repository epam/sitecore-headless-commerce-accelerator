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

// import the babel polyfill here instead of via webpack
import 'babel-polyfill';

import * as React from 'react';
import * as ReactDOM from 'react-dom';

import { LayoutServiceData } from '@sitecore-jss/sitecore-jss';
import i18n from 'i18next';

import { dataProvider } from 'Foundation/ReactJss/client';

import App from './App';
import Root from './Root';
import initialState from './Root/initialState';
import AppStore from './store';

// styles entry point
import { ConnectedRouter } from 'connected-react-router';
import GraphQLClientFactory from 'Foundation/ReactJss/client/graphQL/GraphQLClientFactory';
import 'Foundation/UI/client/common/scss/style.scss';
import i18nInit from './App/i18n';

const DEFAULT_LANGUAGE = 'en';

const render = (renderFunction: ReactDOM.Renderer, layoutServiceData: LayoutServiceData, dictionary: i18n.ResourceKey) => {
  const rootElement = document.getElementById('app');
  const defaultViewBag = {
    language: 'en',
  };
  const store = new AppStore(initialState(layoutServiceData.sitecore, defaultViewBag));

  i18nInit(layoutServiceData.sitecore.context.language, dictionary).then(() => {
    renderFunction(
    // tslint:disable-next-line: jsx-wrap-multiline
        <Root store={store.instance} graphQLClient={GraphQLClientFactory('', false)}>
          <ConnectedRouter history={store.history}>
            <App />
          </ConnectedRouter>
        </Root>,
        rootElement);

  });
};
const getRoute = (fullItemPath: string) => {
  const languageCodeRegExp = /^\/\w+(-\w+)/g;

  const pathLanguageMatches = fullItemPath.match(languageCodeRegExp);
  if (!!pathLanguageMatches) {
    return {
      itemPath: fullItemPath.replace(languageCodeRegExp, ''),
      language: pathLanguageMatches[0].replace(/^\//g, ''),
    };
  }

  const standardScLanguageRegExp = /^\/en/g;

  const defaultLanguageMatch = fullItemPath.match(standardScLanguageRegExp);
  if (defaultLanguageMatch) {
    return {
      itemPath: fullItemPath.replace(standardScLanguageRegExp, ''),
      language: defaultLanguageMatch[0].replace(/^\//g, ''),
    };
  }

  return {
    itemPath: fullItemPath,
    language: DEFAULT_LANGUAGE,
  };
};

const initialize = async (jssState?: LayoutServiceData, dictionary?: i18n.ResourceKey) => {
  // if sitecoreData is not available fetch it from SC
  if (!jssState) {
    try {
      const { language, itemPath } = getRoute(location.pathname);

      const decodedItemPath = decodeURI(itemPath);
      const data: LayoutServiceData = await dataProvider.getRouteData(decodedItemPath, language);
      render(ReactDOM.render, data, dictionary);
    } catch (e) {
      const body = document.getElementsByTagName('body')[0];
      body.innerHTML = `<h4>${e.message || 'An error occurred while running the app'}</h4>`;
    }
  } else {
    // when React initializes from a SSR-based initial state, you need to render with `hydrate` instead of `render`
    render(ReactDOM.hydrate, jssState, dictionary);
  }
};

initialize(window['__JSS_STATE__'], window['__DICTIONARY__']);
