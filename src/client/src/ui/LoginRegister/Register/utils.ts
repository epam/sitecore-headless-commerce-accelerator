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

import { validateEmail } from 'Foundation/utils/validation';
import { FormValues } from 'Foundation/ReactJss/Form';

import { FORM_FIELDS } from './constants';

const { FIRST_NAME, LAST_NAME, EMAIL, PASSWORD, CONFIRM_PASSWORD, TERMS_CONDITIONS } = FORM_FIELDS;

export const validate = (formValues: FormValues) => {
  const firstName = get(formValues, [FIRST_NAME]);
  const lastName = get(formValues, [LAST_NAME]);
  const email = get(formValues, [EMAIL], null);
  const password = get(formValues, [PASSWORD]);
  const confirmPassword = get(formValues, [CONFIRM_PASSWORD]);
  const termsConditions = get(formValues, [TERMS_CONDITIONS]);

  const formFields = {};

  if (!firstName) {
    formFields['firstName'] = {
      hasError: true,
      message: 'First name field is required',
    };
  }

  if (!lastName) {
    formFields['lastName'] = {
      hasError: true,
      message: 'Last name field is required',
    };
  }

  if (!email) {
    formFields['email'] = {
      hasError: true,
      message: 'Email field is required',
    };
  } else if (!validateEmail(email)) {
    formFields['email'] = {
      hasError: true,
      message: 'Email is invalid',
    };
  }

  if (!password) {
    formFields['password'] = {
      hasError: true,
      message: 'Password field is required',
    };
  } else if (String(password).length < 6) {
    formFields['password'] = {
      hasError: true,
      message: 'Must be at least 6 characters',
    };
  }

  if (!confirmPassword) {
    formFields['confirmPassword'] = {
      hasError: true,
      message: 'Confirm password field is required',
    };
  } else if (confirmPassword !== password) {
    formFields['confirmPassword'] = {
      hasError: true,
      message: 'Passwords do not match',
    };
  }

  if (!termsConditions) {
    formFields['termsConditions'] = {
      hasError: true,
      message: 'Terms and conditions is required',
    };
  }

  return formFields;
};
