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

import { tryParseUrlSearch } from 'Foundation/Base';

import { FACET_PARAMETER_NAME, facetsManager } from 'Feature/Catalog/Integration/ProductsSearch';

import { ProductListHeaderProps } from './models';

import './styles.scss';

export class ProductListHeader extends React.Component<ProductListHeaderProps> {
  public render() {
    const { search, itemsCount } = this.props;
    const parsedSearch = tryParseUrlSearch(search);
    const appliedFacets = facetsManager(parsedSearch[FACET_PARAMETER_NAME]).getFacets();
    return (
      <div className="product_list_header">
        <div className="header">
          <div className="header_stats">
            <span>{itemsCount} products</span>
          </div>
        </div>
        <div className="filter-labels">
          {Object.keys(appliedFacets).map((facetName) => {
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
