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

import * as Jss from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';

import { OrderSummaryProps, OrderSummaryState } from './models';

import './styles.scss';

export class OrderSummaryComponent extends Jss.SafePureComponent<OrderSummaryProps, OrderSummaryState> {
  private promoCodeInput: HTMLInputElement | null;

  constructor(props: OrderSummaryProps) {
    super(props);

    this.state = {
      appliedCodes: [],
      hasBeenApplied: false,
      promoCodeIsEmpty: false,
    };
  }

  public addPromoCode() {
    const promoCode = this.promoCodeInput.value;
    const { isSuccess } = this.props;
    if (promoCode) {
      this.props.AddPromoCode({ promoCode });
      if (isSuccess) {
        this.setState({
          appliedCodes: [...this.state.appliedCodes, promoCode]
        });
      } else {
        if (this.state.appliedCodes.some((code) => code === promoCode)) {
          this.setState({ hasBeenApplied: true });
        }
      }
    } else {
      this.setState({
        promoCodeIsEmpty: true,
      });
    }
  }

  public safeRender() {
    const { isLoading, price, isFailure, isSuccess, adjustments } = this.props;
    const { promoCodeIsEmpty, hasBeenApplied } = this.state;
    return (
      <section className="orderSummary">
        <div className="col-lg-4 col-md-6">
          <div className="column tax">
            <div className="titleWrap">
              <h4 className="titleWrap-title">Estimate Shipping And Tax</h4>
            </div>
            <div className="subTitleWrap">
              <p>Enter your destination to get a shipping estimate.</p>
            </div>
            <div className="countrySelectWrapper">
              <label>* Region / State</label>
              <select className="">
                <option>Canada</option>
                <option>United States</option>
              </select>
            </div>
            <div className="zipCodeWrapper">
              <label>* Zip/Postal Code</label>
              <input type="text" />
            </div>
            <button className="cartBtn">Get A Quote</button>
          </div>
        </div>
        <div className="col-lg-4 col-md-6">
          <div className="column tax">
            <div className="titleWrap">
              <h4 className="titleWrap-title">Use Promotional Code</h4>
            </div>
            {adjustments && adjustments.length !== 0 && (
              <div className="promo-code-list">
                <ul className="adjustment-list">
                  {adjustments.map((item: string, index: number) => (
                    <div key={index} className="adjustment-item">
                      <li>{item}</li>
                    </div>
                  ))}
                </ul>
              </div>
            )}
            <div className="subTitleWrap">
              <p>Enter your promotional code if you have one.</p>
            </div>
            <div className="zipCodeWrapper">
              <input type="text" disabled={isLoading} ref={(el) => (this.promoCodeInput = el)} required={true} />
            </div>
            {isSuccess
              ? <p className="valid-promo-code-msg">Promo code applied!</p>
              : (
                isFailure && <p className="invalid-promo-code-msg">{hasBeenApplied ? 'Promo code already applied!' : 'Invalid promo code'}</p>
              )
            }
            {promoCodeIsEmpty && <p className="invalid-promo-code-msg">Promo code can not be empty</p>}
            <button className="cartBtn" disabled={isLoading}  onClick={(e) => this.addPromoCode()}>
              Apply Promotional
              {isLoading && <i className="fa fa-spinner fa-spin" />}
            </button>
          </div>
        </div>
        <div className="col-lg-4 col-md-12">
          <div className="column tax">
            <div className="titleWrap">
              <h4 className="titleWrap-title">Cart Total</h4>
            </div>
            <div className="subTotal">
              <label>Merchandise Subtotal:</label>
              <span>${price.subtotal.toFixed(2)}</span>
            </div>
            <div className="total">
              <label>Estimated Total:</label>
              <span>${price.total.toFixed(2)}</span>
            </div>
            <div className="checkoutWrapper">
              <NavigationLink to="/Checkout/Shipping" className="cartBtn checkout">
                Proceed to Checkout
              </NavigationLink>
            </div>
          </div>
        </div>
      </section>
    );
  }
}
