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

import * as JSS from 'Foundation/ReactJss';

import { FooterLinksProps } from './models';
import { LINK_TYPES } from '../DropdownFooterLinks';

import { cnFooterLinks } from './cn';
import './styles.scss';

class FooterLinksComponent extends JSS.SafePureComponent<FooterLinksProps, {}> {
  protected safeRender() {
    const {
      fields: {
        data: {
          datasource: { links },
        },
      },
    } = this.props;

    return (
      <ul className={cnFooterLinks()}>
        {links &&
          links.items &&
          links.items.map((link, index) => {
            const {
              uri,
              isPrimary: {
                jss: { value },
              },
            } = link;
            const type = value ? LINK_TYPES.PRIMARY : LINK_TYPES.SECONDARY;
            const isPrimary = type === LINK_TYPES.PRIMARY;
            return (
              <li key={index} className={cnFooterLinks('LinkWrapper')}>
                {isPrimary ? (
                  <div className={cnFooterLinks('Link', { type })}>{uri.jss.value.text}</div>
                ) : (
                  <Link field={uri.jss} className={cnFooterLinks('Link', { type })} />
                )}
              </li>
            );
          })}
      </ul>
    );
  }
}

export const FooterLinks = JSS.rendering(FooterLinksComponent);
