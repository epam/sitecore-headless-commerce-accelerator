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

import { NavigationLink } from 'ui/NavigationLink';

import { ActiveOrderItemProps } from './models';
import { getFormattedDate, getFormattedStatus } from './utils';

import { cnActiveOrderItem } from './cn';
import './ActiveOrderItem.scss';

const MAX_IMAGES_COUNT = 3;

export const ActiveOrderItem = (props: ActiveOrderItemProps) => {
  const { order } = props;
  const orderID = order.orderID.split('-').pop().slice(-11);

  const formattedDate = getFormattedDate(order.orderDate);
  const formattedStatus = getFormattedStatus(order.status);

  const path = `/Checkout/Confirmation?trackingNumber=${order.trackingNumber}`;
  const linkMoreText =
    order.cartLines.length > MAX_IMAGES_COUNT && `+ ${order.cartLines.length - MAX_IMAGES_COUNT} more`;

  return (
    <div className={cnActiveOrderItem()}>
      <div className={cnActiveOrderItem('Info')}>
        <div className={cnActiveOrderItem('Element', { id: true })}>
          <NavigationLink to={path}>{orderID}</NavigationLink>
        </div>
        <div className={cnActiveOrderItem('Element', { date: true })}>{formattedDate}</div>
        <div className={cnActiveOrderItem('Element', { price: true })}>
          {order.price.currencySymbol + order.price.total}
        </div>
        <div className={cnActiveOrderItem('Element', { status: true })}>{formattedStatus}</div>
      </div>
      <div className={cnActiveOrderItem('Images')}>
        {order.cartLines.slice(0, MAX_IMAGES_COUNT).map((cartLine, cartLineIndex) => (
          <div key={cartLineIndex} className={cnActiveOrderItem('ImageContainer')}>
            <img className={cnActiveOrderItem('Image')} src={cartLine.variant.imageUrls[0]} alt="product image" />
          </div>
        ))}
        {order.cartLines.length > MAX_IMAGES_COUNT && (
          <NavigationLink className={cnActiveOrderItem('Link')} to={path}>
            {linkMoreText}
          </NavigationLink>
        )}
      </div>
    </div>
  );
};
