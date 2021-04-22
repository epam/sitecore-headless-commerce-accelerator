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

import * as JSS from 'Foundation/ReactJss';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import * as ProductsSearchSuggestions from 'Feature/Catalog/Integration/ProductsSearchSuggestions';
import * as Context from 'Foundation/ReactJss/SitecoreContext';

import { closeHamburgerMenu } from '../NavigationMenu/Integration';
import { NavigationSearchComponent } from './Component';
import { AppState } from './models';

const mapStateToProps = (state: AppState) => {
  const searchSuggestionsStatus = ProductsSearchSuggestions.productsSearchSuggestionsStatus(state);

  return {
    searchSuggestionsStatus,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      ChangeRoute: Context.ChangeRoute,
      closeHamburgerMenu,
      requestSuggestions: ProductsSearchSuggestions.RequestSuggestions,
      resetSuggestionsState: ProductsSearchSuggestions.ResetState,
    },
    dispatch,
  );

export const NavigationSearch = compose(
  JSS.rendering,
  connect(mapStateToProps, mapDispatchToProps),
)(NavigationSearchComponent);
