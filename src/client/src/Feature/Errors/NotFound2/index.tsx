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

import { NavigationLink } from 'Foundation/UI';
import { NotFoundProps, NotFoundState } from './models';
import './styles.scss';

export class NotFound2 extends JSS.SafePureComponent<NotFoundProps, NotFoundState> {
  protected safeRender() {
    return (
      <div className="notfound">
        <div className="notfound-404">
          404
        </div>
        <div className="notfound-desc">Oops! Nothing was found</div>
        <div className="notfound-subdesc">The page you are looking for might have been removed had its name changed or is temporarily unavailable.
          <NavigationLink to="/" className="notfound-subdesc-link">
            {' '}Return to homepage
          </NavigationLink>
        </div>
      </div>
    );
  }
}
