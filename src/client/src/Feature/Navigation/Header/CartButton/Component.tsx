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

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';

import { CartView } from '../../../../Feature/Checkout/CartView';
import { Desktop, Mobile } from '../../../../Foundation/UI/common/components/Responsive';
import { CartButtonProps, CartButtonState } from './models';

export class CartButtonComponent extends JSS.SafePureComponent<CartButtonProps, CartButtonState> {
  constructor(props: CartButtonProps) {
    super(props);
    this.state = {
      cartVisible: false,
    };
  }

  public componentDidMount() {
    document.addEventListener('mousedown', this.handleClickOutside);
  }

  public componentDidUnMount() {
    document.removeEventListener('mousedown', this.handleClickOutside);
  }

  protected safeRender() {
    const { cartQuantity } = this.props;
    const { cartVisible } = this.state;

    const handleClick = () => {
      this.setState({ cartVisible: !this.state.cartVisible });
    };

    return (
      <div className="navigation-buttons_item cart-wrap" style={{ position: 'relative' }}>
        <Desktop>
          <a onClick={() => handleClick()}>
            <i className="pe-7s-shopbag">
              <span className="quantity">{cartQuantity}</span>
            </i>
          </a>
        </Desktop>
        <Mobile>
          <a href="/cart">
            <i className="pe-7s-shopbag">
              <span className="quantity">{cartQuantity}</span>
            </i>
          </a>
        </Mobile>
        <div className={`shopping-cart-view ${cartVisible ? 'visible-cart' : ''}`}>
          <CartView />
        </div>
      </div>
    );
  }

  private handleClickOutside = (e: MouseEvent) => {
    const targetElement = e.target as Element;
    if (this.state.cartVisible) {
      const cartClassSelector = '.shopping-cart-view';
      const cartHeaderClass = '.pe-7s-shopbag';
      const navigateButtonsViewClass = '.shopping-cart-view-populated-buttons-view';
      const navigateButtonsCheckoutClass = '.shopping-cart-view-populated-buttons-checkout';
      if (
        (targetElement.closest(navigateButtonsViewClass) && targetElement.matches(navigateButtonsViewClass)) ||
        (targetElement.closest(navigateButtonsCheckoutClass) && targetElement.matches(navigateButtonsCheckoutClass))
      ) {
        this.setState({
          cartVisible: false,
        });
      }
      if (
        !targetElement.closest(cartClassSelector) &&
        !targetElement.matches(cartClassSelector) &&
        !targetElement.closest(cartHeaderClass) &&
        !targetElement.matches(cartHeaderClass)
      ) {
        this.setState({
          cartVisible: false,
        });
      }
    }
  };
}
