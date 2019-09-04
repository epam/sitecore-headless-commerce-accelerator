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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { LoadingStatus } from 'Foundation/Integration/client';

import * as OrderHistoryModule from 'Feature/Checkout/client/Integration/OrderHistory';

import { OrderHistoryControl } from './Component';
import { AppState, OrderHistoryDispatchProps, OrderHistoryOwnProps, OrderHistoryStateProps } from './models';

const mapStateToProps = (state: AppState): OrderHistoryStateProps => {
  const status = OrderHistoryModule.orderHistoryStatus(state);
  const orders = OrderHistoryModule.orderHistoryList(state);
  const isLastPage = OrderHistoryModule.orderHistoryIsLastPage(state);

  return {
    isLastPage,
    isLoading: status === LoadingStatus.Loading,
    orders,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      GetOrderHistory: OrderHistoryModule.GetOrderHistory,
      LoadMore: OrderHistoryModule.OrderHistoryLoadMore
    },
    dispatch
  );
};

const connectedToStore = connect<OrderHistoryStateProps, OrderHistoryDispatchProps, OrderHistoryOwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(OrderHistoryControl);

export const OrderHistory = withExperienceEditorChromes(connectedToStore);
