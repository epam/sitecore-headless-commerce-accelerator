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

import axios from 'axios';

import * as Commerce from 'Foundation/Commerce/client';
import { Result } from 'Foundation/Integration/client';

import Braintree from 'braintree-web';

import { BillingDataResponse, CreditCard, DeliveryDataResponse, SetShippingMethodsResponse, ShippingMethodsResponse } from './models';

const routeBase = '/apix/client/commerce/checkout';

export const getDeliveryData = async (): Promise<Result<Commerce.DeliveryModel>> => {
  try {
    const response = await axios.get<DeliveryDataResponse>(`${routeBase}/getDeliveryData`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getShippingMethods = async (): Promise<Result<Commerce.ShippingModel>> => {
  try {
    const response = await axios.get<ShippingMethodsResponse>(`${routeBase}/getShippingMethods`);

    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const setShippingMethods = async (requestPayload: Commerce.SetShippingArgs): Promise<Result<Commerce.SetShippingModel>> => {
  try {
    const response = await axios.post<SetShippingMethodsResponse>(`${routeBase}/setShippingMethods`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const getBillingData = async (): Promise<Result<Commerce.BillingModel>> => {
  try {
    const response = await axios.get<BillingDataResponse>(`${routeBase}/getBillingData`);
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

export const setPaymentMethods = async (requestPayload: Commerce.SetPaymentArgs): Promise<Result<Commerce.VoidResult>> => {
  try {
    const response = await axios.post<any>(`${routeBase}/setPaymentMethods`, requestPayload);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};

export const submitOrder = async (): Promise<Result<Commerce.SubmitOrderModel>> => {
  try {
    const response = await axios.post<any>(`${routeBase}/submitOrder`);
    const { data } = response;
    if (data.status !== 'ok') {
      return { error: new Error('Failure') };
    }
    return { data: data.data };
  } catch (e) {
    return { error: e };
  }
};
