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

import { ShoppingCart } from 'Feature/Checkout/client/Integration/api';
import * as JSS from 'Foundation/ReactJss/client';

import { Common } from '../../../Catalog/client/Integration';
import { AddToCartProps, AddToCartState } from './models';

export default class AddToCartComponent extends JSS.SafePureComponent<AddToCartProps, AddToCartState> {
  constructor(props: AddToCartProps) {
    super(props);
  }

  protected safeRender() {
    const { isLoading, productVariant } = this.props;
    const isDisabled = isLoading || !productVariant || productVariant.stockStatusName !== Common.StockStatus.InStock;
    return (
      <>
        <button disabled={isDisabled} title="Add to Cart" onClick={(e) => this.addToCart(e)} className="btn btn-main btn-add">
            {isLoading && <i className="fa fa-spinner fa-spin" />}
            &nbsp;Add to Cart <span>+</span>
        </button>
      </>
    );
  }

  private addToCart(e: React.MouseEvent<HTMLButtonElement>) {
    const { productId, productVariant } = this.props;
    // TODO: allow user to select variant
    const quantity = 1;
    const addToCartModel: ShoppingCart.CartItemDto = {
      productId,
      quantity,
      variantId: productVariant.variantId,
    };

    this.props.AddToCart(addToCartModel);
  }
}
