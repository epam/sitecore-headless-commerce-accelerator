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
import { DependentField, FieldSet, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';

import { CheckoutStepType } from 'Feature/Checkout/Integration/Checkout';

import { AddressOptions } from './AddressOptions';
import { ADDRESS_TYPE, FIELDS } from './constants';
import { BillingProps, BillingState } from './models';

import './styles.scss';

export default class BillingComponent extends Jss.SafePureComponent<BillingProps, BillingState> {
  public constructor(props: BillingProps) {
    super(props);
    const selectedAddressOption = this.props.useForBillingAddress ? ADDRESS_TYPE.SAME_AS_SHIPPING : ADDRESS_TYPE.NEW;
    this.state = {
      email: '',
      selectedAddressOption,
    };

    this.handleAddressOptionChange = this.handleAddressOptionChange.bind(this);
  }
  public componentDidMount() {
    const { commerceUser } = this.props;
    if (!!commerceUser && !!commerceUser.customerId) {
      this.setState({ email: commerceUser.email });
    }
  }
  public componentWillMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Billing);
    }
  }
  public safeRender() {
    const { fields, commerceUser } = this.props;
    const { email } = this.state;
    return (
      <Form className="thick-theme">
        <div className="billing-shipping">
          <Text field={{ value: 'Billing' }} tag="h2" />
          <fieldset>
            <Text field={{ value: 'Bill To' }} tag="h3" />
            <div className="row">
              <div className="col-md-12">
                <AddressOptions
                  selectedAddressOption={this.state.selectedAddressOption}
                  onAddressOptionChange={this.handleAddressOptionChange}
                />
              </div>
            </div>
          </fieldset>
          <FieldSet
            customVisibility={(formValues) => this.state.selectedAddressOption === ADDRESS_TYPE.NEW}
          >
            <fieldset>
              <Text field={{ value: 'Billing Location' }} tag="h2" />
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'First Name' }} tag="label" className="required" />
                  <Input name={FIELDS.FIRST_NAME} type="text" required={true} maxLength={100} />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'Last Name' }} tag="label" className="required" />
                  <Input name={FIELDS.LAST_NAME} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Address Line 1' }} tag="label" className="required" />
                  <Input name={FIELDS.ADDRESS_LINE} type="text" required={true} maxLength={100} />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'City' }} tag="label" className="required" />
                  <Input name={FIELDS.CITY} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Country' }} tag="label" className="required" />
                  <Select fullWidth={true} name={FIELDS.COUNTRY} required={true}>
                    <option>Not Selected</option>
                    {fields.countries.map((country, index) => (
                      <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                        {country.name}
                      </option>
                    ))}
                  </Select>
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'Province' }} tag="label" className="required" />
                  <DependentField>
                    {(form) => (
                      <Select
                        fullWidth={true}
                        name={FIELDS.PROVINCE}
                        required={true}
                        disabled={!form.values[FIELDS.COUNTRY]}
                      >
                        <option value="">Not Selected</option>
                        {this.renderSubdivisions(form.values[FIELDS.COUNTRY] as string)}
                      </Select>
                    )}
                  </DependentField>
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Postal Code' }} tag="label" className="required" />
                  <Input name={FIELDS.POSTAL_CODE} type="text" required={true} maxLength={100} />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <div className="sub-text">
                    <Text field={{ value: 'Email Address' }} tag="label" className="required" />
                    <Input
                      name={FIELDS.EMAIL}
                      type="email"
                      required={true}
                      disabled={!!commerceUser && !!commerceUser.customerId}
                      onChange={(e) => this.handleEmailInput(e)}
                      value={email}
                      maxLength={100}
                    />
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
        </div>
      </Form>
    );
  }

  private getSelectedCountry(countryCode: string) {
    const { fields } = this.props;

    return fields.countries.find((c) => c.countryCode === countryCode);
  }

  private handleEmailInput(e: React.FormEvent<HTMLInputElement>) {
    this.setState({ email: e.currentTarget.value });
  }

  private handleAddressOptionChange(value: string) {
    this.setState({ selectedAddressOption: value });
  }

  private handleSaveAndContinueClick(formValues: FormValues) {
    const { SubmitStep } = this.props;
    const { email, selectedAddressOption } = this.state;

    const useSameAsShipping = selectedAddressOption === ADDRESS_TYPE.SAME_AS_SHIPPING;

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
        email: email as string,
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
