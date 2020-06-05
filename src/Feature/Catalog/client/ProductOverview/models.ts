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

import * as JSS from 'Foundation/ReactJss/client';

import { ProductContext } from 'Foundation/Commerce/client';
import { Variant } from 'Foundation/Commerce/client/dataModel.Generated';

import * as ProductVariant from 'Feature/Catalog/client/Integration/ProductVariant';

export interface ProductOverviewOwnProps extends JSS.RenderingWithContext<JSS.BaseDataSourceItem, ProductOwerviewContext> {}

export interface ProductOverviewStateProps {
  selectedVariant: Variant;
}
export interface ProductOverviewDispatchProps {}

export interface ProductOverviewProps
  extends ProductOverviewOwnProps,
    ProductOverviewStateProps,
    ProductOverviewDispatchProps {}
export interface ProductOverviewState extends JSS.SafePureComponentState {}

export interface AppState extends ProductVariant.ProductVariantGlobalState {}

export interface ProductOwerviewContext extends ProductContext, JSS.ImageFallbackContext {}
