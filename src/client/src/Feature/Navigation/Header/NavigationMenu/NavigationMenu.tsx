//    Copyright 2021 EPAM Systems, Inc.
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

import { Image, Link, Placeholder } from '@sitecore-jss/sitecore-jss-react';
import { get } from 'lodash';
import React, { FC, MouseEvent, useCallback } from 'react';
import { useDispatch } from 'react-redux';

import {
  GraphQLField,
  GraphQLListField,
  GraphQLRendering,
  ImageField,
  LinkField,
  TextField,
} from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';

import { NavigationLinks } from '../NavigationLinks';
import { permanentNavigationLinks } from './constants';
import { closeHamburgerMenu } from './Integration';

import './NavigationMenu.scss';

type CommerceCategory = {
  name: string;
};

type MenuCommerceItemDataSource = {
  title: GraphQLField<TextField>;
  image: GraphQLField<ImageField>;
  commerceCategories: GraphQLListField<CommerceCategory>;
};

type MenuLinkDataSource = {
  id: string;
  uri: GraphQLField<LinkField>;
};

type NavigationMenuDataSource = {
  id: string;
  menuCommerceItems: GraphQLListField<MenuCommerceItemDataSource>;
  menuLinks: GraphQLListField<MenuLinkDataSource>;
};

type NavigationMenuProps = GraphQLRendering<NavigationMenuDataSource>;

export const NavigationMenu: FC<NavigationMenuProps> = ({ fields, rendering }) => {
  const dispatch = useDispatch();

  const dropMenuMobile = (e: MouseEvent<HTMLElement>, dropdownType: string) => {
    const elem = e.currentTarget as HTMLElement;

    e.preventDefault();
    elem.nextElementSibling.classList.toggle(dropdownType);
    elem.lastElementChild.classList.toggle('fa-minus');
  };

  const handleCategoryClick = useCallback(() => {
    dispatch(closeHamburgerMenu());
  }, [dispatch]);

  const { menuCommerceItems, menuLinks } = get(fields, ['data', 'datasource']);

  return (
    <nav className="navigation">
      <ul className="navigation-content">
        {menuCommerceItems &&
          menuCommerceItems.items &&
          menuCommerceItems.items.map((menuCommerceItem, menuCommerceItemIndex) => {
            const {
              title: {
                jss: { value },
              },
              image,
              commerceCategories,
            } = menuCommerceItem;

            const commerceNavigationLinks = commerceCategories.items.map(({ name }) => {
              return {
                title: name,
                url: `/shop/${name}`,
              };
            });

            return (
              <div key={menuCommerceItemIndex}>
                <li className="navigation_item">
                  <span className="navigation_link">
                    {value}
                    <i className="fa fa-angle-down" />
                  </span>
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
                  <div className="navigation_link" onClick={(e) => dropMenuMobile(e, 'drop-open')}>
                    <a className="navigation_link_first-level-title" href={`/shop/${value}`}>
                      {value}
                    </a>
                    <i className="fa fa-plus" />
                  </div>
                  <div className="dropdown">
                    <nav>
                      <ul>
                        <li onClick={(e) => dropMenuMobile(e, 'drop-category-open')}>
                          <span className="title">Category</span>
                          <i className="fa fa-plus" />
                        </li>
                        <ul className="drop-category">
                          {commerceCategories &&
                            commerceCategories.items &&
                            commerceCategories.items.map((category, categoryIndex) => {
                              const { name } = category;
                              const link = '/shop/' + name;
                              return (
                                <li key={categoryIndex} data-autotests={`submenuCommerceItem_${name}`}>
                                  <NavigationLink to={link} onClick={handleCategoryClick}>
                                    {name}
                                  </NavigationLink>
                                </li>
                              );
                            })}
                        </ul>
                      </ul>
                      <ul>
                        <li onClick={(e) => dropMenuMobile(e, 'drop-feature-open')}>
                          <span className="title">Featured</span>
                          <i className="fa fa-plus" />
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
        <div>
          {menuLinks &&
            menuLinks.items &&
            menuLinks.items.map((menuLink) => (
              <li key={menuLink.id} className="navigation_item navigation_mobile_item-contact">
                <Link field={menuLink.uri.jss} className="navigation_link" />
              </li>
            ))}
        </div>
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
        <Placeholder name="widgets-social" rendering={rendering} />
      </ul>
    </nav>
  );
};
