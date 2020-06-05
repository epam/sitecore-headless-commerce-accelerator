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

import * as JSS from 'Foundation/ReactJss/client';
import * as React from 'react';

import { QuickNavigationProps, QuickNavigationState } from './models';
import './styles.scss';

class QuickNavigationComponent extends JSS.SafePureComponent<QuickNavigationProps, QuickNavigationState> {
  protected safeRender() {
    return (
      <nav className="quick-navigation">
        <ul>
          <li>
            <a href="">Store Locator</a>
          </li>
          <li>
            <a href="">Online Flyer</a>
          </li>
          <li>
            <a href="">Language/Currency</a>
          </li>
        </ul>
      </nav>
    );
  }
}

export const QuickNavigation = JSS.rendering(QuickNavigationComponent);
