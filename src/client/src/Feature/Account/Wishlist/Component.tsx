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
    return (
      <div className="wishlist-container">
        {items.length === 0 ? (
          <div className="wishlist_container-empty">
            <div className="heart-icon">
              <i className="pe-7s-like" />
            </div>
            <label className="wishlist_label-empty">No items found in wishlist</label>
            <NavigationLink to={`/search`}>
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
                <tr className="wishlist_table_row">
                  <th className="wishlist_table_header">IMAGE</th>
                  <th className="wishlist_table_header">PRODUCT NAME</th>
                  <th className="wishlist_table_header">UNIT PRICE</th>
                  <th className="wishlist_table_header">ADD TO CART</th>
                  <th className="wishlist_table_header">ACTION</th>
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
                <button className="wishlist-continue-btn">Continue Shopping</button>
              </NavigationLink>
              <button className="clear-wishlist-btn">Clear Wishlist</button>
            </div>
          </>
        )}
      </div>
    );
  }
}
