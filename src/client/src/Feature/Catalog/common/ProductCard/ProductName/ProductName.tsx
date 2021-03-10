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

import React, { FC, useCallback, useContext } from 'react';

import { eventHub, events } from 'Foundation/EventHub';
import { NavigationLink } from 'Foundation/UI';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';
import './ProductName.scss';

export type ProductNameProps = {
  className?: string;
};

export const ProductName: FC<ProductNameProps> = ({ className }) => {
  const contextData = useContext(ProductCardContext);
  const { product, selectedVariant } = contextData;
  const { productId, displayName } = selectedVariant;

  const handleClickLink = useCallback(() => {
    eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...product, list: window.location.pathname });
  }, [product]);

  const path = `/product/${productId}`;

  return (
    <div className={cnProductCard('ProductName', [className])}>
      <NavigationLink to={path} onClick={handleClickLink}>
        <span>{displayName}</span>
      </NavigationLink>
    </div>
  );
};
