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

import * as JSS from 'Foundation/ReactJss';

import { OrderSummaryPriceLines } from 'Feature/Checkout/common/OrderSummaryPriceLines';

import { CartLinesSummary } from './components';
import { OrderInformationProps, OrderInformationState } from './models';

import './styles.scss';

export class OrderInformationComponent extends JSS.SafePureComponent<OrderInformationProps, OrderInformationState> {
  public componentDidMount() {
    this.props.LoadCart();
  }
  public safeRender() {
    const { shoppingCartData, sitecoreContext } = this.props;

    const price = shoppingCartData ? shoppingCartData.price : null;
    const cartLines = shoppingCartData ? shoppingCartData.cartLines : [];
    return (
      <>
        <Text field={{ value: 'Your Order' }} tag="h3" className="your-order" />
        <div className="order-information-2 no-border">
          <div className="header-information">
            <div className="title">Product</div>
            <div className="title">Total</div>
          </div>
          <CartLinesSummary cartLines={cartLines} fallbackImageUrl={sitecoreContext.fallbackImageUrl} />
          {price && <OrderSummaryPriceLines price={price} className="summary" />}
        </div>
      </>
    );
  }
}
