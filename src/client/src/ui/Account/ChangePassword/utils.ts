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

import { FormValues } from 'Foundation/ReactJss/Form';
import { CHANGE_PASSWORD_FORM_FIELDS } from './constants';

export const CHANGE_PASSWORD_UTILS_CONTENT = {
  INVALID_OLD_PASSWORD_ERROR: 'Incorrect password',
  INVALID_NEW_PASSWORD_ERROR: 'Incorrect password. Specify minimum 6 characters',
  NEW_AND_CONFIRM_PASSWORDS_MATCH_ERROR: "The passwords don't match",
  OLD_AND_NEW_PASSWORDS_MATCH_ERROR: 'You have already used this password. Enter a new password',
};

export const CHANGE_PASSWORD_KEYS = {
  OLD_PASSWORD: 'oldPassword',
  NEW_PASSWORD: 'newPassword',
  CONFIRM_NEW_PASSWORD: 'confirmNewPassword',
};

export const validate = (formValues: FormValues) => {
  const oldPassword: string = get(formValues, [CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD], null);
  const newPassword: string = get(formValues, [CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD], null);
  const confirmNewPassword: string = get(formValues, [CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD_CONFIRM], null);

  const formFields = {};

  if (!oldPassword || oldPassword?.length < 6) {
    formFields[CHANGE_PASSWORD_KEYS.OLD_PASSWORD] = {
      hasError: true,
      message: CHANGE_PASSWORD_UTILS_CONTENT.INVALID_OLD_PASSWORD_ERROR,
    };
  }

  if (!newPassword || newPassword.length < 6) {
    formFields[CHANGE_PASSWORD_KEYS.NEW_PASSWORD] = {
      hasError: true,
      message: CHANGE_PASSWORD_UTILS_CONTENT.INVALID_NEW_PASSWORD_ERROR,
    };
  }

  if (!confirmNewPassword || confirmNewPassword.length < 6) {
    formFields[CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD] = {
      hasError: true,
      message: CHANGE_PASSWORD_UTILS_CONTENT.INVALID_NEW_PASSWORD_ERROR,
    };
  }

  if (newPassword !== confirmNewPassword) {
    formFields[CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD] = {
      hasError: true,
      message: CHANGE_PASSWORD_UTILS_CONTENT.NEW_AND_CONFIRM_PASSWORDS_MATCH_ERROR,
    };
  }

  if (oldPassword === newPassword) {
    formFields[CHANGE_PASSWORD_KEYS.NEW_PASSWORD] = {
      hasError: true,
      message: CHANGE_PASSWORD_UTILS_CONTENT.OLD_AND_NEW_PASSWORDS_MATCH_ERROR,
    };
  }

  return formFields;
};

export const setServerErrors = (errorMessage: string) => {
  const formFields = {};

  formFields[CHANGE_PASSWORD_KEYS.OLD_PASSWORD] = {
    hasError: true,
    message: errorMessage,
  };

  return formFields;
};
