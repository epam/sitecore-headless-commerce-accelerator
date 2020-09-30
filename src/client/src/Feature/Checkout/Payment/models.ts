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

// tslint:disable-next-line:no-commented-code
// import * as Commerce from 'Foundation/Commerce';
import * as Jss from 'Foundation/ReactJss';

import * as Checkout from 'Feature/Checkout/Integration/Checkout';

export interface PaymentOwnProps extends Jss.RenderingWithContext<Jss.BaseDataSourceItem> {}
export interface PaymentStateProps {
  isSubmitting: boolean;
  paymentStatusFailed: boolean;
}

export interface PaymentDispatchProps {
  InitStep: (step: Checkout.CheckoutStepType) => void;
  SubmitStep: (stepValues: Checkout.StepValues) => void;
}

export interface PaymentProps extends PaymentOwnProps, PaymentStateProps, PaymentDispatchProps {}
export interface PaymentState extends Jss.SafePureComponentState {
  formIsValid: boolean;
}

export interface AppState extends Checkout.GlobalCheckoutState {}
