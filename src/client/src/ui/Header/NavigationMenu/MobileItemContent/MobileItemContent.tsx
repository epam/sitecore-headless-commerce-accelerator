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

import { permanentNavigationLinks } from '../constants';
import { DesktopItemContentProps } from '../DesktopItemContent';
import { Dropdown } from '../Dropdown';
import { MobileLinks } from '../MobileLinks';

import { cnNavigation } from '../cn';
import './MobileItemContent.scss';

type MobileItemContentProps = Pick<DesktopItemContentProps, 'commerceItemName' | 'commerceItemNavigationLinks'>;

export const MobileItemContent: FC<MobileItemContentProps> = ({ commerceItemName, commerceItemNavigationLinks }) => {
  return (
    <div className={cnNavigation('MobileItemContent')}>
      <Dropdown firstLevel={true} title={commerceItemName}>
        <Dropdown title="Category">
          <MobileLinks links={commerceItemNavigationLinks} />
        </Dropdown>
        <Dropdown title="Featured">
          <MobileLinks links={permanentNavigationLinks} />
        </Dropdown>
      </Dropdown>
    </div>
  );
};
