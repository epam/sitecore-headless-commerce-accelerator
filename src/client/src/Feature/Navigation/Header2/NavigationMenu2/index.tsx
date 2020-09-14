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
import { NavigationLink } from 'Foundation/UI';
import * as React from 'react';

import { Image } from '@sitecore-jss/sitecore-jss-react';
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
  protected dropMenuMobile(e: React.MouseEvent<HTMLElement>, dropdownType: string) {
    const elem = e.target as HTMLElement;
    e.preventDefault();
    elem.parentElement.nextElementSibling.classList.toggle(dropdownType);
    elem.classList.toggle('fa-minus');
  }
  protected safeRender() {
    const { menuItems } = this.props.fields.data.datasource;
    return (
      <nav className="navigation">
        <ul className="navigation-content">
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
                <div key={menuItemIndex}>
                  <li className="navigation_item">
                    <a className="navigation_link" href={`/shop2/${value}`}>
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
                  <li className="navigation_mobile_item">
                    <div className="navigation_link">
                      <a className="navigation_link_first-level-title" href={`/shop2/${value}`}>{value}</a>
                      <i className="fa fa-plus" onClick={(e) => this.dropMenuMobile(e, 'drop-open')} />
                    </div>
                    <div className="dropdown">
                      <nav>
                        <ul>
                          <li>
                            <span className="title">Category</span>
                            <i className="fa fa-plus" onClick={(e) => this.dropMenuMobile(e, 'drop-category-open')} />
                          </li>
                          <ul className="drop-category">
                            {commerceCategories &&
                              commerceCategories.items &&
                              commerceCategories.items.map((category, categoryIndex) => {
                                const { name } = category;
                                const link = '/shop/' + name;
                                return (
                                  <li key={categoryIndex} data-autotests={`subMenuItem_${name}`}>
                                    <NavigationLink to={link}>{name}</NavigationLink>
                                  </li>
                                );
                              })}
                          </ul>
                        </ul>
                        <ul>
                          <li>
                            <span className="title">Featured</span>
                            <i className="fa fa-plus" onClick={(e) => this.dropMenuMobile(e, 'drop-feature-open')} />
                          </li>
                          <ul className="drop-feature">
                            <li data-autotests="newNavigationLink">
                              <NavigationLink to="/new">New</NavigationLink>
                            </li>
                            <li data-autotests="promotionsNavigationLink">
                              <NavigationLink to="/Promitions">Promotions</NavigationLink>
                            </li>
                            <li data-autotests="blogNavigationLink">
                              <NavigationLink to="/Blog">Blog</NavigationLink>
                            </li>
                          </ul>
                        </ul>
                      </nav>
                      <aside>
                        <a href="">
                          <Image media={image.jss} />
                        </a>
                      </aside>
                    </div>
                  </li>
                </div>
              );
            })}
          <li className="navigation_item navigation_mobile_item-contact">
            <NavigationLink className="navigation_link" to="/home2">
              Contact Us
            </NavigationLink>
          </li>
          <div className="dropdown-mobile dropdown-mobile-lang">
            <div className="dropdown-mobile_title">Choose Language</div>
            <select>
              <option value="en">English</option>
            </select>
          </div>
          <div className="dropdown-mobile dropdown-mobile-currency">
            <div className="dropdown-mobile_title">Choose Currency</div>
            <select>
              <option value="USD">USD</option>
            </select>
          </div>
          <div className="widgets-social">
            <a href="//twitter.com" title="Twitter">
              <i className="fa fa-twitter" />
            </a>
            <a href="//instagram.com" title="Instagram">
              <i className="fa fa-instagram" />
            </a>
            <a href="//facebook.com" title="Facebook">
              <i className="fa fa-facebook" />
            </a>
            <a href="//pinterest.com" title="Pinterest">
              <i className="fa fa-pinterest" />
            </a>
          </div>
        </ul>
      </nav>
    );
  }
}

export const NavigationMenu2 = JSS.rendering(NavigationMenuComponent);
