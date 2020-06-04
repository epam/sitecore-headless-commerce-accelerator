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

import { Variant } from 'Foundation/Commerce/client';

import { AppState as SitecoreState } from './../common/models';

export interface SelectedProductVariant {
  [productId: string]: Variant;
}

export interface SelectedProductVariantPayload extends SelectedProductVariant {}

export interface SelectedProductVariantState {
  selectedProductVariant: SelectedProductVariant;
}

export interface ProductVariantGlobalState extends SitecoreState, SelectedProductVariantState {}
