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
import { ShippingProps, ShippingState } from './models';

import './styles.scss';

export default class ShippingComponent extends Jss.SafePureComponent<ShippingProps, ShippingState> {
  public constructor(props: ShippingProps) {
    super(props);
    const { commerceUser } = this.props;
    const selectedAddressOption = commerceUser && commerceUser.customerId ? ADDRESS_TYPE.SAVED : ADDRESS_TYPE.NEW;
    const disableSaveToMyAccount = !(commerceUser && commerceUser.customerId);
    this.state = {
      disableSaveToMyAccount,
      selectedAddressOption,
    };
  }

  public componentDidMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.InitStep(CheckoutStepType.Fulfillment);
    }
  }

  // tslint:disable-next-line: no-big-function
  protected safeRender() {
    const { deliveryInfo, isSubmitting, isLoading, fields, shippingInfo, commerceUser } = this.props;

    return (
      <Form>
        {(isLoading || isSubmitting) && (
          <div className="shipping-loading-overlay">
            <div className="loading" />
          </div>
        )}
        <section className="shipping">
          <Text field={{ value: 'Shipping' }} tag="h1" />
          <FieldSet>
            <Text field={{ value: 'Ship To:' }} tag="h2" />
            <div className="row">
              <div className="col-md-12">
                <ul className="options">
                  <li>
                    <Input
                      type="radio"
                      id="r1"
                      name={FIELDS.ADDRESS_TYPE}
                      defaultChecked={this.state.selectedAddressOption === ADDRESS_TYPE.NEW}
                      defaultValue={ADDRESS_TYPE.NEW}
                      onChange={(e) => this.handleAddressOptionChange(e)}
                    />
                    <Text field={{ value: 'A New Address' }} tag="label" htmlFor="r1" />
                  </li>
                  <li>
                    <Input
                      type="radio"
                      id="r2"
                      name={FIELDS.ADDRESS_TYPE}
                      defaultChecked={this.state.selectedAddressOption === ADDRESS_TYPE.SAVED}
                      defaultValue={ADDRESS_TYPE.SAVED}
                      onChange={(e) => this.handleAddressOptionChange(e)}
                    />
                    <Text field={{ value: 'A Saved Address' }} tag="label" htmlFor="r2" />
                  </li>
                </ul>
              </div>
            </div>
          </FieldSet>
          <FieldSet
            customVisibility={(formValues) => {
              return this.state.selectedAddressOption === ADDRESS_TYPE.NEW;
            }}
          >
            <Text field={{ value: 'Shipping Location:' }} tag="h2" />
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
                <Select name={FIELDS.COUNTRY} type="text" required={true}>
                  <option value="">Not Selected</option>
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
          </FieldSet>
          <FieldSet
            customVisibility={(formValues) => {
              return this.state.selectedAddressOption === ADDRESS_TYPE.SAVED;
            }}
          >
            <div className="row">
              <div className="col-sm-12">
                <Text field={{ value: 'Shipping Location:' }} tag="h2" />
                <Select name={FIELDS.SELECTED_ADDRESS} defaultValue="" required={true}>
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
                    <Input type="checkbox" name={FIELDS.USE_FOR_BILLING} id="use-for-billing" />
                    <label htmlFor="use-for-billing">Also use for billing address</label>
                  </li>
                  <li>
                    <Input type="checkbox" name={FIELDS.CONTAINS_GIFT} id="contains-gift" />
                    <label htmlFor="contains-gift">This order contains a gift</label>
                  </li>
                </ul>
              </div>
              {this.state.selectedAddressOption === ADDRESS_TYPE.NEW && (
                <div className="col-sm-6">
                  <Input type="checkbox" name={FIELDS.SAVE_TO_MY_ACCOUNT} id="save-to-account"  disabled={this.state.disableSaveToMyAccount} />
                  <label htmlFor="save-to-account">
                    <Text field={{ value: 'Save this address to' }} tag="span" />{' '}
                    <Text field={{ value: 'My Account.' }} tag="strong" />
                  </label>
                  <br />
                  <Text
                    field={{ value: 'Create Account' }}
                    className="right-car create"
                    href="/account/sign-up"
                    tag="a"
                  />
                </div>
              )}
            </div>
          </FieldSet>
          <FieldSet>
            <Text field={{ value: 'Shipping Method:' }} tag="h2" />
            <div className="row">
              <div className="col-sm-12">
                <Select name={FIELDS.SELECTED_SHIPPING_METHOD} defaultValue="" required={true}>
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
                  disabled={isSubmitting}
                  className="btn animated btn-animated-main"
                  onSubmitHandler={(formValues) => this.handleSaveAndContinueClick(formValues)}
                >
                  <Text field={{ value: 'Save & Continue' }} tag="span" />
                </Submit>
              </div>
            </div>
          </FieldSet>
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
          shippingMethod,
        },
      });
    }
  }

  private getShippingAddress(formValues: FormValues) {
    const { deliveryInfo } = this.props;

    const addressTypeValue: string = formValues[FIELDS.ADDRESS_TYPE] as string;

    if (addressTypeValue === ADDRESS_TYPE.SAVED) {
      const selectedAddress = formValues[FIELDS.SELECTED_ADDRESS];
      return (
        deliveryInfo.data &&
        deliveryInfo.data.userAddresses &&
        deliveryInfo.data.userAddresses.find((a) => a.partyId === selectedAddress)
      );
    }

    if (addressTypeValue === ADDRESS_TYPE.NEW) {
      const selectedCountry = this.getSelectedCountry(formValues[FIELDS.COUNTRY] as string);
      return {
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
