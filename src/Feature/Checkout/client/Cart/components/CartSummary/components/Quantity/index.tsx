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

import { ShoppingCart as ShoppingCartApi } from 'Feature/Checkout/client/Integration/api';
import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

import { QuantityProps, QuantityState } from './models';

export class Quantity extends Jss.SafePureComponent<QuantityProps, QuantityState> {

  constructor(props: QuantityProps) {
    super(props);
    const quantity = this.props.cartLine.quantity;
    this.state = {
      quantity
    };
  }

  public setQuantity(e: React.ChangeEvent<HTMLInputElement>) {
    const quantityStr = e.target.value;
    const quantity = parseInt(quantityStr, 10);
    if (quantity >= 0) {
      this.setState({quantity});
    }
  }

  public updateCartLine(cartLine: ShoppingCart.ShoppingCartLine) {
    const quantity = this.state.quantity;
    if (quantity >= 0) {
      const updateCartLineModel: ShoppingCartApi.CartItemDto = {
        productId: cartLine.product.productId,
        quantity,
        variantId: cartLine.variant.variantId,
      };
      this.props.UpdateCartLine(updateCartLineModel);
    }
  }

  public safeRender() {
    const cartLine = this.props.cartLine;
    const quantity = this.state.quantity;
    return (
      <div className="product-qty">
        <label htmlFor={'qty-' + cartLine.id}>Qty:</label>
        <input
          type="text"
          max="99"
          id={`qty-${cartLine.id}`}
          value={quantity}
          onChange={(e) => this.setQuantity(e)}
          onBlur={(e) => this.updateCartLine(cartLine)}
        />
      </div>
    );
  }
}
