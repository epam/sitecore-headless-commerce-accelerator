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

import * as Jss from 'Foundation/ReactJss/client';

import * as ProductsSearch from 'Feature/Catalog/client/Integration/ProductsSearch';

import * as commonModels from 'Feature/Catalog/client/Integration/common/models';

export interface ProductsSearchOwnProps extends RouterProps {}
export interface ProductsSearchStateProps {
  productSearchParams: ProductsSearch.Params;
  isLoading: boolean;
}
export interface ProductsSearchDispatchProps {
  ChangeRoute: (newRoute: string) => void;
}

export interface ProductsSearchProps
extends ProductsSearchOwnProps,
ProductsSearchStateProps,
ProductsSearchDispatchProps {}

export interface ProductsSearchOwnState extends Jss.SafePureComponentState {
  initialKeyword: string;
  keyword: string;
  submitted: boolean;
}

export interface AppState extends ProductsSearch.GlobalProductSearchState, commonModels.AppState {}
