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

import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators } from 'redux';

import { LoadingStatus } from 'models';
import { renderingWithContext } from 'Foundation/ReactJss';

import * as OrderModule from 'services/order';

import { OrderHistoryWrapper } from './OrderHistoryWrapper';
import { AppState, OrderHistoryDispatchProps, OrderHistoryOwnProps, OrderHistoryStateProps } from './models';

const mapStateToProps = (state: AppState): OrderHistoryStateProps => {
  const status = OrderModule.orderHistoryStatus(state);
  const orders = OrderModule.orderHistoryList(state);
  const isLastPage = OrderModule.orderHistoryIsLastPage(state);

  return {
    isLastPage,
    loaded: status === LoadingStatus.Loaded,
    orders,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      GetOrderHistory: OrderModule.GetOrderHistory,
      LoadMore: OrderModule.OrderHistoryLoadMore,
    },
    dispatch,
  );
};

const connectedToStore = connect<OrderHistoryStateProps, OrderHistoryDispatchProps, OrderHistoryOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const OrderHistory = compose(connectedToStore, renderingWithContext)(OrderHistoryWrapper);
