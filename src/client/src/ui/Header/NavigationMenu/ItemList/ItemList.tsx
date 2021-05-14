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

import React, { FC } from 'react';

import { cnNavigation } from '../cn';
import { Item } from '../Item';
import { MenuItem } from '../MenuItem';
import { NavigationMenuDataSource } from '../NavigationMenu';

import './ItemList.scss';

type ItemListProps = Pick<NavigationMenuDataSource, 'menuCommerceItems' | 'menuLinks'>;

export const ItemList: FC<ItemListProps> = ({ menuCommerceItems, menuLinks }) => {
  return (
    <ul className={cnNavigation('ItemList')}>
      {menuCommerceItems &&
        menuCommerceItems.items &&
        menuCommerceItems.items.map((menuCommerceItem) => {
          return <Item key={menuCommerceItem.id} {...menuCommerceItem} />;
        })}
      {menuLinks &&
        menuLinks.items &&
        menuLinks.items.map((menuLinkItem) => {
          return <MenuItem key={menuLinkItem.id} {...menuLinkItem} />;
        })}
    </ul>
  );
};
