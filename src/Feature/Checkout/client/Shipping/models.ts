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

import * as Commerce from 'Foundation/Commerce/client';
import * as Jss from 'Foundation/ReactJss/client';

import * as Checkout from 'Feature/Checkout/client/Integration/Checkout';

export interface ShippingDataSource extends Jss.BaseDataSourceItem {
  countries: Commerce.CountryRegion[];
}

export interface ShippingOwnProps extends Jss.RenderingWithContext<ShippingDataSource> {}
export interface ShippingStateProps {
  deliveryInfo: Checkout.Data<Commerce.DeliveryInfo>;
  shippingInfo: Checkout.Data<Commerce.ShippingInfo>;
  isSubmitting: boolean;
  isLoading: boolean;
  commerceUser: Commerce.User;
}
export interface ShippingDispatchProps {
  InitStep: (step: Checkout.CheckoutStepType) => void;
  SubmitStep: (stepValues: Checkout.StepValues) => void;
  AddAddressToAccount: (address: Commerce.Address) => void;
}

export interface ShippingProps extends ShippingOwnProps, ShippingStateProps, ShippingDispatchProps {}

export interface ShippingState extends Jss.SafePureComponentState {
  selectedAddressOption: string;
  disableSaveToMyAccount: boolean;
}
export interface AppState extends Checkout.GlobalCheckoutState, Checkout.AppState {}
