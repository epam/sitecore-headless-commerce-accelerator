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

import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import { renderingWithContext } from 'Foundation/ReactJss/client';

import * as Checkout from 'Feature/Checkout/client/Integration/Checkout';

import BillingComponent from './Component';
import { AppState, BillingDispatchProps, BillingOwnProps, BillingStateProps } from './models';

const mapStateToProps = (state: AppState): BillingStateProps => {
  const stepValues = Checkout.stepValues(state);
  const useForBillingAddress = stepValues.shipping && stepValues.shipping.options && stepValues.shipping.options.useForBillingAddress;
  return {
    useForBillingAddress
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      InitStep: Checkout.InitStep,
      SubmitStep: Checkout.SubmitStep,
    },
    dispatch
  );

const connectedToStore = connect<BillingStateProps, BillingDispatchProps, BillingOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const Billing = compose(connectedToStore, renderingWithContext)(BillingComponent);
