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

import * as React from 'react';

import * as Jss from 'Foundation/ReactJss/client';
import { NavigationLink } from 'Foundation/UI/client';

import { ProductListItemProps } from './models';

import './styles.scss';

export class ProductListItem extends Jss.SafePureComponent<ProductListItemProps, Jss.SafePureComponentState> {
  public safeRender() {
    return (
      <figure className="listing-grid-item">
        <div className="img-wrap">
          <img
            src={!!this.props.imageUrls[0] ? this.props.imageUrls[0] : 'http://via.placeholder.com/350x150'}
            alt="product image"
          />
          <NavigationLink className="btn btn-main btn-quickView" to={`/product/${this.props.productId}`}>
            Quick View
          </NavigationLink>
          <NavigationLink className="btn btn-main btn-viewProduct" to={`/product/${this.props.productId}`}>
            View Product
          </NavigationLink>
        </div>
        <figcaption>
          <div className="price price--adjusted">
            <div className="price__full">
              <span className="price__currency">{this.props.currencySymbol}</span>
              <span className="price__amount">{this.props.listPrice.toFixed(2)}</span>
            </div>
            <div className="price__current">
              <span className="price__currency">{this.props.currencySymbol}</span>
              <span className="price__amount">{this.props.adjustedPrice.toFixed(2)}</span>
            </div>
          </div>
          <div className="brand">{this.props.brand}</div>
          <h2>{this.props.displayName}</h2>
          <input type="checkbox" />
          <label className="compare">Compare</label>
        </figcaption>
      </figure>
    );
  }
}
