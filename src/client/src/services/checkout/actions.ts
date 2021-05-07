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

import * as Commerce from 'Foundation/Commerce';
import { LoadingStatus } from 'Foundation/Integration';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';
import * as Models from './models';

export const GetCheckoutData: actionCreators.GetCheckoutData = () => ({
  type: sagaActionTypes.GET_CHECKOUT_DATA,
});

export const InitStep: actionCreators.InitStep = (type: Models.CheckoutStepType) => ({
  payload: { type },
  type: sagaActionTypes.INIT_STEP,
});
export const SubmitStep: actionCreators.SubmitStep = (stepValues: Models.StepValuesPayload) => ({
  payload: {
    ...stepValues,
  },
  type: sagaActionTypes.SUBMIT_STEP,
});

export const GetDeliveryInfoRequest: actionCreators.GetCheckoutDataRequest = () => ({
  payload: {
    delivery: {
      status: LoadingStatus.Loading,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST,
});

export const GetShippingInfoRequest: actionCreators.GetCheckoutDataRequest = () => ({
  payload: {
    shipping: {
      status: LoadingStatus.Loading,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST,
});

export const GetBillingInfoRequest: actionCreators.GetCheckoutDataRequest = () => ({
  payload: {
    billing: {
      status: LoadingStatus.Loading,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST,
});

export const GetDeliveryInfoFailure: actionCreators.GetCheckoutDataFailure = (error: string, stack?: string) => ({
  payload: {
    delivery: {
      error,
      stack,
      status: LoadingStatus.Failure,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE,
});

export const GetShippingInfoFailure: actionCreators.GetCheckoutDataFailure = (error: string, stack?: string) => ({
  payload: {
    shipping: {
      error,
      stack,
      status: LoadingStatus.Failure,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE,
});

export const GetBillingInfoFailure: actionCreators.GetCheckoutDataFailure = (error: string, stack?: string) => ({
  payload: {
    billing: {
      error,
      stack,
      status: LoadingStatus.Failure,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE,
});

export const GetDeliveryInfoSuccess: actionCreators.GetCheckoutDataSuccess = (data: Commerce.DeliveryInfo) => ({
  payload: {
    delivery: {
      data,
      status: LoadingStatus.Loaded,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS,
});

export const GetShippingInfoSuccess: actionCreators.GetCheckoutDataSuccess = (data: Commerce.ShippingInfo) => ({
  payload: {
    shipping: {
      data,
      status: LoadingStatus.Loaded,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS,
});

export const GetBillingInfoSuccess: actionCreators.GetCheckoutDataSuccess = (data: Commerce.BillingInfo) => ({
  payload: {
    billing: {
      data,
      status: LoadingStatus.Loaded,
    },
  },
  type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS,
});

export const SetStepValues: actionCreators.SetStepValues = (stepValues: Models.StepValuesPayload) => ({
  payload: {
    ...stepValues,
  },
  type: reducerActionTypes.SET_STEP_VALUES,
});

export const SetCurrentStep: actionCreators.SetCurrentStep = (payload: Models.CurrentStepPayload) => ({
  payload: { ...payload },
  type: reducerActionTypes.SET_CURRENT_STEP,
});

export const SubmitStepRequest: actionCreators.SubmitStepRequest = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.SUBMIT_STEP_REQUEST,
});

export const SubmitStepFailure: actionCreators.SubmitStepFailure = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.SUBMIT_STEP_FAILURE,
});

export const SubmitStepSuccess: actionCreators.SubmitStepSuccess = () => ({
  payload: {
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.SUBMIT_STEP_SUCCESS,
});

export const ResetDeliveryInfo: actionCreators.ResetDeliveryInfo = () => ({
  payload: {
    delivery: {
      status: LoadingStatus.NotLoaded,
    },
  },
  type: reducerActionTypes.RESET_DELIVERY_INFO,
});
