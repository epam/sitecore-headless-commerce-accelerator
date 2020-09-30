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

import { resolveColor } from 'Foundation/Commerce';
import * as Jss from 'Foundation/ReactJss';

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
      <div className="order-summary-wrapper-2">
        <section className="container">
          <div className="row">
            <div className="col-md-12">
              <div className="order-summary">
                <Text field={{ value: 'Order Summary' }} tag="h3" className="summary-title" />
                <div className="product-container-2">
                  <table>
                    <thead>
                      <tr>
                        <th>IMAGE</th>
                        <th>Product Name</th>
                        <th>QTY</th>
                        <th>SUBTOTAL</th>
                      </tr>
                    </thead>
                    <tbody>
                      {order.cartLines.map((cartLine, index) => {
                        let imageUrl = fallbackImageUrl;

                        if (!!cartLine.variant.imageUrls && cartLine.variant.imageUrls.length > 0) {
                          imageUrl = cartLine.variant.imageUrls[0];
                        } else if (!!cartLine.product.imageUrls && cartLine.product.imageUrls.length > 0) {
                          imageUrl = cartLine.product.imageUrls[0];
                        }

                        return (
                          <tr key={`${cartLine.id}-${cartLine.variant.productId}-${cartLine.variant.variantId}`}>
                            <td className="product-image">
                              <img className="image-border" src={imageUrl} alt="Product image" />
                            </td>
                            <td className="product-name">
                              <div>
                                <div className="brand">{cartLine.variant.brand}</div>
                                <div className="full-name">{cartLine.variant.displayName}</div>
                              </div>
                            </td>
                            <td className="product-features">
                              {cartLine.variant.properties.color && (
                                <span className="color">
                                  <Text field={fields.colorTitle} tag="span" className="color-title" />
                                  <span
                                    className={classnames({ 'color-name': true, 'selected': true })}
                                    style={{
                                      background: resolveColor(cartLine.variant.properties.color, productColors),
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
                                <span>{cartLine.quantity}</span>
                              </span>
                            </td>
                            <td className="product-price nopadding-bottom">
                              <span>{cartLine.price.currencySymbol}</span>
                              <span className="price-amount">{cartLine.price.total.toFixed(2)}</span>
                              {!!cartLine.price.totalSavings && (
                                <>
                                  <br />
                                  <Text field={fields.priceWasTitle} tag="span" />{' '}
                                  <span className="price-was-sign">{cartLine.price.currencySymbol}</span>
                                  <span className="price-was-amount">{cartLine.price.subtotal.toFixed(2)}</span>
                                </>
                              )}
                            </td>
                          </tr>
                        );
                      })}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}
