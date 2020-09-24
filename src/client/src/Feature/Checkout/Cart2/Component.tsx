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

import { CartSummary, OrderSummary } from './components';
import { CartProps, CartState } from './models';

import './styles.scss';

export default class Cart extends Jss.SafePureComponent<CartProps, CartState> {
  public componentDidMount() {
    this.props.LoadCart();
  }
  public safeRender() {
    const { shoppingCartData, isLoading, sitecoreContext } = this.props;

    return (
      <>
        <div className="row">
          <div className="col-xs-12">
            <header className="title">Your cart items</header>
          </div>
        </div>
        <div className="row">
            <>
              <div className="col-xs-12">
                {isLoading ? (
                  <div className="cartSummary2-loading-overlay">
                    <div className="loading" />
                  </div>
                ) : (
                  <CartSummary
                    cartLines={shoppingCartData.cartLines}
                    productColors={sitecoreContext.productColors}
                    fallbackImageUrl={sitecoreContext.fallbackImageUrl}
                  />
                )
              }
              </div>
            </>
        </div>
        <div className="action_container">
          <NavigationLink to={`/`}>
            <button>Continue Shopping</button>
          </NavigationLink>
        </div>
        <div className="row last-row">
          <OrderSummary price={shoppingCartData.price} rendering={this.props.rendering} />
        </div>
      </>
    );
  }
}
