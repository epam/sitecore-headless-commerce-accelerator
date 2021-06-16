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
import { withRouter } from 'react-router';
import { compose } from 'recompose';
import { bindActionCreators } from 'redux';

import { LoadingStatus } from 'models';
import { rendering } from 'Foundation/ReactJss';
import * as ProductSearch from 'services/search';

import ProductFiltersComponent from './Component';
import { AppState, ProductFiltersDispatchProps, ProductFiltersOwnProps, ProductFiltersStateProps } from './models';

const mapStateToProps = (state: AppState, ownProps: ProductFiltersOwnProps) => {
  const { search } = ownProps.history.location;
  const facets = ProductSearch.productSearchFacets(state) || [];
  const items = ProductSearch.productSearchItems(state);
  const status = ProductSearch.productsSearchStatus(state);
  const totalItemCount = ProductSearch.productsSearch(state).totalItemCount;
  return {
    facets,
    isLoading: status === LoadingStatus.Loading,
    items,
    search,
    totalItemCount,
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ApplyFacet: ProductSearch.ApplyFacet,
      DiscardFacet: ProductSearch.DiscardFacet,
    },
    dispatch,
  );

const connectedToStore = connect<ProductFiltersStateProps, ProductFiltersDispatchProps, ProductFiltersOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const ProductFilters = compose(withRouter, rendering, connectedToStore)(ProductFiltersComponent);
