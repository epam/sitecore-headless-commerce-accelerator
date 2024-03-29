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
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';
import { BackToTop } from 'components/BackToTop';

import { GlobalFooterProps } from './models';

import { cnFooter } from './cn';
import './styles.scss';

class GlobalFooterComponent extends JSS.SafePureComponent<GlobalFooterProps, {}> {
  protected safeRender() {
    return (
      <footer id="footer-main" className={cnFooter(null, ['bg-gray pt-100 pb-100'])}>
        <div className={cnFooter('Wrap')}>
          <Placeholder name="footer-content" rendering={this.props.rendering} />
        </div>
        <BackToTop disabled={false} />
      </footer>
    );
  }
}

export const GlobalFooter = JSS.rendering(GlobalFooterComponent);
