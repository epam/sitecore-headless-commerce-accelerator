//    Copyright 2022 EPAM Systems, Inc.
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
import * as Jss from 'Foundation/ReactJss';

import { get, isEmpty } from 'lodash';

import { DependentField, Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';
import { Button, Select as PureSelect } from 'components';
import { CountryRegion } from 'services/commerce';

import { ADDRESS_MANAGER_FIELDS } from './constants';
import { ShippingAddressFormProps } from './models';
import { validate } from './utils';

import { cnShippingAddressForm } from './cn';
import './ShippingAddressForm.scss';

const SHIPPING_ADDRESS_FORM_CONTENT = {
  NOT_SELECTED: 'Not Selected',
  SUBMIT: 'Submit',
  CANCEL: 'Cancel',
};

export const ShippingAddressForm = (props: ShippingAddressFormProps) => {
  const { countries, defaultValues, submitAction, toggleForm } = props;
  const [stateFormFields, setStateFormFields] = useState({});

  const firstName = defaultValues ? defaultValues.firstName : '';
  const lastName = defaultValues ? defaultValues.lastName : '';
  const address1 = defaultValues ? defaultValues.address1 : '';
  const city = defaultValues ? defaultValues.city : '';
  const zipPostalCode = defaultValues ? defaultValues.zipPostalCode : '';
  const state = defaultValues ? defaultValues.state : '';
  const country = defaultValues ? defaultValues.countryCode : '';

  const getSelectedCountry = (selectedCountryCode: string) => {
    return countries.find((c: CountryRegion) => c.countryCode === selectedCountryCode);
  };

  const submitAddressForm = (formValues: FormValues) => {
    const formFields = validate(formValues);
    const selectedCountry = getSelectedCountry(formValues[ADDRESS_MANAGER_FIELDS.COUNTRY] as string);

    if (isEmpty(formFields)) {
      const address = {
        ...defaultValues,
        address1: formValues[ADDRESS_MANAGER_FIELDS.ADDRESS_LINE] as string,
        city: formValues[ADDRESS_MANAGER_FIELDS.CITY] as string,
        country: formValues[ADDRESS_MANAGER_FIELDS.COUNTRY] as string,
        countryCode: selectedCountry.countryCode,
        firstName: formValues[ADDRESS_MANAGER_FIELDS.FIRST_NAME] as string,
        lastName: formValues[ADDRESS_MANAGER_FIELDS.LAST_NAME] as string,
        state: formValues[ADDRESS_MANAGER_FIELDS.STATE] as string,
        zipPostalCode: formValues[ADDRESS_MANAGER_FIELDS.POSTAL_CODE] as string,
      };

      submitAction(address);
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

  const handlerFocusField = (field: string) => {
    setStateFormFields((value) => ({
      ...value,
      [field]: {
        hasError: false,
      },
    }));
  };

  return (
    <div className={cnShippingAddressForm()}>
      <Form className={cnShippingAddressForm('Form')}>
        <div className="row">
          <div className={cnShippingAddressForm('FormField col-lg-12 col-md-12')}>
            <Jss.Text field={{ value: 'First Name:', editable: 'First Name *' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.FIRST_NAME}
              type="text"
              maxLength={100}
              defaultValue={firstName}
              fullWidth={true}
              error={get(stateFormFields, ['firstName', 'hasError'], false)}
              helperText={get(stateFormFields, ['firstName', 'message'])}
              handlerFocusField={() => handlerFocusField('firstName')}
            />
          </div>
          <div className={cnShippingAddressForm('FormField col-lg-12 col-md-12')}>
            <Jss.Text field={{ value: 'Last Name:', editable: 'Last Name *' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.LAST_NAME}
              type="text"
              maxLength={100}
              defaultValue={lastName}
              fullWidth={true}
              error={get(stateFormFields, ['lastName', 'hasError'], false)}
              helperText={get(stateFormFields, ['lastName', 'message'])}
              handlerFocusField={() => handlerFocusField('lastName')}
            />
          </div>
          <div className={cnShippingAddressForm('FormField col-lg-12 col-md-12')}>
            <Jss.Text field={{ value: 'Address Line:', editable: 'Address Line *' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.ADDRESS_LINE}
              type="text"
              maxLength={100}
              defaultValue={address1}
              fullWidth={true}
              error={get(stateFormFields, ['addressLine', 'hasError'], false)}
              helperText={get(stateFormFields, ['addressLine', 'message'])}
              handlerFocusField={() => handlerFocusField('addressLine')}
            />
          </div>
          <div className={cnShippingAddressForm('FormField col-lg-12 col-md-12')}>
            <Jss.Text field={{ value: 'City:', editable: 'City *' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.CITY}
              type="text"
              maxLength={100}
              defaultValue={city}
              fullWidth={true}
              error={get(stateFormFields, ['city', 'hasError'], false)}
              helperText={get(stateFormFields, ['city', 'message'])}
              handlerFocusField={() => handlerFocusField('city')}
            />
          </div>
          <div className="col-lg-6 col-md-6 select-country-state">
            <Jss.Text field={{ value: 'Country:', editable: 'Country *' }} tag="label" className="required" />
            <Select
              fullWidth={true}
              name={ADDRESS_MANAGER_FIELDS.COUNTRY}
              defaultValue={country}
              className="Select_address_form"
              error={get(stateFormFields, ['country', 'hasError'], false)}
              helperText={get(stateFormFields, ['country', 'message'])}
            >
              <option value="">{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
              {countries.map((c, index) => (
                <option key={index} value={c.countryCode}>
                  {c.name}
                </option>
              ))}
            </Select>
          </div>
          <div className="col-lg-6 col-md-6 select-country-state">
            <Jss.Text field={{ value: 'State:', editable: 'State *' }} tag="label" className="required" />
            <DependentField>
              {(form) =>
                form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] ? (
                  <Select
                    fullWidth={true}
                    name={ADDRESS_MANAGER_FIELDS.STATE}
                    defaultValue={state}
                    error={get(stateFormFields, ['state', 'hasError'], false)}
                    helperText={get(stateFormFields, ['state', 'message'])}
                  >
                    <option value="">{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                    {renderSubdivisions(form.values[ADDRESS_MANAGER_FIELDS.COUNTRY] as string)}
                  </Select>
                ) : (
                  <PureSelect disabled={true} fullWidth={true}>
                    <option>{SHIPPING_ADDRESS_FORM_CONTENT.NOT_SELECTED}</option>
                  </PureSelect>
                )
              }
            </DependentField>
          </div>
          <div className={cnShippingAddressForm('FormField col-ms-12')}>
            <Jss.Text field={{ value: 'Postal Code:', editable: 'Postal Code *' }} tag="label" className="required" />
            <Input
              name={ADDRESS_MANAGER_FIELDS.POSTAL_CODE}
              type="text"
              maxLength={100}
              defaultValue={zipPostalCode}
              fullWidth={true}
              error={get(stateFormFields, ['postalCode', 'hasError'], false)}
              helperText={get(stateFormFields, ['postalCode', 'message'])}
              handlerFocusField={() => handlerFocusField('postalCode')}
            />
          </div>
        </div>
        <div className={cnShippingAddressForm('SubmitContainer')}>
          <Submit
            className={cnShippingAddressForm('SubmitButton')}
            buttonTheme="default"
            onSubmitHandler={(formValues: FormValues) => {
              submitAddressForm(formValues);
            }}
          >
            {SHIPPING_ADDRESS_FORM_CONTENT.SUBMIT}
          </Submit>
          <Button className={cnShippingAddressForm('CancelButton')} buttonTheme="default" onClick={() => toggleForm()}>
            {SHIPPING_ADDRESS_FORM_CONTENT.CANCEL}
          </Button>
        </div>
      </Form>
    </div>
  );
};
