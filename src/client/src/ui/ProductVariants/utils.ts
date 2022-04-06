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

import { get } from 'lodash';

import { tryParseUrlSearch } from 'utils';
import { VARIANT_PARAMETER_NAME } from 'services/productVariant';
import { Variant } from 'services/commerce';

export const getVariantIdFromQuery = (query: string) => {
  const parsedQuery = tryParseUrlSearch(query);

  return get(parsedQuery, VARIANT_PARAMETER_NAME, '');
};

export const sortVariantsArrayByNestedProperty = (prop: string, array: Variant[]) => {
  const splitProp = prop.split('.');
  const length = splitProp.length;

  array.sort(function (a: any, b: any) {
    let i = 0;
    while (i < length) {
      a = a[splitProp[i]];
      b = b[splitProp[i]];
      i++;
    }
    if (a < b) {
      return -1;
    } else if (a > b) {
      return 1;
    } else {
      return 0;
    }
  });

  return array;
};
