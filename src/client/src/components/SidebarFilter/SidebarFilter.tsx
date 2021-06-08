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

import React, { ChangeEvent, FC, HTMLProps, useState } from 'react';

import { Icon } from 'components';

import { cnSidebarFilter } from './cn';
import { FiltersList } from './FiltersList';

import './SidebarFilter.scss';

export type FacetValue = {
  name: string;
  displayName?: string;
  aggregateCount: number;
  value?: string;
};

export type Facet = {
  displayName?: string;
  foundValues: FacetValue[];
  name: string;
  values: any[];
  facetType?: 'checkbox' | 'tag';
};

export type SidebarFilterProps = HTMLProps<HTMLDivElement> & {
  facet: Facet;
  isApplied: (name: string, value: string) => boolean;
  onFacetChange: (name: string, value: string, e: ChangeEvent<HTMLInputElement>) => void;
  type?: 'checkbox' | 'tag';
};

export const SidebarFilter: FC<SidebarFilterProps> = ({
  className,
  facet,
  isApplied,
  onFacetChange,
  type = 'checkbox',
}) => {
  const [isValuesVisible, setIsValuesVisible] = useState(true);
  return (
    <div className={cnSidebarFilter(null, [className])}>
      <h4 className={cnSidebarFilter('Title')}>{facet.displayName || facet.name}</h4>
      {isValuesVisible && (
        <FiltersList
          facetName={facet.name}
          foundValues={facet.foundValues}
          isApplied={isApplied}
          onFacetChange={onFacetChange}
          type={type}
        />
      )}
      <div
        className={cnSidebarFilter('VisibilityToggler')}
        onClick={() => setIsValuesVisible((previousValue) => !previousValue)}
      >
        {isValuesVisible ? (
          <span>
            Hide all
            <Icon icon="icon-angle-up" className={cnSidebarFilter('Icon')} />
          </span>
        ) : (
          <span>
            Show all
            <Icon icon="icon-angle-down" className={cnSidebarFilter('Icon')} />
          </span>
        )}
      </div>
    </div>
  );
};
