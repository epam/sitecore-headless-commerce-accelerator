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
import { DependentField, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/client/Form';

import { ADDRESS_MANAGER_FIELDS } from './constants';
import { AddressFromProps } from './models';

import './styles.scss';

export default class AddressForm extends React.Component<AddressFromProps> {
  public render() {
    const { countries, defaultValues } = this.props;

    const firstName = defaultValues ? defaultValues.firstName : '';
    const lastName = defaultValues ? defaultValues.lastName : '';
    const address1 = defaultValues ? defaultValues.address1 : '';
    const city = defaultValues ? defaultValues.city : '';
    const zipPostalCode = defaultValues ? defaultValues.zipPostalCode : '';

    return (
      <Form className="address-form">
        <div className="row">
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'First Name:', editable: 'First Name' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.FIRST_NAME}
              type="text"
              required={true}
              maxLength={100}
              defaultValue={firstName}
            />
          </div>
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'Last Name:', editable: 'Last Name' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.LAST_NAME}
              type="text"
              required={true}
              maxLength={100}
              defaultValue={lastName}
            />
          </div>
        </div>
        <div className="row">
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'Address Line:', editable: 'Address Line:' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.ADDRESS_LINE}
              type="text"
              required={true}
              maxLength={100}
              defaultValue={address1}
            />
          </div>
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'City:', editable: 'City:' }} tag="label" className="required" />
            <Input name={ADDRESS_MANAGER_FIELDS.CITY} type="text" required={true} maxLength={100} defaultValue={city} />
          </div>
        </div>
        <div className="row">
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'Country:', editable: 'Country' }} tag="label" className="required" />
            <Select name={ADDRESS_MANAGER_FIELDS.COUNTRY} required={true} defaultValue={defaultValues.countryCode}>
              <option value="">Not Selected</option>
              {countries.map((c, index) => (
                <option key={index} value={c.countryCode}>
                  {c.name}
                </option>
              ))}
            </Select>
          </div>
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'State:', editable: 'State' }} tag="label" className="required" />
            <DependentField>
              {(form) => form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] ? (
                <Select name={ADDRESS_MANAGER_FIELDS.PROVINCE} required={true} defaultValue={defaultValues.state}>
                  <option value="">Not Selected</option>
                  {this.renderSubdivisions(form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] as string)}
                </Select>
              ) : (
                <select disabled={true}>
                  <option>Not Selected</option>
                </select>
              )}
            </DependentField>
          </div>
        </div>
        <div className="row">
          <div className="col-ms-6">
            <Jss.Text field={{ value: 'Postal Code:', editable: 'Postal Code' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.POSTAL_CODE}
              type="text"
              required={true}
              maxLength={100}
              defaultValue={zipPostalCode}
            />
          </div>
        </div>
        <Submit className="btn btn-outline-main" onSubmitHandler={(formValues) => this.submitAddressForm(formValues)}>
          Submit
        </Submit>
        <button className="btn btn-outline-focus" onClick={() => this.props.ToggleForm()}>
          Cancel
        </button>
      </Form>
    );
  }

  private submitAddressForm(formValues: FormValues) {
    const { defaultValues, SubmitAction } = this.props;
    const selectedCountry = this.getSelectedCountry(formValues[ADDRESS_MANAGER_FIELDS.COUNTRY] as string);

    const address = {
      ...defaultValues,
      address1: formValues[ADDRESS_MANAGER_FIELDS.ADDRESS_LINE] as string,
      city: formValues[ADDRESS_MANAGER_FIELDS.CITY] as string,
      country: selectedCountry.name,
      countryCode: selectedCountry.countryCode,
      firstName: formValues[ADDRESS_MANAGER_FIELDS.FIRST_NAME] as string,
      isPrimary: false,
      lastName: formValues[ADDRESS_MANAGER_FIELDS.LAST_NAME] as string,
      state: formValues[ADDRESS_MANAGER_FIELDS.PROVINCE] as string,
      zipPostalCode: formValues[ADDRESS_MANAGER_FIELDS.POSTAL_CODE] as string,
    };

    SubmitAction(address);
  }

  private getSelectedCountry(selectedCountryCode: string) {
    const { countries } = this.props;

    return countries.find((c) => c.countryCode === selectedCountryCode);
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
