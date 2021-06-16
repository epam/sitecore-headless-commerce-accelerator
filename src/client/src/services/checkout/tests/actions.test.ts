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

import * as Commerce from 'Foundation/Commerce';
import { LoadingStatus } from 'models';

import * as actions from '../actions';
import { reducerActionTypes, sagaActionTypes } from '../constants';
import * as Models from '../models';

describe('Checkout Data actions', () => {
    test('should return action for GET_CHECKOUT_DATA', () => {
        const actual = actions.GetCheckoutData();
        expect(actual).toEqual({
            type: sagaActionTypes.GET_CHECKOUT_DATA
        });
    });

    test('should return action for GET_CHECKOUT_DATA_REQUEST with Delivery Loading', () => {
        const actual = actions.GetDeliveryInfoRequest();
        expect(actual).toEqual({
            payload: {
                delivery: {
                    status: LoadingStatus.Loading
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST
        });
    });

    test('should return action for GET_CHECKOUT_DATA_REQUEST with Shipping Loading', () => {
        const actual = actions.GetShippingInfoRequest();
        expect(actual).toEqual({
            payload: {
                shipping: {
                    status: LoadingStatus.Loading
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST
        });
    });

    test('should return action for GET_CHECKOUT_DATA_REQUEST with Billing Loading', () => {
        const actual = actions.GetBillingInfoRequest();
        expect(actual).toEqual({
            payload: {
                billing: {
                    status: LoadingStatus.Loading
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_REQUEST
        });
    });

    test('should return action for GET_CHECKOUT_DATA_FAILURE with delivery error', () => {
        const fakeError = 'fakeError';
        const actual = actions.GetDeliveryInfoFailure(fakeError);
        expect(actual).toEqual({
            payload: {
                delivery: {
                    error: fakeError,
                    status: LoadingStatus.Failure
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE
        });
    });

    test('should return action for GET_CHECKOUT_DATA_FAILURE with shipping error', () => {
        const fakeError = 'fakeError';
        const actual = actions.GetShippingInfoFailure(fakeError);
        expect(actual).toEqual({
            payload: {
                shipping: {
                    error: fakeError,
                    status: LoadingStatus.Failure
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE
        });
    });

    test('should return action for GET_CHECKOUT_DATA_FAILURE with billing error', () => {
        const fakeError = 'fakeError';
        const actual = actions.GetBillingInfoFailure(fakeError);
        expect(actual).toEqual({
            payload: {
                billing: {
                    error: fakeError,
                    status: LoadingStatus.Failure
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_FAILURE
        });
    });

    test('should return action for GET_CHECKOUT_DATA_SUCCESS with Delivery Loaded', () => {
        const expectedDeliveryInfo: Commerce.DeliveryInfo = {
            newPartyId: 'fakeId',
            shippingOptions: [],
            userAddresses: []
        };
        const actual = actions.GetDeliveryInfoSuccess(expectedDeliveryInfo);
        expect(actual).toEqual({
            payload: {
                delivery: {
                    data: expectedDeliveryInfo,
                    status: LoadingStatus.Loaded
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS
        });
    });

    test('should return action for GET_CHECKOUT_DATA_SUCCESS with Shipping Loaded', () => {
        const expectedShippingModel: Commerce.ShippingInfo = {
            shippingMethods: [],
        };
        const actual = actions.GetShippingInfoSuccess(expectedShippingModel);
        expect(actual).toEqual({
            payload: {
                shipping: {
                    data: expectedShippingModel,
                    status: LoadingStatus.Loaded
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS
        });
    });

    test('should return action for GET_CHECKOUT_DATA_SUCCESS with Billing Loaded', () => {
        const expectedBillingInfo: Commerce.BillingInfo = {
            paymentClientToken: 'fakeToken',
            paymentMethods: [],
            paymentOptions: [],
        };
        const actual = actions.GetBillingInfoSuccess(expectedBillingInfo);
        expect(actual).toEqual({
            payload: {
                billing: {
                    data: expectedBillingInfo,
                    status: LoadingStatus.Loaded
                }
            },
            type: reducerActionTypes.GET_CHECKOUT_DATA_SUCCESS
        });
    });
});

describe('Checkout Step actions', () => {
    test('should return action for INIT_STEP with given Step Type', () => {
        const expectedStepType = Models.CheckoutStepType.Initial;
        const actual = actions.InitStep(expectedStepType);
        expect(actual).toEqual({
            payload: { type: expectedStepType },
            type: sagaActionTypes.INIT_STEP
        });
    });

    test('should return action for SUBMIT_STEP with given Step Values', () => {
        const expectedStepValues: Models.StepValuesPayload = {};
        const actual = actions.SubmitStep(expectedStepValues);
        expect(actual).toEqual({
            payload: expectedStepValues,
            type: sagaActionTypes.SUBMIT_STEP
        });
    });

    test('should return action for SET_STEP_VALUES', () => {
        const expectedPayload: Models.StepValuesPayload = {};
        const actual = actions.SetStepValues(expectedPayload);
        expect(actual).toEqual({
            payload: expectedPayload,
            type: reducerActionTypes.SET_STEP_VALUES
        });
    });

    test('should return action for SET_CURRENT_STEP', () => {
        const expectedPayload: Models.CurrentStepPayload = {};
        const actual = actions.SetCurrentStep(expectedPayload);
        expect(actual).toEqual({
            payload: expectedPayload,
            type: reducerActionTypes.SET_CURRENT_STEP
        });
    });

    test('should return action for SUBMIT_STEP_REQUEST', () => {
        const actual = actions.SubmitStepRequest();
        expect(actual).toEqual({
            payload: {
                status: LoadingStatus.Loading
            },
            type: reducerActionTypes.SUBMIT_STEP_REQUEST
        });
    });

    test('should return action for SUBMIT_STEP_FAILURE', () => {
        const fakeError = 'fakeError';
        const actual = actions.SubmitStepFailure(fakeError);
        expect(actual).toEqual({
            payload: {
                error: fakeError,
                status: LoadingStatus.Failure
            },
            type: reducerActionTypes.SUBMIT_STEP_FAILURE
        });
    });

    test('should return action for SUBMIT_STEP_SUCCESS', () => {
        const actual = actions.SubmitStepSuccess();
        expect(actual).toEqual({
            payload: {
                status: LoadingStatus.Loaded
            },
            type: reducerActionTypes.SUBMIT_STEP_SUCCESS
        });
    });
});
