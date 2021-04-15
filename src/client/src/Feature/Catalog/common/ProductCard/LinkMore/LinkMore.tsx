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

import React, { FC, useCallback, useContext } from 'react';

import { eventHub, events } from 'Foundation/EventHub';
import { NavigationLink } from 'Foundation/UI';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';
import './LinkMore.scss';

export type LinkMoreProps = {
  className?: string;
};

export const LinkMore: FC<LinkMoreProps> = ({ className, children }) => {
  const contextData = useContext(ProductCardContext);
  const { product, selectedVariant } = contextData;
  const { productId } = selectedVariant;

  const handleClickLink = useCallback(() => {
    eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...product, list: window.location.pathname });
  }, [product]);

  const path = `/product/${productId}`;

  return (
    <NavigationLink className={cnProductCard('LinkMore', [className])} to={path} onClick={handleClickLink}>
      <span>{children}</span>
    </NavigationLink>
  );
};
