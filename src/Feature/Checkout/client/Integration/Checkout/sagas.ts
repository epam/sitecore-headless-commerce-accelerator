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

import { SagaIterator } from 'redux-saga';
import { all, call, fork, put, select, takeEvery, takeLatest } from 'redux-saga/effects';

import { Checkout } from 'Feature/Checkout/client/Integration/api';
import * as Commerce from 'Foundation/Commerce/client';

import { Action, LoadingStatus, Result } from 'Foundation/Integration/client';

import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';
import { CreditCard } from '../api/Checkout';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import {
  BillingStep,
  CheckoutStepType,
  CurrentStep,
  CurrentStepPayload,
  Data,
  PaymentStep,
  ShippingStep,
  StepValues,
} from './models';
import * as selectors from './selectors';

export function* handleDeliveryDataResult(deliveryDataResponse: Result<Commerce.DeliveryModel>) {
  const { data, error } = deliveryDataResponse;
  if (error) {
    return yield put(actions.GetDeliveryDataFailure(error.message || 'Error Occured'));
  }

  yield put(actions.GetDeliveryDataSuccess(data));
}

export function* handleShippingDataResult(shippingResponse: Result<Commerce.ShippingModel>) {
  const { data, error } = shippingResponse;
  if (error) {
    return yield put(actions.GetShippingMethodsDataFailure(error.message || 'Error Occured'));
  }

  yield put(actions.GetShippingMethodsDataSuccess(data));
}

export function* handleBillingDataResult(billingDataResponse: Result<Commerce.BillingModel>) {
  const { data, error }: Result<Commerce.BillingModel> = billingDataResponse;

  if (error) {
    return yield put(actions.GetBillingDataFailure(error.message || 'Error Occured'));
  }

  yield put(actions.GetBillingDataSuccess(data));
}

export function* getCheckoutData() {
  const apiCalls = [];
  const resultIndex = {
    billingDataIndex: -1,
    deliveryDataIndex: -1,
    shippingDataIndex: -1,
  };

  const deliveryDataState: Data<Commerce.DeliveryModel> = yield select(selectors.checkoutDeliveryData);
  if (deliveryDataState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetDeliveryDataRequest());
    resultIndex.deliveryDataIndex = apiCalls.push(call(Checkout.getDeliveryData)) - 1;
  }

  const shippingState: Data<Commerce.ShippingModel> = yield select(selectors.checkoutShippingData);
  if (shippingState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetShippingMethodsDataRequest());
    resultIndex.shippingDataIndex = apiCalls.push(call(Checkout.getShippingMethods)) - 1;
  }

  const billingState: Data<Commerce.BillingModel> = yield select(selectors.checkoutBillingData);
  if (billingState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetBillingDataRequest());
    resultIndex.billingDataIndex = apiCalls.push(call(Checkout.getBillingData)) - 1;
  }
  const resultList: Array<Result<Commerce.DeliveryModel | Commerce.ShippingModel | Commerce.BillingModel>> = yield all(
    apiCalls
  );

  const { billingDataIndex, deliveryDataIndex, shippingDataIndex } = resultIndex;
  const resultHandlers = resultList.map((result, index) => {
    if (index === deliveryDataIndex) {
      return fork(handleDeliveryDataResult, result as Result<Commerce.DeliveryModel>);
    }

    if (index === shippingDataIndex) {
      return fork(handleShippingDataResult, result as Result<Commerce.ShippingModel>);
    }

    if (index === billingDataIndex) {
      return fork(handleBillingDataResult, result as Result<Commerce.BillingModel>);
    }

    return null;
  });

  yield all(resultHandlers);
}

export function* initStep(action: Action<CurrentStepPayload>) {
  const { payload } = action;

  const stepValues: StepValues = yield select(selectors.stepValues);

  if (payload.type === CheckoutStepType.Billing && !stepValues.shipping) {
    return yield put(ChangeRoute('/Checkout/Shipping'));
  }

  if (payload.type === CheckoutStepType.Payment && (!stepValues.billing || !stepValues.shipping)) {
    return yield put(ChangeRoute('/Checkout/Shipping'));
  }

  yield put(actions.SetCurrentStep(payload));
  yield fork(getCheckoutData);
}

