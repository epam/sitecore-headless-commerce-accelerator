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

import React, { FC, useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { eventHub, events } from 'services/eventHub';
import { LoadingStatus } from 'models';
import { NavigationLink } from 'ui/NavigationLink';

import { authenticationProcess, logoutProcess } from 'services/authentication';
import {
  LoadCart as loadCart,
  RemoveCartLine as removeCartLine,
  shoppingCart,
  ShoppingCartLine,
} from 'services/shoppingCart';

import { Button, Icon } from 'components';

import './CartView.scss';

type CartViewPropsType = {
  toggleClick: () => void;
};

export const CartViewComponent: FC<CartViewPropsType> = ({ toggleClick }) => {
  const dispatch = useDispatch();
  const { data: shoppingCartData } = useSelector(shoppingCart);
  const { status: authenticationProcessStatus } = useSelector(authenticationProcess);
  const { status: logoutProcessStatus } = useSelector(logoutProcess);

  useEffect(() => {
    dispatch(loadCart());
  }, []);

  useEffect(() => {
    if (authenticationProcessStatus === LoadingStatus.Loaded || logoutProcessStatus === LoadingStatus.Loaded) {
      dispatch(loadCart());
    }
  }, [authenticationProcessStatus, logoutProcessStatus]);

  const onRemoveCartLine = useCallback(
    (cartLine: ShoppingCartLine) => {
      dispatch(removeCartLine(cartLine));
    },
    [dispatch],
  );

  let cartTotalPrice = 0;

  return shoppingCartData && shoppingCartData.cartLines.length > 0 ? (
    <div className="shopping-cart-view-populated">
      <ul>
        {shoppingCartData.cartLines.map((single, key) => {
          const price =
            single.variant.listPrice > single.variant.adjustedPrice
              ? single.variant.adjustedPrice
              : single.variant.listPrice;
          cartTotalPrice += single.price.total;
          const link = `/product/${single.variant.productId}?v=${single.variant.variantId}`;

          return (
            <li className="shopping-cart-view-populated-single-item" key={key}>
              <div className="shopping-cart-view-populated-single-item-img">
                <NavigationLink
                  to={link}
                  onClick={() => eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...single.variant })}
                >
                  <img alt="" src={single.variant.imageUrls[0]} className="img-fluid" />
                </NavigationLink>
              </div>
              <div className="shopping-cart-view-populated-single-item-title">
                <h4>
                  <NavigationLink
                    to={link}
                    onClick={() => eventHub.publish(events.PRODUCT_LIST.PRODUCT_CLICKED, { ...single.variant })}
                  >
                    {` ${single.variant.displayName} `}
                  </NavigationLink>
                </h4>
                <h6>Qty: {single.quantity}</h6>
                <span>{single.variant.currencySymbol + price.toFixed(2)}</span>
                {(single.variant.properties.color || single.variant.properties.size) && (
                  <div className="shopping-cart-view-populated-single-item-title-variation">
                    <span>Color: {single.variant.properties.color ? single.variant.properties.color : 'x'}</span>
                    <span>Size: {single.variant.properties.size ? single.variant.properties.size : 'x'}</span>
                  </div>
                )}
              </div>
              <div className="shopping-cart-view-populated-single-item-delete">
                <button title={'Remove from Cart'} onClick={() => onRemoveCartLine(single)}>
                  <Icon icon="icon-close" size="l" />
                </button>
              </div>
            </li>
          );
        })}
      </ul>
      <div className="shopping-cart-view-populated-total">
        <h4>
          Total : <span>{'$' + cartTotalPrice.toFixed(2)}</span>
        </h4>
      </div>
      <div className="shopping-cart-view-populated-buttons">
        <Button
          className="ShoppingCartView-Button"
          buttonType="link"
          buttonTheme="transparentSlide"
          fullWidth={true}
          href="/cart"
          onClick={toggleClick}
        >
          View Cart
        </Button>
        <Button
          className="ShoppingCartView-Button"
          buttonType="link"
          buttonTheme="transparentSlide"
          fullWidth={true}
          href="/checkout/shipping"
          onClick={toggleClick}
        >
          Checkout
        </Button>
      </div>
    </div>
  ) : (
    <p className="shopping-cart-view-empty-text">No items added to cart</p>
  );
};
