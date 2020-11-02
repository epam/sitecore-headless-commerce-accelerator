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

import classNames from 'classnames';
import * as React from 'react';

import { FilterProps, FilterState } from './models';

export class Filter extends React.Component<FilterProps, FilterState> {
  public constructor(props: FilterProps) {
    super(props);

    this.state = {
      isValuesVisible: true,
    };
  }
  public render() {
    const { first, facet, IsApplied, HandleFacetOnChange } = this.props;
    const { isValuesVisible } = this.state;
    return (
      <div className={classNames({ 'filter-first': first })}>
        <div className="product-filter">
          <div className="product-filter_toggle">
            <h3 className="product-filter_name">{facet.displayName}</h3>
          </div>
          {isValuesVisible && (
            <ul className="product-filter_options options show-all-links">
              {facet.foundValues &&
                facet.foundValues
                  .filter((v) => v.aggregateCount !== 0)
                  .sort((a, b) => {
                    const nameA = a.name.toLowerCase();
                    const nameB = b.name.toLowerCase();
                    if (nameA < nameB) {
                      return -1;
                    }
                    if (nameA > nameB) {
                      return 1;
                    }
                    return 0;
                  })
                  .map((foundValue, i) => {
                    const id = `${foundValue.name}${foundValue.aggregateCount}${i}`;
                    return (
                      <li key={i}>
                        <label className="container">
                          <input
                            type="checkbox"
                            id={id}
                            checked={IsApplied(facet.name, foundValue.name)}
                            onChange={(e) => HandleFacetOnChange(facet.name, foundValue.name, e)}
                          />
                          <span className="checkbox" title={foundValue.name} />
                          <label className="name" htmlFor={id} title={foundValue.name}>{`${foundValue.name}`}</label>
                          <span className="total">{foundValue.aggregateCount}</span>
                        </label>
                      </li>
                    );
                  })}
            </ul>
          )}
          <a
            onClick={(e) => this.toggleFacetVisibility(e)}
            className={classNames('view-all', { 'hide-all': isValuesVisible })}
          >
            <span>
              {isValuesVisible ? (
                <>
                  Hide all
                  <i className="fa fa-angle-up"/>
                </>
              ) : (
                <>
                  Show All
                  <i className="fa fa-angle-down"/>
                </>
              )}
            </span>
          </a>
        </div>
      </div>
    );
  }

  private toggleFacetVisibility(e: React.MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();
    e.stopPropagation();

    this.setState({
      isValuesVisible: !this.state.isValuesVisible,
    });
  }
}
