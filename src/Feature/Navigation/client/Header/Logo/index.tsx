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

import { Image } from '@sitecore-jss/sitecore-jss-react';
import { NavigationLink } from 'Foundation/UI/client';

import { LogoProps, LogoState } from './models';
import './styles.scss';

class LogoComponent extends JSS.SafePureComponent<LogoProps, LogoState> {
  protected safeRender() {
    const { datasource } = this.props.fields.data;

    return (
      <NavigationLink to="/" className="navigation-logo">
        <Image media={datasource.image.jss} />
      </NavigationLink>
    );
  }
}

export const Logo = JSS.rendering(LogoComponent);
