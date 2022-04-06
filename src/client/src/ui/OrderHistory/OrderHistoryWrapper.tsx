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

import React from 'react';

import { SafePureComponent } from 'Foundation/ReactJss';

import { AccountWrapper } from 'ui/AccountWrapper';

import { OrderHistoryProps, OrderHistoryState } from './models';
import { OrderHistoryComponent } from './Component';

const ORDER_HISTORY_WRAPPER_CONTENT = {
  TITLE: 'Order History',
  BACK: 'Back to My account',
  PATH: '/MyAccount',
};

export class OrderHistoryWrapper extends SafePureComponent<OrderHistoryProps, OrderHistoryState> {
  public safeRender() {
    const { orders, isLastPage, loaded, sitecoreContext, rendering, fields, GetOrderHistory, LoadMore } = this.props;

    return (
      <AccountWrapper
        title={ORDER_HISTORY_WRAPPER_CONTENT.TITLE}
        leaveLinkText={ORDER_HISTORY_WRAPPER_CONTENT.BACK}
        leaveLinkPath={ORDER_HISTORY_WRAPPER_CONTENT.PATH}
        isLeaveLink={true}
        rendering={rendering}
      >
        <OrderHistoryComponent
          sitecoreContext={sitecoreContext}
          rendering={rendering}
          orders={orders}
          loaded={loaded}
          isLastPage={isLastPage}
          GetOrderHistory={GetOrderHistory}
          LoadMore={LoadMore}
          fields={fields}
        />
      </AccountWrapper>
    );
  }
}
