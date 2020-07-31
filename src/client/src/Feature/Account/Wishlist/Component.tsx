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

import { WishlistProps, WishlistState } from './models';
import './styles.scss';

export class WishlistComponent extends JSS.SafePureComponent<WishlistProps, WishlistState> {
  public componentDidMount() {
    this.props.GetWishlist();
  }

  protected safeRender() {
    const { title } = this.props.fields.data.datasource;
    const { items } = this.props;
    console.log(items);
    return (
      <div className="wishlist-container">
        <div className="row">
          <div className="col-xs-12">
            <div className="wishlist-title">{title.jss.value}</div>
          </div>
        </div>
        <ul className="wishlist row">
          {items.map((item, index) => (
            <li className="wishlist__item-col col-sm-4 col-xs-12" key={index}>
              <WishlistItem item={item} />
            </li>
          ))}
        </ul>
      </div>
    );
  }
}
