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

import React, { FC } from 'react';

import { Image } from '@sitecore-jss/sitecore-jss-react';

import { ImageField } from 'Foundation/ReactJss';
import { NavigationLinks, permanentNavigationLinks } from 'ui/Header';

import { Icon } from 'components';

import { cnNavigation } from '../cn';
import './DesktopItemContent.scss';

export type commerceNavigationLinkType = {
  title: string;
  url: string;
};

export type DesktopItemContentProps = {
  commerceItemNavigationLinks: commerceNavigationLinkType[];
  commerceItemName: string;
  commerceItemImage: ImageField;
  isOpen?: boolean;
};

export const DesktopItemContent: FC<DesktopItemContentProps> = ({
  commerceItemName,
  commerceItemNavigationLinks,
  commerceItemImage,
  isOpen = false,
}) => {
  return (
    <div className={cnNavigation('DesktopItemContent')}>
      <span className={cnNavigation('DesktopItemHeader', { opened: isOpen })}>
        {commerceItemName}
        <Icon icon="icon-angle-down" className={cnNavigation('AngleDownIcon')} />
      </span>
      <ul className={cnNavigation('DesktopSubmenu', { opened: isOpen })}>
        <li className={cnNavigation('DesktopSubmenuColumn')}>
          <NavigationLinks
            className={cnNavigation('DesktopSubmenuLink')}
            links={[{ title: 'Category' }, ...commerceItemNavigationLinks]}
            titleClass={cnNavigation('DesktopSubmenuTitle')}
          />
        </li>
        <li className={cnNavigation('DesktopSubmenuColumn')}>
          <NavigationLinks
            className={cnNavigation('DesktopSubmenuLink')}
            links={permanentNavigationLinks}
            titleClass={cnNavigation('DesktopSubmenuTitle')}
          />
        </li>
        <li className={cnNavigation('DesktopSubmenuColumn')}>
          <Image className={cnNavigation('DesktopItemImage')} media={commerceItemImage} />
        </li>
      </ul>
    </div>
  );
};
