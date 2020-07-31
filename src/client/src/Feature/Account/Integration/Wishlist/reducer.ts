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

import { Action, LoadingStatus } from 'Foundation/Integration';

import { reducerTypes } from './constants';
import { WishlistState } from './models';

export const initialWishlistState: WishlistState = {
  items: [],
  status: LoadingStatus.NotLoaded,
};

export default (state: WishlistState = { ...initialWishlistState }, action: Action) => {
  switch (action.type) {
    case reducerTypes.ADD_WISHLIST_ITEM_FAILURE:
    case reducerTypes.ADD_WISHLIST_ITEM_REQUEST:
    case reducerTypes.REMOVE_WISHLIST_ITEM_FAILURE:
    case reducerTypes.REMOVE_WISHLIST_ITEM_REQUEST:
    case reducerTypes.GET_WISHLIST_FAILURE:
    case reducerTypes.GET_WISHLIST_REQUEST:
    case reducerTypes.WISHLIST_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default: {
      return state;
    }
  }
};
