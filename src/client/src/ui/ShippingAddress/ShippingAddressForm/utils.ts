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

import { get } from 'lodash';

import { validateValue, INPUT_REGEX, ADDRESS_LINE_REGEX, CITY_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

import { ADDRESS_MANAGER_FIELDS } from './constants';

const { FIRST_NAME, LAST_NAME, ADDRESS_LINE, CITY, POSTAL_CODE, COUNTRY, STATE } = ADDRESS_MANAGER_FIELDS;

const ERROR_MESSAGES_CONTENT = {
  FIRST_NAME: {
    IS_INVALID: 'First name is invalid',
    IS_REQUIRED: 'First name field is required',
  },
  LAST_NAME: {
    IS_INVALID: 'Last name is invalid',
    IS_REQUIRED: 'Last name field is required',
  },
  ADDRESS_LINE: {
    IS_INVALID: 'Address line is invalid',
    IS_REQUIRED: 'Address line field is required',
  },
  CITY: {
    IS_INVALID: 'City is invalid',
    IS_REQUIRED: 'City field is required',
  },
  POSTAL_CODE: {
    IS_INVALID: 'Postal code is invalid',
    IS_REQUIRED: 'Postal code field is required',
  },
  COUNTRY: {
    IS_REQUIRED: 'Country field is required',
  },
  STATE: {
    IS_REQUIRED: 'State field is required',
  },
};

export const validate = (formValues: FormValues) => {
  const firstName: string = get(formValues, [FIRST_NAME], null);
  const lastName: string = get(formValues, [LAST_NAME], null);
  const addressLine: string = get(formValues, [ADDRESS_LINE], null);
  const city: string = get(formValues, [CITY], null);
  const postalCode: string = get(formValues, [POSTAL_CODE], null);
  const country: string = get(formValues, [COUNTRY], null);
  const state: string = get(formValues, [STATE], null);

  const formFields = {};

  if (!country) {
    formFields['country'] = {
      hasError: true,
      message: ERROR_MESSAGES_CONTENT.COUNTRY.IS_REQUIRED,
    };
  }

  if (!state) {
    formFields['state'] = {
      hasError: true,
      message: ERROR_MESSAGES_CONTENT.STATE.IS_REQUIRED,
    };
  }

  if (!postalCode) {
    formFields['postalCode'] = {
      hasError: true,
      message: ERROR_MESSAGES_CONTENT.POSTAL_CODE.IS_REQUIRED,
    };
  }

  if (!validateValue(INPUT_REGEX, firstName)) {
    formFields['firstName'] = {
      hasError: true,
      message: firstName ? ERROR_MESSAGES_CONTENT.FIRST_NAME.IS_INVALID : ERROR_MESSAGES_CONTENT.FIRST_NAME.IS_REQUIRED,
    };
  }

  if (!validateValue(INPUT_REGEX, lastName)) {
    formFields['lastName'] = {
      hasError: true,
      message: lastName ? ERROR_MESSAGES_CONTENT.LAST_NAME.IS_INVALID : ERROR_MESSAGES_CONTENT.LAST_NAME.IS_REQUIRED,
    };
  }

  if (!validateValue(ADDRESS_LINE_REGEX, addressLine)) {
    formFields['addressLine'] = {
      hasError: true,
      message: addressLine
        ? ERROR_MESSAGES_CONTENT.ADDRESS_LINE.IS_INVALID
        : ERROR_MESSAGES_CONTENT.ADDRESS_LINE.IS_REQUIRED,
    };
  }

  if (!validateValue(CITY_REGEX, city)) {
    formFields['city'] = {
      hasError: true,
      message: city ? ERROR_MESSAGES_CONTENT.CITY.IS_INVALID : ERROR_MESSAGES_CONTENT.CITY.IS_REQUIRED,
    };
  }

  return formFields;
};
