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

import React, { FC, useCallback } from 'react';

import { noop } from 'lodash';

import { NavigationLink } from 'ui/NavigationLink';
import { SuggestionProduct } from 'services/search';

import { cnNavigationSearch } from '../cn';
import './SuggestionItem.scss';

export type SuggestionItemProps = {
  product: SuggestionProduct;

  onClick?: (product: SuggestionProduct) => void;
};

export const SuggestionItem: FC<SuggestionItemProps> = ({ product, onClick = noop }) => {
  const { brand, imageUrl, displayName, currencySymbol, price, productId } = product;
  const productDescription = `${brand} ${displayName}`;

  const handleClick = useCallback(() => {
    onClick(product);
  }, [product, onClick]);

  return (
    <NavigationLink className={cnNavigationSearch('SuggestionItem')} to={`/product/${productId}`} onClick={handleClick}>
      <div className={cnNavigationSearch('LeftColumn')}>
        <img className={cnNavigationSearch('Image')} src={imageUrl} />
      </div>
      <div className={cnNavigationSearch('RightColumn')}>
        <span className={cnNavigationSearch('Description')}>{productDescription}</span>
        <div className={cnNavigationSearch('PriceTag')}>
          <span>{currencySymbol}</span>
          <span>{price}</span>
        </div>
      </div>
    </NavigationLink>
  );
};
