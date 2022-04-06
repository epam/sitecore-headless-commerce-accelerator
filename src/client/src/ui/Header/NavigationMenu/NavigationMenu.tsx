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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import { get } from 'lodash';
import React, { FC } from 'react';

import { GraphQLField, GraphQLListField, GraphQLRendering, ImageFieldValue, LinkFieldValue } from 'Foundation/ReactJss';

import { cnNavigation } from './cn';
import { navigationSelectors } from './constants';
import { ItemList } from './ItemList';
import { NavigationSelect } from './NavigationSelect';

import './NavigationMenu.scss';

type CommerceCategory = {
  name: string;
};

export type MenuCommerceItemDataSource = {
  id: string;
  title: GraphQLField<string>;
  image: GraphQLField<ImageFieldValue>;
  commerceCategories: GraphQLListField<CommerceCategory>;
};

export type MenuLinkDataSource = {
  id: string;
  uri: GraphQLField<LinkFieldValue>;
};

export type NavigationMenuDataSource = {
  id: string;
  menuCommerceItems: GraphQLListField<MenuCommerceItemDataSource>;
  menuLinks: GraphQLListField<MenuLinkDataSource>;
};

type NavigationMenuProps = GraphQLRendering<NavigationMenuDataSource>;

export const NavigationMenu: FC<NavigationMenuProps> = ({ fields, rendering }) => {
  const menu = get(fields, ['data', 'datasource']);

  return (
    <nav className={cnNavigation()}>
      {menu && menu.menuCommerceItems && menu.menuLinks && (
        <ItemList menuCommerceItems={menu.menuCommerceItems} menuLinks={menu.menuLinks} />
      )}
      <div className={cnNavigation('Selectors')}>
        {navigationSelectors.map(({ options, title }, idx) => (
          <NavigationSelect key={`${title}${idx}`} options={options} title={title} />
        ))}
      </div>
      <div className={cnNavigation('SocialLinks')}>
        <Placeholder name="widgets-social" rendering={rendering} />
      </div>
    </nav>
  );
};
