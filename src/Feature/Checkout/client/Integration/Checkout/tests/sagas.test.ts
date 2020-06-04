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

import { cloneableGenerator, SagaIteratorClone } from '@redux-saga/testing-utils';
import { all, call, fork, put, select } from 'redux-saga/effects';

import { Address } from 'Foundation/Commerce/client';
import { LoadingStatus } from 'Foundation/Integration/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import { SetShippingOptionsRequest } from 'Feature/Checkout/client/dataModel.Generated';
import { Checkout } from 'Feature/Checkout/client/Integration/api';

import { CheckoutStepType, StepValuesPayload } from '..';
import { CreditCard } from '../../api/Checkout';
import { loadCart } from '../../ShoppingCart/sagas';
import * as actions from '../actions';
import * as sagas from '../sagas';
import * as selectors from '../selectors';

describe('getCheckoutData', () => {
  const gen = sagas.getCheckoutData();

  test('should select checkoutDeliveryInfo', () => {
    const expected = select(selectors.checkoutDeliveryInfo);
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });

  test('should put GetDeliveryInfoRequest when it is not loaded', () => {
    const expected = put(actions.GetDeliveryInfoRequest());
    const actual = gen.next({
      status: LoadingStatus.NotLoaded,
    });
    expect(actual.value).toEqual(expected);
  });

  test('should select checkoutShippingInfo', () => {
    const expected = select(selectors.checkoutShippingInfo);
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });

  test('should put GetShippingInfoRequest when it is not loaded', () => {
    const expected = put(actions.GetShippingInfoRequest());
    const actual = gen.next({
      status: LoadingStatus.NotLoaded,
    });
    expect(actual.value).toEqual(expected);
  });

  test('should select billingState', () => {
    const expected = select(selectors.checkoutBillingInfo);
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });

  test('should put GetBillingInfoRequest when it is not loaded', () => {
    const expected = put(actions.GetBillingInfoRequest());
    const actual = gen.next({
      status: LoadingStatus.NotLoaded,
    });
    expect(actual.value).toEqual(expected);
  });

  test('should call all: delivery, shipping, billing', () => {
    const expected = all([
      call(Checkout.getDeliveryInfo),
      call(Checkout.getShippingInfo),
      call(Checkout.getBillingInfo),
    ]);
    const actual = gen.next({});
    expect(actual.value).toEqual(expected);
  });

  test('should fork all: handleDeliveryInfoResult, handleShippingInfoResult, handleBillingInfoResult', () => {
    const expected = all([
      fork(sagas.handleDeliveryInfoResult, {}),
      fork(sagas.handleShippingInfoResult, {}),
      fork(sagas.handleBillingInfoResult, {}),
    ]);
    const actual = gen.next([{}, {}, {}]);
    expect(actual.value).toEqual(expected);
  });
});

describe('initStep', () => {
  describe('Billing case', () => {
    const initBillingAction = actions.InitStep(CheckoutStepType.Billing);
    const gen = cloneableGenerator(sagas.initStep)(initBillingAction);
    test('should select stepValues', () => testSelectStepValues(gen));
    test('should put ChangeRoute to Shipping, when Billing init without Fulfillment info', () =>
      testPutChangeRouteToShipping(gen, {}));
    test('should put SetCurrentStep, when Billing init with Fulfillment info', () => {
      const expected = put(actions.SetCurrentStep(initBillingAction.payload));
      const actual = gen.next({ shipping: {} });
      expect(actual.value).toEqual(expected);
    });
    test('should fork getCheckoutData', () => testForkGetCheckoutData(gen));
  });

  describe('Payment case', () => {
    const initPaymentAction = actions.InitStep(CheckoutStepType.Payment);
    const gen = cloneableGenerator(sagas.initStep)(initPaymentAction);
    test('should select stepValues', () => testSelectStepValues(gen));
    test('should put ChangeRoute to Shipping, when Payment init without Billing and Fulfillment info', () =>
      testPutChangeRouteToShipping(gen, {}));
    test('should put ChangeRoute to Shipping, when Payment init with Billing info but without Fulfillment info', () =>
      testPutChangeRouteToShipping(gen, { billing: {} }));
    test('should put ChangeRoute to Shipping, when Payment init with Fulfillment info but without Billing info', () =>
      testPutChangeRouteToShipping(gen, { fulfillment: {} }));
    test('should put SetCurrentStep, when Payment init with Fulfillment and Billing info', () => {
      const expected = put(actions.SetCurrentStep(initPaymentAction.payload));
      const actual = gen.next({ shipping: {}, billing: {} });
      expect(actual.value).toEqual(expected);
    });
    test('should fork getCheckoutData', () => testForkGetCheckoutData(gen));
  });

  function testSelectStepValues(gen: SagaIteratorClone) {
    const expected = select(selectors.stepValues);
    const actual = gen.next({});
    expect(actual.value).toEqual(expected);
  }

  function testForkGetCheckoutData(gen: SagaIteratorClone) {
    const expected = fork(sagas.getCheckoutData);
    const actual = gen.next({});
    expect(actual.value).toEqual(expected);
  }

  function testPutChangeRouteToShipping(gen: SagaIteratorClone, stepValues: any) {
    const backToShippingGen = gen.clone();
    const expected = put(ChangeRoute('/Checkout/Shipping'));
    const actual = backToShippingGen.next(stepValues);
    expect(actual.value).toEqual(expected);
  }
});

