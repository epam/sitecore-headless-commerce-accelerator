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

import React, { useRef } from 'react';
import Swiper from 'react-id-swiper';

import { Order } from 'services/commerce';
import { Spinner } from 'components';
import { useWindowSize } from 'hooks';
import { TABLET_MAX_SCREEN_WIDTH } from 'components/Responsive/constants';

import { ActiveOrderItem } from '../ActiveOrderItem';
import { ActiveOrderListProps } from './models';

import { cnActiveOrderList } from './cn';
import './ActiveOrderList.scss';

const ACTIVE_ORDER_LIST_CONTENT = {
  DEFAULT_TEXT: '0 active orders ',
};

export const ActiveOrderList = (props: ActiveOrderListProps) => {
  const { loaded, activeOrdersToShowDesktop, activeOrdersToShowMobile } = props;
  const ref = useRef(null);
  const { width } = useWindowSize();
  const isMobileMode = width <= TABLET_MAX_SCREEN_WIDTH;

  const swiperParams = {
    freeMode: false,
    height: 140,
    // The slidesPerView property of SwiperConfigInterface expects a value of type number or a value of type 'auto':
    // slidesPerView?: number | 'auto',
    // Typescript considers 'auto' as of type string in this object,
    // so we force the compiler to see it as a value of type 'auto' by casting it 'auto' as 'auto'
    slidesPerView: 'auto' as 'auto',
  };

  const renderActiveOrderList = () => {
    if (isMobileMode) {
      return loaded ? (
        <div className={cnActiveOrderList()}>
          <Swiper {...swiperParams} ref={ref}>
            {activeOrdersToShowMobile?.map((order: Order) => (
              <div key={order.id}>
                <ActiveOrderItem order={order} />
              </div>
            ))}
          </Swiper>
        </div>
      ) : (
        <Spinner className="spinner_small" data-autotests="loading_spinner" />
      );
    }

    return loaded ? (
      <div className={cnActiveOrderList()}>
        {activeOrdersToShowDesktop?.length
          ? activeOrdersToShowDesktop.map((order: Order) => {
              return <ActiveOrderItem key={order.id} order={order} />;
            })
          : ACTIVE_ORDER_LIST_CONTENT.DEFAULT_TEXT}
      </div>
    ) : (
      <Spinner className="spinner_small" data-autotests="loading_spinner" />
    );
  };

  return <div className={cnActiveOrderList()}>{renderActiveOrderList()}</div>;
};
