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

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

import { Text, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { ConfirmationProps, ConfirmationState } from './models';

class ConfirmationControl extends Jss.SafePureComponent<ConfirmationProps, ConfirmationState> {
  public safeRender() {
    const { addresses, payments, price, shippings } = this.props.order;
    return (
      <div className="order-confirmation-wrapper">
        <section className="container">
          <div className="row">
            <div className="col-md-12 nopadding">
              <div className="order-confirmation">
                <div className="container">
                  <div className="row">
                    <div className="col-md-12">
                      <div className="color-title">
                        <Text tag="h1" className="title wishlist-title" field={{ value: 'Order Confirmation' }} />
                        <div className="color-bar" />
                      </div>
                    </div>
                  </div>
                </div>
                <div className="order-confirmation-container">
                  <div className="col-md-6">
                    <div className="info-colomn1">
                      {shippings.map((shipping, i) => {
                        const address = addresses.find((item) => item.partyId === shipping.partyId);
                        return (
                          <div className="shipping-info" key={`${shipping.externalId}-${i}`}>
                            <div className="info1">
                              <Text field={{ value: 'Shipping To:' }} tag="span" className="title-text" />
                              <Text field={{ value: 'Address:' }} tag="span" className="subtitle-text" />
                              <span>{address.firstName} {address.lastName}</span>
                              <span>{address.address1}</span>
                              <span>{address.city}, {address.state} {address.zipPostalCode}</span>
                            </div>
                            <div className="info2">
                              <span>Method: {shipping.name}</span>
                              <span>Status: Pending</span>
                            </div>
                          </div>
                        );
                      })}
                      {payments.map((payment, i) => {
                        const address = addresses.find((item) => item.partyId === payment.partyID);
                        return (
                          <div className="billing-info" key={`${payment.paymentMethodId}-${i}`}>
                            <div className="info1">
                              <Text field={{ value: 'Billing Information:' }} tag="span" className="title-text" />
                              <Text field={{ value: 'Address:' }} tag="span" className="subtitle-text" />
                              <span>{address.firstName} {address.lastName}</span>
                              <span>{address.address1}</span>
                              <span>{address.city}, {address.state} {address.zipPostalCode}</span>
                            </div>
                            {/*<div className="info2">
                              <span>Method: Card Type: VISA</span>
                              <span>Card Number: ****** 6731</span>
                              <span>Expiration: 07/17</span>
                              </div>*/}
                          </div>
                        );
                      })}
                    </div>
                  </div>
                  <div className="col-md-6">
                    <div className="info-colomn2">
                      <div className="payment-info">
                        <div className="payment-info-title">
                          <Text field={{ value: 'Payment Information:' }} tag="span" />
                        </div>
                        <div className="payment-info-price">
                          <table className="tp-table1">
                            <tbody>
                              <tr>
                                <td className="tp-td1">Subtotal</td>
                                <td className="tp-td2">{price.currencySymbol}{price.subtotal.toFixed(2)}</td>
                              </tr>
                              <tr>
                                <td className="tp-td1">Shipping Fees</td>
                                <td className="tp-td2">{price.currencySymbol}{price.shippingTotal.toFixed(2)}</td>
                              </tr>
                              <tr>
                                <td className="tp-td1 tp-padding">Taxes</td>
                                <td className="tp-td2 tp-padding">{price.currencySymbol}{price.taxTotal.toFixed(2)}</td>
                              </tr>
                              <tr>
                                <td className="tp-td1 tp-top-padding tp-bottom-line">Savings</td>
                                <td className="tp-td2 tp-top-padding  tp-bottom-line">-{price.currencySymbol}{price.totalSavings.toFixed(2)}</td>
                              </tr>
                              {/* <tr>
                                <td className="tp-td1 tp-bottom-line">Total</td>
                                <td className="tp-td2 tp-bottom-line">{price.currencySymbol}{price.total}</td>
                              </tr> */}
                            </tbody>
                          </table>
                        </div>
                        <div className="payment-info-total">
                          <div className="order-total-text">Order Total:</div>
                          <div className="order-price nopadding-bottom">
                            <sup>{price.currencySymbol}</sup>
                            <sup className="order-amount">{price.total.toFixed(2)}</sup>
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

export const Confirmation = withExperienceEditorChromes(ConfirmationControl);
