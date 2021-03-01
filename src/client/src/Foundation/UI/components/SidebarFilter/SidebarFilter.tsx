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

import { Facet } from 'Foundation/Commerce';

import { cnSidebarFilter } from './cn';
import { FiltersList } from './FiltersList';

import './SidebarFilter.scss';

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
      <h4 className={cnSidebarFilter('Title')}>{facet.displayName}</h4>
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
            <i className={cnSidebarFilter('Icon', ['fa fa-angle-up'])} />
          </span>
        ) : (
          <span>
            Show all
            <i className={cnSidebarFilter('Icon', ['fa fa-angle-down'])} />
          </span>
        )}
      </div>
    </div>
  );
};
