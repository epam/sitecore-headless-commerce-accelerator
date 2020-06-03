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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import classnames from 'classnames';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { GridProps, GridState } from '../models';

class OneColumnComponent extends JSS.SafePureComponent<GridProps, GridState> {
  public safeRender() {
    return (
      <main>
        <div
          className={classnames('container', { [this.props.params.wrapperClass]: !!this.props.params.wrapperClass })}
        >
          <div className="row">
            <div className="col-md-12">
              <Placeholder name="w-col-wide" rendering={this.props.rendering} />
            </div>
          </div>
        </div>
      </main>
    );
  }
}

export const OneColumn = JSS.rendering(OneColumnComponent);
