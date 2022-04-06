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

import React, { useState } from 'react';

import { get, isEmpty } from 'lodash';
import * as Jss from 'Foundation/ReactJss';

import { Button, Select as PureSelect } from 'components';
import { DependentField, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';
import { validate } from './utils';
import { ADDRESS_MANAGER_FIELDS } from './constants';
import { AddressFromProps } from './models';

import { cnAddressForm } from './cn';
import './AddressForm.scss';

export const AddressForm = (props: AddressFromProps) => {
  const { countries, defaultValues, SubmitAction } = props;
  const [stateFormFields, setStateFormFields] = useState({});

  const firstName = defaultValues ? defaultValues.firstName : '';
  const lastName = defaultValues ? defaultValues.lastName : '';
  const address1 = defaultValues ? defaultValues.address1 : '';
  const city = defaultValues ? defaultValues.city : '';
  const zipPostalCode = defaultValues ? defaultValues.zipPostalCode : '';

  const getSelectedCountry = (selectedCountryCode: string) => {
    return countries.find((c) => c.countryCode === selectedCountryCode);
  };

  const submitAddressForm = (formValues: FormValues) => {
    const formFields = validate(formValues);
    const selectedCountry = getSelectedCountry(formValues[ADDRESS_MANAGER_FIELDS.COUNTRY] as string);

    if (isEmpty(formFields)) {
      const address = {
        ...defaultValues,
        address1: formValues[ADDRESS_MANAGER_FIELDS.ADDRESS_LINE] as string,
        city: formValues[ADDRESS_MANAGER_FIELDS.CITY] as string,
        country: selectedCountry.name,
        countryCode: selectedCountry.countryCode,
        firstName: formValues[ADDRESS_MANAGER_FIELDS.FIRST_NAME] as string,
        lastName: formValues[ADDRESS_MANAGER_FIELDS.LAST_NAME] as string,
        state: formValues[ADDRESS_MANAGER_FIELDS.PROVINCE] as string,
        zipPostalCode: formValues[ADDRESS_MANAGER_FIELDS.POSTAL_CODE] as string,
      };

      SubmitAction(address);
    }
    setStateFormFields(formFields);
  };

  const renderSubdivisions = (countryCode: string) => {
    const selectedCountry = getSelectedCountry(countryCode);

    if (!selectedCountry) {
      return null;
    }

    return selectedCountry.subdivisions.map((state, index) => (
      <option key={index} value={state.code}>
        {state.name}
      </option>
    ));
  };

  return (
    <div className={cnAddressForm('account-details-form_main active')}>
      <div className="account-details-form_main_container">
        <div className="form-title">
          <h4>{defaultValues ? 'EDIT' : 'ADD NEW'} ADDRESS</h4>
          <h5>Your Address</h5>
        </div>
        <Form>
          <div className="row">
            <div className={cnAddressForm('FormField col-lg-12 col-md-12')}>
              <Jss.Text field={{ value: 'First Name:', editable: 'First Name' }} tag="label" className="required" />
              <Input
                name={ADDRESS_MANAGER_FIELDS.FIRST_NAME}
                type="text"
                required={true}
                maxLength={100}
                defaultValue={firstName}
                fullWidth={true}
                error={get(stateFormFields, ['firstName', 'hasError'], false)}
                helperText={get(stateFormFields, ['firstName', 'message'])}
              />
            </div>
            <div className={cnAddressForm('FormField col-lg-12 col-md-12')}>
              <Jss.Text field={{ value: 'Last Name:', editable: 'Last Name' }} tag="label" className="required" />
              <Input
                name={ADDRESS_MANAGER_FIELDS.LAST_NAME}
                type="text"
                required={true}
                maxLength={100}
                defaultValue={lastName}
                fullWidth={true}
                error={get(stateFormFields, ['lastName', 'hasError'], false)}
                helperText={get(stateFormFields, ['lastName', 'message'])}
              />
            </div>
            <div className={cnAddressForm('FormField col-lg-12 col-md-12')}>
              <Jss.Text
                field={{ value: 'Address Line:', editable: 'Address Line:' }}
                tag="label"
                className="required"
              />
              <Input
                name={ADDRESS_MANAGER_FIELDS.ADDRESS_LINE}
                type="text"
                required={true}
                maxLength={100}
                defaultValue={address1}
                fullWidth={true}
                error={get(stateFormFields, ['addressLine', 'hasError'], false)}
                helperText={get(stateFormFields, ['addressLine', 'message'])}
              />
            </div>
            <div className={cnAddressForm('FormField col-lg-12 col-md-12')}>
              <Jss.Text field={{ value: 'City:', editable: 'City:' }} tag="label" className="required" />
              <Input
                name={ADDRESS_MANAGER_FIELDS.CITY}
                type="text"
                required={true}
                maxLength={100}
                defaultValue={city}
                fullWidth={true}
                error={get(stateFormFields, ['city', 'hasError'], false)}
                helperText={get(stateFormFields, ['city', 'message'])}
              />
            </div>
            <div className="col-lg-6 col-md-6 select-country-state">
              <Jss.Text field={{ value: 'Country:', editable: 'Country' }} tag="label" className="required" />
              <Select
                fullWidth={true}
                name={ADDRESS_MANAGER_FIELDS.COUNTRY}
                required={true}
                defaultValue={defaultValues.countryCode}
                className="Select_address_form"
              >
                <option value="">Not Selected</option>
                {countries.map((c, index) => (
                  <option key={index} value={c.countryCode}>
                    {c.name}
                  </option>
                ))}
              </Select>
            </div>
            <div className="col-lg-6 col-md-6 select-country-state">
              <Jss.Text field={{ value: 'State:', editable: 'State' }} tag="label" className="required" />
              <DependentField>
                {(form) =>
                  form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] ? (
                    <Select
                      fullWidth={true}
                      name={ADDRESS_MANAGER_FIELDS.PROVINCE}
                      required={true}
                      defaultValue={defaultValues.state}
                    >
                      <option value="">Not Selected</option>
                      {renderSubdivisions(form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] as string)}
                    </Select>
                  ) : (
                    <PureSelect disabled={true} fullWidth={true}>
                      <option>Not Selected</option>
                    </PureSelect>
                  )
                }
              </DependentField>
            </div>
            <div className={cnAddressForm('FormField col-ms-12')}>
              <Jss.Text field={{ value: 'Postal Code:', editable: 'Postal Code' }} tag="label" className="required" />
              <Input
                name={ADDRESS_MANAGER_FIELDS.POSTAL_CODE}
                type="text"
                required={true}
                maxLength={100}
                defaultValue={zipPostalCode}
                fullWidth={true}
                error={get(stateFormFields, ['postalCode', 'hasError'], false)}
                helperText={get(stateFormFields, ['postalCode', 'message'])}
              />
            </div>
          </div>
          <div className="submit-container">
            <Submit buttonTheme="grey" onSubmitHandler={(formValues: FormValues) => submitAddressForm(formValues)}>
              Submit
            </Submit>
            <Button className="AddressManager-CancelButton" buttonTheme="grey" onClick={props.ToggleForm}>
              Cancel
            </Button>
          </div>
        </Form>
      </div>
    </div>
  );
};
