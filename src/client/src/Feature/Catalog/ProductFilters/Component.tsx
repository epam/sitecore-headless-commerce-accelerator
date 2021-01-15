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
import * as Jss from 'Foundation/ReactJss';

import { FACET_PARAMETER_NAME, facetsManager } from 'Feature/Catalog/Integration/ProductsSearch';

import { Filter } from './components';
import { ProductFiltersOwnState, ProductFiltersProps } from './models';

import './styles.scss';

export default class ProductFiltersComponent extends Jss.SafePureComponent<
  ProductFiltersProps,
  ProductFiltersOwnState
> {
  constructor(props: ProductFiltersProps) {
    super(props);

    this.isApplied = this.isApplied.bind(this);
    this.handleFacetOnChange = this.handleFacetOnChange.bind(this);
  }

  protected safeRender() {
    const { facets, isLoading, items, totalItemCount } = this.props;
    return (
      <div className="filters">
        <section className="productGridFilter">
          {isLoading && <div className="overlay" />}
          {items.length !== 0
            && totalItemCount !== 0
            && !isLoading
            && facets.map((facet, index) => (
                <Filter
                  key={index}
                  facet={facet}
                  first={index === 0}
                  IsApplied={this.isApplied}
                  HandleFacetOnChange={this.handleFacetOnChange}
                />
          ))}
          <div className="filter-last" />
        </section>
      </div>
    );
  }

  private handleFacetOnChange(name: string, value: string, e: React.ChangeEvent<HTMLInputElement>) {
    const { search } = this.props;
    if (e.target.checked) {
      this.props.ApplyFacet(name, value, search);
    } else {
      this.props.DiscardFacet(name, value, search);
    }
  }

  private isApplied(name: string, value: string) {
    const { search } = this.props;
    const parsedSearch = tryParseUrlSearch(search);
    const appliedFacets = facetsManager(parsedSearch[FACET_PARAMETER_NAME]).getFacets();

    const facet = appliedFacets[name];

    if (!facet) {
      return false;
    }

    return facet.indexOf(value) !== -1;
  }
}
