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

import React, { FC, useContext } from 'react';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';
import './Price.scss';

export type PriceProps = {
  className?: string;
};

const getPrice = (value: number) => value.toFixed(2);

export const Price: FC<PriceProps> = ({ className }) => {
  const contextData = useContext(ProductCardContext);
  const { selectedVariant } = contextData;
  const { currencySymbol, adjustedPrice, listPrice: fullPrice } = selectedVariant;

  const isDifferentPrice = adjustedPrice < fullPrice;

  return (
    <div className={cnProductCard('Price', [className])}>
      {isDifferentPrice && (
        <>
          <span className={cnProductCard('Currency')}>{currencySymbol}</span>
          <span className={cnProductCard('AdjustedPrice')}>{getPrice(adjustedPrice)}</span>
          <span className={cnProductCard('Separator')}>–</span>
        </>
      )}

      <span className={cnProductCard('FullPriceContainer', { lineThrough: isDifferentPrice })}>
        <span className={cnProductCard('Currency')}>{currencySymbol}</span>
        <span className={cnProductCard('FullPrice')}>{getPrice(fullPrice)}</span>
      </span>
    </div>
  );
};
