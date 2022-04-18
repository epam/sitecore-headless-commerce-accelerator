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

import { Location } from 'history';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators } from 'redux';

import { tryParseUrlSearch } from 'utils';
import { renderingWithContext } from 'Foundation/ReactJss';

import { currentOrder, GetOrder } from 'services/order';

import { OrderConfirmationComponent } from './Component';
import {
  AppState,
  OrderConfirmationDispatchProps,
  OrderConfirmationOwnProps,
  OrderConfirmationStateProps,
} from './models';

const mapStateToProps = (state: AppState): OrderConfirmationStateProps => {
  const location: Location = state.router.location;
  const trackingNumber = tryParseUrlSearch(location.search).trackingNumber;

  const order = currentOrder(state);
  return {
    currentOrder: order,
    trackingNumber,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      GetOrder,
    },
    dispatch,
  );
};

const connectedToStore = connect<
  OrderConfirmationStateProps,
  OrderConfirmationDispatchProps,
  OrderConfirmationOwnProps
>(mapStateToProps, mapDispatchToProps);

export const OrderConfirmation = compose(connectedToStore, renderingWithContext)(OrderConfirmationComponent);
