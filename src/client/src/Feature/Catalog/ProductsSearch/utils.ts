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

import { tryParseUrlSearch } from 'Foundation/Base';

import { FACET_PARAMETER_NAME, KEYWORD_PARAMETER_NAME, Params } from 'services/productsSearch';

export const getKeywordFromSearch = (search: string) => {
  const parsedSearch = tryParseUrlSearch(search);

  return parsedSearch[KEYWORD_PARAMETER_NAME] || '';
};

export const buildQueryString = (value: string, params: Params) => {
  const newSearchQuery = [];

  newSearchQuery.push(`${KEYWORD_PARAMETER_NAME}=${value}`);

  if (params[FACET_PARAMETER_NAME]) {
    newSearchQuery.push(`${FACET_PARAMETER_NAME}=${encodeURIComponent(params[FACET_PARAMETER_NAME])}`);
  }

  return newSearchQuery.length !== 0 ? `?${newSearchQuery.join('&')}` : '';
};
