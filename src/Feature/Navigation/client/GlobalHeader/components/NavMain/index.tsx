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

import classNames from 'classnames';

import * as JSS from 'Foundation/ReactJss/client';
import { NavigationLink } from 'Foundation/UI/client';

import { NavMainProps, NavMainState } from './models';
import './styles.scss';

export default class NavMain extends JSS.SafePureComponent<NavMainProps, NavMainState> {

  protected dropMenu(e: React.MouseEvent<HTMLElement>) {
    const elem = e.target as HTMLElement;
    e.preventDefault();
    elem.nextElementSibling.classList.toggle('drop-open');
    elem.classList.toggle('i-open');
}
  protected safeRender() {
    const { menuItems } = this.props;

    return (
      <nav id="nav-main">
        <ul>
          {menuItems.items &&
            menuItems.items.map((menuItem, menuItemIndex) => {
              const { title, image } = menuItem;

              return (
                <li key={menuItemIndex} className={classNames('menu-item', {'right-item': menuItemIndex > (menuItems.items.length / 2)})}>
                  <a href="">{title.jss.value}</a>
                  <i simple-drawer="simple-drawer" className="fa fa-caret-down" onClick={(e) => this.dropMenu(e)}/>
                  <div className="dropdown">
                    <nav>
                      <ul>
                        <li>
                          <span className="title">Category</span>
                        </li>
                        {menuItem.commerceCategories.items &&
                          menuItem.commerceCategories.items.map((category, categoryIndex) => {
                            const { name } = category;
                            const link = '/shop/' + name;
                            return (
                              <li key={categoryIndex}>
                                <NavigationLink to={link}>{name}</NavigationLink>
                              </li>
                            );
                          })}
                      </ul>
                      <ul>
                        <li>
                          <span className="title">Featured</span>
                        </li>
                        <li>
                          <NavigationLink to="/new">New</NavigationLink>
                        </li>
                        <li>
                          <NavigationLink to="/Promitions">Promotions</NavigationLink>
                        </li>
                        <li>
                          <NavigationLink to="/Blog">Blog</NavigationLink>
                        </li>
                      </ul>
                    </nav>
                    <aside>
                      <a href="">
                        <img src={image.jss.value.src} />
                      </a>
                    </aside>
                  </div>
                </li>
              );
            })}
        </ul>
      </nav>
    );
  }
}
