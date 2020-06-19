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

import { WishListButtonProps, WishListButtonState } from './models';

export class WishListButtonComponent extends JSS.SafePureComponent<WishListButtonProps, WishListButtonState> {
  protected safeRender() {
    return (
      <NavigationLink className="user-navigation-btn" to="/wishlist">
        <i className="fa fa-heart" />
        <span>Wishlist</span>
      </NavigationLink>
    );
  }
}

export const WishListButton = WishListButtonComponent;
