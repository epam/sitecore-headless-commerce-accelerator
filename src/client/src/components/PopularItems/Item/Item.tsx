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

import { NavigationLink } from 'ui/NavigationLink';

import { cnPopularItems } from '../cn';
import { PopularItemProps } from '../PopularItems';

import './Item.scss';

export const Item: FC<PopularItemProps> = ({ itemHeading, itemId, itemImage, itemLink, itemTag }) => {
  return (
    <li className={cnPopularItems('Item')}>
      <div className={cnPopularItems('ImgContainer')}>
        <NavigationLink className={cnPopularItems('Link')} to={`${itemLink}${itemId}`}>
          <figure>
            <img className={cnPopularItems('Img')} src={itemImage} alt={itemHeading} />
          </figure>
        </NavigationLink>
      </div>
      <div className={cnPopularItems('Info')}>
        <span className={cnPopularItems('InfoTag')}>{itemTag}</span>
        <NavigationLink className={cnPopularItems('Link')} to={`${itemLink}${itemId}`}>
          <h4 className={cnPopularItems('InfoHeading')}>{itemHeading}</h4>
        </NavigationLink>
      </div>
    </li>
  );
};
