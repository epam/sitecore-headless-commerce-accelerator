//    Copyright 2019 EPAM Systems, Inc.
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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import { OrderSummaryPriceLines } from 'Feature/Checkout/client/common/OrderSummaryPriceLines';
import * as Jss from 'Foundation/ReactJss/client';
import toggleBar from 'Foundation/UI/client/common/utility';

import { OrderSummaryProps, OrderSummaryState } from './models';

import './styles.scss';

export class OrderSummaryComponent extends Jss.SafePureComponent<OrderSummaryProps, OrderSummaryState> {
    private promoCodeInput: HTMLInputElement | null;

  constructor(props: OrderSummaryProps) {
    super(props);
  }

  public addPromoCode() {
    const promoCode = this.promoCodeInput.value;
    this.props.AddPromoCode({promoCode});
  }

  public safeRender() {
    const { isLoading, isFailure, price, adjustments } = this.props;
    return (
      <section className="orderSummary">
        <h2>Order Summary</h2>
        <OrderSummaryPriceLines price={price} className="orderSummary-list" />
        <div className="orderSummary-freeShipping">
          <p>
            You're only <b>${(25).toFixed(2)}</b> away from free shipping!
          </p>
          <a href="" title="Details" className="details">
            Details
          </a>
        </div>
        <div className="orderSummary-promoCode is-open">
          <h3 onClick={(e) => toggleBar(e)}>Promotional code?</h3>
            <div className="field">
              {adjustments && adjustments.length ? (
                <>
                  <label htmlFor="promo-code">Adjustments:</label>
                  <ul>
                    {adjustments.map((item: string, index: number) =>
                      <li key={index}>{item}</li>
                    )}
                  </ul>
                </>
              ) : (
                <>
                  {isFailure && <p>Invalid promo code</p>}
                  <label htmlFor="promo-code">Enter promo code here:</label>
                  <input disabled={isLoading} type="text" id="promo-code" ref={(el) => this.promoCodeInput = el} />
                  <button disabled={isLoading} onClick={(e) => this.addPromoCode()} className="btn small">
                    Apply
                    {isLoading && <i className="fa fa-spinner fa-spin" />}
                  </button>
                </>
              )}
            </div>
        </div>
        <div className="orderSummary-checkout">
          <Placeholder name="order-actions" {...this.props} />
        </div>
      </section>
    );
  }
}
