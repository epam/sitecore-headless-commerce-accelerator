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

import * as React from 'react';

import * as Jss from 'Foundation/ReactJss/client';

import { FACET_PARAMETER_NAME, KEYWORD_PARAMETER_NAME } from 'Feature/Catalog/client/Integration/ProductsSearch';
import { tryParseUrlSearch } from 'Foundation/Extensions/client';

import { ProductsSearchOwnState, ProductsSearchProps } from './models';
import './styles.scss';

export default class ProductsSearch extends Jss.SafePureComponent<ProductsSearchProps, ProductsSearchOwnState> {
  constructor(props: ProductsSearchProps) {
    super(props);

    this.state = {
      keyword: '',
      submitted: false,
    };
  }

  public componentWillMount() {
    this.setState({
      keyword: this.getKeywordFromSearch(),
    });
  }

  protected safeRender() {
    const { isLoading } = this.props;
    const { keyword } = this.state;
    const q = this.props.productSearchParams.q;
    return (
      <section className="products-search">
        <div className="search-wrap">
          <form onSubmit={(e) => this.handleFormSubmit(e)}>
            {isLoading
              ? (<input type="search" value={q !== this.state.keyword ? q : keyword} />)
              : (<input onChange={(e) => this.handleKeywordChange(e)} type="search" defaultValue={q} />)}
            <button type="button" onClick={(e) => this.handleFormClear(e)}>
              <i className="fa fa-times" />
            </button>
          </form>
        </div>
      </section>
    );
  }
  private getKeywordFromSearch() {
    const search = this.props.history.location.search;
    const parsedSearch = tryParseUrlSearch(search);

    return parsedSearch[KEYWORD_PARAMETER_NAME] || '';
  }

  private handleKeywordChange(e: React.ChangeEvent<HTMLInputElement>) {
    this.setState({
      keyword: e.target.value,
    });
  }

  private handleFormSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const { keyword } = this.state;
    this.startSearch(keyword);
  }

  private handleFormClear(e: React.MouseEvent<HTMLButtonElement>) {
    this.setState({ keyword: '', submitted: true });
    const { ChangeRoute, history } = this.props;
    ChangeRoute(history.location.pathname);
  }

  private startSearch(keyword: string) {
    const params = this.props.productSearchParams;
    const newSearchQuery = [];
    if (keyword) {
      newSearchQuery.push(`${KEYWORD_PARAMETER_NAME}=${keyword}`);
    }
    if (params.f) {
      newSearchQuery.push(`${FACET_PARAMETER_NAME}=${encodeURIComponent(params.f)}`);
    }
    const newSearchQueryString = newSearchQuery.length !== 0 ? `?${newSearchQuery.join('&')}` : '';
    const { ChangeRoute, history } = this.props;
    this.setState({ submitted: true });
    ChangeRoute(`${history.location.pathname}${newSearchQueryString}`);
  }
}
