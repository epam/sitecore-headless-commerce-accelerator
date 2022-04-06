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

import { useSelector } from 'react-redux';

import { LoadingStatus } from 'models';
import { actionTypes, shoppingCart, ShoppingCartLine } from 'services/shoppingCart';

import { CartSummaryOwnProps } from './models';

import { ProductListItem } from 'ui/ProductListItem';

import './styles.scss';

export const CartSummary = (props: CartSummaryOwnProps) => {
  const { cartLines, fallbackImageUrl } = props;
  const shoppingCartState = useSelector(shoppingCart);

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
          <div className="cartList">
            {cartLines.map((cartLine: ShoppingCartLine) => {
              let imageUrl = fallbackImageUrl;
              if (!!cartLine.variant.imageUrls && cartLine.variant.imageUrls.length > 0) {
                imageUrl = cartLine.variant.imageUrls[0];
              } else if (!!cartLine.product.imageUrls && cartLine.product.imageUrls.length > 0) {
                imageUrl = cartLine.product.imageUrls[0];
              }

              return (
                <ProductListItem
                  cartLineItem={cartLine}
                  cartLineItemUrl={imageUrl}
                  key={cartLine.id}
                  data-autotests={`cartProduct_${cartLine.id}`}
                />
              );
            })}
          </div>
        </section>
      </div>
    </>
  );
};
