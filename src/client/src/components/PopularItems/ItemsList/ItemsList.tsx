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

import React, { FC } from 'react';

import { cnPopularItems } from '../cn';
import { Item } from '../Item';
import { PopularItemsProps } from '../PopularItems';

import './ItemsList.scss';

type ItemsListProps = Pick<PopularItemsProps, 'className' | 'items'>;

export const ItemsList: FC<ItemsListProps> = ({ className, items }) => {
  return (
    <ul className={cnPopularItems('List', [className])}>
      {items.map((item, i) => (
        <Item
          itemHeading={item.itemHeading}
          itemId={item.itemId}
          itemImage={item.itemImage}
          itemLink={item.itemLink}
          itemTag={item.itemTag}
          key={i}
        />
      ))}
    </ul>
  );
};
