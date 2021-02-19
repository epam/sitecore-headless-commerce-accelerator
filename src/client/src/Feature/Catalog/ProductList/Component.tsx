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

import classnames from 'classnames';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { ProductListHeader, ProductListItem } from './components';
import { ProductListProps } from './models';
import './styles.scss';

export default class ProductListComponent extends JSS.SafePureComponent<ProductListProps, {}> {
  public componentWillMount() {
    this.initSearch();
  }

  public componentDidUpdate(prevProps: ProductListProps) {
    this.initSearch(prevProps.categoryId, prevProps.search);
  }

  public safeRender() {
    const {
      currentPageNumber,
      totalPageCount,
      totalItemCount,
      search,
      items,
      isLoading,
      DiscardFacet,
      sitecoreContext,
    } = this.props;
    const showLoadMore = totalPageCount !== 0 && currentPageNumber !== totalPageCount - 1;
    return (
      <section className="listing-product-grid">
      {items.length !== 0 && !isLoading && (
        <ProductListHeader
          currentPageNumber={currentPageNumber}
          search={search}
          isLoading={isLoading}
          DiscardFacet={DiscardFacet}
          itemsCount={totalItemCount}
        />)}
        <ul>
          {isLoading && (
            <div className="loading-overlay">
              <div className="loading" />
            </div>
          )}
          {items.length === 0 && !isLoading && (
            <div className="not-found">
              <h3>No results found</h3>
              <h4>Search tips</h4>
              <ul>
                <li>Make sure that all words are spelled correctly</li>
                <li>Try more general keywords</li>
              </ul>
            </div>
          )}
          {items.map((product, index) => (
            <li key={index}>
              <ProductListItem product={product} fallbackImageUrl={sitecoreContext.fallbackImageUrl} />
              <span className="triangle" />
            </li>
          ))}
        </ul>
        <div className={classnames({ 'is-loading': isLoading, 'show-load-btn': !isLoading && showLoadMore })}>
          <div className="lazyLoad_spinner spinner">
            <div className="object object-one" />
            <div className="object object-two" />
            <div className="object object-three" />
          </div>
          <div className="lazyLoad_loadMore">
            {items.length !== 0 && showLoadMore && (
              <a className="btn-load-more" href="#" onClick={(e) => this.loadMoreHandler(e)}>
                Load More
              </a>
            )}
          </div>
        </div>
      </section>
    );
  }

  private loadMoreHandler(e: React.MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();
    this.props.LoadMore();
  }

  private initSearch(prevCategoryId: string = '', prevSearch: string = '') {
    const { InitSearch, categoryId, search } = this.props;

    if ((categoryId && prevCategoryId !== categoryId) || prevSearch !== search) {
      InitSearch({ categoryId, search });
    }
  }
}
