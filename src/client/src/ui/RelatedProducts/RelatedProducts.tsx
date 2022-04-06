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

import React, { FC, useCallback, useRef } from 'react';

import { get } from 'lodash';
import Swiper from 'react-id-swiper';

import { Product } from 'services/commerce';

import { Button, Icon } from 'components';

import { Item } from './Item';
import { mockData } from './mock';

import { cnRelatedProducts } from './cn';
import './RelatedProducts.scss';

const NUMBER_OF_SLIDES = 4;
const DEFAULT_NUMBER_OF_SLIDES = 1;

export type RelatedProductsProps = {
  fallbackImageUrl?: string | null;
  products?: Product[];
  productColors: Record<string, string>;
};

const params = {
  breakpoints: {
    480: {
      slidesPerView: 1,
      spaceBetween: 25,
    },
    1024: {
      slidesPerView: 2,
      spaceBetween: 25,
    },
    1310: {
      slidesPerView: 3,
      spaceBetween: 25,
    },
    1440: {
      slidesPerView: NUMBER_OF_SLIDES,
      spaceBetween: 0,
    },
  },
  slidesPerView: DEFAULT_NUMBER_OF_SLIDES,
  spaceBetween: 25,
};

export const RelatedProducts: FC<RelatedProductsProps> = ({
  fallbackImageUrl = null,
  productColors,
  products = mockData,
}) => {
  const ref = useRef(null);

  const handleClickBackward = useCallback(() => {
    const swiper = get(ref, ['current', 'swiper']);

    if (swiper) {
      swiper.slidePrev();
    }
  }, []);

  const handleClickForward = useCallback(() => {
    const swiper = get(ref, ['current', 'swiper']);

    if (swiper) {
      swiper.slideNext();
    }
  }, []);

  return (
    <div className={cnRelatedProducts()}>
      <div className={cnRelatedProducts('Title')}>Recommended Products</div>
      <div className={cnRelatedProducts('Container')}>
        <Button className={cnRelatedProducts('BackwardButton')} buttonTheme="clear" onClick={handleClickBackward}>
          <Icon icon="icon-angle-left" />
        </Button>
        <Swiper {...params} ref={ref}>
          {products.map((product: Product, i: number) => (
            <div key={`${product.productId}-${i}`} className={cnRelatedProducts('ItemContainer')}>
              <Item product={product} productColors={productColors} fallbackImageUrl={fallbackImageUrl} />
            </div>
          ))}
        </Swiper>
        <Button className={cnRelatedProducts('ForwardButton')} buttonTheme="clear" onClick={handleClickForward}>
          <Icon icon="icon-angle-right" />
        </Button>
      </div>
    </div>
  );
};
