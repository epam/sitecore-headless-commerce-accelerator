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

import { RouterProps } from 'react-router';

import * as commonModels from 'Feature/Catalog/Integration/common/models';
import * as JSS from 'Foundation/ReactJss';
import * as Search from 'services/search';

export interface ProductListOwnProps
  extends RouterProps,
    JSS.RenderingWithContext<JSS.BaseDataSourceItem, JSS.ImageFallbackContext> {}

export interface ProductListStateProps {
  categoryId: string;
  currentPageNumber: number;
  search: string;
  isLoading: boolean;
  sortingDirection: string;
  sortingField: string;
  totalPageCount: number;
  totalItemCount: number;
  itemsPerPage: number;
  items: Search.Product[];
}
export interface ProductListOwnState extends JSS.SafePureComponentState {
  firstLoad: boolean;
}
export interface ProductListDispatchProps {
  InitSearch: (payload: Search.InitSearchPayload) => void;
  ClearSearch: () => void;
  LoadMore: () => void;
  DiscardFacet: (name: string, value: string, search: string) => void;
  ChangeSorting: (payload: Search.ChangeSortingTypePayload) => void;
}
export interface ProductListProps extends ProductListOwnProps, ProductListStateProps, ProductListDispatchProps {}

export interface AppState extends Search.GlobalSearchState, commonModels.AppState {}
