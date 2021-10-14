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

import { validateValue, INPUT_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

import { ACCOUNT_DETAILS_FORM_FIELDS } from './constants';

const { FIRST_NAME, LAST_NAME } = ACCOUNT_DETAILS_FORM_FIELDS;

export const validate = (formValues: FormValues) => {
  const firstName: string = get(formValues, [FIRST_NAME], null);
  const lastName: string = get(formValues, [LAST_NAME], null);

  const formFields = {};

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

  return formFields;
};
