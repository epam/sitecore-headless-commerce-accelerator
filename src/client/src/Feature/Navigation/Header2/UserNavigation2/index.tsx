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

import { CartButton2 } from '../CartButton2';
import { MenuButton } from '../MenuButton';
import { UserButton2 } from '../UserButton2';
import { WishlistButton2 } from '../WishlistButton2';

import { UserNavigationProps, UserNavigationState } from './models';

import './styles.scss';

class UserNavigationComponent extends JSS.SafePureComponent<UserNavigationProps, UserNavigationState> {
  protected safeRender() {
    return (
      <nav className="navigation-buttons">
        <UserButton2 />
        <div className="navigation-buttons_item store-locator">
          <NavigationLink to="/storelocator2">
            <i className="pe-7s-map-marker" />
          </NavigationLink>
        </div>
        <WishlistButton2 />
        <CartButton2 />
        <MenuButton />
      </nav>
    );
  }
}

export const UserNavigation2 = JSS.rendering(UserNavigationComponent);
