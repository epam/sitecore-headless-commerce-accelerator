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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import * as Checkout from 'Feature/Checkout/client/Integration/Checkout';

import Component from './Component';
import * as Models from './models';

const mapStateToProps = (state: Models.AppState): Models.BillingStateProps => {
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

const connectedComponent = connect<Models.BillingStateProps, Models.BillingDispatchProps, Models.BillingOwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(Component);

export const Billing = withExperienceEditorChromes(connectedComponent);
