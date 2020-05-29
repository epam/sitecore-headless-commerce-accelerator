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
import { Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/client/Form';

import { CheckoutStepType } from 'Feature/Checkout/client/Integration/Checkout';

import { CreditCardTypes } from './components';
import { FIELDS } from './constants';
import { PaymentProps, PaymentState } from './models';

import './styles.scss';

export default class PaymentComponent extends Jss.SafePureComponent<PaymentProps, PaymentState> {
  public constructor(props: PaymentProps) {
    super(props);

    this.state = {
      formIsValid: true,
    };
  }

  public componentWillMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Payment);
    }
  }

  public safeRender() {
    const { isSubmitting } = this.props;
    const { formIsValid } = this.state;

    return (
      <Form className="thick-theme">
        {isSubmitting && (
          <div className="billing-shipping-info-loading-overlay">
            <div className="loading" />
          </div>
        )}
        {!formIsValid && <div>Validation failed! Check credit card data.</div>}
        <section>
          <Text field={{ value: 'Payment' }} tag="h1" />
          <fieldset>
            <div className="row">
              <div className="col-md-12">
                <Text field={{ value: 'Payment Options:' }} tag="h2" />
                <Text field={{ value: 'Card Number:' }} tag="label" className="required" />
                <div className="credit-cards-row">
                  <div className="credit-cards-col">
                    <Input
                      type="text"
                      className="card-number"
                      name={FIELDS.CARD_NUMBER}
                      minLength={16}
                      maxLength={16}
                      required={true}
                    />
                  </div>
                  <CreditCardTypes />
                </div>
                <Text field={{ value: 'Expires:' }} tag="label" className="required" />
                <div className="select cc-month">
                  <Select name={FIELDS.EXPIRES_MONTH} required={true} defaultValue={'1'}>
                    {[
                      'January',
                      'February',
                      'March',
                      'April',
                      'May',
                      'June',
                      'July',
                      'August',
                      'September',
                      'October',
                      'November',
                      'December',
                    ].map((monthName, index) => (
                      <option value={index + 1} key={monthName}>
                        {monthName}
                      </option>
                    ))}
                  </Select>
                </div>
                <span className="slashy">/</span>
                <div className="select cc-year">
                  <Select
                    name={FIELDS.EXPIRES_YEAR}
                    required={true}
                    defaultValue={(new Date().getFullYear() + 1).toString()}
                  >
                    {new Array<number>(10)
                      .fill(new Date().getFullYear())
                      .map((date, index) => `${date + index}`)
                      .map((year) => (
                        <option value={year} key={year}>
                          {year}
                        </option>
                      ))}
                  </Select>
                </div>
                <Text field={{ value: 'Security Code:' }} tag="label" className="required" />
                <Input type="text" className="security-code" name={FIELDS.SECURITY_CODE} required={true} />
              </div>
              <div className="col-sm-12">
                <Submit
                  disabled={isSubmitting}
                  className="btn animated"
                  onSubmitHandler={(formValues) => this.handlePlaceOrderClick(formValues)}
                >
                  <Text field={{ value: 'Place your order' }} tag="span" />
                </Submit>
              </div>
            </div>
          </fieldset>
        </section>
      </Form>
    );
  }

  private handlePlaceOrderClick(formValues: FormValues) {
    const { SubmitStep } = this.props;

    const creditCard = {
      cardNumber: formValues[FIELDS.CARD_NUMBER] as string,
      expiresMonth: formValues[FIELDS.EXPIRES_MONTH] as number,
      expiresYear: formValues[FIELDS.EXPIRES_YEAR] as number,
      securityCode: formValues[FIELDS.SECURITY_CODE] as string,
    };

    SubmitStep({ payment: { creditCard } });
  }
}
