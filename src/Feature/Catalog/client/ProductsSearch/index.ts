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
import { rendering } from 'Foundation/ReactJss/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import * as ProductSearch from 'Feature/Catalog/client/Integration/ProductsSearch';

import ProductsSearchComponent from './Component';
import { AppState, ProductsSearchDispatchProps, ProductsSearchOwnProps, ProductsSearchStateProps } from './models';

const mapStateToProps = (state: AppState, ownProps: ProductsSearchOwnProps) => {
  const productSearchParams = ProductSearch.productSearchParams(state);
  const status = ProductSearch.productsSearchStatus(state);
  const isLoading = status === LoadingStatus.Loading;

  return {
    isLoading,
    productSearchParams
  };
};

const mapDispatchToProps = (dispatch: any) =>
  bindActionCreators(
    {
      ChangeRoute,
    },
    dispatch
  );

const connectedToStore = connect<ProductsSearchStateProps, ProductsSearchDispatchProps, ProductsSearchOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const ProductsSearch = compose(withRouter, rendering, connectedToStore)(ProductsSearchComponent);
