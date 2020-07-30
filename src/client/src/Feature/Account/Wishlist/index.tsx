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

import { WishlistItem } from './components/WishlistItem';
import { WishlistProps } from './models';
import './styles.scss';

const itemsMock = [
  {
    brand: 'Helix Seeker Drones',
    img: 'http://int-cd.hca.azure.epmc-stc.projects.epam.com/-/media/Images/Habitat/7042128_01.ashx',
    itemName: 'Seeker I Drone—2.7K 12MP',
    price: {
      currency: '$',
      priceCurrent: '196.00',
      priceFull: '196.00',
    },
  },
  {
    brand: 'Helix Seeker Drones',
    img: 'http://int-cd.hca.azure.epmc-stc.projects.epam.com/-/media/Images/Habitat/7042129_01.ashx',
    itemName: 'Seeker II Drone—4K 12MP',
    price: {
      currency: '$',
      priceCurrent: '394.00',
      priceFull: '394.00',
    },
  },
  {
    brand: 'Helix Seeker Drones',
    img: 'http://int-cd.hca.azure.epmc-stc.projects.epam.com/-/media/Images/Habitat/7042130_01.ashx',
    itemName: 'Seeker III Drone—1GB microSD Quad',
    price: {
      currency: '$',
      priceCurrent: '89.99',
      priceFull: '89.99',
    },
  },
  {
    brand: 'Helix Seeker Drones',
    img: 'http://int-cd.hca.azure.epmc-stc.projects.epam.com/-/media/Images/Habitat/7042131_01.ashx',
    itemName: 'Seeker II Drone—4K 12MP',
    price: {
      currency: '$',
      priceCurrent: '109.99',
      priceFull: '109.99',
    },
  },
];

class WishlistComponent extends JSS.SafePureComponent<WishlistProps, {}> {
  protected safeRender() {
    const { title } = this.props.fields.data.datasource;

    return (
      <div className="wishlist-container">
        <div className="row">
          <div className="col-xs-12">
            <div className="wishlist-title">{title.jss.value}</div>
          </div>
        </div>
        <ul className="wishlist row">
          {itemsMock.map((item, index) => (
            <li className="wishlist__item-col col-sm-4 col-xs-12" key={index}>
              <WishlistItem item={item} />
            </li>
          ))}
        </ul>
      </div>
    );
  }
}
export const Wishlist = JSS.rendering(WishlistComponent);
