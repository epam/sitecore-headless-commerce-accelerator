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

import { NavigationLink } from 'ui/NavigationLink';

import { cnImage } from './cn';
import './Image.scss';

type ImageProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
  urlImg?: string;
};

export const Image = (props: ImageProps) => {
  const { wishlistItem, urlImg, outOfStockItem } = props;
  const item = wishlistItem || outOfStockItem;

  return (
    <div className={cnImage({ outOfStockItem: !!outOfStockItem })}>
      <div className={cnImage('ProductImage', { outOfStockItem: !!outOfStockItem })}>
        {item ? (
          <NavigationLink to={`/product/${item.productId}`}>
            <img src={item.imageUrls[0]} alt="" />
          </NavigationLink>
        ) : (
          <img src={urlImg} alt="Product image" />
        )}
      </div>
    </div>
  );
};
