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

import * as Jss from 'Foundation/ReactJss/client';
import { Carousel, NavigationLink } from 'Foundation/UI/client';

import './styles.scss';

import { OrderHistoryItemProps } from './models';

export class OrderHistoryItem extends Jss.SafePureComponent<OrderHistoryItemProps, Jss.SafePureComponentState> {
  public safeRender() {
    const { order, fallbackImageUrl} = this.props;
    return (
      <div className="order-list-item-header col-md-12">
        <div className="order-list-item-header__title">
          <NavigationLink to={`/Checkout/Confirmation?trackingNumber=${order.trackingNumber}`}>{order.trackingNumber}</NavigationLink>
        </div>
        <div className="order-list-item-header__content">
          <div className="order-list-item-header__content-line">
            <span className="status">{order.status}</span>
            <span className="price">
              {order.price.currencySymbol} {order.price.total}
            </span>
          </div>
          <Carousel
            className="order-cart-lines-carousel"
            buttonPreviousText={<i className="fa fa-angle-left" />}
            buttonNextText={<i className="fa fa-angle-right" />}
            options={{ slidesPerView: 'auto', spaceBetween: 10 }}
          >
            {order.cartLines.map((cartLine, cartLineIndex) => (
              <div className="swiper-slide" key={cartLineIndex}>
                <figure className="cart-line-item">
                  <span className="quantity">
                    <i className="fa fa-times" />
                    {cartLine.quantity}
                  </span>
                  <div className="img-wrap">
                    <img src={!!cartLine.variant.imageUrls[0] ? cartLine.variant.imageUrls[0] : fallbackImageUrl} alt="product image" />
                    <NavigationLink className="btn btn-main btn-viewProduct" to={`/product/${cartLine.product.productId}`}>View Product</NavigationLink>
                  </div>
                  <figcaption>
                    <div className="price">
                      <div className="price__full">
                        <span className="price__currency">{cartLine.price.currencySymbol}</span>
                        <span className="price__amount">{cartLine.price.total}</span>
                      </div>
                    </div>
                    <div className="brand">{cartLine.variant.brand}</div>
                    <h2>{cartLine.variant.displayName}</h2>
                  </figcaption>
                </figure>
              </div>
            ))}
          </Carousel>
        </div>
      </div>
    );
  }
}
