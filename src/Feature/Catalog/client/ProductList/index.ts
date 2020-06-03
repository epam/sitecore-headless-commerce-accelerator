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
import { withRouter } from 'react-router';
import { compose } from 'recompose';
import { bindActionCreators } from 'redux';

import { LoadingStatus } from 'Foundation/Integration/client';
import { renderingWithContext } from 'Foundation/ReactJss/client';

import * as commonSelectors from 'Feature/Catalog/client/Integration/common/selectors';
import * as ProductSearch from 'Feature/Catalog/client/Integration/ProductsSearch';

import ProductListComponent from './Component';
import { AppState, ProductListDispatchProps, ProductListOwnProps, ProductListStateProps } from './models';

const mapStateToProps = (state: AppState, ownProps: ProductListOwnProps) => {
  const search = ownProps.history.location.search;
  const category = commonSelectors.category(state);
  const categoryId = category ? category.sitecoreId : '';
  const items = ProductSearch.productSearchItems(state) || [];
  const status = ProductSearch.productsSearchStatus(state);
  const currentPageNumber = ProductSearch.productSearchCurrentPageNumber(state);
  const totalPageCount = ProductSearch.productSearchTotalPageCount(state);
  const totalItemCount = ProductSearch.productsSearch(state).totalItemCount;
  const itemsPerPage = Number(ProductSearch.productSearchParams(state).ps) || 12;

  return {
    categoryId,
    currentPageNumber,
    isLoading: status === LoadingStatus.Loading,
    items,
    itemsPerPage,
    search,
    totalItemCount,
    totalPageCount
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ClearSearch: ProductSearch.ClearSearch,
      DiscardFacet: ProductSearch.DiscardFacet,
      InitSearch: ProductSearch.InitialSearch,
      LoadMore: ProductSearch.LoadMore,
    },
    dispatch
  );

const connectedToStore = connect<ProductListStateProps, ProductListDispatchProps, ProductListOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const ProductList = compose(withRouter, renderingWithContext, connectedToStore)(ProductListComponent);