describe('submitStep', () => {
  const payload: StepValuesPayload = {};
  const submitBillingAction = actions.SubmitStep(payload);
  const gen = cloneableGenerator(sagas.submitStep)(submitBillingAction);

  test('should select currentStep', () => {
    const expected = select(selectors.currentStep);
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });

  test('should call submitFulfillmentStep, when current step is Fulfillment', () => {
    const generator = gen.clone();

    const callEffect = call(sagas.submitFulfillmentStep, payload.shipping);
    let actual = generator.next({ type: CheckoutStepType.Fulfillment });
    expect(actual.value).toEqual(callEffect);

    const forkEffect = fork(loadCart);
    actual = generator.next();
    expect(actual.value).toEqual(forkEffect);

    actual = generator.next();
    expect(actual.done).toEqual(true);
  });

  test('should fork submitBillingStep, when current step is Billing', () => {
    const expected = fork(sagas.submitBillingStep, payload.billing);
    const actual = gen.clone().next({ type: CheckoutStepType.Billing });
    expect(actual.value).toEqual(expected);
  });

  test('should fork submitPaymentStep, when current step is Payment', () => {
    const expected = fork(sagas.submitPaymentStep, payload.payment);
    const actual = gen.clone().next({ type: CheckoutStepType.Payment });
    expect(actual.value).toEqual(expected);
  });
});

describe('submitFulfillmentStep', () => {
  describe('invalid fulfillment case', () => {
    const payload: StepValuesPayload = {};
    const gen = cloneableGenerator(sagas.submitFulfillmentStep)(payload.shipping);
    test('should throw exception', () => {
      try {
        gen.next();
      } catch (e) {
        expect((e as Error).message).toContain('Fulfillment is not defined');
      }
    });
  });

  describe('valid fulfillment case', () => {
    const payload: StepValuesPayload = {
      shipping: {
        address: {},
        shippingMethod: {
          shippingMethodId: 'cf0af82a-e1b8-45c2-91db-7b9847af287c',
          shippingMethodName: 'Standard',
        },
      } as any,
    };
    const gen = cloneableGenerator(sagas.submitFulfillmentStep)(payload.shipping);

    test('should select checkoutShippingInfo', () => verifyShouldSelect(gen, selectors.checkoutShippingInfo, {}));
    test('should throw exception, when shippingInfo was not loaded', () => {
      try {
        gen.clone().next({});
      } catch (e) {
        expect((e as Error).message).toContain('ShippingInfo was not loaded');
      }
    });

    test('should select checkoutDeliveryInfo', () =>
      verifyShouldSelect(gen, selectors.checkoutDeliveryInfo, { data: {} }));
    // tslint:disable-next-line
    test('should throw exception, when deliveryInfo was not loaded', () => {
      try {
        gen.clone().next({});
      } catch (e) {
        expect((e as Error).message).toContain('DeliveryInfo was not loaded');
      }
    });

    test('should put SubmitStepRequest, when shippingInfo and deliveryInfo were loaded', () => {
      const expected = put(actions.SubmitStepRequest());
      const actual = gen.next({ data: { newPartyId: 'TEST2', shippingMethods: [] } });
      expect(actual.value).toEqual(expected);
    });

    const address = {
      externalId: 'TEST2',
      name: 'TEMP_NAME',
      partyId: 'TEST2',
    } as any;

    const shippingMethod = {
      partyId: 'TEST2',
      shippingMethodId: 'cf0af82a-e1b8-45c2-91db-7b9847af287c',
      shippingMethodName: 'Standard',
      shippingPreferenceType: '1',
    } as any;

    test('should call setShippingOptions', () => {
      const fakeShippingArgs: SetShippingOptionsRequest = {
        orderShippingPreferenceType: '1',
        shippingAddresses: [address],
        shippingMethods: [shippingMethod],
      };
      const expected = call(Checkout.setShippingOptions, fakeShippingArgs);
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });

    test('should SubmitStepFailure, when setShippingOptions result has error', () => {
      const fakeMessage = 'fakeMessage';
      const fakeStack = 'fakeStack';
      const expected = put(actions.SubmitStepFailure(fakeMessage, fakeStack));
      const actual = gen.clone().next({
        error: {
          message: fakeMessage,
          stack: fakeStack,
        },
      });
      expect(actual.value).toEqual(expected);
    });

    test('should put SubmitStepSuccess', () => {
      const expected = put(actions.SubmitStepSuccess());
      const actual = gen.next({});
      expect(actual.value).toEqual(expected);
    });

    test('should put SetStepValues', () => {
      const expected = put(
        actions.SetStepValues({
          shipping: {
            address,
            shippingMethod,
          } as any,
        }),
      );
      const actual = gen.next();
      expect(actual.value).toEqual(expected);
    });
  });
});

