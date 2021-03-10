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

import { Rating } from 'Foundation/UI/components/Rating';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';

export type ProductRatingProps = {
  className?: string;
};

const MOCK_RATING = 4;

export const ProductRating: FC<ProductRatingProps> = ({ className }) => {
  const contextData = useContext(ProductCardContext);
  const { selectedVariant } = contextData;
  const { customerAverageRating } = selectedVariant;

  return (
    <Rating className={cnProductCard('ProductRating', [className])} rating={customerAverageRating || MOCK_RATING} />
  );
};
