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

import React from 'react';

import { Product } from 'services/commerce';
import { ShoppingCartLine } from 'services/shoppingCart';

import { Image } from './Image';
import { Description } from './Description';
import { Actions } from './Actions';
import { OrderedItemPrice } from './OrderedItemPrice';

import { cnProductListItem } from './cn';
import './ProductListItem.scss';

type ProductListItemProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  orderedItem?: ShoppingCartLine;
  cartLineItem?: ShoppingCartLine;
  cartLineItemUrl?: string;
  orderedItemUrl?: string;
};

const WRAPPER_CLASS = {
  ORDERED_ITEM: 'WrapperOrderedItemInfo',
  OUT_OF_STOCK_ITEM: 'WrapperOutOfStockItemInfo',
  DEFAULT: 'Wrapper',
};

export const ProductListItem = (props: ProductListItemProps) => {
  const { wishlistItem, orderedItemUrl, orderedItem, cartLineItem, cartLineItemUrl, outOfStockItem } = props;
  const urlImg = orderedItemUrl || cartLineItemUrl;
  const wrapperClass =
    (orderedItem && WRAPPER_CLASS.ORDERED_ITEM) ||
    (outOfStockItem && WRAPPER_CLASS.OUT_OF_STOCK_ITEM) ||
    WRAPPER_CLASS.DEFAULT;

  return (
    <div className={cnProductListItem()}>
      <Image wishlistItem={wishlistItem} outOfStockItem={outOfStockItem} urlImg={urlImg} />
      <div className={cnProductListItem(wrapperClass)}>
        <Description
          wishlistItem={wishlistItem}
          orderedItem={orderedItem}
          cartLineItem={cartLineItem}
          outOfStockItem={outOfStockItem}
        />
        {wishlistItem || cartLineItem || outOfStockItem ? (
          <Actions wishlistItem={wishlistItem} cartLineItem={cartLineItem} outOfStockItem={outOfStockItem} />
        ) : (
          <OrderedItemPrice orderedItem={orderedItem} />
        )}
      </div>
    </div>
  );
};
