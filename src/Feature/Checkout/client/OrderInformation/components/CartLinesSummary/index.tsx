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

import * as Jss from 'Foundation/ReactJss/client';

import { CartLinesSummaryProps, CartLinesSummaryState } from './models';

import './styles.scss';

export class CartLinesSummary extends Jss.SafePureComponent<CartLinesSummaryProps, CartLinesSummaryState> {
  public safeRender() {
    const { cartLines, fallbackImageUrl } = this.props;

    return (
      <ul className="cart">
        {cartLines &&
          cartLines.map((item, index) => {
            let imageUrl = fallbackImageUrl;

            if (!!item.variant.imageUrls && item.variant.imageUrls.length > 0) {
              imageUrl = item.variant.imageUrls[0];
            } else if (!!item.product.imageUrls && item.product.imageUrls.length > 0) {
              imageUrl = item.product.imageUrls[0];
            }

            return (
              <li key={index}>
                <figure>
                  <img src={imageUrl} />
                  <figcaption>
                    <Text field={{ value: item.product.brand }} tag="h4" />
                    <Text field={{ value: item.variant.displayName }} tag="span" />
                    <br />
                    <span className="price">
                      {item.price.currencySymbol}
                      {item.price.total.toFixed(2)}
                    </span>
                  </figcaption>
                </figure>
              </li>
            );
          })}
      </ul>
    );
  }
}
