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

import * as JSS from 'Foundation/ReactJss';

import * as commonModels from 'Feature/Catalog/Integration/common/models';
import * as ProductsSearch from 'Feature/Catalog/Integration/ProductsSearch';

export interface ProductShopOwnProps
  extends RouterProps,
    JSS.RenderingWithContext<JSS.BaseDataSourceItem, JSS.ImageFallbackContext> {}

export interface ProductShopOwnState extends JSS.SafePureComponentState {
  firstLoad: boolean;
}
export interface ProductShopStateProps {
  isLoading: boolean;
}
export interface ProductShopDispatchProps {}
export interface ProductShopProps extends ProductShopStateProps, ProductShopOwnProps, ProductShopDispatchProps {}

export interface AppState extends ProductsSearch.GlobalProductSearchState, commonModels.AppState {}
