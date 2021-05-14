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

import React, { FC, useCallback, useMemo, useState } from 'react';

import { Product } from 'services/productsSearch';

import { ProductCardContext } from './context';
import { getInitialVariant } from './utils';

import { cnProductCard } from './cn';

import './ProductCard.scss';

export type ProductCardProps = {
  className?: string;
  product: Product;
  fallbackImageUrl: string | null;
  productColors: Record<string, string>;
};

export const ProductCard: FC<ProductCardProps> = ({
  className,
  children,
  fallbackImageUrl,
  product,
  productColors,
}) => {
  const initialVariant = useMemo(() => getInitialVariant(product, productColors), []);

  const [selectedVariant, setSelectedVariant] = useState(initialVariant);

  const handleChangeVariant = useCallback(
    (variantId: string) => {
      const variant = product.variants.find((x) => x.variantId === variantId);
      setSelectedVariant(variant);
    },
    [product, setSelectedVariant],
  );

  const context = useMemo(
    () => ({ product, fallbackImageUrl, productColors, selectedVariant, onChangeVariant: handleChangeVariant }),
    [product, fallbackImageUrl, productColors, selectedVariant, handleChangeVariant],
  );

  return (
    <ProductCardContext.Provider value={context}>
      <div className={cnProductCard(null, [className])}>{children}</div>
    </ProductCardContext.Provider>
  );
};
