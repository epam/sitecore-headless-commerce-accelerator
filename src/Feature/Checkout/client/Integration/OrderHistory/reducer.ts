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

import { Action, LoadingStatus } from 'Foundation/Integration/client';

import { actionTypes } from './actionTypes';
import { CurrentOrderHistoryState } from './models';

export const initialState: CurrentOrderHistoryState = {
  currentPageNumber: 0,
  isLastPage: false,
  status: LoadingStatus.NotLoaded
};

export default (state: CurrentOrderHistoryState = initialState, action: Action) => {
  switch (action.type) {
    case actionTypes.ORDER_HISTORY_LOAD_MORE:
    case actionTypes.GET_ORDER_HISTORY:
    case actionTypes.GET_ORDER_HISTORY_REQUEST:
    case actionTypes.GET_ORDER_HISTORY_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};
