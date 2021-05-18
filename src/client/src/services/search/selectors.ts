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

import { GlobalSearchState } from './models';

export const productsSearchSuggestions = (state: GlobalSearchState) => state.search.productSearchSuggestion;

export const productsSearchSuggestionsStatus = (state: GlobalSearchState) => productsSearchSuggestions(state).status;

export const productsSearchSuggestionsProducts = (state: GlobalSearchState) =>
  productsSearchSuggestions(state).products || [];

export const productsSearch = (state: GlobalSearchState) => state.search.productSearch;
export const productsSearchStatus = (state: GlobalSearchState) => productsSearch(state).status;
export const productSearchFacets = (state: GlobalSearchState) => productsSearch(state).facets;
export const productSearchItems = (state: GlobalSearchState) => productsSearch(state).items || [];
export const productSearchParams = (state: GlobalSearchState) => productsSearch(state).params;
export const productSearchCurrentPageNumber = (state: GlobalSearchState) => productsSearch(state).currentPageNumber;
export const productSearchTotalPageCount = (state: GlobalSearchState) => productsSearch(state).totalPageCount || 0;
