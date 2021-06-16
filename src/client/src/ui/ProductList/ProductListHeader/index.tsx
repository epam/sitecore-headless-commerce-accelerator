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

import * as React from 'react';

import { tryParseUrlSearch } from 'utils';
import * as Jss from 'Foundation/ReactJss';
import { FACET_PARAMETER_NAME, facetsManager, PRODUCTS_PER_PAGE } from 'services/search';

import { Icon } from 'components';

import { ProductListSorting } from '../ProductListSorting';
import { ProductListHeaderProps, ProductListHeaderState } from './models';

import './styles.scss';

export class ProductListHeader extends Jss.SafePureComponent<ProductListHeaderProps, ProductListHeaderState> {
  constructor(props: ProductListHeaderProps) {
    super(props);

    this.state = {
      numberOfDisplayedItems: 0,
    };
  }
  public componentDidUpdate() {
    const { currentPageNumber, itemsCount } = this.props;
    let count = 0;
    if (itemsCount < PRODUCTS_PER_PAGE) {
      this.setState({ numberOfDisplayedItems: itemsCount });
    } else {
      count = (currentPageNumber + 1) * PRODUCTS_PER_PAGE;
      count > itemsCount
        ? this.setState({ numberOfDisplayedItems: itemsCount })
        : this.setState({ numberOfDisplayedItems: count });
    }
  }

  public safeRender() {
    const { search, itemsCount, isLoading, sortingDirection, sortingField, ChangeSorting } = this.props;
    const { numberOfDisplayedItems } = this.state;
    const parsedSearch = tryParseUrlSearch(search);
    const appliedFacets = facetsManager(parsedSearch[FACET_PARAMETER_NAME]).getFacets();
    return (
      <div className="product_list_header">
        <div className="header">
          {!isLoading && (
            <div className="header_panel">
              <ProductListSorting ChangeSorting={ChangeSorting} selectedOption={{ sortingDirection, sortingField }} />
              <div className="header_stats">
                <span>
                  Showing {numberOfDisplayedItems} of {itemsCount} results
                </span>
              </div>
            </div>
          )}
        </div>
        <div className="filter-labels">
          {!isLoading &&
            Object.keys(appliedFacets).map((facetName) => {
              const facet = appliedFacets[facetName];
              return facet.map((value, valueIndex) => (
                <div
                  className="filter-label"
                  key={valueIndex}
                  onClick={(e) => this.handleDiscardFacetClick(facetName, value, e)}
                >
                  <Icon icon="icon-close" />
                  <span className="filter-label_text">{value}</span>
                </div>
              ));
            })}
        </div>
      </div>
    );
  }

  private handleDiscardFacetClick(name: string, value: string, e: React.MouseEvent<HTMLDivElement>) {
    const { isLoading, search, DiscardFacet } = this.props;
    if (!isLoading) {
      DiscardFacet(name, value, search);
    }
  }
}
