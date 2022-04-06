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

import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { GetAllOrders, getAllOrders, getAllOrdersStatus } from 'services/order';
import { Order } from 'services/commerce';
import { LoadingStatus } from 'models';

import { SectionWrapper } from '../SectionWrapper';
import { ActiveOrderList } from './ActiveOrderList';

import { cnActiveOrder } from './cn';
import './ActiveOrder.scss';

const ACTIVE_ORDER_CONTENT = {
  TITLE: 'Active orders',
  LINK_TEXT: 'view all',
  PATH: '/MyAccount/Order-History',
};

const ORDER_STATUSES = {
  COMPLETED: 'Completed',
  WAITING_FOR_AVAILABILITY: 'WaitingForAvailability',
  PENDING: 'Pending',
  RELEASED: 'Released',
  ON_HOLD: 'OnHold',
};

export const ActiveOrder = () => {
  const dispatch = useDispatch();
  const allOrdersStatus = useSelector(getAllOrdersStatus);
  const loaded = allOrdersStatus === LoadingStatus.Loaded;

  const allOrders = useSelector(getAllOrders);
  const activeOrders = allOrders?.filter((order: Order) => {
    return (
      order.status === ORDER_STATUSES.PENDING ||
      order.status === ORDER_STATUSES.WAITING_FOR_AVAILABILITY ||
      order.status === ORDER_STATUSES.RELEASED ||
      order.status === ORDER_STATUSES.ON_HOLD
    );
  });
  const itemsCount = activeOrders?.length;
  const activeOrdersToShowDesktop = activeOrders?.slice(-2);
  const activeOrdersToShowMobile = activeOrders;

  useEffect(() => {
    dispatch(GetAllOrders());
  }, []);

  return (
    <div className={cnActiveOrder({ isHiddenOnMobile: !itemsCount })}>
      <SectionWrapper
        title={ACTIVE_ORDER_CONTENT.TITLE}
        linkText={ACTIVE_ORDER_CONTENT.LINK_TEXT}
        path={ACTIVE_ORDER_CONTENT.PATH}
        contentType={'borderTop'}
        itemsCount={itemsCount}
        isLinkVisible={!!itemsCount}
      >
        <ActiveOrderList
          activeOrdersToShowDesktop={activeOrdersToShowDesktop}
          activeOrdersToShowMobile={activeOrdersToShowMobile}
          loaded={loaded}
        />
      </SectionWrapper>
    </div>
  );
};
