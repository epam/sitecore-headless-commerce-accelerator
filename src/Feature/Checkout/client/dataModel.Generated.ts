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

// tslint:disable:indent array-type
// tslint:disable: no-use-before-declare

  export interface AddCartLineRequest extends CartLineRequest {
    quantity: number;
  }
  export interface CartLineRequest {
    productId: string;
    variantId: string;
  }
  export interface GetOrdersRequest {
    count: number;
    fromDate: Date;
    page: number;
    untilDate: Date;
  }
  export interface PromoCodeRequest {
    promoCode: string;
  }
  export interface RemoveCartLineRequest extends CartLineRequest {
  }
  export interface SetPaymentInfoRequest {
    billingAddress: Address;
    federatedPayment: FederatedPaymentInfo;
  }
  export interface SetShippingOptionsRequest {
    orderShippingPreferenceType: string;
    shippingAddresses: Address[];
    shippingMethods: ShippingMethod[];
  }
  export interface UpdateCartLineRequest extends CartLineRequest {
    quantity: number;
  }
  export interface Address {
    address1: string;
    address2: string;
    city: string;
    country: string;
    countryCode: string;
    email: string;
    externalId: string;
    firstName: string;
    isPrimary: boolean;
    lastName: string;
    name: string;
    partyId: string;
    state: string;
    zipPostalCode: string;
  }
  export interface FederatedPaymentInfo {
    cardToken: string;
    partyId: string;
    paymentMethodId: string;
  }
  export interface ShippingMethod {
    description: string;
    externalId: string;
    lineIds: string[];
    name: string;
    partyId: string;
    shippingPreferenceType: string;
  }
