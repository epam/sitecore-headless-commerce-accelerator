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

import { keyMirror } from 'Foundation/ReactJss';

export const FIELDS = keyMirror({
  CARD_NUMBER: null,
  CARD_OWNER: null,
  CARD_TYPE: null,
  EXPIRES_MONTH: null,
  EXPIRES_YEAR: null,
  SECURITY_CODE: null,
});

export const FIELD_TYPES = {
  CARD_NUMBER: 'cardNumber',
  CARD_OWNER: 'cardOwner',
  CARD_TYPE: 'cardType',
  EXPIRES_MONTH: 'expiresMonth',
  EXPIRES_YEAR: 'expiresYear',
  SECURITY_CODE: 'securityCode',
};

export const MONTH_LIST = [
  'January',
  'February',
  'March',
  'April',
  'May',
  'June',
  'July',
  'August',
  'September',
  'October',
  'November',
  'December',
];

export const DEFAULT_MONTH = '1';
