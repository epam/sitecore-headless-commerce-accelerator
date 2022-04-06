//    Copyright 2021 EPAM Systems, Inc.
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

import React, { useState } from 'react';
import { Link } from '@sitecore-jss/sitecore-jss-react';

import { Icon } from 'components';
import { useWindowSize } from 'hooks/useWindowSize';

import { FooterLinks } from 'ui/FooterLinks';

import { FooterLinksProps } from './models';
import { TABLET_MAX_SCREEN_WIDTH } from 'components/Responsive/constants';

import { cnDropdownFooterLinks } from './cn';
import './DropdownFooterLinks.scss';

export const LINK_TYPES = {
  PRIMARY: 'primary',
  SECONDARY: 'secondary',
};

const LINK_WRAPPER_TYPES = {
  ACCORDION: 'accordion',
};

export const DropdownFooterLinks = (props: FooterLinksProps) => {
  const {
    fields,
    fields: {
      data: {
        datasource: { links },
      },
    },
  } = props;

  const [isOpen, setIsOpen] = useState(false);
  const { width } = useWindowSize();
  const isMobileMode = width < TABLET_MAX_SCREEN_WIDTH;

  const toggleAccordion = () => {
    setIsOpen(!isOpen);
  };

  return isMobileMode ? (
    <ul className={cnDropdownFooterLinks()}>
      {links &&
        links.items &&
        links.items.map((link, index) => {
          const {
            uri: { jss },
            isPrimary: {
              jss: { value },
            },
            id,
          } = link;
          const type = value ? LINK_TYPES.PRIMARY : LINK_TYPES.SECONDARY;
          return index === 0 ? (
            <li
              key={id}
              className={cnDropdownFooterLinks('LinkWrapper', { type: LINK_WRAPPER_TYPES.ACCORDION })}
              onClick={toggleAccordion}
            >
              <div className={cnDropdownFooterLinks('Link', { type })}>{jss.value.text}</div>
              <Icon icon="icon-angle-down" size="l" />
            </li>
          ) : isOpen ? (
            <li key={id} className={cnDropdownFooterLinks('LinkWrapper')}>
              <Link field={jss} className={cnDropdownFooterLinks('Link', { type })} />
            </li>
          ) : null;
        })}
    </ul>
  ) : (
    <FooterLinks fields={fields} rendering={props.rendering} />
  );
};
