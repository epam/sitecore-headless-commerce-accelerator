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

import 'isomorphic-fetch';

import { ConnectedRouter } from 'connected-react-router';
import * as React from 'react';
import * as ReactDOMServer from 'react-dom/server';
import Helmet from 'react-helmet';
import serializeJavascript from 'serialize-javascript';

import App from './App';
import Html from './App/html';
import i18nInit from './App/i18n';
import { AppState } from './models';
import Root from './Root';
import initialState from './Root/initialState';
import AppStore from './Store';

import GraphQLClientFactory from 'Foundation/ReactJss/graphQL/GraphQLClientFactory';
import indexTemplate from './build/index.html';

const parseServerData = (data: string | object, viewBag: string | object) => {
  const parsedData = data instanceof Object ? data : JSON.parse(data);
  const parsedViewBag = viewBag instanceof Object ? viewBag : JSON.parse(viewBag);

  return {
    sitecoreData: parsedData,
    viewBag: parsedViewBag,
  };
};

/** Asserts that a string replace actually replaced something */
function assertReplace(str: string, value: any, replacement: any) {
  let success = false;
  const result = str.replace(value, () => {
    success = true;
    return replacement;
  });

  if (!success) {
    throw new Error(
      `Unable to match replace token '${value}' in public/index.html template. If the HTML shell for the app is modified, also fix the replaces in server.js. Server-side rendering has failed!`,
    );
  }

  return result;
}

export const renderView = (callback: (error: Error, ssr: any) => void, path: string, data: string, viewBag: string) => {
  try {
    /*
      Data from server is double-encoded since MS JSS does not allow control
      over JSON serialization format.
    */
    const parsedData = parseServerData(data, viewBag);
    const state: AppState = initialState(parsedData.sitecoreData.sitecore, parsedData.viewBag);
    const BUNDLE_OUTPUT_PATH = '/dist/hca';

    i18nInit(parsedData.sitecoreData.sitecore.context.language, parsedData.viewBag.dictionary)
      .then(() => {
        const store = new AppStore(state, true);

        const component = (
          <Root store={store.instance} graphQLClient={GraphQLClientFactory('', true)}>
            <ConnectedRouter history={store.history}>
              <App />
            </ConnectedRouter>
          </Root>
        );
        return ReactDOMServer.renderToString(
          <Html component={component} initialState={state} distPath={BUNDLE_OUTPUT_PATH} path={path} />,
        );
      })
      .then((renderedAppHtml: string) => {
        const helmet = Helmet.renderStatic();

        // We remove the viewBag from the server-side state before sending it back to the client.
        // This saves bandwidth, because by default the viewBag contains the translation dictionary,
        // which is better cached as a separate client HTTP request than on every page, and HTTP context
        // information that is not meaningful to the client-side rendering.
        // If you wish to place items in the viewbag that are needed by client-side rendering, this
        // can be removed - but still delete state.viewBag.dictionary, at least.
        delete state.viewBag;

        // We add the GraphQL state to the SSR state so that we can avoid refetching queries after client load
        // Not using GraphQL? Get rid of this.
        // state.APOLLO_STATE = graphQLClient.cache.extract();

        // Inject the rendered app into the index.html template (built from /public/index.html)
        // IMPORTANT: use serialize-javascript or similar instead of JSON.stringify() to emit initial state,
        // or else you're vulnerable to XSS.
        let html = indexTemplate;

        // write the React app
        html = assertReplace(html, '<div id="root"></div>', `<div id="root">${renderedAppHtml}</div>`);
        // write the string version of our state
        html = assertReplace(
          html,
          '<script type="application/json" id="__JSS_STATE__">null',
          `<script type="application/json" id="__JSS_STATE__">${serializeJavascript(state, {
            isJSON: true,
          })}`,
        );

        // render <head> contents from react-helmet
        html = assertReplace(
          html,
          '<head>',
          `<head>${helmet.title.toString()}${helmet.meta.toString()}${helmet.link.toString()}`,
        );

        callback(null, { html });
      })
      .catch((error) => callback(error, null));
  } catch (err) {
    // need to ensure the callback is always invoked no matter what
    // or else SSR will hang
    callback(err, null);
  }
};
