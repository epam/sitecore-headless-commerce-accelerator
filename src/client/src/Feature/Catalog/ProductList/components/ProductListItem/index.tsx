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

import { ProductRating } from 'Feature/Catalog/ProductRating';
import { eventHub, events } from 'Foundation/EventHub';
import * as Jss from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';
import { ImageSlider, Slide } from 'Foundation/UI/components/ImageSlider';

import { ProductListItemProps } from './models';

import { cnProductListItem } from './cn';
import './styles.scss';

export class ProductListItem extends Jss.SafePureComponent<ProductListItemProps, Jss.SafePureComponentState> {
  public safeRender() {
    const { product, fallbackImageUrl } = this.props;
    const { productId, imageUrls } = product;
    const hasImages = imageUrls.length !== 0;

    return (
      <figure className="listing-grid-item">
        <div className={cnProductListItem('ImageWrapper')}>
          <div className={cnProductListItem('ImageInner')}>
            <NavigationLink
              to={`/product/${productId}`}
              onClick={() =>
                eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...product, list: window.location.pathname })
              }
            >
              <ImageSlider>
                {hasImages ? (
                  imageUrls.map((url, i) => <Slide key={`${productId}-${i}`} url={url} />)
                ) : fallbackImageUrl ? (
                  <Slide url={fallbackImageUrl} />
                ) : null}
              </ImageSlider>
            </NavigationLink>
          </div>
        </div>

        <figcaption>
          <NavigationLink
            to={`/product/${productId}`}
            onClick={() =>
              eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...product, list: window.location.pathname })
            }
          >
            <h3>{product.displayName}</h3>
          </NavigationLink>
          <ProductRating rating={product.customerAverageRating} />
          <div className="price price--adjusted">
            <div className="price_current">
              <span className="price_currency">{product.currencySymbol}</span>
              <span className="price_amount">{product.adjustedPrice.toFixed(2)}</span>
            </div>
            {product.adjustedPrice < product.listPrice && (
              <div>
                <span>&nbsp;-&nbsp; </span>
                <span className="price_full">
                  <span className="price_currency">{product.currencySymbol}</span>
                  <span className="price_amount">{product.listPrice.toFixed(2)}</span>
                </span>
              </div>
            )}
          </div>
          {/* Compare feature commented out
          <input type="checkbox" />
          <label className="compare">Compare</label> */}
        </figcaption>
      </figure>
    );
  }
}
