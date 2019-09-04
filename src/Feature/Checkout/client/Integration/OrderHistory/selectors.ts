//    Copyright 2019 EPAM Systems, Inc.
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

import { GlobalCurrentOrderHistoryState } from './models';

const getOrderHistoryContext = (state: GlobalCurrentOrderHistoryState) => state.orderHistory;

export const orderHistoryIsLastPage = (state: GlobalCurrentOrderHistoryState) => getOrderHistoryContext(state).isLastPage;
export const orderHistoryCurrentPageNumber = (state: GlobalCurrentOrderHistoryState) => getOrderHistoryContext(state).currentPageNumber;
export const orderHistoryList = (state: GlobalCurrentOrderHistoryState) => getOrderHistoryContext(state).orders;
export const orderHistoryStatus = (state: GlobalCurrentOrderHistoryState) => getOrderHistoryContext(state).status;
