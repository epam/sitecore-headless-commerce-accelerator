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

import axios from 'axios';
import Braintree from 'braintree-web';

import * as Base from 'Foundation/Base/client';
import * as Commerce from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import { SetPaymentInfoRequest, SetShippingOptionsRequest } from 'Feature/Checkout/client/dataModel.Generated';

import { BillingInfoResponse, CreditCard, DeliveryInfoResponse, SetPaymentInfoResponse, SetShippingOptionsResponse, ShippingInfoResponse, SubmitOrderResponse } from './models';

const routeBase = '/apix/client/commerce/checkout';

export const getDeliveryInfo = async (): Promise<Result<Commerce.DeliveryInfo>> => {
  try {
    const response = await axios.get<DeliveryInfoResponse>(`${routeBase}/deliveryInfo`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getShippingInfo = async (): Promise<Result<Commerce.ShippingInfo>> => {
  try {
    const response = await axios.get<ShippingInfoResponse>(`${routeBase}/shippingInfo`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const setShippingOptions = async (requestPayload: SetShippingOptionsRequest): Promise<Result<Base.VoidResult>> => {
  try {
    const response = await axios.post<SetShippingOptionsResponse>(`${routeBase}/shippingOptions`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getBillingInfo = async (): Promise<Result<Commerce.BillingInfo>> => {
  try {
    const response = await axios.get<BillingInfoResponse>(`${routeBase}/billingInfo`);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const submitCreditCard = async (creditCard: CreditCard): Promise<Result<string>> => {
    const request = {
      data: { creditCard },
      endpoint: 'payment_methods/credit_cards',
      method: 'post'
    };
    return new Promise<Result<string>>((resolve, reject) => {
      Braintree.client.create({ authorization: creditCard.authToken }, (createErr, client: Braintree.Client) => {
        if (createErr) {
          resolve({ error: new Error(createErr.message)});
        } else {
          client.request(request, (err, response) => {
            if (err) {
              resolve({ error: new Error(err.message) });
            } else {
              resolve({ data: response.creditCards[0].nonce });
            }
          });
        }
      });
    });
};

export const setPaymentInfo = async (requestPayload: SetPaymentInfoRequest): Promise<Result<Base.VoidResult>> => {
  try {
    const response = await axios.post<SetPaymentInfoResponse>(`${routeBase}/paymentInfo`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const submitOrder = async (): Promise<Result<Commerce.OrderConfirmation>> => {
  try {
    const response = await axios.post<SubmitOrderResponse>(`${routeBase}/orders`);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};
