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
import { Action } from 'models';

import * as Models from './models';

export type GetCheckoutDataRequest = () => Action<Models.CheckoutDataPayload>;
export type GetCheckoutDataFailure = (error: string, stack?: string) => Action<Models.CheckoutDataPayload>;
export type GetCheckoutDataSuccess = (
  data: Commerce.DeliveryInfo | Commerce.ShippingInfo | Commerce.BillingInfo,
) => Action<Models.CheckoutDataPayload>;

export type SetCurrentStep = (payload: Models.CurrentStepPayload) => Action<Models.CurrentStepPayload>;
export type SubmitStepRequest = () => Action<Models.CurrentStepPayload>;
export type SubmitStepFailure = (error: string, stack?: string) => Action<Models.CurrentStepPayload>;
export type SubmitStepSuccess = () => Action<Models.CurrentStepPayload>;

export type SetStepValues = (stepValues: Models.StepValuesPayload) => Action<Models.StepValuesPayload>;

export type GetCheckoutData = () => Action;
export type InitStep = (step: Models.CheckoutStepType) => Action<Models.CurrentStepPayload>;

export type SubmitStep = (stepValues: Models.StepValuesPayload) => Action<Models.StepValuesPayload>;

export type ResetDeliveryInfo = () => Action<Models.CheckoutDataPayload>;
