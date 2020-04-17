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

import { Sitecore } from '../models';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';

export const InitializationComplete: actionCreators.InitializationComplete = () => ({
  type: sagaActionTypes.INITIALIZATION_COMPLETE,
});

export const ChangeRoute: actionCreators.ChangeRoute = (newRoute: string, shouldPushNewRoute: boolean = true) => ({
  payload: { newRoute, shouldPushNewRoute },
  type: sagaActionTypes.CHANGE_ROUTE,
});

export const SetLoadedUrl: actionCreators.SetLoadedUrl = (loadedUrl: string) => ({
  payload: {
    loadedUrl,
  },
  type: reducerActionTypes.SET_LOADED_URL,
});

export const GetSitecoreContextRequest: StatusType = () => ({
  payload: {
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.GET_SITECORE_CONTEXT_REQUEST,
});
export const GetSitecoreContextFailure: FailureType = (error: string, stack?) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.GET_SITECORE_CONTEXT_FAILURE,
});

export const GetSitecoreContextSuccess: actionCreators.GetSitecoreContextSuccess = (sitecore: Sitecore<{}, {}>) => ({
  payload: {
    ...sitecore,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.GET_SITECORE_CONTEXT_SUCCESS,
});
