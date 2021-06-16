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

import { LoadingStatus } from 'models';
import { renderingWithContext } from 'Foundation/ReactJss';
import * as Search from 'services/search';

import ProductShopComponent from './Component';
import { AppState, ProductShopDispatchProps, ProductShopOwnProps, ProductShopStateProps } from './models';

const mapStateToProps = (state: AppState) => {
  const status = Search.productsSearchStatus(state);
  return {
    isLoading: status === LoadingStatus.Loading,
  };
};

const connectedToStore = connect<ProductShopStateProps, ProductShopOwnProps, ProductShopDispatchProps>(mapStateToProps);

export const ProductShop = compose(withRouter, renderingWithContext, connectedToStore)(ProductShopComponent);
