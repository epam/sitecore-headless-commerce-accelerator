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

import React, { useCallback, useState } from 'react';

import { useDispatch } from 'react-redux';

import { debounce } from 'lodash';

import { ShoppingCartLine, UpdateCartLine as updateCartLine } from 'services/shoppingCart';

import { QuantityPicker } from 'components';

import { cnQuantityControl } from './cn';
import './QuantityControl.scss';

type QuantityControlProps = {
  cartLineItem: ShoppingCartLine;
};

const MIN_QUANTITY = 1;
const MAX_QUANTITY = 99;
const DEBOUNCE_DELAY = 1000;

export const QuantityControl = (props: QuantityControlProps) => {
  const { cartLineItem } = props;
  const [quantity, setQuantity] = useState(cartLineItem.quantity);
  const dispatch = useDispatch();

  const debouncedUpdateCartLine = debounce((value: number) => {
    dispatch(
      updateCartLine({
        productId: cartLineItem.product.productId,
        quantity: value,
        variantId: cartLineItem.variant.variantId,
      }),
    );
    setQuantity(() => value);
  }, DEBOUNCE_DELAY);

  const handleQuantityChange = useCallback((value: number) => {
    debouncedUpdateCartLine(value);
  }, []);

  return (
    <div className={cnQuantityControl()}>
      <div className={cnQuantityControl('ProductSubtotal')}>
        <span>{cartLineItem.price.currencySymbol}</span>
        {cartLineItem.price.total.toFixed(2)}
      </div>
      <div className={cnQuantityControl('ProductQuantity')}>
        <QuantityPicker
          value={quantity}
          onChange={handleQuantityChange}
          min={MIN_QUANTITY}
          max={MAX_QUANTITY}
          theme="grey"
        />
      </div>
    </div>
  );
};
