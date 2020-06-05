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

import axios from 'axios';

import { UserContext } from './models';

export const registerCommerceInterceptor = (context: UserContext) => {
    axios.interceptors.request.use(
      (config) => {
        if (!config.url.startsWith('/apix/client/commerce')) {
          return config;
        }

        if (context && context.commerceUser && context.commerceUser.contactId) {
          config.headers.CommerceUserId = context.commerceUser.contactId;
        }

        return config;
      }
    );
};
