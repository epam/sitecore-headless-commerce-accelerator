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

export class Quantity extends Jss.SafePureComponent<QuantityProps, QuantityState> {
  constructor(props: QuantityProps) {
    super(props);
    const quantity = this.props.cartLine.quantity;
    this.state = {
      quantityString: quantity.toString(),
    };
  }

  public setQuantityString(e: React.ChangeEvent<HTMLInputElement>) {
    const quantityStr = e.target.value;
    const quantity = parseInt(quantityStr, 10);
    if (!isNaN(quantity)) {
      this.setState({
        quantityString: quantity.toString(),
      });
    } else {
      this.setState({
        quantityString: '',
      });
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
      <div className="product-qty">
        <label htmlFor={'qty-' + cartLine.id}>Qty:</label>
        <input
          type="text"
          id={`qty-${cartLine.id}`}
          value={quantityString}
          onChange={(e) => this.setQuantityString(e)}
          onBlur={(e) => this.updateCartLine(cartLine)}
        />
      </div>
    );
  }
}
