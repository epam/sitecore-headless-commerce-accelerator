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

import { Status, VoidResult } from 'models';

import * as Commerce from 'services/commerce';
import { SitecoreState } from 'Foundation/ReactJss';

export enum CheckoutStepType {
  Initial = 'Initial',
  Fulfillment = 'Fulfillment',
  Billing = 'Billing',
  Payment = 'Payment',
  Confirmation = 'Confirmation',
}

export interface CurrentStep extends Status {
  type: CheckoutStepType;
}

export interface CurrentStepPayload extends Partial<CurrentStep> {}

export interface FulfillmentOptions {
  useForBillingAddress: boolean;
  saveToMyAccount: boolean;
  selectedAddressOption: string;
}

export interface ShippingStep {
  fulfillmentType: string;
  address: Commerce.Address;
  saveToMyAccount: boolean;
  shippingMethod: Commerce.ShippingMethod;
  options: FulfillmentOptions;
}

export interface BillingStep {
  useSameAsShipping: boolean;
  address?: Commerce.Address;
}

export interface PaymentStep {
  creditCard: {
    cardNumber: string;
    expiresMonth: number;
    expiresYear: number;
    securityCode: string;
  };
}

export interface StepValues {
  shipping?: ShippingStep;
  billing?: BillingStep;
  payment?: PaymentStep;
}

export interface StepValuesPayload extends Partial<StepValues> {}
export interface Data<T> extends Status {
  data?: T;
}
export interface CheckoutData {
  delivery: Data<Commerce.DeliveryInfo>;
  shipping: Data<Commerce.ShippingInfo>;
  billing: Data<Commerce.BillingInfo>;
}

export interface CheckoutDataPayload extends Partial<CheckoutData> {}

export interface CheckoutState {
  data: CheckoutData;
  stepValues: StepValues;
  currentStep: CurrentStep;
}

export interface AppState extends SitecoreState<Commerce.UserContext> {}

export interface GlobalCheckoutState {
  checkout: CheckoutState;
}

export interface Response<T> {
  data: T;
  status: string;
}

export interface DeliveryInfoResponse extends Response<Commerce.DeliveryInfo> {}

export interface ShippingInfoResponse extends Response<Commerce.ShippingInfo> {}

export interface SetShippingOptionsResponse extends Response<VoidResult> {}

export interface BillingInfoResponse extends Response<Commerce.BillingInfo> {}

export interface SetPaymentInfoResponse extends Response<VoidResult> {}

export interface SubmitOrderResponse extends Response<Commerce.OrderConfirmation> {}

export interface CreditCard {
  cvv: string;
  expirationDate: string;
  number: string;
  authToken: string;
}
