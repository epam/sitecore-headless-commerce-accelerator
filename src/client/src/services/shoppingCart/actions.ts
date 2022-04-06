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

import { FailureType, LoadingStatus, StatusType } from 'models';

import * as DataModels from 'services/checkout/models/generated';

import * as actionCreators from './actionCreators';
import { actionTypes } from './actionTypes';
import { ShoppingCartData, ShoppingCartLine } from './models';

export const LoadCart: actionCreators.LoadCart = () => ({
  type: actionTypes.LOAD_CART,
});

export const GetCartRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.CART_GET_REQUEST,
});

export const GetCartFailure: FailureType = (error: string, stack?) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.CART_GET_FAILURE,
});

export const GetCartSuccess: actionCreators.GetCartSuccess = (data: ShoppingCartData) => ({
  payload: {
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.CART_GET_SUCCESS,
});

export const AddToCart: actionCreators.AddToCart = (model: DataModels.AddCartLineRequest) => ({
  payload: {
    ...model,
  },
  type: actionTypes.ADD_TO_CART,
});

export const AddToCartRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.ADD_TO_CART_REQUEST,
});

export const AddToCartFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.ADD_TO_CART_FAILURE,
});

export const AddToCartSuccess: actionCreators.AddToCartSuccess = (data: ShoppingCartData) => ({
  payload: {
    cartUpdated: true,
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.ADD_TO_CART_SUCCESS,
});

export const UpdateCartLine: actionCreators.UpdateCartLine = (model: DataModels.UpdateCartLineRequest) => ({
  payload: {
    ...model,
  },
  type: actionTypes.UPDATE_CART_LINE,
});

export const UpdateCartLineRequest: StatusType = () => ({
  type: actionTypes.UPDATE_CART_LINE_REQUEST,
});

export const UpdateCartLineFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.UPDATE_CART_LINE_FAILURE,
});

export const UpdateCartLineSuccess: actionCreators.UpdateCartLineSuccess = (data: ShoppingCartData) => ({
  payload: {
    cartUpdated: true,
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.UPDATE_CART_LINE_SUCCESS,
});

export const RemoveCartLine: actionCreators.RemoveCartLine = (model: ShoppingCartLine) => ({
  payload: {
    name: model.product.displayName,
    productId: model.product.productId,
    quantity: model.quantity,
    variantId: model.variant.variantId,
    variantName: model.variant.displayName,
  },
  type: actionTypes.REMOVE_CART_LINE,
});

export const RemoveCartLineRequest: StatusType = () => ({
  type: actionTypes.REMOVE_CART_LINE_REQUEST,
});

export const RemoveCartLineFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.REMOVE_CART_LINE_FAILURE,
});

export const RemoveCartLineSuccess: actionCreators.RemoveCartLineSuccess = (data: ShoppingCartData) => ({
  payload: {
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.REMOVE_CART_LINE_SUCCESS,
});

export const CleanCart: actionCreators.CleanCart = () => ({
  type: actionTypes.CLEAN_CART,
});

export const CleanCartRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.CLEAN_CART_REQUEST,
});

export const CleanCartFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.CLEAN_CART_FAILURE,
});

export const CleanCartSuccess: actionCreators.CleanCartSuccess = (data: ShoppingCartData) => ({
  payload: {
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.CLEAN_CART_SUCCESS,
});
export const updateCartItemRequest: actionCreators.UpdateCartItemRequest = (payload) => ({
  payload,
  type: actionTypes.UPDATE_CART_ITEM_REQUEST,
});

export const updateCartItemSuccess: actionCreators.UpdateCartItemSuccess = (payload) => ({
  payload,
  type: actionTypes.UPDATE_CART_ITEM_SUCCESS,
});

export const updateCartItemFailure: actionCreators.UpdateCartItemFailure = (payload) => ({
  payload,
  type: actionTypes.UPDATE_CART_ITEM_FAILURE,
});
