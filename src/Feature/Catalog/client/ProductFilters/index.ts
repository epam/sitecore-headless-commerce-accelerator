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

import * as ProductSearch from 'Feature/Catalog/client/Integration/ProductsSearch';
import { LoadingStatus } from 'Foundation/Integration/client';

import ProductFiltersComponent from './Component';
import { AppState, ProductFiltersDispatchProps, ProductFiltersOwnProps, ProductFiltersStateProps } from './models';

const mapStateToProps = (state: AppState, ownProps: ProductFiltersOwnProps) => {
  const { search } = ownProps.history.location;
  const facets = ProductSearch.productSearchFacets(state) || [];
  const status = ProductSearch.productsSearchStatus(state);
  return {
    facets,
    isLoading: status === LoadingStatus.Loading,
    search,
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ApplyFacet: ProductSearch.ApplyFacet,
      DiscardFacet: ProductSearch.DiscardFacet,
    },
    dispatch
  );

const connectedToStore = connect<ProductFiltersStateProps, ProductFiltersDispatchProps, ProductFiltersOwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(ProductFiltersComponent);

export const ProductFilters = withExperienceEditorChromes(connectedToStore);
