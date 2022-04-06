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

import { validateValue, SECURITY_CODE_REGEX, CREDIT_CARD_REGEX, CARD_OWNER_REGEX } from 'utils';
import { FormValues } from 'Foundation/ReactJss/Form';

import { FIELDS } from './constants';

const { CARD_NUMBER, CARD_OWNER, SECURITY_CODE, CARD_TYPE } = FIELDS;

export const validate = (formValues: FormValues) => {
  const securityCode: string = get(formValues, [SECURITY_CODE], null);
  const cardOwner: string = get(formValues, [CARD_OWNER], null);
  const cardNumber: string = get(formValues, [CARD_NUMBER], null);
  const cardType: string = get(formValues, [CARD_TYPE], null);

  const formFields = {};

  if (!cardType) {
    formFields['cardType'] = {
      hasError: true,
      message: cardType ? 'Card type is invalid' : 'Card type field is required',
    };
  }

  if (!validateValue(SECURITY_CODE_REGEX, securityCode)) {
    formFields['securityCode'] = {
      hasError: true,
      message: securityCode ? 'Security code is invalid' : 'Security code field is required',
    };
  }

  if (!validateValue(CARD_OWNER_REGEX, cardOwner)) {
    formFields['cardOwner'] = {
      hasError: true,
      message: cardOwner ? 'Card owner is invalid' : 'Card owner field is required',
    };
  }

  if (!validateValue(CREDIT_CARD_REGEX, cardNumber)) {
    formFields['cardNumber'] = {
      hasError: true,
      message: cardNumber ? 'Card number is invalid' : 'Card number field is required',
    };
  }

  return formFields;
};
