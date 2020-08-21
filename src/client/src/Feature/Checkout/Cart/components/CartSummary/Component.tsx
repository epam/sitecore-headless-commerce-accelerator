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

import * as Jss from 'Foundation/ReactJss';

import { resolveColor } from 'Foundation/Commerce';
import { NavigationLink } from 'Foundation/UI';
import toggleBar from 'Foundation/UI/common/utility';

import { Quantity } from './components';
import { CartSummaryProps, CartSummaryState } from './models';

import './styles.scss';

export class CartSummaryComponent extends Jss.SafePureComponent<CartSummaryProps, CartSummaryState> {
  constructor(props: CartSummaryProps) {
    super(props);
  }

  public safeRender() {
    const { AddWishlistItem, UpdateCartLine, RemoveCartLine } = this.props;
    const { cartLines, isLoading, productColors, fallbackImageUrl } = this.props;

    return (
      <>
        {isLoading && (
          <div className="cartSummary-loading-overlay">
            <div className="loading" />
          </div>
        )}
        <section className="cartSummary" data-autotests="cartSummarySection">
          <header className="cartSummary-header">
            <h2 className="header-title">
              Item<span>(s)</span>
            </h2>
          </header>
          <ul className="cartList" data-autotests="cartList">
            {cartLines.map((cartLine) => {
              let imageUrl = fallbackImageUrl;

              if (!!cartLine.variant.imageUrls && cartLine.variant.imageUrls.length > 0) {
                imageUrl = cartLine.variant.imageUrls[0];
              } else if (!!cartLine.product.imageUrls && cartLine.product.imageUrls.length > 0) {
                imageUrl = cartLine.product.imageUrls[0];
              }

              return (
                <li className="cartList-product" key={cartLine.id} data-autotests={`cartProduct_${cartLine.id}`}>
                  <div className="product">
                    <figure className="row product-summary" data-autotests="productSummary">
                      <div className="col-xs-6 col-sm-4 col-lg-3 product-image">
                        <img src={imageUrl} />
                      </div>
                      <figcaption className="col-xs-6 col-sm-8 col-lg-9 product-caption">
                        <div className="row">
                          <div className="col-sm-6 col-lg-4">
                            <div className="product-info" data-autotests="productInfo">
                              <div className="product-heading">
                                <div className="heading-brand" data-autotests="productInfoBrand">
                                  {cartLine.variant.brand || cartLine.product.brand}
                                </div>
                                <NavigationLink
                                  className="heading-productTitle"
                                  to={`/product/${cartLine.product.productId}`}
                                  data-autotests="productInfoTitle"
                                >
                                  {cartLine.variant.displayName || cartLine.product.displayName}
                                </NavigationLink>
                              </div>
                              <div className="product-options" data-autotests="productOptions">
                                {cartLine.variant.properties.color && (
                                  <div className="option">
                                    Color:
                                    <span
                                      style={{
                                        backgroundColor: resolveColor(cartLine.variant.properties.color, productColors),
                                      }}
                                      className="color"
                                    />
                                  </div>
                                )}
                                {cartLine.variant.properties.size && (
                                  <div className="option">
                                    Size:<span className="size">{cartLine.variant.properties.size}</span>
                                  </div>
                                )}
                              </div>
                            </div>
                          </div>
                          <div className="col-sm-6 col-lg-8">
                            <div className="row">
                              <div className="col-lg-5">
                                <Quantity cartLine={cartLine} UpdateCartLine={UpdateCartLine} />
                              </div>
                              <div className="col-lg-7">
                                <div className="product-price" data-autotests="productPrice">
                                  <span className="label">Item Price:</span>
                                  <span className="amount">
                                    <span className="currency">{cartLine.variant.currencySymbol}</span>
                                    {cartLine.variant.adjustedPrice.toFixed(2)}
                                  </span>
                                </div>
                                <div className="product-total" data-autotests="productTotal">
                                  <span className="label">Total:</span>
                                  <span className="amount">
                                    <span className="currency">{cartLine.price.currencySymbol}</span>
                                    {cartLine.price.total.toFixed(2)}
                                  </span>
                                </div>
                                <div className="product-actions" data-autotests="productActions">
                                  <a className="action action-remove" onClick={(e) => RemoveCartLine(cartLine)}>
                                    <span>Remove Item</span>
                                  </a>
                                  <a
                                    className="action action-addToWishlist"
                                    onClick={(e) => AddWishlistItem(cartLine.variant)}
                                  >
                                    <span>Add to Wishlist</span>
                                  </a>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </figcaption>
                    </figure>
                    <div className="row">
                      <div className="col-xs-12">
                        <div className="product-detail" data-autotests="productDetail">
                          <h3 onClick={(e) => toggleBar(e)} className="detail-header">
                            Additional Information
                            <i className="fa fa-caret-right" />
                          </h3>
                          <div className="detail-description">
                            {cartLine.variant.description || cartLine.product.description}
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </li>
              );
            })}
          </ul>
        </section>
      </>
    );
  }
}
