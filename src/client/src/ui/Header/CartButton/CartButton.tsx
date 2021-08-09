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

import React, { useRef, useState, useEffect } from 'react';

import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

import { shoppingCartData } from 'services/shoppingCart';
import { useWindowSize } from 'hooks/useWindowSize';

import { Icon } from 'components';

import { MOBILE_MAX_SCREEN_WIDTH } from './constants';

import { CartView } from 'ui/CartView';

export const CartButton = () => {
  const [cartVisible, setCartVisible] = useState(false);
  const ref = useRef(null);
  const cartData = useSelector(shoppingCartData);
  const cartQuantity = cartData && cartData.cartLines ? cartData.cartLines.length : 0;
  const { width } = useWindowSize();
  const isMobileMode = width < MOBILE_MAX_SCREEN_WIDTH;

  const handleClick = () => {
    setCartVisible(!cartVisible);
  };
  const toggleClick = () => {
    setCartVisible(false);
  };
  const handleClickOutside = (e: MouseEvent) => {
    if (ref.current && !ref.current.contains(e.target) && cartVisible) {
      setCartVisible(!cartVisible);
    }
  };
  useEffect(() => {
    document.addEventListener('click', handleClickOutside, false);

    return () => {
      document.removeEventListener('click', handleClickOutside, false);
    };
  });

  return (
    <div className="navigation-buttons_item cart-wrap" style={{ position: 'relative' }} ref={ref}>
      {isMobileMode ? (
        <Link to="/cart">
          <Icon icon="icon-shopbag" size="l">
            <span className="quantity">{cartQuantity}</span>
          </Icon>
        </Link>
      ) : (
        <a onClick={() => handleClick()}>
          <Icon icon="icon-shopbag" size="l">
            <span className="quantity">{cartQuantity}</span>
          </Icon>
        </a>
      )}
      <div className={`shopping-cart-view ${cartVisible ? 'visible-cart' : ''}`}>
        <CartView toggleClick={toggleClick} />
      </div>
    </div>
  );
};
