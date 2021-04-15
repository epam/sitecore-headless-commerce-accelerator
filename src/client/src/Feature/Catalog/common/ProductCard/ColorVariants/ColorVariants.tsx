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

import React, { FC, useContext, useMemo } from 'react';

import { get } from 'lodash';

import { ProductCardContext } from '../context';
import { getColorVariants } from '../utils';

import { ColorVariant } from '../ColorVariant';
import { LinkMore } from '../LinkMore';

import { cnProductCard } from '../cn';

import './ColorVariants.scss';

const MAX_ITEMS = 3;

export type ColorVariantsProps = {
  className?: string;
};

export const ColorVariants: FC<ColorVariantsProps> = ({ className }) => {
  const contextData = useContext(ProductCardContext);
  const { product, productColors } = contextData;
  const colorVariants = useMemo(() => getColorVariants(product, productColors), [product, productColors]);

  return (
    <div className={cnProductCard('ColorVariants', [className])}>
      {colorVariants.slice(0, MAX_ITEMS).map((x) => {
        const { variantId } = x;
        const propertyColor = get(x, ['properties', 'color']);
        const color = get(productColors, [propertyColor]);

        return <ColorVariant key={variantId} variantId={variantId} color={color} />;
      })}

      {colorVariants.length > MAX_ITEMS && <LinkMore>{`+ ${colorVariants.length - MAX_ITEMS} more`}</LinkMore>}
    </div>
  );
};
