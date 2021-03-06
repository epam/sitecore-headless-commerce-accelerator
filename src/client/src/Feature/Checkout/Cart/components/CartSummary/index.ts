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
import { bindActionCreators } from 'redux';

import { actionTypes, shoppingCart } from 'Feature/Checkout/Integration/ShoppingCart';
import * as actions from 'Feature/Checkout/Integration/ShoppingCart/actions';
import { LoadingStatus } from 'Foundation/Integration';
import { CartSummaryComponent } from './Component';

import { AppState, CartSummaryDispatchProps, CartSummaryOwnProps, CartSummaryStateProps } from './models';

const mapStateToProps = (state: AppState): CartSummaryStateProps => {
  const shoppingCartState = shoppingCart(state);
  const isLoading =
    shoppingCartState.status === LoadingStatus.Loading &&
    (shoppingCartState.actionType === actionTypes.UPDATE_CART_LINE_REQUEST ||
      shoppingCartState.actionType === actionTypes.REMOVE_CART_LINE_REQUEST);
  return { isLoading };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      RemoveCartLine: actions.RemoveCartLine,
      UpdateCartLine: actions.UpdateCartLine,
    },
    dispatch,
  );
};

const connectedToStore = connect<CartSummaryStateProps, CartSummaryDispatchProps, CartSummaryOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const CartSummary = connectedToStore(CartSummaryComponent);
