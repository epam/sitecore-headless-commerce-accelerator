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

import {
  productsSearchSuggestionsProducts,
  productsSearchSuggestionsStatus,
  ResetSuggestionsState,
} from 'services/search';

import { AppState, SuggestionListDispatchProps, SuggestionListOwnProps, SuggestionListStateProps } from './models';
import { SuggestionListComponent } from './SuggestionList';

const mapStateToProps = (state: AppState) => {
  const products = productsSearchSuggestionsProducts(state);
  const status = productsSearchSuggestionsStatus(state);

  return {
    products,
    status,
  };
};

const mapDispatchToProps = {
  resetState: ResetSuggestionsState,
};

const connectedToStore = connect<SuggestionListStateProps, SuggestionListDispatchProps, SuggestionListOwnProps>(
  mapStateToProps,
  mapDispatchToProps,
);

export const SuggestionList = connectedToStore(SuggestionListComponent);
