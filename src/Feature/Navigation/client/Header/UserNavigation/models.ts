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

import * as JSS from 'Foundation/ReactJss/client';

import { RouterProps } from 'react-router';

import { GlobalAuthenticationState } from 'Feature/Account/client/Integration/Authentication';
import { GlobalShoppingCartState } from 'Feature/Checkout/client/Integration/ShoppingCart';
import * as Commerce from 'Foundation/Commerce/client';

export interface UserNavigationOwnProps extends RouterProps {}

export interface UserNavigationStateProps {
  cartQuantity: number;
  commerceUser: Commerce.User;
}

export interface UserNavigationProps extends JSS.Rendering, UserNavigationOwnProps, UserNavigationStateProps {}

export interface UserNavigationState extends JSS.SafePureComponentState {
  userFormVisible: boolean;
}

export interface AppState
  extends GlobalAuthenticationState,
    JSS.RoutingState,
    GlobalShoppingCartState,
    JSS.SitecoreState<Commerce.UserContext> {}
