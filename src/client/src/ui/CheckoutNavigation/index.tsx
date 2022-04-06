//    Copyright 2022 EPAM Systems, Inc.
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

import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { NavigationLink } from 'ui/NavigationLink';
import { Icon } from 'components';

import { NavigationProps, NavigationState } from './models';

import './styles.scss';

class NavigationComponent extends JSS.SafePureComponent<NavigationProps, NavigationState> {
  public safeRender() {
    const { checkoutSteps, backToLink } = this.props.fields;

    const getActivePageIndex = () => {
      const { checkoutSteps } = this.props.fields;
      const { routeFields } = this.props.sitecoreContext;
      return checkoutSteps?.findIndex((checkoutStep) => {
        return routeFields?.id === checkoutStep.fields.id;
      });
    };

    return (
      <nav className="nav-checkout">
        <ul>
          {checkoutSteps &&
            checkoutSteps.map((checkoutStep, checkoutStepIndex) => {
              const { checkoutStepName } = checkoutStep.fields;
              const previosItem = checkoutStepIndex - 1;
              const previosPage = checkoutSteps[previosItem]
                ? checkoutSteps[previosItem].fields.checkoutStepName.value
                : '';

              const stepClassName =
                this.props.sitecoreContext.routeFields &&
                this.props.sitecoreContext.routeFields.id === checkoutStep.fields.id
                  ? 'active'
                  : '';

              const activePageIndex = getActivePageIndex();
              const isPreviousPage = checkoutStepIndex < activePageIndex;

              return (
                <li key={checkoutStepIndex} className={stepClassName}>
                  {checkoutStepIndex > 0 && (
                    <NavigationLink className="BackButton" to={'/Checkout/' + previosPage}>
                      <Icon icon="icon-angle-left" size="xxl" />
                    </NavigationLink>
                  )}
                  {isPreviousPage ? (
                    <NavigationLink to={'/Checkout/' + checkoutStepName.value}>{checkoutStepName.value}</NavigationLink>
                  ) : (
                    <div className="CheckoutStep">{checkoutStepName.value}</div>
                  )}
                </li>
              );
            })}
        </ul>
        <span className="back-to">
          <NavigationLink to={backToLink.value.href}>{backToLink.value.text}</NavigationLink>
        </span>
      </nav>
    );
  }
}

export const CheckoutNavigation = JSS.renderingWithContext(NavigationComponent);
