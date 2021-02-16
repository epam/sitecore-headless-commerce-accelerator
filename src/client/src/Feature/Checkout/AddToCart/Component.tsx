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

import * as JSS from 'Foundation/ReactJss';

import { Common } from 'Feature/Catalog/Integration';
import { QuantityProductCommon } from 'Feature/Checkout/common/Quantity';
import { ShoppingCart } from 'Feature/Checkout/Integration/api';
import { Button } from 'Foundation/UI/components/Button';

import { AddToCartProps, AddToCartState } from './models';

import './styles.scss';

export default class AddToCartComponent extends JSS.SafePureComponent<AddToCartProps, AddToCartState> {
  constructor(props: AddToCartProps) {
    super(props);
    this.state = {
      quantityCount: 1,
    };
  }

  public setQuantity(isIncrease: boolean) {
    const { quantityCount } = this.state;
    if (isIncrease === true) {
      this.setState({ quantityCount: quantityCount + 1 });
    }
    if (quantityCount > 1 && isIncrease === false) {
      this.setState({ quantityCount: quantityCount - 1 });
    }
  }
  protected safeRender() {
    const { quantityCount } = this.state;
    const { isLoading, variant } = this.props;
    const isDisabled = isLoading || !variant || variant.stockStatusName !== Common.StockStatus.InStock;
    return (
      <>
        <QuantityProductCommon
          setQuantity={(isIncrease: boolean) => this.setQuantity(isIncrease)}
          quantityString={quantityCount.toString()}
        />
        <Button
          className="AddToCart-AddToCartButton"
          disabled={isDisabled}
          title="Add to Cart"
          onClick={this.addToCart}
          buttonTheme="defaultSlide"
        >
          {isLoading && <i className="fa fa-spinner fa-spin" />}
          &nbsp;
          {isDisabled && !isLoading ? 'Out of Stock' : 'Add to Cart'}
        </Button>
      </>
    );
  }

  private addToCart = (e: React.MouseEvent<HTMLButtonElement>) => {
    const { productId, variant } = this.props;
    const { quantityCount } = this.state;
    // TODO: allow user to select variant
    const addToCartModel: ShoppingCart.CartItemDto = {
      productId,
      quantity: quantityCount,
      variantId: variant.variantId,
    };

    this.props.AddToCart(addToCartModel);
  };
}
