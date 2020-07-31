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
import { bindActionCreators, Dispatch } from 'redux';

import * as JSS from 'Foundation/ReactJss';

import * as ProductVariant from 'Feature/Catalog/Integration/ProductVariant';
import { AddWishlistItem } from '../Integration/Wishlist';

import { AddToWishlistComponent } from './Component';
import { AddToWishlistDispatchProps, AddToWishlistStoreProps, AppState } from './models';

const mapStateToProps = (state: AppState): AddToWishlistStoreProps => {
  const product = JSS.sitecoreContext(state).product;
  return {
    item: ProductVariant.selectedProductVariant(state, product.productId),
  };
};

const mapDispatchToProps = (dispatch: Dispatch): AddToWishlistDispatchProps => {
  return bindActionCreators(
    {
      AddWishlistItem,
    },
    dispatch,
  );
};

export const AddToWishlist = compose(
  JSS.rendering,
  connect(mapStateToProps, mapDispatchToProps),
)(AddToWishlistComponent);
