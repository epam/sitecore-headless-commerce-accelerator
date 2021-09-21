//    Copyright 2021 EPAM Systems, Inc.
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
import { actionTypes, updateCartItemRequest, shoppingCart } from 'services/shoppingCart';
import { selector } from 'services/wishlist';

import { LoadingStatus } from 'models';
import { renderingWithContext } from 'Foundation/ReactJss';
import { ProductActionsComponent } from './ProductActions';

import { AppState, ProductActionsDispatchProps, ProductActionsOwnProps, ProductActionsStateProps } from './models';

const mapStateToProps = (state: AppState) => {
  const shoppingCartState = shoppingCart(state);
  const commerceUser = ProductVariant.commerceUser(state);
  const productId = ProductVariant.productId(state);
  const variants = ProductVariant.variants(state);
  const isLoading =
    shoppingCartState.status === LoadingStatus.Loading &&
    shoppingCartState.actionType === actionTypes.ADD_TO_CART_REQUEST;

  return {
    commerceUser,
    isLoading,
    productId,
    variants,
    wishlistStatus: selector.wishlistStatus(state),
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      updateCartItemRequest,
    },
    dispatch,
  );
};

const connectedToStore = connect<ProductActionsStateProps, ProductActionsDispatchProps, ProductActionsOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const ProductActions = compose(renderingWithContext, connectedToStore)(ProductActionsComponent);
