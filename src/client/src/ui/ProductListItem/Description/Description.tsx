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

import { ProductCardContext } from 'ui/ProductCard/context';

import { Title } from './Title';
import { Options } from './Options';
import { QuantityControl } from './QuantityControl';

import { cnDescription } from './cn';
import './Description.scss';
import { AdditionalInformation } from './AdditionalInformation';

type DescriptionProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  orderedItem?: ShoppingCartLine;
  cartLineItem?: ShoppingCartLine;
};

const WRAPPER_CLASS = {
  WISHLIST_ITEM: 'WrapWishlistInfoProduct',
  OUT_OF_STOCK_ITEM: 'WrapOutOfStockInfoProduct',
  DEFAULT: 'WrapInfoProduct',
};

export const Description = (props: DescriptionProps) => {
  const { wishlistItem, orderedItem, cartLineItem, outOfStockItem } = props;
  const context = (wishlistItem || outOfStockItem) && {
    product: wishlistItem || outOfStockItem,
    selectedVariant: wishlistItem?.variants[0] || outOfStockItem?.variants[0],
    onChangeVariant: (variantId: string) => {
      return;
    },
  };

  const wrapperClass =
    (wishlistItem && WRAPPER_CLASS.WISHLIST_ITEM) ||
    (outOfStockItem && WRAPPER_CLASS.OUT_OF_STOCK_ITEM) ||
    WRAPPER_CLASS.DEFAULT;

  return (
    <div className={cnDescription({ outOfStockItem: !!outOfStockItem })}>
      <div className={cnDescription(wrapperClass)}>
        <div className={cnDescription('Information')}>
          <Title
            wishlistItem={wishlistItem}
            cartLineItem={cartLineItem}
            orderedItem={orderedItem}
            outOfStockItem={outOfStockItem}
          />
          {(wishlistItem || outOfStockItem) && (
            <AdditionalInformation wishlistItem={wishlistItem} outOfStockItem={outOfStockItem} />
          )}
        </div>
        <ProductCardContext.Provider value={context}>
          <Options
            wishlistItem={wishlistItem}
            cartLineItem={cartLineItem}
            orderedItem={orderedItem}
            outOfStockItem={outOfStockItem}
          />
        </ProductCardContext.Provider>
      </div>
      {cartLineItem && <QuantityControl cartLineItem={cartLineItem} />}
    </div>
  );
};
