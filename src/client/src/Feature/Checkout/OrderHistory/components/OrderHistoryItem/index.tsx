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

import * as Jss from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';
import Swiper, { SwiperRefNode } from 'react-id-swiper';

import './styles.scss';

import { OrderHistoryItemProps } from './models';

export class OrderHistoryItem extends Jss.SafePureComponent<OrderHistoryItemProps, Jss.SafePureComponentState> {
  private readonly imageSwiperRef: React.MutableRefObject<SwiperRefNode>;
  constructor(props: OrderHistoryItemProps) {
    super(props);
    this.imageSwiperRef = React.createRef();

    this.goPrev = this.goPrev.bind(this);
    this.goNext = this.goNext.bind(this);
  }
  public goPrev() {
    const imageSwiper = this.imageSwiperRef.current.swiper;
    if (imageSwiper) {
      const currentIndex = imageSwiper.activeIndex;
      imageSwiper.slideTo(currentIndex - 1);
    }
  }
  public goNext() {
    const imageSwiper = this.imageSwiperRef.current.swiper;
    if (imageSwiper) {
      const currentIndex = imageSwiper.activeIndex;
      imageSwiper.slideTo(currentIndex + 1);
    }
  }

  public safeRender() {
    const { order, fallbackImageUrl } = this.props;
    const maxDisplayableSlides = 4;
    const thumbnailSwiperParams = {
      freeMode: false,
      loop: false,
      slidesPerView: 4,
      spaceBetween: 10,
      touchRatio: 0.2,
    };

    return (
      <div className="order-list-item-header col-md-12">
        <div className="order-list-item-header_title">
          <div
            className={`order-list-item-header_title-wrapper ${
              order && order.cartLines.length > maxDisplayableSlides && 'mr-160'
            }`}
          >
            <NavigationLink to={`/Checkout/Confirmation?trackingNumber=${order.trackingNumber}`}>
              Order Item
            </NavigationLink>
            <span className="order-list-item-header_title-wrapper-price">
              {order.price.currencySymbol} {order.price.total}
            </span>
            <span className="order-list-item-header_title-wrapper-status">{order.status}</span>
          </div>
          <button
            className={`order-list-item-header_title-reorder ${
              order && order.cartLines.length > maxDisplayableSlides && 'r-82'
            }`}
          >
            Reorder
          </button>
          {order && order.cartLines.length > 4 && (
            <>
              <button className="order-list-item-header_title-carousel-prev" onClick={this.goPrev}>
                <i className="pe-7s-angle-left" />
              </button>
              <button className="order-list-item-header_title-carousel-next" onClick={this.goNext}>
                <i className="pe-7s-angle-right" />
              </button>
            </>
          )}
        </div>
        <div className="order-list-item_content">
          <Swiper {...thumbnailSwiperParams} ref={this.imageSwiperRef} activeSlideKey="0">
            {order.cartLines.map((cartLine, cartLineIndex) => (
              <div className="cart-line-item" key={cartLineIndex}>
                <div className="product-img-badges">
                  <span className="pink">
                    {cartLine.price.currencySymbol}
                    {cartLine.product.listPrice}
                  </span>
                  <span className="purple">
                    <i className="fa fa-times" />
                    <span className="quantity-text">{cartLine.quantity}</span>
                  </span>
                </div>
                <div className="img-wrap">
                  <NavigationLink to={`/product/${cartLine.product.productId}`}>
                    <img
                      src={!!cartLine.variant.imageUrls[0] ? cartLine.variant.imageUrls[0] : fallbackImageUrl}
                      alt="product image"
                    />
                  </NavigationLink>
                </div>
                <div className="product_name">
                  <NavigationLink to={`/product/${cartLine.product.productId}`}>
                    <h2>{cartLine.variant.displayName}</h2>
                  </NavigationLink>
                </div>
                <div className="price">
                  <div className="price_full">
                    <span className="price_currency">{cartLine.price.currencySymbol}</span>
                    <span className="price_amount">{cartLine.price.total}</span>
                  </div>
                  {cartLine.product.adjustedPrice < cartLine.product.listPrice && (
                    <div>
                      <span>&nbsp;-&nbsp;</span>
                      <span className="price_original">
                        <span className="price_currency">{cartLine.price.currencySymbol}</span>
                        <span className="price_amount">{cartLine.product.listPrice * cartLine.quantity}</span>
                      </span>
                    </div>
                  )}
                </div>
              </div>
            ))}
          </Swiper>
        </div>
      </div>
    );
  }
}
