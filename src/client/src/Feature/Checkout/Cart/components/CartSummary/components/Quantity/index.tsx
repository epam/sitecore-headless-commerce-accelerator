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

import { ShoppingCart as ShoppingCartApi } from 'Feature/Checkout/Integration/api';
import * as ShoppingCart from 'Feature/Checkout/Integration/ShoppingCart';

import { QuantityProps, QuantityState } from './models';

import './styles.scss';

export class Quantity extends Jss.SafePureComponent<QuantityProps, QuantityState> {
  constructor(props: QuantityProps) {
    super(props);
    const quantity = this.props.cartLine.quantity;
    this.state = {
      quantityString: quantity.toString(),
    };
  }

  public setQuantityString(increase: boolean) {
    const { quantityString } = this.state;
    const { RemoveCartLine, cartLine } = this.props;
    const quantity = parseInt(quantityString, 10);
    const newQuantity = increase ? quantity + 1 : quantity - 1;
    if (!increase && quantity === 1) {
      RemoveCartLine(cartLine);
    } else {
      this.setState({ quantityString: newQuantity.toString() }, () => this.updateCartLine(cartLine));
    }
  }

  public updateCartLine(cartLine: ShoppingCart.ShoppingCartLine) {
    const quantity = parseInt(this.state.quantityString, 10);
    if (quantity >= 0) {
      const updateCartLineModel: ShoppingCartApi.CartItemDto = {
        productId: cartLine.product.productId,
        quantity,
        variantId: cartLine.variant.variantId,
      };
      this.props.UpdateCartLine(updateCartLineModel);
    } else {
      this.setState({
        quantityString: cartLine.quantity.toString(),
      });
    }
  }

  public safeRender() {
    const cartLine = this.props.cartLine;
    const quantityString = this.state.quantityString;
    return (
      <div className="quantity" data-autotests="productQty">
        <input type="button" value="-" className="qty-button" onClick={() => this.setQuantityString(false)} />
        <input type="text" id={`qty-${cartLine.id}`} value={quantityString} disabled={true} />
        <input type="button" value="+" className="qty-button" onClick={() => this.setQuantityString(true)} />
      </div>
    );
  }
}
