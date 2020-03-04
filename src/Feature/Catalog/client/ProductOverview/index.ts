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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { ProductVariant } from 'Feature/Catalog/client/Integration';
import ProductOverviewComponent from './Component';

import { AppState, ProductOverviewDispatchProps, ProductOverviewOwnProps, ProductOverviewStateProps } from './models';

const mapStateToProps = (state: AppState): ProductOverviewStateProps => {
  const productId = ProductVariant.productId(state);
  const selectedVariant = ProductVariant.selectedProductVariant(state, productId);
  return {
    selectedVariant,
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({}, dispatch);
};

const connectedToStore = connect<ProductOverviewStateProps, ProductOverviewDispatchProps, ProductOverviewOwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(ProductOverviewComponent);

export const ProductOverview = withExperienceEditorChromes(connectedToStore);
