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

import { eventHub, events } from 'services/eventHub';

import { Product } from 'services/commerce';
import { ShoppingCartLine } from 'services/shoppingCart';

import { NavigationLink } from 'ui/NavigationLink';

import { cnTitle } from './cn';
import './Title.scss';

type TitleProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  orderedItem?: ShoppingCartLine;
  cartLineItem?: ShoppingCartLine;
};

const ITEM_NAME_CLASS = {
  WISHLIST_ITEM: 'WishlistItemName',
  CART_LINE_ITEM: 'CartLineItemName',
  ORDERED_ITEM: 'OrderedItemName',
  OUT_OF_STOCK_ITEM: 'OutOfStockItemName',
};

export const Title = (props: TitleProps) => {
  const { wishlistItem, orderedItem, cartLineItem, outOfStockItem } = props;

  return (
    <div className={cnTitle()}>
      {(wishlistItem || cartLineItem) && (
        <NavigationLink
          to={
            wishlistItem
              ? `/product/${wishlistItem.productId}`
              : `/product/${cartLineItem.product.productId}?v=${cartLineItem.variant.variantId}`
          }
          onClick={() =>
            eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, {
              ...cartLineItem.variant,
              list: window.location.pathname,
            })
          }
        >
          <p className={cnTitle(wishlistItem ? ITEM_NAME_CLASS.WISHLIST_ITEM : ITEM_NAME_CLASS.CART_LINE_ITEM)}>
            {wishlistItem
              ? wishlistItem.displayName
              : cartLineItem.variant.displayName || cartLineItem.product.displayName}
          </p>
        </NavigationLink>
      )}
      {orderedItem && <div className={cnTitle(ITEM_NAME_CLASS.ORDERED_ITEM)}>{orderedItem.variant.displayName}</div>}
      {outOfStockItem && (
        <NavigationLink
          to={`/product/${outOfStockItem.productId}`}
          onClick={() =>
            eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, {
              ...cartLineItem.variant,
              list: window.location.pathname,
            })
          }
        >
          <p className={cnTitle(ITEM_NAME_CLASS.OUT_OF_STOCK_ITEM)}>{outOfStockItem.displayName}</p>
        </NavigationLink>
      )}
    </div>
  );
};