describe('submitBillingStep', () => {
  const payload: StepValuesPayload = {
    billing: {} as any,
  };
  const gen = cloneableGenerator(sagas.submitBillingStep)(payload.billing);

  test('should put SetStepValues', () => {
    const expected = put(actions.SetStepValues({ billing: payload.billing }));
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });

  test('should fork nextStep', () => {
    const expected = fork(sagas.nextStep, CheckoutStepType.Billing);
    const actual = gen.next();
    expect(actual.value).toEqual(expected);
  });
});

describe('submitPaymentStep', () => {
  const fakeCreditCard = {
    cardNumber: '4111111111111111',
    expiresMonth: 10,
    expiresYear: 20,
    securityCode: '123',
  };
  const payload: StepValuesPayload = {
    payment: {
      creditCard: fakeCreditCard,
    } as any,
  };
  const stepValues = {
    billing: {
      useSameAsShipping: true,
    },
    shipping: {
      // tslint:disable-next-line: no-object-literal-type-assertion
      address: {} as Address,
    },
  };
  const gen = cloneableGenerator(sagas.submitPaymentStep)(payload.payment);

  test('should select stepValues', () => verifyShouldSelect(gen, selectors.stepValues, {}));

  test('should select checkoutBillingInfo', () => verifyShouldSelect(gen, selectors.checkoutBillingInfo, stepValues));

  test('should call submitCreditCard', () => {
    const fakeToken = 'fakeToken';
    const creditCard: CreditCard = {
      authToken: fakeToken,
      cvv: fakeCreditCard.securityCode,
      expirationDate: `${fakeCreditCard.expiresMonth}/${fakeCreditCard.expiresYear}`,
      number: fakeCreditCard.cardNumber,
    };
    const expected = call(Checkout.submitCreditCard, creditCard);
    const actual = gen.next({
      data: {
        paymentClientToken: fakeToken,
      },
    });
    expect(actual.value).toEqual(expected);
  });

  test('should put SubmitStepFailure, when submitCreditCard returns error', () => {
    const fakeError = {
      message: 'fakeMessage',
    };
    const expected = put(actions.SubmitStepFailure(fakeError.message));
    const actual = gen.clone().next({ error: fakeError });
    expect(actual.value).toEqual(expected);
  });

  test('should fork handleCreditCard, when submitCreditCard returns data', () => {
    const fakeCardToken = 'fakeCardToken';
    const expected = fork(sagas.handleCreditCard, payload.payment, stepValues.shipping.address, fakeCardToken);
    const actual = gen.clone().next({ data: fakeCardToken });
    expect(actual.value).toEqual(expected);
  });
});

type Func1<T1> = (arg1: T1) => any;

function verifyShouldSelect<S>(gen: SagaIteratorClone, selector: Func1<S>, data: any) {
  const expected = select(selector);
  const actual = gen.next(data);
  expect(actual.value).toEqual(expected);
}
