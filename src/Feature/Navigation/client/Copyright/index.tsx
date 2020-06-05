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

import { RichText } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { CopyrightProps, CopyrightState } from './models';

import './styles.scss';

class CopyrightComponent extends JSS.SafePureComponent<CopyrightProps, CopyrightState> {
  protected safeRender() {
    const { fields } = this.props;
    const { datasource } = fields.data;

    return (
      <div className="row">
        <div className="col-xs-12">
          <RichText className="footer-copyright" field={datasource.text.jss} />
        </div>
      </div>
    );
  }
}

export const Copyright = JSS.rendering(CopyrightComponent);
