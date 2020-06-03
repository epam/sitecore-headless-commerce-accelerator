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

import { Link } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { FooterLinksProps, FooterLinksState } from './models';

import './styles.scss';

class FooterLinksComponent extends JSS.SafePureComponent<FooterLinksProps, FooterLinksState> {
  protected safeRender() {
    const { fields } = this.props;
    const { datasource } = fields.data;

    return (
      <ul className="footer-links-list">
        {datasource.links && datasource.links.items && datasource.links.items.map((link, index) => {
            const { uri, isPrimary } = link;
            return (
              <li key={index} className="footer-list-item">
                <Link field={uri.jss} className={`footer-link footer-link-${isPrimary.jss.value ? 'primary' : 'secondary'}`} />
              </li>
            );
        })}
      </ul>
    );
  }
}

export const FooterLinks = JSS.rendering(FooterLinksComponent);