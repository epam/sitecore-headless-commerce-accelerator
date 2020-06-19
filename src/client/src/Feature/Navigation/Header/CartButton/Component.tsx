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

import { NavigationLink } from 'Foundation/UI';

import { CartButtonProps, CartButtonState } from './models';
import './styles.scss';

export class CartButtonComponent extends JSS.SafePureComponent<CartButtonProps, CartButtonState> {
  protected safeRender() {
    const { cartQuantity } = this.props;

    return (
      <NavigationLink className="cart-button user-navigation-btn" to="/cart">
        <i className="fa fa-shopping-cart">{cartQuantity > 0 && <span className="quantity">{cartQuantity}</span>}</i>
        <span>Shopping Cart</span>
      </NavigationLink>
    );
  }
}
