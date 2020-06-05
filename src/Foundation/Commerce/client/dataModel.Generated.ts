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

  export interface ValidateCredentialsResultModel {
    hasValidCredentials: boolean;
  }
  export interface ShippingInfo {
    shippingMethods: ShippingMethod[];
  }
  export interface ShippingMethod {
    description: string;
    externalId: string;
    lineIds: string[];
    name: string;
    partyId: string;
    shippingPreferenceType: string;
  }
  export interface ShippingOption {
    description: string;
    name: string;
    shippingOptionType: number;
    shopName: string;
  }
  export interface FederatedPaymentInfo {
    cardToken: string;
    partyId: string;
    paymentMethodId: string;
  }
  export interface PaymentMethod {
    description: string;
    externalId: string;
  }
  export interface PaymentOption {
    description: string;
    name: string;
    paymentOptionTypeName: string;
  }
  export interface DeliveryInfo extends BaseCheckoutInfo {
    newPartyId: string;
    shippingOptions: ShippingOption[];
  }
  export interface BaseCheckoutInfo {
    userAddresses: Address[];
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
  export interface Cart {
    addresses: Address[];
    adjustments: string[];
    cartLines: CartLine[];
    email: string;
    id: string;
    payment: FederatedPaymentInfo[];
    price: TotalPrice;
    shipping: ShippingMethod[];
  }
  export interface CartLine {
    id: string;
    price: TotalPrice;
    product: Product;
    quantity: number;
    variant: Variant;
  }
  export interface TotalPrice {
    currencyCode: string;
    currencySymbol: string;
    handlingTotal: number;
    shippingTotal: number;
    subtotal: number;
    taxTotal: number;
    total: number;
    totalSavings: number;
  }
  export interface BaseProduct {
    adjustedPrice: number;
    brand: string;
    currencySymbol: string;
    customerAverageRating: number;
    description: string;
    displayName: string;
    imageUrls: string[];
    listPrice: number;
    productId: string;
    stockStatusName: string;
    tags: string[];
  }
  export interface Category {
    childrenCategoryList: string[];
    description: string;
    displayName: string;
    name: string;
    parentCatalogList: string[];
    sitecoreId: string;
  }
  export interface Product extends BaseProduct {
    sitecoreId: string;
    variants: Variant[];
  }
  export interface Variant extends BaseProduct {
    properties: { [key: string]: string };
    variantId: string;
  }
  export interface KeyValuePair<TKey, TValue> {
    key: TKey;
    value: TValue;
  }
  export interface BillingInfo {
    paymentClientToken: string;
    paymentMethods: PaymentMethod[];
    paymentOptions: PaymentOption[];
  }
  export interface Facet {
    displayName: string;
    foundValues: FacetValue[];
    name: string;
    values: any[];
  }
  export interface FacetValue {
    aggregateCount: number;
    name: string;
  }
  export interface ProductSearchResults {
    facets: Facet[];
    products: Product[];
    totalItemCount: number;
    totalPageCount: number;
  }
  export interface User {
    contactId: string;
    customerId: string;
    email: string;
    firstName: string;
    lastName: string;
    userName: string;
  }
  export interface CountryRegion {
    countryCode: string;
    name: string;
    subdivisions: Subdivision[];
  }
  export interface Subdivision {
    code: string;
    name: string;
  }
  export interface Order extends Cart {
    isOfflineOrder: boolean;
    orderDate: Date;
    orderID: string;
    status: string;
    trackingNumber: string;
  }
  export interface OrderConfirmation {
    confirmationId: string;
  }
  export interface ValidateEmailResult {
    inUse: boolean;
  }
