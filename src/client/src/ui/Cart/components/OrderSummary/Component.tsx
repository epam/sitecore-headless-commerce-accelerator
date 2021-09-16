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

import { Button, Input, Select as PureSelect } from 'components';
import { DependentField, Form, Select } from 'Foundation/ReactJss/Form';

import { FIELDS } from './constants';
import { OrderSummaryProps } from './models';

import './styles.scss';
export class OrderSummaryComponent extends Jss.SafePureComponent<OrderSummaryProps, any> {
  constructor(props: OrderSummaryProps) {
    super(props);
  }

  public safeRender() {
    const { price, countries } = this.props;
    return (
      <section className="orderSummary">
        <div className="col-lg-4 col-md-6">
          <div className="column tax">
            <Form>
              <div className="titleWrap">
                <h4 className="titleWrap-title">Estimate Shipping And Tax</h4>
              </div>
              <div className="subTitleWrap">
                <p>Enter your destination to get a shipping estimate.</p>
              </div>
              <div className="countrySelectWrapper">
                <Jss.Text field={{ value: '* Country:', editable: '* Country:' }} tag="label" className="required" />
                <Select
                  className="Select_order_summary"
                  fullWidth={true}
                  name={FIELDS.COUNTRY}
                  type="text"
                  required={true}
                >
                  <option value="">Not Selected</option>
                  {countries.map((country, index) => (
                    <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                      {country.name}
                    </option>
                  ))}
                </Select>
              </div>
              <div className="regionSelectWrapper">
                <Jss.Text
                  field={{ value: '* Region / State:', editable: '* Region / State:' }}
                  tag="label"
                  className="required"
                />
                <DependentField>
                  {(form) =>
                    form.values[FIELDS.COUNTRY] ? (
                      <Select
                        className="Select_order_summary"
                        fullWidth={true}
                        name={FIELDS.PROVINCE}
                        required={true}
                        disabled={!form.values[FIELDS.COUNTRY]}
                      >
                        <option value="">Not Selected</option>
                        {this.renderSubdivisions(form.values[FIELDS.COUNTRY] as string)}
                      </Select>
                    ) : (
                      <PureSelect className="Select_order_summary" disabled={true} fullWidth={true}>
                        <option>Not Selected</option>
                      </PureSelect>
                    )
                  }
                </DependentField>
              </div>
              <div className="zipCodeWrapper">
                <label>* Zip/Postal Code</label>
                <Input type="text" fullWidth={true} />
              </div>
              <Button className="OrderSummary-Button" buttonType="submit" buttonTheme="default">
                Get A Quote
              </Button>
            </Form>
          </div>
        </div>
        <div className="col-lg-4 col-md-12">
          <div className="column tax">
            <div className="titleWrap">
              <h4 className="titleWrap-title">Cart Total</h4>
            </div>
            <div className="subTotal totalDetails">
              <label>Merchandise Subtotal:</label>
              <span>${price.subtotal.toFixed(2)}</span>
            </div>
            <div className="savingsTotal totalDetails">
              <label>Savings (Details) :</label>
              <span>${price.totalSavings.toFixed(2)}</span>
            </div>
            <div className="taxTotal totalDetails">
              <label>Taxes:</label>
              <span>${price.taxTotal.toFixed(2)}</span>
            </div>
            <div className="shippingTotal totalDetails">
              <label>Shipping:</label>
              <span>${price.shippingTotal.toFixed(2)}</span>
            </div>
            <div className="total">
              <label>Estimated Total:</label>
              <span>${price.total.toFixed(2)}</span>
            </div>
            <div className="OrderSummary-ProceedButtonContainer">
              <Button
                className="OrderSummary-Button"
                buttonType="button"
                buttonTheme="default"
                fullWidth={true}
                href="/Checkout/Shipping"
              >
                Proceed to Checkout
              </Button>
            </div>
          </div>
        </div>
      </section>
    );
  }
  private getSelectedCountry(countryCode: string) {
    const { countries } = this.props;

    return countries.find((c) => c.countryCode === countryCode);
  }

  private renderSubdivisions(countryCode: string) {
    const selectedCountry = this.getSelectedCountry(countryCode);
    if (!selectedCountry) {
      return null;
    }

    return selectedCountry.subdivisions.map((state, index) => (
      <option key={index} value={state.code}>
        {state.name}
      </option>
    ));
  }
}
