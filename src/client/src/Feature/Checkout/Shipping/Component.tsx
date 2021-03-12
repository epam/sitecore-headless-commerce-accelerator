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

import * as Jss from 'Foundation/ReactJss';

import { Checkbox, DependentField, FieldSet, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';

import { CheckoutStepType } from 'Feature/Checkout/Integration/Checkout';

import { AddressOptions } from './AddressOptions';
import { ADDRESS_TYPE, FIELDS } from './constants';
import { ShippingProps, ShippingState } from './models';

import './styles.scss';

export default class ShippingComponent extends Jss.SafePureComponent<ShippingProps, ShippingState> {
  public constructor(props: ShippingProps) {
    super(props);
    const { commerceUser } = this.props;
    const selectedAddressOption = commerceUser && commerceUser.customerId ? ADDRESS_TYPE.SAVED : ADDRESS_TYPE.NEW;
    this.state = {
      canResetDeliveryInfo: true,
      containsGift: false,
      email: '',
      saveToAccount: false,
      selectedAddressOption,
      useForBillingAddress: false,
    };

    this.handleAddressOptionChange = this.handleAddressOptionChange.bind(this);
    this.handleUseForBillingAddressChange = this.handleUseForBillingAddressChange.bind(this);
    this.handleContainsGiftChange = this.handleContainsGiftChange.bind(this);
    this.handleSaveToAccountChange = this.handleSaveToAccountChange.bind(this);
  }

  public componentDidMount() {
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
    const { deliveryInfo, isSubmitting, isLoading, fields, shippingInfo, commerceUser } = this.props;
    const { email } = this.state;
    const isLoggedIn = commerceUser && commerceUser.customerId;

    return (
      <Form>
        {(isLoading || isSubmitting) && (
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
                <Input name={FIELDS.FIRST_NAME} type="text" required={true} maxLength={100} fullWidth={true} />
              </div>
              <div className="col-ms-6">
                <Text field={{ value: 'Last Name' }} tag="label" className="input-title required" />
                <Input name={FIELDS.LAST_NAME} type="text" required={true} maxLength={100} fullWidth={true} />
              </div>
            </div>
            <div className="row">
              <div className="col-ms-6">
                <Text field={{ value: 'Address Line 1' }} tag="label" className="input-title required" />
                <Input name={FIELDS.ADDRESS_LINE} type="text" required={true} maxLength={100} fullWidth={true} />
              </div>
              <div className="col-ms-6">
                <Text field={{ value: 'City' }} tag="label" className="input-title required" />
                <Input name={FIELDS.CITY} type="text" required={true} maxLength={100} fullWidth={true} />
              </div>
            </div>
            <div className="row">
              <div className="col-ms-6">
                <Text field={{ value: 'Country' }} tag="label" className="input-title required" />
                <Select fullWidth={true} name={FIELDS.COUNTRY} required={true}>
                  <option value="">Not Selected</option>
                  {fields.countries.map((country, index) => (
                    <option key={`${index}-${country.countryCode}`} value={country.countryCode}>
                      {country.name}
                    </option>
                  ))}
                </Select>
              </div>
              <div className="col-ms-6">
                <Text field={{ value: 'Province' }} tag="label" className="input-title required" />
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
                <Text field={{ value: 'Postal Code' }} tag="label" className="input-title required" />
                <Input name={FIELDS.POSTAL_CODE} type="text" required={true} maxLength={100} fullWidth={true} />
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
                    value={email}
                    maxLength={100}
                    fullWidth={true}
                  />
                  <Text field={{ value: 'For order status and updates' }} tag="sub" />
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
                <Select fullWidth={true} name={FIELDS.SELECTED_ADDRESS} defaultValue="" required={true}>
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
                  <li>
                    <label htmlFor="contains-gift" className="selection-container">
                      <Checkbox
                        name={FIELDS.CONTAINS_GIFT}
                        id="contains-gift"
                        checked={this.state.containsGift}
                        onChange={this.handleContainsGiftChange}
                      />
                      <span className="checkbox-label">This order contains a gift</span>
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
                      href="/account/login-register"
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
                <Select fullWidth={true} name={FIELDS.SELECTED_SHIPPING_METHOD} defaultValue="" required={true}>
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
                  buttonTheme="grey"
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

  private handleContainsGiftChange(e: FormEvent<HTMLInputElement>) {
    this.setState({ containsGift: Boolean(e.currentTarget.checked) });
  }

  private handleSaveToAccountChange(e: FormEvent<HTMLInputElement>) {
    this.setState({ saveToAccount: Boolean(e.currentTarget.checked) });
  }

  // tslint:disable-next-line:cognitive-complexity
  private handleSaveAndContinueClick(formValues: FormValues) {
    const { SubmitStep, shippingInfo, AddAddressToAccount } = this.props;
    const selectedShippingMethodId = formValues[FIELDS.SELECTED_SHIPPING_METHOD];
    const shippingMethod = shippingInfo.data.shippingMethods.find((a) => a.externalId === selectedShippingMethodId);

    const useForBillingAddress: boolean = formValues[FIELDS.USE_FOR_BILLING] as boolean;
    const saveToMyAccount: boolean = formValues[FIELDS.SAVE_TO_MY_ACCOUNT] as boolean;
    const fulfillmentType = '1';
    const options = {
      saveToMyAccount,
      useForBillingAddress,
    };
    const address = this.getShippingAddress(formValues);
    if (saveToMyAccount) {
      AddAddressToAccount(address);
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
            email: commerceUser.email as string,
            externalId: '',
            firstName: formValues[FIELDS.FIRST_NAME] as string,
            isPrimary: false,
            lastName: formValues[FIELDS.LAST_NAME] as string,
            name: '',
            partyId: '',
            state: formValues[FIELDS.PROVINCE] as string,
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
            isPrimary: false,
            lastName: formValues[FIELDS.LAST_NAME] as string,
            name: '',
            partyId: '',
            state: formValues[FIELDS.PROVINCE] as string,
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
