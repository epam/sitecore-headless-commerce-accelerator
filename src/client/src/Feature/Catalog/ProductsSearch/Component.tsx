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

import React, { ChangeEvent } from 'react';

import * as Jss from 'Foundation/ReactJss';

import { FACET_PARAMETER_NAME, KEYWORD_PARAMETER_NAME } from 'Feature/Catalog/Integration/ProductsSearch';
import { tryParseUrlSearch } from 'Foundation/Base';

import { SidebarSearch } from 'components';

import { ProductsSearchOwnState, ProductsSearchProps } from './models';

export default class ProductsSearch extends Jss.SafePureComponent<ProductsSearchProps, ProductsSearchOwnState> {
  public static getDerivedStateFromProps(
    props: ProductsSearchProps,
    state: ProductsSearchOwnState,
  ): ProductsSearchOwnState {
    const q = props.productSearchParams.q || '';
    if (state.initialKeyword !== q) {
      return {
        initialKeyword: q,
        keyword: q,
        submitted: true,
      };
    }

    return state;
  }

  public componentDidMount() {
    const { search } = this.props.history.location;

    if (!search) {
      this.startSearch();
    }
  }

  constructor(props: ProductsSearchProps) {
    super(props);

    this.state = {
      initialKeyword: props.productSearchParams.q,
      keyword: this.getKeywordFromSearch(),
      submitted: false,
    };

    this.handleFormClear = this.handleFormClear.bind(this);
    this.handleFormSubmit = this.handleFormSubmit.bind(this);
  }

  protected safeRender() {
    const { keyword } = this.state;
    const { isLoading } = this.props;

    return (
      <SidebarSearch
        disabled={isLoading}
        value={keyword}
        onChange={this.handleKeywordChange}
        onClear={this.handleFormClear}
        onSubmit={this.handleFormSubmit}
      />
    );
  }

  private getKeywordFromSearch() {
    const search = this.props.history.location.search;
    const parsedSearch = tryParseUrlSearch(search);

    return parsedSearch[KEYWORD_PARAMETER_NAME] || '';
  }

  private handleKeywordChange = (e: ChangeEvent<HTMLInputElement>) => {
    this.setState({
      keyword: e.target.value,
      submitted: false,
    });
  };

  private handleFormSubmit() {
    const { submitted } = this.state;

    if (submitted) {
      return;
    }

    this.startSearch();
  }

  private handleFormClear() {
    this.setState({ keyword: '', submitted: true });
    const { ChangeRoute, history } = this.props;
    const params = new URLSearchParams(history.location.search);
    params.delete('q');
    const encodeParams = encodeURIComponent(params.toString());
    const newUrl = `${history.location.pathname}?${encodeParams}`;
    ChangeRoute(newUrl);
  }

  private startSearch() {
    const { keyword } = this.state;
    const params = this.props.productSearchParams;
    const newSearchQuery = [];

    newSearchQuery.push(`${KEYWORD_PARAMETER_NAME}=${keyword}`);

    if (params.f) {
      newSearchQuery.push(`${FACET_PARAMETER_NAME}=${encodeURIComponent(params.f)}`);
    }
    const newSearchQueryString = newSearchQuery.length !== 0 ? `?${newSearchQuery.join('&')}` : '';
    const { ChangeRoute, history } = this.props;
    this.setState({ submitted: true });
    ChangeRoute(`${history.location.pathname}${newSearchQueryString}`);
  }
}
