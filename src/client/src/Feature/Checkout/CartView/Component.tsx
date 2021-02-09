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
import { CartProps, CartState } from './models';

import './styles.scss';

import { LoadingStatus } from 'Foundation/Integration';

export default class Cart extends Jss.SafePureComponent<CartProps, CartState> {
  public componentDidMount() {
    this.props.LoadCart();
  }
  public componentDidUpdate() {
    const { authenticationProcess, logoutProcess} = this.props;
    if (authenticationProcess.status === LoadingStatus.Loaded || logoutProcess.status === LoadingStatus.Loaded) {
      this.props.LoadCart();
    }
  }
  public safeRender() {
    const { shoppingCartData, RemoveCartLine, ToggleClick } = this.props;
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
            return (
              <li className="shopping-cart-view-populated-single-item" key={key}>
                <div className="shopping-cart-view-populated-single-item-img">
                  <NavigationLink to={'/product/' + single.variant.productId}>
                    <img alt="" src={single.variant.imageUrls[0]} className="img-fluid" />
                  </NavigationLink>
                </div>
                <div className="shopping-cart-view-populated-single-item-title">
                  <h4>
                    <NavigationLink to={'/product/' + single.variant.productId}>
                      {' '}
                      {single.variant.displayName}{' '}
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
                  <button onClick={(e) => RemoveCartLine(single)}>
                    <i className="fa fa-times-circle" />
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
          <a className="shopping-cart-view-populated-buttons-view" href="/cart" onClick={ToggleClick}>
            View Cart
          </a>
          <a className="shopping-cart-view-populated-buttons-checkout" href="/checkout/shipping" onClick={ToggleClick}>
            Checkout
          </a>
        </div>
      </div>
    ) : (
      <p className="shopping-cart-view-empty-text">No items added to cart</p>
    );
  }
}
