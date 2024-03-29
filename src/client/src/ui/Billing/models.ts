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

import * as Commerce from 'services/commerce';
import * as Jss from 'Foundation/ReactJss';
import * as Checkout from 'services/checkout';

export interface BillingDataSource extends Jss.BaseDataSourceItem {
  countries: Commerce.CountryRegion[];
}
export interface BillingOwnProps extends Jss.RenderingWithContext<BillingDataSource> {}
export interface BillingStateProps {
  useForBillingAddress: boolean;
  commerceUser: Commerce.User;
  stepValues: Checkout.StepValues;
}
export interface BillingDispatchProps {
  InitStep: (step: Checkout.CheckoutStepType) => void;
  SubmitStep: (stepValues: Checkout.StepValues) => void;
}
export interface BillingProps extends BillingOwnProps, BillingDispatchProps, BillingStateProps {}

export interface BillingState extends Jss.SafePureComponentState {
  email: string;
  selectedAddressOption: string;
  stateFormFields: object;
  guestEmail: string;
}

export interface AppState extends Checkout.GlobalCheckoutState, Checkout.AppState {}
