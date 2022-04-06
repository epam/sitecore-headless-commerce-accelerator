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

import React from 'react';

import { Product } from 'services/commerce';

import { cnAdditionalInformation } from './cn';
import './AdditionalInformation.scss';

type AdditionalInformationProps = {
  wishlistItem?: Product;
  outOfStockItem?: Product;
};

const fields = {
  colorTitle: {
    value: 'Color:',
  },
  sizeTitle: {
    value: 'Size:',
  },
};

export const AdditionalInformation = (props: AdditionalInformationProps) => {
  const { wishlistItem, outOfStockItem } = props;
  const item = wishlistItem || outOfStockItem;

  return (
    <div className={cnAdditionalInformation({ outOfStockItem: !!outOfStockItem })}>
      {item.variants[0].properties?.color && (
        <div className={cnAdditionalInformation('Color')}>
          <span>{fields.colorTitle.value + ' ' + item.variants[0].properties?.color}</span>
        </div>
      )}
      {item.variants[0].properties?.size && (
        <div className={cnAdditionalInformation('Size')}>
          <span>{fields.sizeTitle.value + ' ' + item.variants[0].properties?.size}</span>
        </div>
      )}
    </div>
  );
};
