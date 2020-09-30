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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as Jss from 'Foundation/ReactJss';

import { CartLinesSummaryProps, CartLinesSummaryState } from './models';

import './styles.scss';

export class CartLinesSummary extends Jss.SafePureComponent<CartLinesSummaryProps, CartLinesSummaryState> {
  public safeRender() {
    const { cartLines } = this.props;

    return (
      <ul className="cart2">
        {cartLines &&
          cartLines.map((item, index) => {
            return (
              <li key={index}>
                <figure>
                  <figcaption>
                    <div className="product-title">
                      <Text field={{ value: item.product.brand }} tag="div" className="product-brand" />
                      <Text field={{ value: item.variant.displayName }} tag="span" className="product-name" />
                      <span className="product-quantity-prefix"> X </span>
                      <Text field={{ value: item.quantity.toString() }} tag="span" className="product-quantity" />
                    </div>
                    <div className="product-price">
                      {item.price.currencySymbol}
                      {item.price.total.toFixed(2)}
                    </div>
                  </figcaption>
                </figure>
              </li>
            );
          })}
      </ul>
    );
  }
}
