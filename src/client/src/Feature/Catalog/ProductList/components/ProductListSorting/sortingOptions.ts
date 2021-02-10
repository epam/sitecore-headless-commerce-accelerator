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

import { SortingType } from './ProductListSorting';

export const defaultSortingOption: SortingType = {
  isAlphabetical: true,
  sortingDirection: '0',
  sortingField: 'brand',
};

export const sortingOptions: SortingType[] = [
  {
    isAlphabetical: true,
    sortingDirection: '0',
    sortingField: 'brand',
  },
  {
    isAlphabetical: true,
    sortingDirection: '1',
    sortingField: 'brand',
  },
  {
    isAlphabetical: true,
    sortingDirection: '0',
    sortingField: 'name',
  },
  {
    isAlphabetical: true,
    sortingDirection: '1',
    sortingField: 'name',
  },
  {
    isAlphabetical: false,
    sortingDirection: '0',
    sortingField: 'price',
  },
  {
    isAlphabetical: false,
    sortingDirection: '1',
    sortingField: 'price',
  },
  {
    isAlphabetical: false,
    sortingDirection: '0',
    sortingField: 'rating',
  },
  {
    isAlphabetical: false,
    sortingDirection: '1',
    sortingField: 'rating',
  },
];
