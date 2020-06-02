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

import * as Jss from 'Foundation/ReactJss/client';

import { CartSummary, OrderSummary } from './components';
import { CartProps, CartState } from './models';

import './styles.scss';

export default class Cart extends Jss.SafePureComponent<CartProps, CartState> {
  public componentDidMount() {
    this.props.LoadCart();
  }
  public safeRender() {
    const { shoppingCartData, sitecoreContext } = this.props;

    return (
      <>
        <div className="row">
          <div className="col-xs-12">
            <header className="color-title">
              <h1 className="title">Shopping Cart</h1>
              <div className="color-bar" />
            </header>
          </div>
        </div>
        <div className="row">
          {!!shoppingCartData && (
            <>
              <div className="col-md-8">
                {shoppingCartData.cartLines && (
                  <CartSummary
                    cartLines={shoppingCartData.cartLines}
                    productColors={sitecoreContext.productColors}
                    fallbackImageUrl={sitecoreContext.fallbackImageUrl}
                  />
                )}
              </div>
              <div className="col-md-4">
                {shoppingCartData.price && (
                  <OrderSummary price={shoppingCartData.price} rendering={this.props.rendering} />
                )}
              </div>
            </>
          )}
        </div>
      </>
    );
  }
}
