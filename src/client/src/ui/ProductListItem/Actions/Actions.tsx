//    Copyright 2021 EPAM Systems, Inc.
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

import React, { useEffect, useState } from 'react';

import { useDispatch } from 'react-redux';

import { notify } from 'services/notifications';
import { Product } from 'services/commerce';
import { RemoveCartLine, ShoppingCartLine } from 'services/shoppingCart';

import { Icon } from 'components';

import { cnActions } from './cn';
import './Actions.scss';
type ActionsProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  cartLineItem?: ShoppingCartLine;
};

const REMOVE_BUTTON_TEXT = {
  REMOVE_FROM_CART: 'Remove from Cart',
  REMOVE_DEFAULT: 'Remove item',
};

export const Actions = (props: ActionsProps) => {
  const [displayNotifications] = useState(false);
  const { cartLineItem } = props;
  const dispatch = useDispatch();

  useEffect(() => {
    if (displayNotifications) {
      notify('success', 'Product added!');
    }
  }, [displayNotifications]);


  const getButtonTitle = () => {
    if (cartLineItem) {
      return REMOVE_BUTTON_TEXT.REMOVE_FROM_CART;
    }
    return REMOVE_BUTTON_TEXT.REMOVE_DEFAULT;
  };

  return (
    <div className={cnActions()}>
      <div className={cnActions('PanelBtn')}>
        <button
          title={getButtonTitle()}
          className={cnActions('RemoveBtn')}
          onClick={() => {
            return dispatch(RemoveCartLine(cartLineItem));
          }}
        >
          <Icon icon="icon-close" size="l" />
        </button>
      </div>
    </div>
  );
};
