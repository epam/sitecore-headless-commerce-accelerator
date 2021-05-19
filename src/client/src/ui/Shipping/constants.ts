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

export const FIELDS = keyMirror(
  {
    ADDRESS_LINE: null,
    ADDRESS_TYPE: null,
    CITY: null,
    COUNTRY: null,
    EMAIL: null,
    FIRST_NAME: null,
    LAST_NAME: null,
    POSTAL_CODE: null,
    PROVINCE: null,
    SAVE_TO_MY_ACCOUNT: null,
    SELECTED_ADDRESS: null,
    SELECTED_SHIPPING_METHOD: null,
    USE_FOR_BILLING: null,
  },
  'SHIPPING',
);

export const ADDRESS_TYPE = keyMirror({
  NEW: null,
  SAVED: null,
});
