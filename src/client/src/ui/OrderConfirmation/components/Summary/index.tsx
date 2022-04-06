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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as Jss from 'Foundation/ReactJss';

import { SummaryProps, SummaryState } from './models';

import { ProductListItem } from 'ui/ProductListItem';

import './styles.scss';

export class Summary extends Jss.SafePureComponent<SummaryProps, SummaryState> {
  public safeRender() {
    const { order, fallbackImageUrl } = this.props;
    return (
      <div className="order-summary-wrapper">
        <div className="row">
          <div className="col-md-12">
            <div className="order-summary">
              <Text field={{ value: 'Order Summary' }} tag="h3" className="summary-title" />
              <div className="product-container">
                {order.cartLines.map((orderedItem, index) => {
                  let imageUrl = fallbackImageUrl;

                  if (!!orderedItem.variant.imageUrls && orderedItem.variant.imageUrls.length > 0) {
                    imageUrl = orderedItem.variant.imageUrls[0];
                  } else if (!!orderedItem.product.imageUrls && orderedItem.product.imageUrls.length > 0) {
                    imageUrl = orderedItem.product.imageUrls[0];
                  }

                  return <ProductListItem orderedItem={orderedItem} key={index} orderedItemUrl={imageUrl} />;
                })}
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
