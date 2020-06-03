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

import * as JSS from 'Foundation/ReactJss/client';

import { OrderSummaryPriceLines } from 'Feature/Checkout/client/common/OrderSummaryPriceLines';

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
        <section className="no-border">
          <Text field={{ value: 'shipping' }} tag="h1" />
          {price && <OrderSummaryPriceLines price={price} className="summary" />}
        </section>
        <section className="toggle open">
          <Text field={{ value: 'In Your Cart' }} tag="h1" />
          <CartLinesSummary cartLines={cartLines} fallbackImageUrl={sitecoreContext.fallbackImageUrl} />
        </section>
      </>
    );
  }
}
