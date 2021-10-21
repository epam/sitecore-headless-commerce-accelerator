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

import { FooterLinksProps } from './models';
import { TABLET_MAX_SCREEN_WIDTH } from 'components/Responsive/constants';

import './DropdownFooterLinks.scss';

export const DropdownFooterLinks = (props: FooterLinksProps) => {
  const { fields } = props;
  const { datasource } = fields.data;

  const [isOpen, setIsOpen] = useState(false);
  const { width } = useWindowSize();
  const isMobileMode = width < TABLET_MAX_SCREEN_WIDTH;

  const toggleAccordion = () => {
    setIsOpen(!isOpen);
  };

  return (
    isMobileMode && (
      <ul className="footer-links-list">
        {datasource.links &&
          datasource.links.items &&
          datasource.links.items.map((link, index) => {
            const { uri, isPrimary, id } = link;

            return index === 0 ? (
              <li key={id} className="footer-list-item  footer-list-item-accordion" onClick={toggleAccordion}>
                <div className={`footer-link footer-link-${isPrimary.jss.value ? 'primary' : 'secondary'}`}>
                  {uri.jss.value.text}
                </div>
                <Icon icon="icon-angle-down" size="l" />
              </li>
            ) : isOpen ? (
              <li key={id} className="footer-list-item">
                <Link
                  field={uri.jss}
                  className={`footer-link footer-link-${isPrimary.jss.value ? 'primary' : 'secondary'}`}
                />
              </li>
            ) : null;
          })}
      </ul>
    )
  );
};
