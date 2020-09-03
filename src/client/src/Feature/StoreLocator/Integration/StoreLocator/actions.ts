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

import { Action, FailureType, LoadingStatus, StatusType } from 'Foundation/Integration';
import { actionTypes } from './constants';

import { Store } from 'Feature/StoreLocator/models';

export const GetStores = (): Action<any> => ({
  payload: {},
  type: actionTypes.GET_STORES_STORE_LOCATOR,
});

export const GetStoresRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.GET_STORES_STORE_LOCATOR_REQUEST,
});

export const GetStoresFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.GET_STORES_STORE_LOCATOR_FAILURE,
});

export const GetStoresSuccess = (stores: Store[]): Action<any> => ({
  payload: {
    status: LoadingStatus.Loaded,
    stores,
  },
  type: actionTypes.GET_STORES_STORE_LOCATOR_SUCCESS,
});

export const FindStores = (zipCode: string, countryCode: string, radius: number): Action<any> => ({
  payload: {
    countryCode,
    radius,
    zipCode,
  },
  type: actionTypes.FIND_STORES_STORE_LOCATOR,
});

export const FindStoresRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: actionTypes.FIND_STORES_STORE_LOCATOR_REQUEST,
});

export const FindStoresFailure: FailureType = (error: string, stack?: string) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.FIND_STORES_STORE_LOCATOR_FAILURE,
});

export const FindStoresSuccess = (stores: Store[]): Action<any> => ({
  payload: {
    status: LoadingStatus.Loaded,
    stores,
  },
  type: actionTypes.FIND_STORES_STORE_LOCATOR_SUCCESS,
});
