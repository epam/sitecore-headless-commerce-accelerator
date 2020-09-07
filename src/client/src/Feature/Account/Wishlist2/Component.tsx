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

import { NavigationLink } from 'Foundation/UI';
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
      <div className="wishlist-container" style={{ marginTop: 30 }}>
        {items.length === 0 ? (
          <div className="wishlist_container-empty">
            <div className="heart-icon">
              <i className="pe-7s-like"/>
            </div>
            <label className="wishlist_label-empty">No items found in wishlist</label>
            <NavigationLink to={`/`}>
              <button className="wishlist_btn-empty">Add Items</button>
            </NavigationLink>
          </div>
        ) : (
          <>
            <div className="row">
              <div className="col-xs-12">
                <div className="wishlist-title">{title.jss.value} Items</div>
              </div>
            </div>
            <table>
              <thead className="wishlist_table_header_container">
                <tr>
                  <th>IMAGE</th>
                  <th>PRODUCT NAME</th>
                  <th>UNIT PRICE</th>
                  <th>ADD TO CART</th>
                  <th>ACTION</th>
                </tr>
              </thead>
              <tbody>
                {items.map((item, index) => (
                  <WishlistItem item={item} key={index} />
                ))}
              </tbody>
            </table>
            <div className="action_container">
              <NavigationLink to={`/`}>
                <button>Continue Shopping</button>
              </NavigationLink>
              <button>Clear Wishlist</button>
            </div>
          </>
        )}
      </div>
    );
  }
}
