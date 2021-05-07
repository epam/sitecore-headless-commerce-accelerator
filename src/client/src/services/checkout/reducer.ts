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

import { combineReducers } from 'redux';

import { Action, LoadingStatus } from 'Foundation/Integration';

import { reducerActionTypes } from './constants';
import { CheckoutData, CheckoutState, CheckoutStepType, CurrentStep, StepValues } from './models';

const currentStepInitialState: CurrentStep = {
  status: LoadingStatus.NotLoaded,
  type: CheckoutStepType.Initial,
};

const currentStepReducer = (state: CurrentStep = currentStepInitialState, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.SUBMIT_STEP_FAILURE:
    case reducerActionTypes.SUBMIT_STEP_REQUEST:
    case reducerActionTypes.SUBMIT_STEP_SUCCESS:
    case reducerActionTypes.SET_CURRENT_STEP: {
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

const dataInitialState: CheckoutData = {
  billing: {
    status: LoadingStatus.NotLoaded,
  },
  delivery: {
    status: LoadingStatus.NotLoaded,
  },
  shipping: {
    status: LoadingStatus.NotLoaded,
  },
};

const dataReducer = (state: CheckoutData = dataInitialState, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.RESET_DELIVERY_INFO:
    case reducerActionTypes.GET_CHECKOUT_DATA_FAILURE:
    case reducerActionTypes.GET_CHECKOUT_DATA_REQUEST:
    case reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS: {
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

export const stepValuesInitialState: StepValues = {};

const stepValuesReducer = (state: StepValues = stepValuesInitialState, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.SET_STEP_VALUES: {
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

export default combineReducers<CheckoutState>({
  currentStep: currentStepReducer,
  data: dataReducer,
  stepValues: stepValuesReducer,
});
