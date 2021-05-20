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

import * as ProductVariant from 'services/productVariant';
import { actionTypes, AddToCart, shoppingCart } from 'services/shoppingCart';

import { LoadingStatus } from 'Foundation/Integration';
import { renderingWithContext } from 'Foundation/ReactJss';
import { ProductActionsComponent } from './ProductActions';

import { AppState, ProductActionsDispatchProps, ProductActionsOwnProps, ProductActionsStateProps } from './models';

const mapStateToProps = (state: AppState) => {
  const shoppingCartState = shoppingCart(state);
  const commerceUser = ProductVariant.commerceUser(state);
  const productId = ProductVariant.productId(state);
  const variant = ProductVariant.selectedProductVariant(state, productId);
  const isLoading = shoppingCartState.status === LoadingStatus.Loading &&
    shoppingCartState.actionType === actionTypes.ADD_TO_CART_REQUEST;

  return {
    commerceUser,
    isLoading,
    productId,
    variant,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      AddToCart,
    },
    dispatch
  );
};

const connectedToStore = connect<ProductActionsStateProps, ProductActionsDispatchProps, ProductActionsOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const ProductActions = compose(renderingWithContext, connectedToStore)(ProductActionsComponent);
