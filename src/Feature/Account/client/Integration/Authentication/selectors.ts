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

import { UserContext } from 'Foundation/Commerce/client';
import { SitecoreState } from 'Foundation/ReactJss/client';

import { GlobalAuthenticationState } from './models';

export const commerceUser = (state: SitecoreState<UserContext>) => state.sitecore.context.commerceUser;

export const authentication = (state: GlobalAuthenticationState) => state.authentication;
export const authenticationProcess = (state: GlobalAuthenticationState) => authentication(state).authenticationProcess;
