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

import * as Base from 'Foundation/Base/client';
import * as Commerce from 'Foundation/Commerce/client';
import { Action, LoadingStatus, Result } from 'Foundation/Integration/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import { Checkout } from 'Feature/Checkout/client/Integration/api';

import { SetPaymentInfoRequest, SetShippingOptionsRequest } from '../../dataModel.Generated';
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

import { loadCart } from '../ShoppingCart/sagas';

export function* handleDeliveryInfoResult(deliveryInfoResponse: Result<Commerce.DeliveryInfo>) {
  const { data, error } = deliveryInfoResponse;
  if (error) {
    return yield put(actions.GetDeliveryInfoFailure(error.message || 'Error Occurred'));
  }

  yield put(actions.GetDeliveryInfoSuccess(data));
}

export function* handleShippingInfoResult(shippingResponse: Result<Commerce.ShippingInfo>) {
  const { data, error } = shippingResponse;
  if (error) {
    return yield put(actions.GetShippingInfoFailure(error.message || 'Error Occurred'));
  }

  yield put(actions.GetShippingInfoSuccess(data));
}

export function* handleBillingInfoResult(billingInfoResponse: Result<Commerce.BillingInfo>) {
  const { data, error }: Result<Commerce.BillingInfo> = billingInfoResponse;

  if (error) {
    return yield put(actions.GetBillingInfoFailure(error.message || 'Error Occurred'));
  }

  yield put(actions.GetBillingInfoSuccess(data));
}

export function* getCheckoutData() {
  const apiCalls = [];
  const resultIndex = {
    billingInfoIndex: -1,
    deliveryInfoIndex: -1,
    shippingInfoIndex: -1,
  };

  const deliveryInfoState: Data<Commerce.DeliveryInfo> = yield select(selectors.checkoutDeliveryInfo);
  if (deliveryInfoState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetDeliveryInfoRequest());
    resultIndex.deliveryInfoIndex = apiCalls.push(call(Checkout.getDeliveryInfo)) - 1;
  }

  const shippingInfoState: Data<Commerce.ShippingInfo> = yield select(selectors.checkoutShippingInfo);
  if (shippingInfoState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetShippingInfoRequest());
    resultIndex.shippingInfoIndex = apiCalls.push(call(Checkout.getShippingInfo)) - 1;
  }

  const billingInfoState: Data<Commerce.BillingInfo> = yield select(selectors.checkoutBillingInfo);
  if (billingInfoState.status === LoadingStatus.NotLoaded) {
    yield put(actions.GetBillingInfoRequest());
    resultIndex.billingInfoIndex = apiCalls.push(call(Checkout.getBillingInfo)) - 1;
  }
  const resultList: Array<Result<Commerce.DeliveryInfo | Commerce.ShippingInfo | Commerce.BillingInfo>> = yield all(
    apiCalls,
  );

  const { billingInfoIndex, deliveryInfoIndex, shippingInfoIndex } = resultIndex;
  const resultHandlers = resultList.map((result, index) => {
    if (index === deliveryInfoIndex) {
      return fork(handleDeliveryInfoResult, result as Result<Commerce.DeliveryInfo>);
    }

    if (index === shippingInfoIndex) {
      return fork(handleShippingInfoResult, result as Result<Commerce.ShippingInfo>);
    }

    if (index === billingInfoIndex) {
      return fork(handleBillingInfoResult, result as Result<Commerce.BillingInfo>);
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

  const shippingInfo: Data<Commerce.ShippingInfo> = yield select(selectors.checkoutShippingInfo);
  if (!shippingInfo.data) {
    throw new Error('ShippingInfo was not loaded');
  }

  const deliveryInfo: Data<Commerce.DeliveryInfo> = yield select(selectors.checkoutDeliveryInfo);
  if (!deliveryInfo.data) {
    throw new Error('DeliveryInfo was not loaded');
  }

  if (!address.name) {
    address.name = 'TEMP_NAME';
  }

  if (!address.partyId) {
    address.partyId = deliveryInfo.data.newPartyId;
    address.externalId = deliveryInfo.data.newPartyId;
  }

  shippingMethod.partyId = address.externalId;
  shippingMethod.shippingPreferenceType = '1';

  const setShippingOptionsRequest: SetShippingOptionsRequest = {
    orderShippingPreferenceType: '1',
    shippingAddresses: [address],
    shippingMethods: [shippingMethod],
  };

  yield put(actions.SubmitStepRequest());
  const { error }: Result<Base.VoidResult> = yield call(Checkout.setShippingOptions, setShippingOptionsRequest);

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
      }),
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

  const checkoutBillingInfo: Data<Commerce.BillingInfo> = yield select(selectors.checkoutBillingInfo);
  if (!checkoutBillingInfo.data) {
    throw new Error('CheckoutBillingInfo was not loaded');
  }

  const creditCard: CreditCard = {
    authToken: checkoutBillingInfo.data.paymentClientToken,
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

export function* handleCreditCard(payment: PaymentStep, billingAddress: Commerce.Address, cardToken: string) {
  const setPaymentInfoRequest: SetPaymentInfoRequest = {
    billingAddress,
    federatedPayment: {
      cardToken,
      partyId: null,
      paymentMethodId: '',
    },
  };
  yield put(actions.SubmitStepRequest());
  const { error }: Result<Base.VoidResult> = yield call(Checkout.setPaymentInfo, setPaymentInfoRequest);
  if (error) {
    yield put(actions.SubmitStepFailure(error.message || 'can not update shipping details'));
  } else {
    const { data, error: placeOrderError }: Result<Commerce.OrderConfirmation> = yield call(Checkout.submitOrder);
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
          yield call(submitFulfillmentStep, payload.shipping);
          yield fork(loadCart);
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
