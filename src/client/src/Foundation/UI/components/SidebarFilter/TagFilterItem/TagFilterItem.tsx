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

import React, { ChangeEvent, FC, useCallback } from 'react';

import { get } from 'lodash';

import { Checkbox } from 'Foundation/UI/components/Checkbox';

import { cnSidebarFilter } from '../cn';
import { FilterItemProps } from '../FiltersList';

import './TagFilterItem.scss';

export const TagFilterItem: FC<FilterItemProps> = ({
  className,
  facetName,
  foundValue,
  id,
  isApplied,
  onFacetChange,
}) => {
  const name = get(foundValue, ['name']);
  const displayName = get(foundValue, ['displayName']) || name;
  const value = get(foundValue, ['value']) || name;

  const handleChange = useCallback(
    (event: ChangeEvent<HTMLInputElement>) => {
      onFacetChange(facetName, value, event);
    },
    [facetName, value, onFacetChange],
  );

  return (
    <li className={cnSidebarFilter('TagFilterItem', [className])}>
      <label>
        <Checkbox
          checked={isApplied(facetName, value)}
          className={cnSidebarFilter('CheckboxControl', { hidden: true })}
          id={id}
          onChange={handleChange}
        />
        <label
          className={cnSidebarFilter('TagLabel', { checked: isApplied(facetName, value) })}
          htmlFor={id}
          title={name}
        >{`${displayName}`}</label>
      </label>
    </li>
  );
};
