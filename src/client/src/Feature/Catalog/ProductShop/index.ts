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

import ProductShopComponent from './Component';

import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { compose } from 'recompose';
import { AppState, ProductShopDispatchProps, ProductShopOwnProps, ProductShopStateProps } from './models';

import * as ProductSearch from 'Feature/Catalog/Integration/ProductsSearch';
import { LoadingStatus } from 'Foundation/Integration';
import { renderingWithContext } from 'Foundation/ReactJss';

const mapStateToProps = (state: AppState) => {
  const status = ProductSearch.productsSearchStatus(state);
  return {
    isLoading: status === LoadingStatus.Loading,
  };
};

const connectedToStore = connect<ProductShopStateProps, ProductShopOwnProps, ProductShopDispatchProps>(mapStateToProps);

export const ProductShop = compose(withRouter, renderingWithContext, connectedToStore)(ProductShopComponent);
