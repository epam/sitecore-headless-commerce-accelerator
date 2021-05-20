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

import React, { FC } from 'react';

import { Button } from 'components';

import { ProductRating } from 'ui/ProductRating';
import { Product } from 'Foundation/Commerce/dataModel.Generated';

import { cnSubscriptionMessage } from './cn';
import './SubscriptionMessage.scss';

type Props = {
  product: Product;
  configuration: {
    includeDetails: boolean;
  };
};

export const SubscriptionMessage: FC<Props> = ({ product, configuration }) => {
  return (
    <div className={cnSubscriptionMessage()}>
      <h5 className={cnSubscriptionMessage('Title')}>You are subscribed to notifications</h5>
      <p>Information about the product will appear in your personal account and will be sent to your email</p>
      {configuration.includeDetails && (
        <>
          <hr />
          <img src={product.imageUrls[0]} className={cnSubscriptionMessage('ProductImage')} />
          <p>{product.displayName}</p>
          <ProductRating rating={product.customerAverageRating} />
          <p className={cnSubscriptionMessage('ProductPrice')}>
            {product.currencySymbol}
            {product.listPrice}
          </p>
        </>
      )}
      <Button buttonTheme={'transparentSlide'}>Unsubscribe</Button>
    </div>
  );
};
