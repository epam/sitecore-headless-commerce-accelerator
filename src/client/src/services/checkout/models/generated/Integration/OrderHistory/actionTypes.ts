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

import keyMirror from 'keymirror';

export const actionTypes = keyMirror({
  GET_ORDER_HISTORY: null,
  GET_ORDER_HISTORY_FAILURE: null,
  GET_ORDER_HISTORY_REQUEST: null,
  GET_ORDER_HISTORY_SUCCESS: null,
  ORDER_HISTORY_LOAD_MORE: null
});
