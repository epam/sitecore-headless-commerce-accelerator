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

import * as JSS from 'Foundation/ReactJss';
import Logo from 'static/images/logo-main-black.png';

import { CopyrightProps, CopyrightState } from './models';

import { cnFooterCopyright } from './cn';
import './styles.scss';

class CopyrightComponent extends JSS.SafePureComponent<CopyrightProps, CopyrightState> {
  protected safeRender() {
    const {
      fields: {
        data: {
          datasource: {
            text: { jss },
          },
        },
      },
    } = this.props;

    return (
      <div>
        <img src={Logo} alt="Discover" className={cnFooterCopyright('Logo')} />
        <RichText className={cnFooterCopyright()} field={jss} />
      </div>
    );
  }
}

export const Copyright = JSS.rendering(CopyrightComponent);
