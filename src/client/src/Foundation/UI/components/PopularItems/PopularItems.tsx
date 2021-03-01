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

import React, { FC, HTMLProps } from 'react';

import { cnPopularItems } from './cn';
import { ItemsList } from './ItemsList';

import './PopularItems.scss';

export type PopularItemProps = {
  itemHeading: string;
  itemId: string;
  itemImage: string;
  itemLink: string;
  itemTag: string;
};

export type PopularItemsProps = HTMLProps<HTMLDivElement> & {
  componentHeading?: string;
  items: PopularItemProps[];
};

export const PopularItems: FC<PopularItemsProps> = ({ componentHeading, className, items = [] }) => {
  return (
    <div className={cnPopularItems(null, [className])}>
      <h4 className={cnPopularItems('Title')}>{componentHeading}</h4>
      {items.length !== 0 && <ItemsList items={items} />}
    </div>
  );
};
