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

import * as JSS from 'Foundation/ReactJss';
import { GlobalWishlistState } from '../Integration/Wishlist';

import { Variant } from 'Foundation/Commerce';

export interface WishlistDataSource extends JSS.BaseDataSourceItem {
  title: JSS.GraphQLField<JSS.TextField>;
}

export interface WishlistStoreProps extends JSS.SafePureComponentState {
  items: Variant[];
}

export interface WishlistDispatchProps {
  GetWishlist: () => void;
}

export interface WishlistState extends JSS.SafePureComponentState {}
export interface WishlistProps
  extends JSS.GraphQLRendering<WishlistDataSource>,
    WishlistDispatchProps,
    WishlistStoreProps {}

export interface AppState extends GlobalWishlistState {}
