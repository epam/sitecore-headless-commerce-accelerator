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

import React from 'react';

import * as JSS from 'Foundation/ReactJss/client';
import { NavigationLink } from 'Foundation/UI/client';

import { GoToCheckoutProps, GoToCheckoutState } from './models';

class GoToCheckoutComponent extends JSS.SafePureComponent<GoToCheckoutProps, GoToCheckoutState> {
  public safeRender() {
    const checkoutText = 'Checkout';
    const { checkoutSteps } = this.props.fields;
    return (
      <div className="orderSummary-checkout">
        {!!checkoutSteps && (
          <NavigationLink to={this.props.fields.url} className="btn">
            {checkoutText}
          </NavigationLink>
        )}
      </div>
    );
  }
}
export const GoToCheckout = JSS.rendering(GoToCheckoutComponent);
