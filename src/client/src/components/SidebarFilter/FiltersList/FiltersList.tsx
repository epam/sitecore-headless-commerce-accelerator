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

import React, { FC } from 'react';

import { FacetValue } from 'services/commerce';

import { CheckboxFilterItem } from '../CheckboxFilterItem';
import { cnSidebarFilter } from '../cn';
import { SidebarFilterProps } from '../SidebarFilter';
import { TagFilterItem } from '../TagFilterItem';

import './FiltersList.scss';

type FilterItemPropsNames = 'className' | 'isApplied' | 'onFacetChange';
type FiltersListPropsNames = FilterItemPropsNames | 'type';
type FiltersListProps = Pick<SidebarFilterProps, FiltersListPropsNames> & {
  facetName: string;
  foundValues: FacetValue[];
};

export type FilterItemProps = Pick<FiltersListProps, 'facetName'> &
  Pick<SidebarFilterProps, FilterItemPropsNames> & {
    foundValue: FacetValue;
    id: string;
  };

export const FiltersList: FC<FiltersListProps> = ({
  className,
  facetName,
  foundValues,
  isApplied,
  onFacetChange,
  type,
}) => {
  return (
    <ul className={cnSidebarFilter('List', { tagList: type === 'tag' }, [className])}>
      {foundValues.map((foundValue, i) => {
        const id = `${foundValue.name}${foundValue.aggregateCount}${i}`;
        return type === 'checkbox' ? (
          <CheckboxFilterItem
            facetName={facetName}
            foundValue={foundValue}
            id={id}
            isApplied={isApplied}
            key={i}
            onFacetChange={onFacetChange}
          />
        ) : (
          <TagFilterItem
            facetName={facetName}
            foundValue={foundValue}
            id={id}
            isApplied={isApplied}
            key={i}
            onFacetChange={onFacetChange}
          />
        );
      })}
    </ul>
  );
};
