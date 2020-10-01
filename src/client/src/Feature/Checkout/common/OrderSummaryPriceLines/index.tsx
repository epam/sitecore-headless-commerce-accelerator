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

import { OrderSummaryPriceLinesProps, OrderSummaryPriceLinesState } from './models';

import './styles.scss';

export class OrderSummaryPriceLines extends Jss.SafePureComponent<
  OrderSummaryPriceLinesProps,
  OrderSummaryPriceLinesState
> {
  public safeRender() {
    const { price, className, ...otherProps } = this.props;
    return (
      <ul {...otherProps} className={'orderSummaryPriceLines2 ' + className}>
        <li>
          <Text field={{ value: 'Merchandise Subtotal:' }} tag="span" className="name" />
          <span className="val">
            {price.currencySymbol}
            {price.subtotal.toFixed(2)}
          </span>
        </li>
        {!!price.taxTotal && (
          <li>
            <Text field={{ value: 'Taxes:' }} tag="span" className="name" />
            <span className="val">
              {price.currencySymbol}
              {price.taxTotal.toFixed(2)}
            </span>
          </li>
        )}
        {!!price.shippingTotal && (
          <li>
            <Text field={{ value: 'Shipping:' }} tag="span" className="name" />
            <span className="val">
              {price.currencySymbol}
              {price.shippingTotal.toFixed(2)}
            </span>
          </li>
        )}
        {!!price.totalSavings && (
          <li>
            <Text field={{ value: 'Savings (Details):' }} tag="span" className="name" />
            <span className="val">
              -{price.currencySymbol}
              {price.totalSavings.toFixed(2)}
            </span>
          </li>
        )}
        <li>
          <Text field={{ value: 'Estimated Total:' }} tag="span" className="name order-total" />
          <span className="val total-price">
            {price.currencySymbol}
            {price.total.toFixed(2)}
          </span>
        </li>
      </ul>
    );
  }
}
