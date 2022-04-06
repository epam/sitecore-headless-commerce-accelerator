//    Copyright 2021 EPAM Systems, Inc.
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

import { get } from 'lodash';

import { validateValue, INPUT_REGEX, ADDRESS_LINE_REGEX, CITY_REGEX, EMAIL_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

export const FIELD_TYPES = {
  FIRST_NAME: 'firstName',
  LAST_NAME: 'lastName',
  ADDRESS_LINE: 'addressLine',
  CITY: 'city',
  POSTAL_CODE: 'postalCode',
  EMAIL: 'email',
};

export const validate = (formValues: FormValues, FIELDS: any) => {
  const { FIRST_NAME, LAST_NAME, ADDRESS_LINE, CITY, POSTAL_CODE, EMAIL } = FIELDS;
  const firstName: string = get(formValues, [FIRST_NAME], null);
  const lastName: string = get(formValues, [LAST_NAME], null);
  const addressLine: string = get(formValues, [ADDRESS_LINE], null);
  const city: string = get(formValues, [CITY], null);
  const email = get(formValues, [EMAIL], null);
  const postalCode: string = get(formValues, [POSTAL_CODE], null);

  const formFields = {};

  if (!postalCode) {
    formFields['postalCode'] = {
      hasError: true,
      message: 'Postal code field is required',
    };
  }

  if (!validateValue(EMAIL_REGEX, email)) {
    formFields[FIELD_TYPES.EMAIL] = {
      hasError: true,
      message: email && 'Email is invalid',
    };
  }

  if (!validateValue(INPUT_REGEX, firstName)) {
    formFields[FIELD_TYPES.FIRST_NAME] = {
      hasError: true,
      message: firstName && 'First name is invalid',
    };
  }

  if (!validateValue(INPUT_REGEX, lastName)) {
    formFields[FIELD_TYPES.LAST_NAME] = {
      hasError: true,
      message: lastName && 'Last name is invalid',
    };
  }

  if (!validateValue(ADDRESS_LINE_REGEX, addressLine)) {
    formFields[FIELD_TYPES.ADDRESS_LINE] = {
      hasError: true,
      message: addressLine && 'Address line is invalid',
    };
  }

  if (!validateValue(CITY_REGEX, city)) {
    formFields[FIELD_TYPES.CITY] = {
      hasError: true,
      message: city && 'City is invalid',
    };
  }

  return formFields;
};
