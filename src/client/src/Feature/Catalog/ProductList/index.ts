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
import Cookies from 'universal-cookie';

import * as commonSelectors from 'Feature/Catalog/Integration/common/selectors';
import { LoadingStatus } from 'Foundation/Integration';
import { renderingWithContext } from 'Foundation/ReactJss';
import * as Search from 'services/search';

import ProductListComponent from './Component';
import { AppState, ProductListDispatchProps, ProductListOwnProps, ProductListStateProps } from './models';

const cookies = new Cookies();

const mapStateToProps = (state: AppState, ownProps: ProductListOwnProps) => {
  const search = ownProps.history.location.search;
  const category = commonSelectors.category(state);
  const categoryId = category ? category.sitecoreId : '';
  const items = Search.productSearchItems(state) || [];
  const status = Search.productsSearchStatus(state);
  const currentPageNumber = Search.productSearchCurrentPageNumber(state);
  const totalPageCount = Search.productSearchTotalPageCount(state);
  const totalItemCount = Search.productsSearch(state).totalItemCount;
  const itemsPerPage = Number(Search.productSearchParams(state).ps) || 12;
  const sortingDirection =
    Search.productSearchParams(state).sd || cookies.get('sortDirection') ? cookies.get('sortDirection') : '0';
  const sortingField =
    Search.productSearchParams(state).s || cookies.get('sortField') ? cookies.get('sortField') : 'brand';

  return {
    categoryId,
    currentPageNumber,
    isLoading: status === LoadingStatus.Loading,
    items,
    itemsPerPage,
    search,
    sortingDirection,
    sortingField,
    totalItemCount,
    totalPageCount,
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ChangeSorting: Search.ChangeSortingType,
      ClearSearch: Search.ClearSearch,
      DiscardFacet: Search.DiscardFacet,
      InitSearch: Search.InitialSearch,
      LoadMore: Search.LoadMore,
    },
    dispatch,
  );

const connectedToStore = connect<ProductListStateProps, ProductListDispatchProps, ProductListOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const ProductList = compose(withRouter, renderingWithContext, connectedToStore)(ProductListComponent);
