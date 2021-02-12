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

import * as JSS from 'Foundation/ReactJss';

import { GlobalAuthenticationState } from 'Feature/Account/Integration/Authentication';
import * as Commerce from 'Foundation/Commerce';

export interface UserButtonProps {
  commerceUser: Commerce.User;
  returnUrl: string;
  Logout: (returnUrl: string) => void;
}

export interface UserButtonState extends JSS.SafePureComponentState {
  isDropdownVisible: boolean;
}

export interface AppState extends GlobalAuthenticationState, JSS.RoutingState, JSS.SitecoreState<Commerce.UserContext> {}
