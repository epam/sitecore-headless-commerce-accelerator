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

import * as React from 'react';

import { Text, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { ListingControlProps, ListingControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

class ListingControl extends Jss.SafePureComponent<ListingControlProps, ListingControlState> {
  public safeRender() {
    return (
      <section className="listing-wishlist-grid">
        <header className="thick-theme">
            <div className="color-title">
                <Text field={{ value: 'Wishlist' }} tag="h1" className="title wishlist-title"/>
                <div className="color-bar" />
            </div>
            <div className="wishlist-filter">
                <Text field={{ value: 'Sorted by:' }} tag="div" className="label" />
                {' '}
                <div className="select">
                    <select>
                        <Text field={{ value: 'Most Recent' }} tag="option" />
                        <Text field={{ value: 'Low to High' }} tag="option" />
                        <Text field={{ value: 'High to Low' }} tag="option" />
                    </select>
                </div>
            </div>
        </header>
        <ul>
            {
                new Array(15)
                    .fill(null)
                    .map((item, index) => (
                        <li key={index}>
                            <figure className="wishlist-grid-item unavailable">
                                <div className="item-imageBlock">
                                    <img src="../../media/images/glasses-for-slider.png" alt=""/>
                                    <Text
                                        field={{ value: 'Sorry, this item is no longer available' }}
                                        tag="span"
                                        className="overlay overlay-unavailable"
                                    />
                                    <Text
                                        field={{ value: 'Item has been added to your shopping cart' }}
                                        tag="span"
                                        className="overlay overlay-added"
                                    />
                                </div>
                                <a className="wishlist-grid-item-remove">
                                    <i className="fa fa-times" />
                                </a>
                                <figcaption className="item-caption">
                                    <div className="item-price">
                                        <Text field={{ value: '$' }} tag="span" className="price-currency" />
                                        <Text field={{ value: '1089.00' }} tag="span" className="price-amount" />
                                    </div>
                                    <div className="item-heading">
                                        <Text
                                            field={{ value: 'Google' }}
                                            tag="div"
                                            className="heading-brand"
                                        />
                                        <Text
                                            field={{ value: 'Google Glass' }}
                                            tag="h2"
                                            className="heading-product"
                                        />
                                    </div>
                                    <div className="item-options">
                                        <div className="color">
                                            <Text field={{ value: 'Color:' }} tag="span" />
                                            <span className="color-sample" style={{ background: '#dadbdd' }} />
                                        </div>
                                        <div className="size">
                                            <Text field={{ value: 'Size:' }} tag="span" />
                                            {' '}
                                            <Text field={{ value: 'L' }} tag="span" />
                                        </div>
                                        <div className="qty">
                                            <Text field={{ value: 'Qty:' }} tag="span" />
                                            {' '}
                                            <Text field={{ value: '1' }} tag="span" />
                                        </div>
                                    </div>
                                    <div className="item-btns">
                                        <Text
                                            field={{ value: 'Find in Store' }}
                                            tag="a"
                                            className="btn small btn-find"
                                            href=""
                                            title=""
                                        />
                                        <Text
                                            field={{ value: 'Move to Cart' }}
                                            tag="a"
                                            className="btn small btn-find"
                                            href=""
                                            title=""
                                        />
                                    </div>
                                </figcaption>
                            </figure>
                        </li>
                    ))
            }
        </ul>
      </section>
    );
  }
}

export const Listing = withExperienceEditorChromes(ListingControl);
