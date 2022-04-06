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

import { get, isEmpty } from 'lodash';
import * as Jss from 'Foundation/ReactJss';

import { DependentField, FieldSet, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';
import { Select as PureSelect } from 'components';
import { CheckoutStepType } from 'services/checkout';

import { AddressOptions } from './AddressOptions';
import { validate } from 'utils';
import { ADDRESS_TYPE, FIELDS, FIELD_TYPES } from './constants';
import { BillingProps, BillingState } from './models';

const BILLING_ADDRESS_FORM_CONTENT = {
  NOT_SELECTED: 'Not Selected',
};

export default class BillingComponent extends Jss.SafePureComponent<BillingProps, BillingState> {
  public constructor(props: BillingProps) {
    super(props);
    const selectedAddressOption =
      this.props.useForBillingAddress || this.props.stepValues.billing?.useSameAsShipping
        ? ADDRESS_TYPE.SAME_AS_SHIPPING
        : ADDRESS_TYPE.NEW;
    const guestEmail = this.props.stepValues.shipping ? this.props.stepValues.shipping.address.email : '';
    this.state = {
      email: '',
      selectedAddressOption,
      stateFormFields: {},
      guestEmail,
    };

    this.handleAddressOptionChange = this.handleAddressOptionChange.bind(this);
  }
  public componentDidMount() {
    const { commerceUser } = this.props;
    if (!!commerceUser && !!commerceUser.customerId) {
      this.setState({ email: commerceUser.email });
    }
  }
  public UNSAFE_componentWillMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Billing);
    }
  }
  public safeRender() {
    const { fields, stepValues } = this.props;
    const { email, guestEmail } = this.state;

    const firstName = stepValues.billing ? stepValues.billing.address?.firstName : '';
    const lastName = stepValues.billing ? stepValues.billing.address?.lastName : '';
    const address1 = stepValues.billing ? stepValues.billing.address?.address1 : '';
    const city = stepValues.billing ? stepValues.billing.address?.city : '';
    const zipPostalCode = stepValues.billing ? stepValues.billing.address?.zipPostalCode : '';
    const state = stepValues.billing ? stepValues.billing.address?.state : '';
    const country = stepValues.billing ? stepValues.billing.address?.countryCode : '';

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
          <FieldSet customVisibility={(formValues) => this.state.selectedAddressOption === ADDRESS_TYPE.NEW}>
            <fieldset>
              <Text field={{ value: 'Billing Location' }} tag="h2" />
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'First Name' }} tag="label" className="required" />
                  <Input
                    name={FIELDS.FIRST_NAME}
                    type="text"
                    required={true}
                    maxLength={100}
                    fullWidth={true}
                    defaultValue={firstName}
                    error={get(this.state.stateFormFields, [FIELD_TYPES.FIRST_NAME, 'hasError'], false)}
                    helperText={get(this.state.stateFormFields, [FIELD_TYPES.FIRST_NAME, 'message'])}
                    handlerFocusField={() => this.handlerFocusField('firstName')}
                  />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'Last Name' }} tag="label" className="required" />
                  <Input
                    name={FIELDS.LAST_NAME}
                    type="text"
                    required={true}
                    maxLength={100}
                    fullWidth={true}
                    defaultValue={lastName}
                    error={get(this.state.stateFormFields, [FIELD_TYPES.LAST_NAME, 'hasError'], false)}
                    helperText={get(this.state.stateFormFields, [FIELD_TYPES.LAST_NAME, 'message'])}
                    handlerFocusField={() => this.handlerFocusField('lastName')}
                  />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Address Line 1' }} tag="label" className="required" />
                  <Input
                    name={FIELDS.ADDRESS_LINE}
                    type="text"
                    required={true}
                    maxLength={100}
                    fullWidth={true}
                    defaultValue={address1}
                    error={get(this.state.stateFormFields, [FIELD_TYPES.ADDRESS_LINE, 'hasError'], false)}
                    helperText={get(this.state.stateFormFields, [FIELD_TYPES.ADDRESS_LINE, 'message'])}
                    handlerFocusField={() => this.handlerFocusField('addressLine')}
                  />
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'City' }} tag="label" className="required" />
                  <Input
                    name={FIELDS.CITY}
                    type="text"
                    required={true}
                    maxLength={100}
                    fullWidth={true}
                    defaultValue={city}
                    error={get(this.state.stateFormFields, [FIELD_TYPES.CITY, 'hasError'], false)}
                    helperText={get(this.state.stateFormFields, [FIELD_TYPES.CITY, 'message'])}
                    handlerFocusField={() => this.handlerFocusField('city')}
                  />
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Country' }} tag="label" className="input-title required" />
                  <Select fullWidth={true} name={FIELDS.COUNTRY} defaultValue={country} required={true}>
                    <option>{BILLING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                    {fields.countries.map((country, index) => (
                      <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                        {country.name}
                      </option>
                    ))}
                  </Select>
                </div>
                <div className="col-ms-6">
                  <Text field={{ value: 'State' }} tag="label" className="required" />
                  <DependentField>
                    {(form) =>
                      form.values[FIELDS.COUNTRY] ? (
                        <Select fullWidth={true} name={FIELDS.STATE} required={true} error={false} defaultValue={state}>
                          <option value="">{BILLING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                          {this.renderSubdivisions(form.values[FIELDS.COUNTRY] as string)}
                        </Select>
                      ) : (
                        <PureSelect disabled={true} fullWidth={true}>
                          <option>{BILLING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                        </PureSelect>
                      )
                    }
                  </DependentField>
                </div>
              </div>
              <div className="row">
                <div className="col-ms-6">
                  <Text field={{ value: 'Postal Code' }} tag="label" className="required" />
                  <Input
                    name={FIELDS.POSTAL_CODE}
                    type="text"
                    required={true}
                    maxLength={100}
                    fullWidth={true}
                    defaultValue={zipPostalCode}
                    error={get(this.state.stateFormFields, [FIELD_TYPES.POSTAL_CODE, 'hasError'], false)}
                    helperText={get(this.state.stateFormFields, [FIELD_TYPES.POSTAL_CODE, 'message'])}
                  />
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
                      disabled={true}
                      onChange={(e) => this.handleEmailInput(e)}
                      value={email ? email : guestEmail}
                      maxLength={100}
                      fullWidth={true}
                      error={get(this.state.stateFormFields, [FIELD_TYPES.EMAIL, 'hasError'], false)}
                      helperText={get(this.state.stateFormFields, [FIELD_TYPES.EMAIL, 'message'])}
                    />
                  </div>
                </div>
              </div>
            </fieldset>
          </FieldSet>
          <div className="row">
            <div className="col-sm-12">
              <Submit
                buttonTheme="default"
                fullWidth={true}
                onSubmitHandler={(formValues) => this.handleSaveAndContinueClick(formValues)}
              >
                <Text field={{ value: 'Save & Continue' }} tag="span" />
              </Submit>
            </div>
          </div>
        </div>
      </Form>
    );
  }

  private handlerFocusField(field: string) {
    this.setState({
      stateFormFields: {
        ...this.state.stateFormFields,
        [field]: {
          hasError: false,
        },
      },
    });
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
    formValues[FIELDS.EMAIL] = this.state.email ? this.state.email : this.state.guestEmail;
    const formFields = validate(formValues, FIELDS);
    const { SubmitStep } = this.props;
    const { selectedAddressOption } = this.state;

    const useSameAsShipping = selectedAddressOption === ADDRESS_TYPE.SAME_AS_SHIPPING;

    if (useSameAsShipping) {
      SubmitStep({
        billing: {
          useSameAsShipping,
        },
      });
    } else {
      if (isEmpty(formFields)) {
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
          lastName: formValues[FIELDS.LAST_NAME] as string,
          name: '',
          partyId: '',
          state: formValues[FIELDS.STATE] as string,
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
    this.setState({ stateFormFields: formFields });
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
