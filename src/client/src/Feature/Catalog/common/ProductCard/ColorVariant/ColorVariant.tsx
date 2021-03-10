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

import { Button } from 'Foundation/UI/components/Button';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';

import './ColorVariant.scss';

export type ColorVariantProps = {
  variantId: string;
  color: string;
};

export const ColorVariant: FC<ColorVariantProps> = ({ variantId, color }) => {
  const contextData = useContext(ProductCardContext);
  const { onChangeVariant, selectedVariant } = contextData;

  const handleClickVariant = useCallback(() => {
    onChangeVariant(variantId);
  }, [variantId, onChangeVariant]);

  const selected = selectedVariant.variantId === variantId;

  return (
    <Button className={cnProductCard('ColorVariant', { selected })} buttonTheme="clear" onClick={handleClickVariant}>
      <span className={cnProductCard('ColorVariantOuter')}>
        <span className={cnProductCard('ColorVariantInner')} style={{ backgroundColor: color }} />
      </span>
    </Button>
  );
};
