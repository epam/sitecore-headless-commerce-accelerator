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

import { LoadingStatus } from 'Foundation/Integration/client';
import { renderingWithContext } from 'Foundation/ReactJss/client';

import * as Checkout from 'Feature/Checkout/client/Integration/Checkout';

import PaymentComponent from './Component';
import * as Models from './models';

const mapStateToProps = (state: Models.AppState) => {
  const currentStep = Checkout.currentStep(state);

  return { isSubmitting: currentStep.status === LoadingStatus.Loading };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      InitStep: Checkout.InitStep,
      SubmitStep: Checkout.SubmitStep,
    },
    dispatch
  );

const connectedToStore = connect<Models.PaymentStateProps, Models.PaymentDispatchProps, Models.PaymentOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const Payment = compose(connectedToStore, renderingWithContext)(PaymentComponent);
