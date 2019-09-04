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

import * as Commerce from 'Foundation/Commerce/client';
import * as Jss from 'Foundation/ReactJss/client';

import * as Checkout from 'Feature/Checkout/client/Integration/Checkout';

export interface ShippingDataSource extends Jss.BaseDataSourceItem {
  countries: Commerce.CountryRegionModel[];
}

export interface ShippingOwnProps extends Jss.Rendering<ShippingDataSource> {}
export interface ShippingStateProps {
  deliveryData: Checkout.Data<Commerce.DeliveryModel>;
  shippingData: Checkout.Data<Commerce.ShippingModel>;
  isSubmitting: boolean;
  isLoading: boolean;
  commerceUser: Commerce.CommerceUserModel;
}
export interface ShippingDispatchProps {
  InitStep: (step: Checkout.CheckoutStepType) => void;
  SubmitStep: (stepValues: Checkout.StepValues) => void;
}

export interface ShippingProps extends ShippingOwnProps, ShippingStateProps, ShippingDispatchProps {}

export interface ShippingState extends Jss.SafePureComponentState {
  selectedAddressOption: string;
}
export interface AppState extends Checkout.GlobalCheckoutState, Checkout.AppState {}
