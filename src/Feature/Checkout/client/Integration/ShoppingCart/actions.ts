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

import { FailureType, LoadingStatus, StatusType } from 'Foundation/Integration/client';

import * as DataModels from 'Feature/Checkout/client/dataModel.Generated';
import * as actionCreators from './actionCreators';
import { actionTypes } from './actionTypes';
import { ShoppingCartData } from './models';

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
  payload: {
    status: LoadingStatus.Loading,
  },
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

export const AddPromoCode: actionCreators.AddPromoCode = (model: DataModels.PromoCodeRequest) => ({
  payload: {
    ...model
  },
  type: actionTypes.ADD_PROMO_CODE,
});

export const AddPromoCodeRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.ADD_PROMO_CODE_REQUEST,
});

export const AddPromoCodeFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.ADD_PROMO_CODE_FAILURE,
});

export const AddPromoCodeSuccess: actionCreators.AddPromoCodeSuccess = (data: ShoppingCartData) => ({
  payload: {
    cartUpdated: true,
    data,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.ADD_PROMO_CODE_SUCCESS,
});
