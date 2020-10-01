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

import { WishlistButtonProps, WishlistButtonState } from './models';

export class WishlistButtonComponent extends JSS.SafePureComponent<WishlistButtonProps, WishlistButtonState> {
  protected safeRender() {
    const { wishlistQuantity } = this.props;
    return (
      <div className="navigation-buttons_item header-wishlist">
        <NavigationLink to="/wishlist">
          <i className="pe-7s-like">
            <span className="quantity">{wishlistQuantity}</span>
          </i>
        </NavigationLink>
      </div>
    );
  }
}
