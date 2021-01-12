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

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';
import { WelcomeProps, WelcomeState } from './models';

import '../styles.scss';

export class Welcome extends JSS.SafePureComponent<WelcomeProps, WelcomeState> {
  protected safeRender() {
    return (
      <div className="welcome-area">
        <div className="container">
          <div className="welcome-content">
            <h5>Who Are We</h5>
            <h1>Welcome To Flone</h1>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, t irure </p>
          </div>
        </div>
      </div>
    );
  }
}
