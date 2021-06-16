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

import React, { PureComponent } from 'react';

import { LoadingStatus } from 'models';

import { SuggestionItem } from '../SuggestionItem';

import { SuggestionListProps } from './models';

import { cnNavigationSearch } from '../cn';
import './SuggestionList.scss';

export class SuggestionListComponent extends PureComponent<SuggestionListProps> {
  public componentWillUnmount() {
    this.props.resetState();
  }

  public render() {
    const { products, status, onItemClick } = this.props;
    const showNotFound = products.length === 0 && status === LoadingStatus.Loaded;

    return (
      <ul className={cnNavigationSearch('SuggestionList')}>
        {showNotFound && (
          <li>
            <span className={cnNavigationSearch('NotFound')}>Not found</span>
          </li>
        )}

        {products.map((product) => (
          <li key={product.productId}>
            <SuggestionItem product={product} onClick={onItemClick} />
          </li>
        ))}
      </ul>
    );
  }
}
