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

import * as JSS from 'Foundation/ReactJss';
import { User } from 'services/commerce';
import { ProductVariantGlobalState } from 'services/productVariant';

export interface AddToCartOwnProps extends JSS.RenderingWithContext<JSS.BaseDataSourceItem> {
  className?: string;
}

export interface AddToCartStateProps {
  commerceUser: User;
}

export interface AddToCartDispatchProps {
}

export interface AppState extends ProductVariantGlobalState {}

export interface AddToCartProps extends AddToCartOwnProps, AddToCartStateProps, AddToCartDispatchProps {}
