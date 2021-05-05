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

import { resolveColor } from 'Foundation/Commerce';
import { Variant } from 'Foundation/Commerce/dataModel.Generated';
import { useSitecoreContext } from 'hooks';
import {
  productId as productIdSelector,
  SelectColorVariant as selectColorVariant,
  variants as variantsSelector,
} from 'services/productVariant';

import { getVariantIdFromQuery } from './utils';

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

      dispatch(selectColorVariant(productId, variant));
    },
    [productId, dispatch],
  );

  useEffect(() => {
    const variantId = getVariantIdFromQuery(routingQuery);

    setSelectedVariantId(variantId ? variantId : variants[0].variantId);

    if (variants && variants.length > 0) {
      const variantIndex = variants.findIndex((variant) => variant.variantId === variantId);

      dispatch(selectColorVariant(productId, variants[variantIndex >= 0 ? variantIndex : 0]));
    }
  }, [productId, routingQuery, variants]);

  return (
    <>
      {variants && variants.length > 1 && (
        <div className="colors-selector">
          <p className="colors-label">Color</p>
          <ul className="colors-list">
            {variants &&
              variants.map((variant, variantIndex) => {
                const colorName = variant.properties['Color'];
                const colorValue = resolveColor(colorName, sitecoreContext.productColors);
                return (
                  <li key={variantIndex} className={'colors-listitem'}>
                    <button
                      style={{ background: colorValue }}
                      onClick={(e) => {
                        variantSelected(e, variant);
                      }}
                      className={`color-variant-button colors-option
                          ${variant.variantId === selectedVariantId ? 'color-variant-button-active' : ''}
                        `}
                    />
                  </li>
                );
              })}
          </ul>
        </div>
      )}
    </>
  );
};
