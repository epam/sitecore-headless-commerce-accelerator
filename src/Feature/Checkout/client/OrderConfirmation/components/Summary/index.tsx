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
import classnames from 'classnames';
import * as React from 'react';

import { resolveColor } from 'Foundation/Commerce/client';
import * as Jss from 'Foundation/ReactJss/client';

import { SummaryProps, SummaryState } from './models';

import './styles.scss';

const fields = {
  colorTitle: {
    value: 'Color:',
  },
  priceWasTitle: {
    value: 'Was',
  },
  quantityTitle: {
    value: 'Quantity:',
  },
  sizeTitle: {
    value: 'Size:',
  },
};

export class Summary extends Jss.SafePureComponent<SummaryProps, SummaryState> {
  public safeRender() {
    const { order, productColors, fallbackImageUrl } = this.props;

    return (
      <div className="order-summary-wrapper">
        <section className="container">
          <div className="row">
            <div className="col-md-12 nopadding">
              <div className="order-summary">
                <div className="container">
                  <div className="row">
                    <div className="col-md-12">
                      <div className="color-title">
                        <Text field={{ value: 'Order Summary' }} tag="h1" className="title wishlist-title" />
                        <div className="color-bar" />
                      </div>
                    </div>
                  </div>
                </div>
                <div className="product-container">
                  {order.cartLines.map((cartLine, index) => {
                    let imageUrl = fallbackImageUrl;

                    if (!!cartLine.variant.imageUrls && cartLine.variant.imageUrls.length > 0) {
                      imageUrl = cartLine.variant.imageUrls[0];
                    } else if (!!cartLine.product.imageUrls && cartLine.product.imageUrls.length > 0) {
                      imageUrl = cartLine.product.imageUrls[0];
                    }

                    return (
                      <div
                        key={`${cartLine.id}-${cartLine.variant.productId}-${cartLine.variant.variantId}`}
                        className={classnames({
                          'product-item-bg': index % 2 === 0,
                          'product-item-wrapper': true,
                        })}
                      >
                        <div className="col-md-3">
                          <div className="product-image">
                            <img className="image-border" src={imageUrl} alt="Product image" />
                          </div>
                        </div>
                        <div className="col-md-3">
                          <div className="product-name">
                            <span className="brand">{cartLine.variant.brand}</span>
                            <span className="full-name">{cartLine.variant.displayName}</span>
                          </div>
                        </div>
                        <div className="col-md-3">
                          <div className="product-features">
                            {cartLine.variant.properties.color && (
                              <span className="color">
                                <Text field={fields.colorTitle} tag="span" className="color-title" />
                                <span
                                  className={classnames({ 'color-name': true, 'selected': true })}
                                  style={{
                                    background: resolveColor(
                                      cartLine.variant.properties.color,
                                      productColors,
                                    ),
                                  }}
                                />
                              </span>
                            )}
                            {cartLine.variant.properties.size && (
                              <span className="size">
                                <Text field={fields.sizeTitle} tag="strong" />{' '}
                                <span>{cartLine.variant.properties.size}</span>
                              </span>
                            )}
                            <span className="quantity">
                              <Text field={fields.quantityTitle} tag="strong" /> <span>{cartLine.quantity}</span>
                            </span>
                          </div>
                        </div>
                        <div className="col-md-3">
                          <div className="product-price nopadding-bottom">
                            <sup>{cartLine.price.currencySymbol}</sup>
                            <span className="price-amount">{cartLine.price.total.toFixed(2)}</span>
                            {!!cartLine.price.totalSavings && (
                              <>
                                <br />
                                <Text field={fields.priceWasTitle} tag="span" />{' '}
                                <sup className="price-was-sign">{cartLine.price.currencySymbol}</sup>
                                <span className="price-was-amount">{cartLine.price.subtotal.toFixed(2)}</span>
                              </>
                            )}
                          </div>
                        </div>
                      </div>
                    );
                  })}
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}
