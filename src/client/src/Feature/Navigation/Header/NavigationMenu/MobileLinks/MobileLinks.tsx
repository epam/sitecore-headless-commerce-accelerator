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

import React, { FC, useCallback } from 'react';
import { useDispatch } from 'react-redux';

import { cnNavigation } from 'Feature/Navigation/Header/NavigationMenu/cn';
import { closeHamburgerMenu } from 'Feature/Navigation/Header/NavigationMenu/Integration';
import { NavigationLink } from 'Foundation/UI';

import './MobileLinks.scss';

type MobileLink = {
  title: string;
  url: string;
};

type MobileLinksProps = {
  links: MobileLink[];
};

export const MobileLinks: FC<MobileLinksProps> = ({ links }) => {
  const dispatch = useDispatch();

  const handleLinkClick = useCallback(() => {
    dispatch(closeHamburgerMenu());
  }, [dispatch]);

  return (
    <ul className={cnNavigation('MobileLinks')}>
      {links.map(({ title, url }, linkIndex) => {
        return (
          url && (
            <li
              key={`${title}${linkIndex}`}
              className={cnNavigation('MobileLinkWrapper')}
              data-autotests={`submenuCommerceItem_${title}`}
            >
              <NavigationLink className={cnNavigation('MobileLink')} to={url} onClick={handleLinkClick}>
                {title}
              </NavigationLink>
            </li>
          )
        );
      })}
    </ul>
  );
};
