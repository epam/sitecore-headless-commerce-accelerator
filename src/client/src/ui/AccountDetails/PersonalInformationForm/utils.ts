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

import { validateValue, PHONE_NUMBER_REGEX, DATE_OF_BIRTH_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

export const FORM_FIELDS_KEYS = {
  FIRST_NAME: 'firstName',
  LAST_NAME: 'lastName',
  DATE_OF_BIRTH: 'dateOfBirth',
  PHONE_NUMBER: 'phoneNumber',
};

const SERVER_ERRORS_KEYS = {
  DATE: 'date',
};

const PHONE_NUMBER_PREFIX = '+';

export const validate = (formValues: FormValues) => {
  const firstName: string = get(formValues, [FORM_FIELDS_KEYS.FIRST_NAME], null);
  const lastName: string = get(formValues, [FORM_FIELDS_KEYS.LAST_NAME], null);
  const phoneNumber: string = get(formValues, [FORM_FIELDS_KEYS.PHONE_NUMBER], null);
  const dateOfBirth: string = get(formValues, [FORM_FIELDS_KEYS.DATE_OF_BIRTH], null);

  const formFields = {};

  if (!firstName) {
    formFields[FORM_FIELDS_KEYS.FIRST_NAME] = {
      hasError: true,
      message: 'Please, specify the value',
    };
  }

  if (!lastName) {
    formFields[FORM_FIELDS_KEYS.LAST_NAME] = {
      hasError: true,
      message: 'Please, specify the value',
    };
  }

  if (phoneNumber && !validateValue(PHONE_NUMBER_REGEX, phoneNumber) && phoneNumber !== '+') {
    formFields[FORM_FIELDS_KEYS.PHONE_NUMBER] = {
      hasError: true,
      message: 'Please, specify only numbers',
    };
  }

  if (dateOfBirth && !validateValue(DATE_OF_BIRTH_REGEX, dateOfBirth)) {
    formFields[FORM_FIELDS_KEYS.DATE_OF_BIRTH] = {
      hasError: true,
      message: 'Please, specify the date in a format DD/MM/YYYY',
    };
  }

  return formFields;
};

export const isPhoneNumberEmpty = (phoneNumber: string) => {
  return phoneNumber === PHONE_NUMBER_PREFIX;
};

export const setServerErrors = (errorMessage: string) => {
  const firstWord = errorMessage.split(' ', 1)[0].toLowerCase();
  const formFields = {};

  if (firstWord === SERVER_ERRORS_KEYS.DATE) {
    formFields[FORM_FIELDS_KEYS.DATE_OF_BIRTH] = {
      hasError: true,
      message: errorMessage,
    };
  }

  return formFields;
};
