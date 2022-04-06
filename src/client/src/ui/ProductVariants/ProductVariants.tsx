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

import React, { FC, MouseEvent, useCallback, useEffect, useState } from 'react';

import { useDispatch, useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import { resolveColor } from 'utils';
import { Variant } from 'services/commerce';
import { useSitecoreContext } from 'hooks';
import {
  productId as productIdSelector,
  SelectProductVariant as selectProductVariant,
  variants as variantsSelector,
} from 'services/productVariant';

import { getVariantIdFromQuery, sortVariantsArrayByNestedProperty } from './utils';

import { cnProductVariants } from './cn';
import './styles.scss';

const PROPERTY_TYPES = {
  SIZE: 'Size',
  COLOR: 'Color',
};

export const ProductVariants: FC = () => {
  const dispatch = useDispatch();

  const productId = useSelector(productIdSelector);
  const variants = useSelector(variantsSelector);

  const sitecoreContext = useSitecoreContext();

  const { search: routingQuery } = useLocation();

  const [selectedVariantId, setSelectedVariantId] = useState('');

  const variantSelected = useCallback(
    (e: MouseEvent<HTMLSpanElement>, variant: Variant) => {
      setSelectedVariantId(variant.variantId);

      dispatch(selectProductVariant(productId, variant));
    },
    [productId, dispatch],
  );

  useEffect(() => {
    const variantId = getVariantIdFromQuery(routingQuery);

    setSelectedVariantId(variantId ? variantId : variants[0].variantId);

    if (variants && variants.length > 0) {
      const variantIndex = variants.findIndex((variant) => variant.variantId === variantId);

      dispatch(selectProductVariant(productId, variants[variantIndex >= 0 ? variantIndex : 0]));
    }
  }, [productId, routingQuery, variants]);

  const hasColorProperty = variants.some((variant) => variant.properties[PROPERTY_TYPES.COLOR] !== '');
  const hasSizeProperty = variants.some((variant) => variant.properties[PROPERTY_TYPES.SIZE] !== '');

  const sortedVariants = hasSizeProperty && sortVariantsArrayByNestedProperty('properties.Size', variants);

  return (
    <>
      {variants && variants.length > 1 && (
        <div className={cnProductVariants()}>
          {(hasColorProperty && (
            <>
              <p className={cnProductVariants('Title')}>Color</p>
              <ul className={cnProductVariants('List')}>
                {variants &&
                  variants.map((variant, variantIndex) => {
                    const colorName = variant.properties[PROPERTY_TYPES.COLOR];
                    const colorValue = resolveColor(colorName, sitecoreContext.productColors);
                    return (
                      <li key={variantIndex} className={cnProductVariants('ListItem')}>
                        <button
                          style={{ background: colorValue }}
                          onClick={(e) => {
                            variantSelected(e, variant);
                          }}
                          className={cnProductVariants('Button', {
                            color: true,
                            active: variant.variantId === selectedVariantId,
                          })}
                        />
                      </li>
                    );
                  })}
              </ul>
            </>
          )) ||
            null}
          {(hasSizeProperty && (
            <>
              <p className={cnProductVariants('Title')}>Size</p>
              <ul className={cnProductVariants('List')}>
                {sortedVariants &&
                  sortedVariants.map((variant: Variant, variantIndex: number) => {
                    const size = variant.properties[PROPERTY_TYPES.SIZE];
                    return (
                      <li key={variantIndex} className={cnProductVariants('ListItem')}>
                        <button
                          onClick={(e) => {
                            variantSelected(e, variant as Variant);
                          }}
                          className={cnProductVariants('Button', {
                            size: true,
                            active: variant.variantId === selectedVariantId,
                          })}
                        >
                          {size}
                        </button>
                      </li>
                    );
                  })}
              </ul>
            </>
          )) ||
            null}
        </div>
      )}
    </>
  );
};
