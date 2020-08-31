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
import './styles.scss';

import { MenuButtonProps, MenuButtonState } from './models';

export class MenuButtonComponent extends JSS.SafePureComponent<MenuButtonProps, MenuButtonState> {
  protected safeRender() {
    return (
      <div className="navigation-buttons_item menu-button">
        <a onClick={this.handleClick}>
          <i className="pe-7s-menu" />
        </a>
      </div>
    );
  }

  private handleClick = () => {
    const header = document.querySelector('[data-el="header"]');
    if (header.classList.contains('header--inactive')) {
      header.classList.remove('header--inactive');
    }
    header.classList.add('header--active');
  };
}

export const MenuButton = JSS.rendering(MenuButtonComponent);
