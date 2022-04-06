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

export const EMAIL_REGEX = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

export const INPUT_REGEX = /^[a-zA-Z]+$/;

export const ADDRESS_LINE_REGEX = /^[A-Za-z0-9'\.\-\s\,]+$/;

export const CITY_REGEX = /^[a-zA-Z',.\s-]{1,25}$/;

export const PHONE_NUMBER_REGEX = /^[+]?\d+$/;

export const DATE_OF_BIRTH_REGEX = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;

export const SECURITY_CODE_REGEX = /^[0-9]{3}$/;

export const CREDIT_CARD_REGEX =
  /(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^3[47][0-9]{13}$)/;

export const CARD_OWNER_REGEX = /^((?:[A-Za-z]+ ?){1,3})$/;
/**
 * Method for validate emails in input field
 * @param email - string of  email in input field
 */

export function validateValue(regex: RegExp, value: string) {
  if (value) {
    return regex.test(value);
  }
  return false;
}
