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

import React, { FC, FormEvent } from 'react';

import { Select, SelectProps } from 'components';
import { ChangeSortingTypePayload } from 'services/search';

import { cnProductList } from '../../cn';
import { defaultSortingOption, sortingOptions } from './sortingOptions';

import './ProductListSorting.scss';

export type SortingType = {
  isAlphabetical?: boolean;
  sortingField?: string;
  sortingDirection?: string;
};

export type SortingProps = SelectProps & {
  options?: SortingType[];
  selectedOption?: SortingType;
  ChangeSorting?: (payload: ChangeSortingTypePayload) => void;
};

const changeSortingTypeHandler = (
  e: FormEvent<HTMLSelectElement>,
  handler: (payload: ChangeSortingTypePayload) => void,
) => {
  const [newSortingField, newSortingDirection] = e.currentTarget.value.split('-');

  const payload = {
    sortingDirection: newSortingDirection,
    sortingField: newSortingField,
  };

  handler(payload);
};

export const ProductListSorting: FC<SortingProps> = ({
  className,
  fullWidth = true,
  options = sortingOptions,
  selectedOption = null,
  ChangeSorting,
  ...rest
}) => {
  return (
    <div className={cnProductList('Sorting', [className])}>
      <Select
        fullWidth={fullWidth}
        defaultValue={
          selectedOption
            ? `${selectedOption.sortingField}-${selectedOption.sortingDirection}`
            : `${defaultSortingOption.sortingField}-${defaultSortingOption.sortingDirection}`
        }
        onChange={(e) => changeSortingTypeHandler(e, ChangeSorting)}
        {...rest}
      >
        {options.map((option, idx) => (
          <option key={`${idx}-${option.sortingField}`} value={`${option.sortingField}-${option.sortingDirection}`}>
            {`${option.sortingField} - ${
              option.sortingDirection === '0'
                ? option.isAlphabetical
                  ? 'A to Z'
                  : 'Low to High'
                : option.isAlphabetical
                ? 'Z to A'
                : 'High to Low'
            }`}
          </option>
        ))}
      </Select>
    </div>
  );
};
