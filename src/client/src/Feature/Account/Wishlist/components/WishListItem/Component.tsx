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

import { WishlistItemProps, WishlistItemState } from './models';

import './styles.scss';

export class WishlistItemComponent extends JSS.SafePureComponent<WishlistItemProps, WishlistItemState> {
  protected safeRender() {
    const { item } = this.props;

    return (
      <div className="wishlist__item">
        <img src={item.imageUrls[0]} alt="" />
        <div className="wishlist__item-brand">{item.brand}</div>
        <NavigationLink className="wishlist__item-name" to={`/product/${item.productId}`}>
          {item.displayName}
        </NavigationLink>
        <div className="wishlist__item-price">
          {item.currencySymbol} {item.listPrice}
        </div>
        <button
          className="wishlist__add-btn"
          onClick={(e) => this.props.AddToCart({ productId: item.productId, variantId: item.variantId, quantity: 1 })}
        >
          Add to cart
        </button>
        <button className="wishlist__remove-btn" onClick={(e) => this.props.RemoveWishlistItem(item.variantId)}>
          Remove
        </button>
      </div>
    );
  }
}
