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

import { Placeholder, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import classnames from 'classnames';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { GridControlProps, GridControlState } from '../models';

class ThreeColumnRowComponent extends JSS.SafePureComponent<GridControlProps, GridControlState> {
  public safeRender() {
    return (
      <div className={classnames('row', { [this.props.params.wrapperClass]: !!this.props.params.wrapperClass })}>
        <div className={this.props.params.firstColumnClass || 'col-xs-4'}>
          <Placeholder name="w-col-1" rendering={this.props.rendering} />
        </div>
        <div className={this.props.params.secondColumnClass || 'col-xs-4'}>
          <Placeholder name="w-col-2" rendering={this.props.rendering} />
        </div>
        <div className={this.props.params.thirdColumnClass || 'col-xs-4'}>
          <Placeholder name="w-col-3" rendering={this.props.rendering} />
        </div>
      </div>
    );
  }
}

export const ThreeColumnRow = withExperienceEditorChromes(ThreeColumnRowComponent);
