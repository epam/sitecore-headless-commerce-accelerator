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

import './styles.scss';

import { Text } from '@sitecore-jss/sitecore-jss-react';
import { ConfirmationProps, ConfirmationState } from './models';

export class Confirmation extends Jss.SafePureComponent<ConfirmationProps, ConfirmationState> {
  public safeRender() {
    const { addresses, payment, price, shipping } = this.props.order;
    return (
      <div className="order-confirmation-wrapper-2">
        <section className="container">
          <div className="row">
            <div className="col-md-12 nopadding">
              <div className="order-confirmation">
                <div className="container">
                  <div className="row">
                    <div className="col-md-12">
                      <Text tag="h3" className="order-confirmation_title" field={{ value: 'Order Confirmation' }} />
                    </div>
                  </div>
                </div>
                <div className="order-confirmation_container">
                  <div className="col-md-4 col-xs-12">
                    <div className="info-column-1 order-form">
                      {shipping.map((shippingMethod, i) => {
                        const address = addresses.find((item) => item.partyId === shippingMethod.partyId);
                        return (
                          <div className="shipping-info" key={`${shippingMethod.externalId}-${i}`}>
                            <div className="info1">
                              <div className="title">
                                <Text field={{ value: 'Shipping To' }} tag="span" className="title_text" />
                              </div>
                              <Text field={{ value: 'Address' }} tag="div" className="sub-title" />
                              <div className="sub-content">
                                <span>{address.firstName} {address.lastName}</span>
                                <span>{address.address1}</span>
                                <span>{address.city}, {address.state} {address.zipPostalCode}</span>
                              </div>
                            </div>
                            <div className="info2">
                              <div className="sub-title">Method </div>
                              <div className="sub-content">{shippingMethod.name}</div>
                              <div className="sub-title">Status</div>
                              <div className="sub-content">Pending</div>
                            </div>
                          </div>
                        );
                      })}
                    </div>
                  </div>
                  <div className="col-md-4 col-xs-12">
                    <div className="info-column-2 order-form">
                      {payment.map((paymentInfo, i) => {
                        const address = addresses.find((item) => item.partyId === paymentInfo.partyId);
                        return (
                          <div className="billing-info" key={`${paymentInfo.paymentMethodId}-${i}`}>
                            <div className="info1">
                              <div className="title">
                                <Text field={{ value: 'Billing Information' }} tag="span" className="title_text" />
                              </div>
                              <Text field={{ value: 'Address' }} tag="div" className="sub-title" />
                              <div className="sub-content">
                                <span>{address.firstName} {address.lastName}</span>
                                <span>{address.address1}</span>
                                <span>{address.city}, {address.state} {address.zipPostalCode}</span>
                              </div>
                            </div>
                          </div>
                        );
                      })}
                    </div>
                  </div>
                  <div className="col-md-4 col-xs-12">
                    <div className="info-column-3 order-form">
                      <div className="payment-information">
                        <div className="title">
                          <Text field={{ value: 'Payment Information:' }} tag="span" className="title_text" />
                        </div>
                        <div className="payment-information-price">
                          <div>
                            <div className="price-item">
                              <span>Subtotal</span>
                              <span>{price.currencySymbol}{price.subtotal.toFixed(2)}</span>
                            </div>
                            <div className="price-item">
                              <span>Shipping Fees</span>
                              <span>{price.currencySymbol}{price.shippingTotal.toFixed(2)}</span>
                            </div>
                            <div className="price-item">
                              <span>Taxes</span>
                              <span>{price.currencySymbol}{price.taxTotal.toFixed(2)}</span>
                            </div>
                            <div className="price-item">
                              <span>Savings</span>
                              <span>-{price.currencySymbol}{price.totalSavings.toFixed(2)}</span>
                            </div>
                            {/* <tr>
                                <td className="tp-td1 tp-bottom-line">Total</td>
                                <td className="tp-td2 tp-bottom-line">{price.currencySymbol}{price.total}</td>
                              </tr> */}
                          </div>
                        </div>
                        <div className="order-total">
                          <div className="order-total_title">Order Total:</div>
                          <div className="order-total_price nopadding-bottom">
                            <span>{price.currencySymbol}</span>
                            <span className="order-amount">{price.total.toFixed(2)}</span>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}