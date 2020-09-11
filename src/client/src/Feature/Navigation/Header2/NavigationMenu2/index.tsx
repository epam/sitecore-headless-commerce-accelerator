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

import { Image } from '@sitecore-jss/sitecore-jss-react';
import { NavigationLink } from 'Foundation/UI';
import { NavigationLinks } from '../NavigationLinks';
import { NavigationMenuProps, NavigationMenuState } from './models';
import './styles.scss';

const permanentNavigationLinks = [
  {
    title: 'Featured',
    url: '',
  },
  {
    title: 'New',
    url: '/New',
  },
  {
    title: 'Promotions',
    url: '/Promotions',
  },
  {
    title: 'Blog',
    url: '/Blog',
  },
];

class NavigationMenuComponent extends JSS.SafePureComponent<NavigationMenuProps, NavigationMenuState> {
  protected safeRender() {
    const { menuItems } = this.props.fields.data.datasource;

    return (
      <nav className="navigation">
        <ul>
          {menuItems &&
            menuItems.items &&
            menuItems.items.map((menuItem, menuItemIndex) => {
              const {
                title: {
                  jss: { value },
                },
                image,
                commerceCategories,
              } = menuItem;

              const commerceNavigationLinks = commerceCategories.items.map(({ name }) => {
                return {
                  title: name,
                  url: `/shop/${name}`,
                };
              });

              return (
                <li key={menuItemIndex} className="navigation_item">
                  <a className="navigation_link" href="">
                    {value}
                    <i className="fa fa-angle-down" />
                  </a>
                  <ul className="navigation_submenu submenu">
                    <li className="submenu_column">
                      <NavigationLinks
                        links={[{ title: 'Category' }, ...commerceNavigationLinks]}
                        titleClass="submenu_title"
                      />
                    </li>
                    <li className="submenu_column">
                      <NavigationLinks links={permanentNavigationLinks} titleClass="submenu_title" />
                    </li>
                    <li className="submenu_column">
                      <a href="">
                        <Image media={image.jss} />
                      </a>
                    </li>
                  </ul>
                </li>
              );
            })}
          <li className="navigation_item">
            <NavigationLink className="navigation_link" to="/">
              Contact Us
            </NavigationLink>
          </li>
        </ul>
      </nav>
    );
  }
}

export const NavigationMenu2 = JSS.rendering(NavigationMenuComponent);
