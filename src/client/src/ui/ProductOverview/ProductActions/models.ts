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

import { User, Variant } from 'services/commerce';
import * as Jss from 'Foundation/ReactJss';

import { ProductVariantGlobalState } from 'services/productVariant';
import { CartItemDto, GlobalShoppingCartState } from 'services/shoppingCart';
import { GlobalWishlistState } from 'services/wishlist';

import { LoadingStatus } from 'models';
import { ProductOverviewContext } from '../models';

export interface ProductActionsOwnProps
  extends Jss.RenderingWithContext<Jss.BaseDataSourceItem, ProductOverviewContext> {}
export interface ProductActionsStateProps {
  isLoading?: boolean;
  productId?: string;
  variants?: Variant[];
  wishlistStatus: LoadingStatus;
  commerceUser: User;
}
export interface ProductActionsDispatchProps {
  updateCartItemRequest: (model: CartItemDto) => void;
}

export interface AppState extends GlobalShoppingCartState, ProductVariantGlobalState, GlobalWishlistState {}

export interface ProductActionsProps
  extends ProductActionsOwnProps,
    ProductActionsStateProps,
    ProductActionsDispatchProps {}
