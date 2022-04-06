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

import React, { FC } from 'react';

import { Product } from 'services/commerce';

import { cnRelatedProducts } from '../cn';

import './Item.scss';

import { AddToCart, Price, ProductCard, ProductName, ProductRating, ThumbnailSlider } from 'ui/ProductCard';

export type ItemProps = {
  fallbackImageUrl: string;
  product: Product;
  productColors: Record<string, string>;
};

export const Item: FC<ItemProps> = ({ fallbackImageUrl, product, productColors }) => {
  return (
    <ProductCard fallbackImageUrl={fallbackImageUrl} product={product} productColors={productColors}>
      <ThumbnailSlider />
      <ProductName />
      <ProductRating className={cnRelatedProducts('Rating')} />
      <Price className={cnRelatedProducts('Price')} />
      <AddToCart />
    </ProductCard>
  );
};
