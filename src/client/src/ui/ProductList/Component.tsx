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

import { ProductItem } from './ProductItem';
import { ProductListHeader } from './ProductListHeader';
import { ProductListOwnState, ProductListProps } from './models';

import './styles.scss';
import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

export default class ProductListComponent extends JSS.SafePureComponent<ProductListProps, ProductListOwnState> {
  public constructor(props: ProductListProps) {
    super(props);

    this.state = {
      firstLoad: true,
    };
  }

  public componentWillMount() {
    this.initSearch();
  }

  public componentDidUpdate(prevProps: ProductListProps) {
    this.initSearch(prevProps.categoryId, prevProps.search);
    if (!this.props.isLoading) {
      this.setState({ firstLoad: false });
    }
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
      sortingDirection,
      sortingField,
      ChangeSorting,
    } = this.props;
    const { firstLoad } = this.state;
    const showLoadMore = totalPageCount !== 0 && currentPageNumber !== totalPageCount - 1;

    return (
      <section className="listing-product-grid">
        {items.length === 0 && (
          <div className="no-results-found">
            <Placeholder name="no-results-found" rendering={this.props.rendering} />
          </div>
        )}
        {items.length !== 0 && !isLoading && (
          <ProductListHeader
            currentPageNumber={currentPageNumber}
            search={search}
            isLoading={isLoading}
            DiscardFacet={DiscardFacet}
            itemsCount={totalItemCount}
            sortingDirection={sortingDirection}
            sortingField={sortingField}
            ChangeSorting={ChangeSorting}
          />
        )}
        <ul>
          {items.length !== 0 &&
            items.map((product) => (
              <li key={product.productId}>
                <ProductItem
                  product={product}
                  productColors={sitecoreContext.productColors}
                  fallbackImageUrl={sitecoreContext.fallbackImageUrl}
                />
                <span className="triangle" />
              </li>
            ))}
        </ul>
        <div className={classnames({ 'is-loading': isLoading, 'show-load-btn': !isLoading && showLoadMore })}>
          <div className={`lazyLoad_spinner spinner ${isLoading && !firstLoad ? 'lazyLoad_spinner_display' : ''}`}>
            <div className="object object-one" />
            <div className="object object-two" />
            <div className="object object-three" />
          </div>
          <div className={`lazyLoad_loadMore ${isLoading ? 'lazyLoad_loadMore_hidden' : ''}`}>
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
    const { InitSearch, categoryId, search, sortingDirection, sortingField } = this.props;

    if ((categoryId && prevCategoryId !== categoryId) || prevSearch !== search) {
      InitSearch({ categoryId, search, sortingDirection, sortingField });
    }
  }
}
