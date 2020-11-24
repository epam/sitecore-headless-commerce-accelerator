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
  private wrapperRef: React.MutableRefObject<HTMLDivElement>;
  // private readonly wrapperRef: React.RefObject<any>;
  constructor(props: CartButtonProps) {
    super(props);
    this.state = {
      cartVisible: false,
    };
    this.wrapperRef = React.createRef<HTMLDivElement>();
  }

  public componentDidMount() {
    document.addEventListener('click', this.handleClickOutside.bind(this), false);
  }

  public componentDidUnMount() {
    document.removeEventListener('click', this.handleClickOutside.bind(this), false);
  }

  protected safeRender() {
    const { cartQuantity } = this.props;
    const { cartVisible } = this.state;

    const handleClick = () => {
      this.setState({ cartVisible: !this.state.cartVisible });
    };

    return (
      <div className="navigation-buttons_item cart-wrap" style={{ position: 'relative' }} ref={this.wrapperRef}>
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
        <div className={`shopping-cart-view ${cartVisible ? 'visible-cart' : ''}`} >
          <CartView ToggleClick={this.toggleClick}/>
        </div>
      </div>
    );
  }
  public toggleClick = () => {
    this.setState({
      cartVisible: false,
    });
  }

  private handleClickOutside(e: MouseEvent) {
    if (
      this.wrapperRef.current &&
      !this.wrapperRef.current.contains(e.target as Node) &&
      this.state.cartVisible
    ) {
      this.setState({
        cartVisible: !this.state.cartVisible,
      });
    }
  }
}
