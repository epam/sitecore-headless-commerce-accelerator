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
import React, { FormEvent } from 'react';

import { get, isEmpty } from 'lodash';
import * as Jss from 'Foundation/ReactJss';

import { Checkbox, DependentField, FieldSet, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';
import { Select as PureSelect } from 'components';
import { CheckoutStepType } from 'services/checkout';

import { AddressOptions } from './AddressOptions';
import { validate } from 'utils';
import { ADDRESS_TYPE, FIELDS, FIELD_TYPES } from './constants';
import { ShippingProps, ShippingState } from './models';
import './styles.scss';

const SHIPPING_ADDRESS_FORM_CONTENT = {
  NOT_SELECTED: 'Not Selected',
};

export default class ShippingComponent extends Jss.SafePureComponent<ShippingProps, ShippingState> {
  public constructor(props: ShippingProps) {
    super(props);
    const { commerceUser, stepValues } = this.props;
    const selectedAddressOption =
      stepValues.shipping?.options?.selectedAddressOption ||
      (commerceUser?.customerId ? ADDRESS_TYPE.SAVED : ADDRESS_TYPE.NEW);
    const guestEmail = stepValues.shipping ? stepValues.shipping.address.email : '';

    this.state = {
      canResetDeliveryInfo: true,
      email: '',
      saveToAccount: false,
      selectedAddressOption,
      useForBillingAddress: false,
      stateFormFields: {},
      guestEmail,
      mounted: false,
    };

    this.handleAddressOptionChange = this.handleAddressOptionChange.bind(this);
    this.handleUseForBillingAddressChange = this.handleUseForBillingAddressChange.bind(this);
    this.handleSaveToAccountChange = this.handleSaveToAccountChange.bind(this);
  }

  public componentDidMount() {
    this.setState({ mounted: true });
    const { commerceUser } = this.props;
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Fulfillment);
    }
    if (!!commerceUser && !!commerceUser.customerId) {
      this.setState({ email: commerceUser.email });
    }
  }

  // tslint:disable-next-line: no-big-function
  protected safeRender() {
    const { deliveryInfo, isSubmitting, isLoading, fields, shippingInfo, commerceUser, stepValues } = this.props;
    const { email, guestEmail, mounted } = this.state;
    const isLoggedIn = commerceUser && commerceUser.customerId;
    const newAddressSelected = stepValues.shipping?.options?.selectedAddressOption === ADDRESS_TYPE.NEW;

    const firstName = newAddressSelected ? stepValues.shipping?.address.firstName : '';
    const lastName = newAddressSelected ? stepValues.shipping?.address.lastName : '';
    const address1 = newAddressSelected ? stepValues.shipping?.address.address1 : '';
    const city = newAddressSelected ? stepValues.shipping?.address.city : '';
    const zipPostalCode = newAddressSelected ? stepValues.shipping?.address.zipPostalCode : '';
    const state = newAddressSelected ? stepValues.shipping?.address.state : '';
    const country = newAddressSelected ? stepValues.shipping?.address.countryCode : '';
    const shippingMethod = stepValues.shipping ? stepValues.shipping.shippingMethod.externalId : '';

    const savedAddress = deliveryInfo?.data?.userAddresses.find((address) => {
      return address.partyId === stepValues?.shipping?.address.partyId;
    });

    return (
      <Form>
        {(!mounted || isLoading || isSubmitting) && (
          <div className="billing-shipping-info-loading-overlay">
            <div className="loading" />
          </div>
        )}
        <div className="billing-shipping">
          <Text field={{ value: 'Shipping' }} tag="h2" />
          <FieldSet>
            <Text field={{ value: 'Ship To' }} tag="h3" />
            <div className="row">
              <div className="col-md-12">
                <AddressOptions
                  isLoggedIn={!!isLoggedIn}
                  selectedAddressOption={this.state.selectedAddressOption}
                  onAddressOptionChange={this.handleAddressOptionChange}
                />
              </div>
            </div>
          </FieldSet>
          <FieldSet
            customVisibility={(formValues) => {
              return this.state.selectedAddressOption === ADDRESS_TYPE.NEW;
            }}
          >
            <Text field={{ value: 'Shipping Location' }} tag="h3" />
            <div className="row">
              <div className="col-ms-6">
                <Text field={{ value: 'First Name' }} tag="label" className="input-title required" />
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
                <Text field={{ value: 'Last Name' }} tag="label" className="input-title required" />
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
                <Text field={{ value: 'Address Line 1' }} tag="label" className="input-title required" />
                <Input
                  name={FIELDS.ADDRESS_LINE}
                  type="text"
                  required={true}
                  maxLength={100}
                  defaultValue={address1}
                  fullWidth={true}
                  error={get(this.state.stateFormFields, [FIELD_TYPES.ADDRESS_LINE, 'hasError'], false)}
                  helperText={get(this.state.stateFormFields, [FIELD_TYPES.ADDRESS_LINE, 'message'])}
                  handlerFocusField={() => this.handlerFocusField('addressLine')}
                />
              </div>
              <div className="col-ms-6">
                <Text field={{ value: 'City' }} tag="label" className="input-title required" />
                <Input
                  name={FIELDS.CITY}
                  type="text"
                  required={true}
                  maxLength={100}
                  defaultValue={city}
                  fullWidth={true}
                  error={get(this.state.stateFormFields, [FIELD_TYPES.CITY, 'hasError'], false)}
                  helperText={get(this.state.stateFormFields, [FIELD_TYPES.CITY, 'message'])}
                  handlerFocusField={() => this.handlerFocusField('city')}
                />
              </div>
            </div>
            <div className="row">
              <div className="col-ms-6">
                <Text field={{ value: 'Country' }} tag="label" className="input-title required" />
                <Select fullWidth={true} name={FIELDS.COUNTRY} defaultValue={country} required={true} error={false}>
                  <option value="">{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                  {fields.countries.map((country, index) => (
                    <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                      {country.name}
                    </option>
                  ))}
                </Select>
              </div>
              <div className="col-ms-6">
                <Text field={{ value: 'State' }} tag="label" className="input-title required" />
                <DependentField>
                  {(form) =>
                    form.values[FIELDS.COUNTRY] ? (
                      <Select fullWidth={true} name={FIELDS.STATE} defaultValue={state} required={true} error={false}>
                        <option value="">{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                        {this.renderSubdivisions(form.values[FIELDS.COUNTRY] as string)}
                      </Select>
                    ) : (
                      <PureSelect disabled={true} fullWidth={true}>
                        <option>{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                      </PureSelect>
                    )
                  }
                </DependentField>
              </div>
            </div>
            <div className="row">
              <div className="col-ms-6">
                <Text field={{ value: 'Postal Code' }} tag="label" className="input-title required" />
                <Input
                  name={FIELDS.POSTAL_CODE}
                  type="text"
                  required={true}
                  maxLength={100}
                  defaultValue={zipPostalCode}
                  fullWidth={true}
                  error={get(this.state.stateFormFields, [FIELD_TYPES.POSTAL_CODE, 'hasError'], false)}
                  helperText={get(this.state.stateFormFields, [FIELD_TYPES.POSTAL_CODE, 'message'])}
                />
              </div>
            </div>
            <div className="row">
              <div className="col-ms-6">
                <div className="sub-text">
                  <Text field={{ value: 'Email Address' }} tag="label" className="input-title required" />
                  <Input
                    name={FIELDS.EMAIL}
                    type="email"
                    required={true}
                    disabled={!!commerceUser && !!commerceUser.customerId}
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
          </FieldSet>
          <FieldSet
            customVisibility={(formValues) => {
              return this.state.selectedAddressOption === ADDRESS_TYPE.SAVED;
            }}
          >
            <div className="row">
              <div className="col-sm-12">
                <Text field={{ value: 'Shipping Location' }} tag="h3" />
                <Select
                  fullWidth={true}
                  name={FIELDS.SELECTED_ADDRESS}
                  defaultValue={savedAddress?.partyId || ''}
                  required={true}
                  error={false}
                >
                  <option disabled={true} value="">
                    Select Option
                  </option>
                  {deliveryInfo &&
                    deliveryInfo.data &&
                    deliveryInfo.data.userAddresses &&
                    deliveryInfo.data.userAddresses.map((address, index) => (
                      <option key={index} value={address.partyId}>
                        {`${address.firstName} ${address.lastName}, ${address.address1}, ${address.state}, ${address.country}`}
                      </option>
                    ))}
                </Select>
              </div>
            </div>
          </FieldSet>
          <FieldSet>
            <div className="row">
              <div className="col-sm-6">
                <ul className="options">
                  <li>
                    <label htmlFor="use-for-billing" className="selection-container">
                      <Checkbox
                        name={FIELDS.USE_FOR_BILLING}
                        id="use-for-billing"
                        checked={this.state.useForBillingAddress}
                        onChange={this.handleUseForBillingAddressChange}
                      />
                      <span className="checkbox-label">Also use for billing address</span>
                    </label>
                  </li>
                </ul>
              </div>
              {this.state.selectedAddressOption === ADDRESS_TYPE.NEW && isLoggedIn && (
                <div className="col-sm-6">
                  <ul className="options">
                    <li>
                      <label htmlFor="save-to-account" className="selection-container">
                        <Checkbox
                          name={FIELDS.SAVE_TO_MY_ACCOUNT}
                          id="save-to-account"
                          checked={this.state.saveToAccount}
                          onChange={this.handleSaveToAccountChange}
                        />
                        <span className="checkbox-label">
                          <Text field={{ value: 'Save this address to' }} tag="span" />{' '}
                          <Text field={{ value: 'My Account.' }} tag="strong" />
                        </span>
                      </label>
                    </li>
                  </ul>
                  {!isLoggedIn && (
                    <Text
                      field={{ value: 'Create Account' }}
                      className="right-car create"
                      href="/login-register"
                      tag="a"
                    />
                  )}
                </div>
              )}
            </div>
          </FieldSet>
          <FieldSet>
            <div className="shipping-method-title">
              <Text field={{ value: 'Shipping Method' }} tag="h3" />
            </div>
            <div className="row">
              <div className="col-sm-12">
                <Select
                  fullWidth={true}
                  name={FIELDS.SELECTED_SHIPPING_METHOD}
                  defaultValue={shippingMethod}
                  required={true}
                  error={false}
                >
                  <option disabled={true} value="">
                    Select Option
                  </option>
                  {shippingInfo &&
                    shippingInfo.data &&
                    shippingInfo.data.shippingMethods &&
                    shippingInfo.data.shippingMethods.map((shippingMethod, index) => (
                      <option key={index} value={shippingMethod.externalId}>
                        {shippingMethod.name}
                      </option>
                    ))}
                </Select>
              </div>
              <div className="col-sm-12">
                <Submit
                  buttonTheme="default"
                  disabled={isSubmitting}
                  fullWidth={true}
                  onSubmitHandler={(formValues) => this.handleSaveAndContinueClick(formValues)}
                >
                  <Text field={{ value: 'Save & Continue' }} tag="span" />
                </Submit>
              </div>
            </div>
          </FieldSet>
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

    if (value === ADDRESS_TYPE.SAVED) {
      const { commerceUser } = this.props;
      const isLoggedIn = commerceUser && commerceUser.customerId;

      if (this.state.canResetDeliveryInfo && isLoggedIn) {
        this.props.ResetDeliveryInfo();

        if (!this.props.sitecoreContext.pageEditing) {
          this.props.InitStep(CheckoutStepType.Fulfillment);
        }

        this.setState({ canResetDeliveryInfo: false });
      }
    }
  }

  private handleUseForBillingAddressChange(e: FormEvent<HTMLInputElement>) {
    this.setState({ useForBillingAddress: Boolean(e.currentTarget.checked) });
  }

  private handleSaveToAccountChange(e: FormEvent<HTMLInputElement>) {
    this.setState({ saveToAccount: Boolean(e.currentTarget.checked) });
  }

  // tslint:disable-next-line:cognitive-complexity
  private handleSaveAndContinueClick(formValues: FormValues) {
    formValues[FIELDS.EMAIL] = this.state.email ? this.state.email : this.state.guestEmail;
    const formFields = validate(formValues, FIELDS);
    const { SubmitStep, shippingInfo, AddAddressToAccount } = this.props;
    const { selectedAddressOption } = this.state;
    const selectedShippingMethodId = formValues[FIELDS.SELECTED_SHIPPING_METHOD];
    const shippingMethod = shippingInfo.data.shippingMethods.find((a) => a.externalId === selectedShippingMethodId);

    const useForBillingAddress: boolean = formValues[FIELDS.USE_FOR_BILLING] as boolean;
    const saveToMyAccount: boolean = formValues[FIELDS.SAVE_TO_MY_ACCOUNT] as boolean;
    const fulfillmentType = '1';
    const options = {
      saveToMyAccount,
      useForBillingAddress,
      selectedAddressOption,
    };
    const address = this.getShippingAddress(formValues);
    if (isEmpty(formFields)) {
      if (saveToMyAccount) {
        AddAddressToAccount(address);
      }
    }
    if (address && shippingMethod) {
      SubmitStep({
        shipping: {
          address,
          fulfillmentType,
          options,
          saveToMyAccount,
          shippingMethod,
        },
      });
    }

    this.setState({ stateFormFields: formFields });
  }

  private getShippingAddress(formValues: FormValues) {
    const { deliveryInfo, commerceUser } = this.props;
    const { selectedAddressOption } = this.state;

    if (selectedAddressOption === ADDRESS_TYPE.SAVED) {
      const selectedAddress = formValues[FIELDS.SELECTED_ADDRESS];
      let address;

      if (deliveryInfo.data && deliveryInfo.data.userAddresses) {
        address = deliveryInfo.data.userAddresses.find((a) => a.partyId === selectedAddress);
      }

      address.email = commerceUser.email;

      return address;
    }

    if (selectedAddressOption === ADDRESS_TYPE.NEW) {
      const isLoggedIn = commerceUser && commerceUser.customerId;
      const selectedCountry = this.getSelectedCountry(formValues[FIELDS.COUNTRY] as string);

      return isLoggedIn
        ? {
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
          }
        : {
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
    }

    return null;
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