export function* submitFulfillmentStep(fulfillment: ShippingStep) {
  if (!fulfillment) {
    throw new Error('Fulfillment is not defined');
  }

  const { address, shippingMethod } = fulfillment;

  const shippingData: Data<Commerce.ShippingModel> = yield select(selectors.checkoutShippingData);
  if (!shippingData.data) {
    throw new Error('ShippingData was not loaded');
  }

  const deliveryData: Data<Commerce.DeliveryModel> = yield select(selectors.checkoutDeliveryData);
  if (!deliveryData.data) {
    throw new Error('DeliveryData was not loaded');
  }

  if (!address.name) {
    address.name = 'TEMP_NAME';
  }

  if (!address.partyId) {
    address.partyId = deliveryData.data.newPartyId;
    address.externalId = deliveryData.data.newPartyId;
  }

  shippingMethod.partyId = address.externalId;
  shippingMethod.shippingPreferenceType = '1';

  const setShippingArgs: Commerce.SetShippingArgs = {
    orderShippingPreferenceType: '1',
    shippingAddresses: [address],
    shippingMethods: [shippingMethod],
  };

  yield put(actions.SubmitStepRequest());
  const { error }: Result<Commerce.SetShippingModel> = yield call(Checkout.setShippingMethods, setShippingArgs);

  if (error) {
    yield put(actions.SubmitStepFailure(error.message, error.stack));
  } else {
    yield put(actions.SubmitStepSuccess());
    // address had some mutation during saga execution, so we put updated address to the state
    yield put(
      actions.SetStepValues({
        shipping: {
          ...fulfillment,
          address,
        },
      })
    );

    yield fork(nextStep, CheckoutStepType.Fulfillment);
  }
}

export function* submitBillingStep(billing: BillingStep) {
  yield put(actions.SetStepValues({ billing }));
  yield fork(nextStep, CheckoutStepType.Billing);
}

export function* submitPaymentStep(payment: PaymentStep): SagaIterator {
  const stepValues: StepValues = yield select(selectors.stepValues);
  const { billing, shipping } = stepValues;
  if (!billing || !shipping) {
    throw new Error('prev steps are not submitted');
  }

  const billingAddress = billing.useSameAsShipping ? shipping.address : billing.address;
  if (!billingAddress) {
    throw new Error('billing address was not setup');
  }

  const checkoutBillingData: Data<Commerce.BillingModel> = yield select(selectors.checkoutBillingData);
  if (!checkoutBillingData.data) {
    throw new Error('CheckoutBillingData was not loaded');
  }

  const creditCard: CreditCard = {
    authToken: checkoutBillingData.data.paymentClientToken,
    cvv: payment.creditCard.securityCode,
    expirationDate: `${payment.creditCard.expiresMonth}/${payment.creditCard.expiresYear}`,
    number: payment.creditCard.cardNumber,
  };

  const { data: cardToken, error: submitCardError }: Result<string> = yield call(Checkout.submitCreditCard, creditCard);

  if (submitCardError) {
    return yield put(actions.SubmitStepFailure(submitCardError.message || 'can not submit credit card'));
  }

  yield fork(handleCreditCard, payment, billingAddress, cardToken);
}

export function* handleCreditCard(payment: PaymentStep, billingAddress: Commerce.AddressModel, cardToken: string) {
  const setPaymentArgs: Commerce.SetPaymentArgs = {
    billingAddress,
    federatedPayment: {
      cardToken,
      partyID: null,
      paymentMethodId: '',
    },
  };
  yield put(actions.SubmitStepRequest());
  const { error }: Result<Commerce.SetPaymentArgs> = yield call(Checkout.setPaymentMethods, setPaymentArgs);
  if (error) {
    yield put(actions.SubmitStepFailure(error.message || 'can not update shipping details'));
  } else {
    const { data, error: placeOrderError }: Result<Commerce.SubmitOrderModel> = yield call(Checkout.submitOrder);
    if (data) {
      yield put(actions.SetStepValues({ payment }));
      yield put(actions.SubmitStepSuccess());
      const trackingNumber = data.confirmationId;
      yield put(ChangeRoute(`/Checkout/Confirmation?trackingNumber=${trackingNumber}`));
    } else {
      yield put(actions.SubmitStepFailure(placeOrderError.message, placeOrderError.stack));
    }
  }
}

export function* nextStep(type: CheckoutStepType) {
  if (type === CheckoutStepType.Fulfillment) {
    yield put(ChangeRoute('/Checkout/Billing'));
  }

  if (type === CheckoutStepType.Billing) {
    yield put(ChangeRoute('/Checkout/Payment'));
  }

  if (type === CheckoutStepType.Payment) {
    yield put(ChangeRoute('/Checkout/Confirmation'));
  }
}

export function* submitStep(action: Action<StepValues>) {
  try {
    const currentStepState: CurrentStep = yield select(selectors.currentStep);

    const { payload } = action;
    switch (currentStepState.type) {
      case CheckoutStepType.Fulfillment:
        {
          yield fork(submitFulfillmentStep, payload.shipping);
        }
        break;
      case CheckoutStepType.Billing:
        {
          const { billing } = payload;
          yield fork(submitBillingStep, billing);
        }
        break;
      case CheckoutStepType.Payment:
        {
          yield fork(submitPaymentStep, payload.payment);
        }
        break;
      default: {
        // Silence is gold
      }
    }
  } catch (e) {
    yield put(actions.SubmitStepFailure(e.message));
  }
}

function* watch(): SagaIterator {
  yield takeLatest(sagaActionTypes.GET_CHECKOUT_DATA, getCheckoutData);
  yield takeLatest(sagaActionTypes.INIT_STEP, initStep);
  yield takeEvery(sagaActionTypes.SUBMIT_STEP, submitStep);
}

export default [watch()];
