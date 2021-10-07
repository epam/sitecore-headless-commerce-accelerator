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

import React, { FC, useState } from 'react';

import { ShoppingCartLine, UpdateCartLine as updateCartLine } from 'services/shoppingCart';

import { QuantityPicker } from 'components';

import './styles.scss';
import { useDispatch } from 'react-redux';

type Props = {
  cartLine: ShoppingCartLine;
};

export const Quantity: FC<Props> = ({ cartLine }) => {
  const [quantity, setQuantity] = useState(cartLine.quantity);
  const dispatch = useDispatch();

  const handleQuantityChange = (value: number) => {
    setQuantity(value);
    dispatch(
      updateCartLine({
        productId: cartLine.product.productId,
        quantity: value,
        variantId: cartLine.variant.variantId,
      }),
    );
  };

  return <QuantityPicker value={quantity} onChange={handleQuantityChange} min={1} max={99} size="l" theme="grey" />;
};
