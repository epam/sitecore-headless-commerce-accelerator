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
import { DependentField, FieldSet, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/client/Form';

import { CheckoutStepType } from 'Feature/Checkout/client/Integration/Checkout';

import { ADDRESS_TYPE, FIELDS } from './constants';
import { BillingProps, BillingState } from './models';

import './styles.scss';

export default class BillingComponent extends Jss.SafePureComponent<BillingProps, BillingState> {
  public constructor(props: BillingProps) {
    super(props);
    const selectedAddressOption = this.props.useForBillingAddress ? ADDRESS_TYPE.SAME_AS_SHIPPING : ADDRESS_TYPE.NEW;
    this.state = {
      selectedAddressOption,
    };
  }
  public componentWillMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Billing);
    }
  }
  public safeRender() {
    const { fields, commerceUser } = this.props;
    return (
      <Form className="thick-theme">
        <section className="billing">
          <Text field={{ value: 'Billing' }} tag="h1" />
          <fieldset>
            <Text field={{ value: 'Bill To:' }} tag="h2" />
            <div className="row">
              <div className="col-md-12">
                <ul className="options">
                  <li>
                    <Input
                      type="radio"
                      id="r11"
                      name={FIELDS.ADDRESS_TYPE}
                      defaultChecked={this.state.selectedAddressOption === ADDRESS_TYPE.NEW}
                      defaultValue={ADDRESS_TYPE.NEW}
                      onChange={(e) => this.handleAddressOptionChange(e)}
                    />
                    <Text field={{ value: 'A New Address' }} tag="label" htmlFor="r11" />
                  </li>
                  <li>
                    <Input
                      type="radio"
                      id="r12"
                      name={FIELDS.ADDRESS_TYPE}
                      defaultChecked={this.state.selectedAddressOption === ADDRESS_TYPE.SAME_AS_SHIPPING}
                      defaultValue={ADDRESS_TYPE.SAME_AS_SHIPPING}
                      onChange={(e) => this.handleAddressOptionChange(e)}
                    />
                    <Text field={{ value: 'Same As Shipping Address' }} tag="label" htmlFor="r12" />
                  </li>
                </ul>
              </div>
            </div>
          </fieldset>
          <FieldSet
            customVisibility={(formValues) => {
              const addressTypeValue = formValues[FIELDS.ADDRESS_TYPE];
              return addressTypeValue === ADDRESS_TYPE.NEW;
            }}
          >
            <fieldset>
              <Text field={{ value: 'Billing Location:' }} tag="h2" />
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'First Name:' }} tag="label" className="required" />
                  <Input name={FIELDS.FIRST_NAME} type="text" required={true} maxLength={100} />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'Last Name:' }} tag="label" className="required" />
                  <Input name={FIELDS.LAST_NAME} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Address Line 1:' }} tag="label" className="required" />
                  <Input name={FIELDS.ADDRESS_LINE} type="text" required={true} maxLength={100} />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'City:' }} tag="label" className="required" />
                  <Input name={FIELDS.CITY} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Country:' }} tag="label" className="required" />
                  <Select name={FIELDS.COUNTRY} required={true}>
                    <option>Not Selected</option>
                    {fields.countries.map((country, index) => (
                      <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                        {country.name}
                      </option>
                    ))}
                  </Select>
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'Province:' }} tag="label" className="required" />
                  <DependentField>
                    {(form) => (
                      <Select name={FIELDS.PROVINCE} required={true} disabled={!form.values[FIELDS.COUNTRY]}>
                        <option value="">Not Selected</option>
                        {this.renderSubdivisions(form.values[FIELDS.COUNTRY] as string)}
                      </Select>
                    )}
                  </DependentField>
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Postal Code:' }} tag="label" className="required" />
                  <Input name={FIELDS.POSTAL_CODE} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <div className="sub-text">
                    <Text field={{ value: 'Email Address:' }} tag="label" className="required" />
                    {commerceUser && commerceUser.customerId ? (
                      <Input
                        name={FIELDS.EMAIL}
                        type="email"
                        disabled={true}
                        value={commerceUser.email}
                        required={true}
                        maxLength={100}
                      />
                    ) : (
                      <Input name={FIELDS.EMAIL} type="email" required={true} maxLength={100} />
                    )}
                    <Text field={{ value: 'For order status and updates' }} tag="sub" />
                  </div>
                </div>
              </div>
            </fieldset>
          </FieldSet>
          <div className="button-spacing-wrap">
            <div className="row">
              <div className="col-sm-12">
                <Submit
                  className="btn animated"
                  onSubmitHandler={(formValues) => this.handleSaveAndContinueClick(formValues)}
                >
                  <Text field={{ value: 'Save & Continue' }} tag="span" />
                </Submit>
              </div>
            </div>
          </div>
        </section>
      </Form>
    );
  }

  private getSelectedCountry(countryCode: string) {
    const { fields } = this.props;

    return fields.countries.find((c) => c.countryCode === countryCode);
  }

  private handleAddressOptionChange(e: React.FormEvent<HTMLInputElement>) {
    this.setState({ selectedAddressOption: e.currentTarget.value });
  }

  private handleSaveAndContinueClick(formValues: FormValues) {
    const { SubmitStep } = this.props;

    const useSameAsShipping = formValues[FIELDS.ADDRESS_TYPE] === ADDRESS_TYPE.SAME_AS_SHIPPING;

    if (useSameAsShipping) {
      SubmitStep({
        billing: {
          useSameAsShipping,
        },
      });
    } else {
      const selectedCountry = this.getSelectedCountry(formValues[FIELDS.COUNTRY] as string);
      const newAddress = {
        address1: formValues[FIELDS.ADDRESS_LINE] as string,
        address2: '',
        city: formValues[FIELDS.CITY] as string,
        country: selectedCountry.name,
        countryCode: selectedCountry.countryCode,
        email: formValues[FIELDS.EMAIL] as string,
        externalId: '',
        firstName: formValues[FIELDS.FIRST_NAME] as string,
        isPrimary: false,
        lastName: formValues[FIELDS.LAST_NAME] as string,
        name: '',
        partyId: '',
        state: formValues[FIELDS.PROVINCE] as string,
        zipPostalCode: formValues[FIELDS.POSTAL_CODE] as string,
      };

      SubmitStep({
        billing: {
          address: newAddress,
          useSameAsShipping: false,
        },
      });
    }
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
