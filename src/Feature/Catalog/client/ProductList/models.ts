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

import { RouterProps } from 'react-router';

import * as JSS from 'Foundation/ReactJss/client';

import * as commonModels from 'Feature/Catalog/client/Integration/common/models';
import * as ProductsSearch from 'Feature/Catalog/client/Integration/ProductsSearch';

export interface ProductListOwnProps extends RouterProps, JSS.RenderingWithContext<JSS.BaseDataSourceItem, JSS.ImageFallbackContext> {}

export interface ProductListStateProps {
  categoryId: string;
  currentPageNumber: number;
  search: string;
  isLoading: boolean;
  totalPageCount: number;
  totalItemCount: number;
  itemsPerPage: number;
  items: ProductsSearch.Product[];
}
export interface ProductListDispatchProps {
  InitSearch: (payload: ProductsSearch.InitSearchPayload) => void;
  ClearSearch: () => void;
  LoadMore: () => void;
  DiscardFacet: (name: string, value: string, search: string) => void;
}
export interface ProductListProps extends ProductListOwnProps, ProductListStateProps, ProductListDispatchProps {}

export interface AppState extends ProductsSearch.GlobalProductSearchState, commonModels.AppState {}
