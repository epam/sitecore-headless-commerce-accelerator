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

import {
  AddToCart,
  ColorVariants,
  Price,
  ProductCard,
  ProductName,
  ProductRating,
  ThumbnailSlider,
} from 'ui/ProductCard';
import { Product } from 'services/search';

import { cnProductList } from '../cn';
import './ProductItem.scss';

export type ProductItemProps = {
  fallbackImageUrl: string;
  product: Product;
  productColors: Record<string, string>;
};

export const ProductItem: FC<ProductItemProps> = ({ fallbackImageUrl, product, productColors }) => (
  <ProductCard
    className={cnProductList('ProductCard')}
    fallbackImageUrl={fallbackImageUrl}
    product={product}
    productColors={productColors}
  >
    <ThumbnailSlider />
    <ColorVariants className={cnProductList('Variants')} />
    <ProductName />
    <ProductRating className={cnProductList('Rating')} />
    <Price className={cnProductList('Price')} />
    <AddToCart />
  </ProductCard>
);
