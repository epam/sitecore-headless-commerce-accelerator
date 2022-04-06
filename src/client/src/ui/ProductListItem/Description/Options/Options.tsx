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

import { ShoppingCartLine } from 'services/shoppingCart';
import { Product } from 'services/commerce';

import { AddToCart } from 'ui/ProductCard';

import { cnOptions } from './cn';
import './Options.scss';

type OptionsProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  orderedItem?: ShoppingCartLine;
  cartLineItem?: ShoppingCartLine;
};

const fields = {
  colorTitle: {
    value: 'Color:',
  },
  priceWasTitle: {
    value: 'Was',
  },
  quantityTitle: {
    value: 'Qty:',
  },
  sizeTitle: {
    value: 'Size:',
  },
};

const OUT_OF_STOCK_STATUSES = {
  OUT_OF_STOCK: 'OutOfStock',
};

export const Options = (props: OptionsProps) => {
  const { wishlistItem, orderedItem, cartLineItem, outOfStockItem } = props;
  const item = orderedItem || cartLineItem;
  const isUpdatedPrice = wishlistItem ? wishlistItem.listPrice > wishlistItem.adjustedPrice : false;

  return (
    <div className={cnOptions({ outOfStockItem: !!outOfStockItem })}>
      {item && (
        <>
          {cartLineItem && (
            <div className={cnOptions('Price')}>
              <span className={cnOptions('currency')}>{cartLineItem.variant.currencySymbol}</span>
              {cartLineItem.variant.listPrice.toFixed(2)}
            </div>
          )}

          {orderedItem && (
            <div className={cnOptions('Quantity')}>
              <span>{fields.quantityTitle.value + ' ' + item.quantity}</span>
            </div>
          )}
          {item.variant.properties.color && (
            <div className={cnOptions('Color')}>
              <span>{fields.colorTitle.value + ' ' + item.variant.properties.color}</span>
            </div>
          )}
          {item.variant.properties.size && (
            <div className={cnOptions('Size')}>
              <span>{fields.sizeTitle.value + ' ' + item.variant.properties.size}</span>
            </div>
          )}
        </>
      )}
      {wishlistItem && (
        <div className={cnOptions('WrapPrice', { outOfStockItem: !!outOfStockItem })}>
          <span className={cnOptions(isUpdatedPrice ? 'discount' : 'origin')}>
            <span className={cnOptions('currency')}>{wishlistItem.currencySymbol}</span>
            {wishlistItem.listPrice.toFixed(2)}
          </span>
          {isUpdatedPrice && (
            <span>
              <span className={cnOptions('currency')}>{wishlistItem.currencySymbol}</span>
              {wishlistItem.adjustedPrice.toFixed(2)}
            </span>
          )}
          <span className={cnOptions('AddButton')}>
            <AddToCart />
          </span>
        </div>
      )}
      {outOfStockItem && (
        <div className={cnOptions('WrapPrice', { outOfStockItem: !!outOfStockItem })}>
          <span className={cnOptions(isUpdatedPrice ? 'discount' : 'origin')}>
            <span className={cnOptions('currency')}>{outOfStockItem.currencySymbol}</span>
            {outOfStockItem.listPrice.toFixed(2)}
          </span>
          {isUpdatedPrice && (
            <span>
              <span className={cnOptions('currency')}>{outOfStockItem.currencySymbol}</span>
              {outOfStockItem.adjustedPrice.toFixed(2)}
            </span>
          )}
          <span
            className={cnOptions('AddButton', {
              outOfStockItem: !!outOfStockItem,
              isOutOfStock: outOfStockItem.stockStatusName === OUT_OF_STOCK_STATUSES.OUT_OF_STOCK,
            })}
          >
            <AddToCart />
          </span>
        </div>
      )}
    </div>
  );
};
