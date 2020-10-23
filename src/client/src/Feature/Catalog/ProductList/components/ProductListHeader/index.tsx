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

import * as Jss from 'Foundation/ReactJss';
import * as React from 'react';

import { tryParseUrlSearch } from 'Foundation/Base';

import { FACET_PARAMETER_NAME, facetsManager } from 'Feature/Catalog/Integration/ProductsSearch';

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
    if (itemsCount < 12) {
      this.setState({ numberOfDisplayedItems: itemsCount });
    } else {
      count = (currentPageNumber + 1) * 12;
      count > itemsCount
        ? this.setState({ numberOfDisplayedItems: itemsCount })
        : this.setState({ numberOfDisplayedItems: count });
    }
  }

  public safeRender() {
    const { search, itemsCount, isLoading } = this.props;
    const { numberOfDisplayedItems } = this.state;
    const parsedSearch = tryParseUrlSearch(search);
    const appliedFacets = facetsManager(parsedSearch[FACET_PARAMETER_NAME]).getFacets();
    return (
      <div className="product_list_header">
        <div className="header">
          {!isLoading && (
            <div className="header_stats">
              <span>
                Showing {numberOfDisplayedItems} of {itemsCount} results
              </span>
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
                  <i className="fa fa-close" />
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
