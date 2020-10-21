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

import { NavigationLink } from 'Foundation/UI';

import { Quantity } from './components';
import { CartSummaryProps, CartSummaryState } from './models';

import './styles.scss';

export class CartSummaryComponent extends Jss.SafePureComponent<CartSummaryProps, CartSummaryState> {
  constructor(props: CartSummaryProps) {
    super(props);
  }

  public safeRender() {
    const { AddWishlistItem, UpdateCartLine, RemoveCartLine } = this.props;
    const { cartLines, isLoading, fallbackImageUrl } = this.props;
    return (
      <>
        {isLoading && (
          <div className="cartSummary-loading-overlay">
            <div className="loading" />
          </div>
        )}
        <section className="cartSummary" data-autotests="cartSummarySection">
          <header className="cartSummary-header">
            <h2 className="col-xs-2 header-title product-image">IMAGE</h2>
            <h2 className="col-xs-2 header-title product-details">PRODUCT NAME</h2>
            <h2 className="col-xs-2 header-title product-price">UNIT PRICE</h2>
            <h2 className="col-xs-2 header-title product-quantity">QTY</h2>
            <h2 className="col-xs-2 header-title product-subtotal">SUBTOTAL</h2>
            <h2 className="col-xs-2 header-title product-actions">ACTION</h2>
          </header>
          <ul className="cartList">
            {cartLines.map((cartLine) => {
              let imageUrl = fallbackImageUrl;
              const isUpdatedPrice = cartLine.variant.listPrice > cartLine.variant.adjustedPrice;
              if (!!cartLine.variant.imageUrls && cartLine.variant.imageUrls.length > 0) {
                imageUrl = cartLine.variant.imageUrls[0];
              } else if (!!cartLine.product.imageUrls && cartLine.product.imageUrls.length > 0) {
                imageUrl = cartLine.product.imageUrls[0];
              }

              return (
                <li className="cartList-product" key={cartLine.id} data-autotests={`cartProduct_${cartLine.id}`}>
                  <div className="product">
                    <div className="col-xs-2 product-image ">
                      <img src={imageUrl} />
                    </div>
                    <div className="col-xs-2 product-details">
                      <div className="product-info">
                        <div className="product-heading">
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
                              <label>Color:</label>
                              <span>{cartLine.variant.properties.color}</span>
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
                    <div className="col-xs-2 amount product-price">
                      <div className={isUpdatedPrice ? 'discount' : 'origin'}>
                        <span className="currency">{cartLine.variant.currencySymbol}</span>
                        {cartLine.variant.listPrice.toFixed(2)}
                      </div>
                      {isUpdatedPrice && (
                        <div>
                          <span className="currency">{cartLine.variant.currencySymbol}</span>
                          {cartLine.variant.adjustedPrice.toFixed(2)}
                        </div>
                      )}
                    </div>
                    <div className="col-xs-2 amount quantity-group product-quantity">
                      <Quantity cartLine={cartLine} UpdateCartLine={UpdateCartLine} RemoveCartLine={RemoveCartLine} />
                    </div>
                    <div className="col-xs-2 amount product-subtotal">
                      <span className="currency">{cartLine.price.currencySymbol}</span>
                      {cartLine.price.total.toFixed(2)}
                    </div>
                    <div className="col-xs-2 actions product-actions">
                      <button>
                        <i className="fa fa-heart fa-lg" onClick={(e) => AddWishlistItem(cartLine.variant)} />
                      </button>
                      <button>
                        <i className="fa fa-times fa-lg" onClick={(e) => RemoveCartLine(cartLine)} />
                      </button>
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
