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

import * as React from 'react';

import { useDispatch, useSelector } from 'react-redux';

import { LoadingStatus } from 'models';
import { eventHub, events } from 'services/eventHub';
import { actionTypes, RemoveCartLine, shoppingCart, ShoppingCartLine } from 'services/shoppingCart';

import { Icon } from 'components';

import { Quantity } from './components';
import { NavigationLink } from 'ui/NavigationLink';

import { CartSummaryOwnProps } from './models';

import './styles.scss';

export const CartSummary = (props: CartSummaryOwnProps) => {
  const { cartLines, fallbackImageUrl } = props;
  const shoppingCartState = useSelector(shoppingCart);
  const dispatch = useDispatch();

  const isLoading =
    shoppingCartState.status === LoadingStatus.Loading &&
    (shoppingCartState.actionType === actionTypes.UPDATE_CART_LINE_REQUEST ||
      shoppingCartState.actionType === actionTypes.REMOVE_CART_LINE_REQUEST);

  return (
    <>
      {isLoading && (
        <div className="cartSummary-loading-overlay">
          <div className="loading" />
        </div>
      )}
      <div className="cart-summary-container">
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
            {cartLines.map((cartLine: ShoppingCartLine) => {
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
                            to={`/product/${cartLine.product.productId}?v=${cartLine.variant.variantId}`}
                            data-autotests="productInfoTitle"
                            onClick={() =>
                              eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, {
                                ...cartLine.variant,
                                list: window.location.pathname,
                              })
                            }
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
                              Size:
                              <span className="size">{cartLine.variant.properties.size}</span>
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
                      <Quantity cartLine={cartLine} />
                    </div>
                    <div className="col-xs-2 amount product-subtotal">
                      <span className="currency">{cartLine.price.currencySymbol}</span>
                      {cartLine.price.total.toFixed(2)}
                    </div>
                    <div className="col-xs-2 actions product-actions">
                      <button onClick={(e) => dispatch(RemoveCartLine(cartLine))}>
                        <Icon icon="icon-close" size="l" />
                      </button>
                    </div>
                  </div>
                </li>
              );
            })}
          </ul>
        </section>
      </div>
    </>
  );
};
