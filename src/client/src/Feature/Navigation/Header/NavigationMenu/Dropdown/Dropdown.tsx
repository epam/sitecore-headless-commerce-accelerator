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

import React, { FC, HTMLProps, useCallback, useState } from 'react';

import { MinusIcon } from 'Foundation/UI/components/MinusIcon';
import { PlusIcon } from 'Foundation/UI/components/PlusIcon';

import { cnNavigation } from '../cn';

import './Dropdown.scss';

type DropdownProps = HTMLProps<HTMLDivElement> & {
  firstLevel?: boolean;
  title: string;
};

export const Dropdown: FC<DropdownProps> = ({ children, firstLevel = false, title }) => {
  const [isExpanded, setIsExpanded] = useState(false);

  const toggleDropdownList = useCallback(() => {
    setIsExpanded((prevIsExpanded) => !prevIsExpanded);
  }, [isExpanded, setIsExpanded]);

  return (
    <div className={cnNavigation('Dropdown')}>
      <div className={cnNavigation('DropdownHeading', { firstLevel })} onClick={toggleDropdownList}>
        <span>{title}</span>
        {isExpanded ? <MinusIcon iconTheme="thin" /> : <PlusIcon iconTheme="thin" />}
      </div>
      <div className={cnNavigation('DropdownContent', { open: isExpanded })}>{children}</div>
    </div>
  );
};
