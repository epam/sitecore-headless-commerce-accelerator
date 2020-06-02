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

import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { Confirmation, Summary, ThankYouMessage  } from './components';
import { OrderConfirmationProps, OrderConfirmationState } from './models';

import './styles.scss';

export class OrderConfirmationComponent extends JSS.SafePureComponent<OrderConfirmationProps, OrderConfirmationState> {
  constructor(props: OrderConfirmationProps) {
    super(props);
  }

  public updateState(props: OrderConfirmationProps) {
    if (props.trackingNumber) {
      this.props.GetOrder(props.trackingNumber);
    }
  }

  public componentWillReceiveProps(nextProps: Readonly<OrderConfirmationProps>) {
    if (nextProps.trackingNumber !== this.props.trackingNumber) {
      this.updateState(nextProps);
    }
  }

  public componentDidMount() {
    this.updateState(this.props);
  }

  public safeRender() {
    const { currentOrder, sitecoreContext } = this.props;
    if (!currentOrder) {
      return null;
    }

    return (
      <>
        <ThankYouMessage order={currentOrder} />
        <Summary order={currentOrder} productColors={sitecoreContext.productColors} fallbackImageUrl={sitecoreContext.fallbackImageUrl} />
        <Confirmation order={currentOrder} />
      </>
    );
  }
}
