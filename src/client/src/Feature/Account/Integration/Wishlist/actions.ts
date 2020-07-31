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

import { Action, FailurePayload, LoadingStatus, Status, StatusPayload } from 'Foundation/Integration';
import { reducerTypes, sagaTypes } from './constants';

import { Variant } from 'Foundation/Commerce';

const Failure = (type: string) => (error: string, stack?: string): Action<FailurePayload> => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type,
});

const Request = (type: string) => (): Action<StatusPayload> => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type,
});

export const AddWishlistItem = (item: Variant): Action<Variant> => ({
  payload: item,
  type: sagaTypes.ADD_WISHLIST_ITEM,
});

export const GetWishlist = (): Action => ({
  type: sagaTypes.GET_WISHLIST,
});

export const RemoveWishlistItem = (id: string): Action<string> => ({
  payload: id,
  type: sagaTypes.REMOVE_WISHLIST_ITEM,
});

export const action = {
  AddWishlist: {
    Failure: Failure(reducerTypes.ADD_WISHLIST_ITEM_FAILURE),
    Request: Request(reducerTypes.ADD_WISHLIST_ITEM_REQUEST),
  },
  GetWishlist: {
    Failure: Failure(reducerTypes.GET_WISHLIST_FAILURE),
    Request: Request(reducerTypes.GET_WISHLIST_REQUEST),
  },
  RemoveWishlist: {
    Failure: Failure(reducerTypes.REMOVE_WISHLIST_ITEM_FAILURE),
    Request: Request(reducerTypes.REMOVE_WISHLIST_ITEM_REQUEST),
  },
  Wishlist: {
    Success: (items: Variant[]): Action<{ items: Variant[] } & Status> => ({
      payload: {
        items,
        status: LoadingStatus.Loaded,
      },
      type: reducerTypes.WISHLIST_SUCCESS,
    }),
  },
};
