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
import { NavigationLink } from 'Foundation/UI';
import * as React from 'react';

import { QuickNavigationProps, QuickNavigationState } from './models';
import './styles.scss';

class QuickNavigationComponent extends JSS.SafePureComponent<QuickNavigationProps, QuickNavigationState> {
  protected safeRender() {
    return (
      <>
        <div className="settings">
          <div className="settings_item">
            <span>English</span>
          </div>
          <div className="settings_item">
            <span>USD</span>
          </div>
        </div>
        <div className="quick-navigation-2">
          <NavigationLink to="/Store Locator">
            <i className="pe-7s-map-marker" />
            Store Locator
          </NavigationLink>
        </div>
      </>
    );
  }
}

export const QuickNavigation2 = JSS.rendering(QuickNavigationComponent);
