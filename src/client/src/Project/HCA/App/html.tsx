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
import * as ReactDOMServer from 'react-dom/server';

const Html = ({ component, initialState, distPath, path }: any) => {
  const content = component ? ReactDOMServer.renderToString(component) : '';

  return (
    <html>
      <head>
        <meta name="viewport" content="width=device-width, initial-scale=1, viewport-fit=cover" />
        <link rel="stylesheet" type="text/css" href={`${distPath}/project/hca/common.css`} />
      </head>
      <body>
        <div id="app" dangerouslySetInnerHTML={{ __html: content }} />
        <script dangerouslySetInnerHTML={{ __html: `window.__data=${JSON.stringify(initialState)};` }} />
        <script src={`${distPath}/project/hca/vendors.bundle.js`} />
        <script src={`${distPath}/project/hca/common.bundle.js`} />
      </body>
    </html>
  );
};

export default Html;
