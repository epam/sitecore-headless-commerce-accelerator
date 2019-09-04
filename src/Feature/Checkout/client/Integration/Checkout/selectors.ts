//    Copyright 2019 EPAM Systems, Inc.
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

import { sitecoreContext } from 'Foundation/ReactJss/client';
import { AppState, GlobalCheckoutState } from './models';

export const checkout = (state: GlobalCheckoutState) => state.checkout;

export const commerceUser = (state: AppState) => sitecoreContext(state).commerceUser;

export const checkoutData = (state: GlobalCheckoutState) => checkout(state).data;
export const checkoutDeliveryData = (state: GlobalCheckoutState) => checkoutData(state).delivery;
export const checkoutShippingData = (state: GlobalCheckoutState) => checkoutData(state).shipping;
export const checkoutBillingData = (state: GlobalCheckoutState) => checkoutData(state).billing;

export const stepValues = (state: GlobalCheckoutState) => checkout(state).stepValues;
export const currentStep = (state: GlobalCheckoutState) => checkout(state).currentStep;
