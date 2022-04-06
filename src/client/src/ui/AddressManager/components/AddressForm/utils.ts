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

import { validateValue, INPUT_REGEX, ADDRESS_LINE_REGEX, CITY_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

import { ADDRESS_MANAGER_FIELDS } from './constants';

const { FIRST_NAME, LAST_NAME, ADDRESS_LINE, CITY, POSTAL_CODE } = ADDRESS_MANAGER_FIELDS;

export const validate = (formValues: FormValues) => {
  const firstName: string = get(formValues, [FIRST_NAME], null);
  const lastName: string = get(formValues, [LAST_NAME], null);
  const addressLine: string = get(formValues, [ADDRESS_LINE], null);
  const city: string = get(formValues, [CITY], null);
  const postalCode: string = get(formValues, [POSTAL_CODE], null);
  const formFields = {};

  if (!postalCode) {
    formFields['postalCode'] = {
      hasError: true,
      message: 'Postal code field is required',
    };
  }

  if (!validateValue(INPUT_REGEX, firstName)) {
    formFields['firstName'] = {
      hasError: true,
      message: firstName ? 'First name is invalid' : 'First name field is required',
    };
  }

  if (!validateValue(INPUT_REGEX, lastName)) {
    formFields['lastName'] = {
      hasError: true,
      message: lastName ? 'Last name is invalid' : 'Last name field is required',
    };
  }

  if (!validateValue(ADDRESS_LINE_REGEX, addressLine)) {
    formFields['addressLine'] = {
      hasError: true,
      message: addressLine ? 'Address line is invalid' : 'Address line field is required',
    };
  }

  if (!validateValue(CITY_REGEX, city)) {
    formFields['city'] = {
      hasError: true,
      message: city ? 'City is invalid' : 'City field is required',
    };
  }

  return formFields;
};
