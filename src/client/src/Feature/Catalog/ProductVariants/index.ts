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

import { renderingWithContext } from 'Foundation/ReactJss';

import * as ProductVariant from 'Feature/Catalog/Integration/ProductVariant';

import ProductVariantsComponent from './Component';
import { AppState, ProductVariantsDispatchProps, ProductVariantsOwnProps, ProductVariantsStateProps } from './models';

const mapStateToProps = (state: AppState): ProductVariantsStateProps => {
  const productId = ProductVariant.productId(state);
  const variants = ProductVariant.variants(state);
  return {
    productId,
    variants,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators(
    {
      SelectColorVariant: ProductVariant.SelectColorVariant,
    },
    dispatch,
  );
};

const connectedToStore = connect<ProductVariantsStateProps, ProductVariantsDispatchProps, ProductVariantsOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const ProductVariants = compose(connectedToStore, renderingWithContext)(ProductVariantsComponent);
