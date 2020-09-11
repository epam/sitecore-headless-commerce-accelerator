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
import Swiper, { ReactIdSwiperProps } from 'react-id-swiper';

import * as JSS from 'Foundation/ReactJss';

import { CarouselItem } from './CarouselItem';

import { CarouselBannerProps, CarouselBannerState } from './models';
import './styles.scss';

export class CarouselBannerComponent extends JSS.SafePureComponent<CarouselBannerProps, CarouselBannerState> {
  public safeRender() {
    const { banners } = this.props.fields.data.datasource;

    const params: ReactIdSwiperProps = {
      autoplay: {
        delay: 5000,
        disableOnInteraction: false,
      },
      effect: 'fade',
      loop: true,
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
      renderNextButton: () => (
        <button className="swiper-button-next ht-swiper-button-nav">
          <i className="pe-7s-angle-right" />
        </button>
      ),
      renderPrevButton: () => (
        <button className="swiper-button-prev ht-swiper-button-nav">
          <i className="pe-7s-angle-left" />
        </button>
      ),
      speed: 1000,
      watchSlidesVisibility: true,
    };

    return (
      <div className="slider-area">
        <div className="slider-active nav-style">
          <Swiper {...params}>
            {banners &&
              banners.items &&
              banners.items.map((banner, i) => {
                const data = {
                  image: banner.image.jss.value.src,
                  link: banner.link.jss.value.href,
                  subtitle: banner.subtitle.jss.value,
                  text: banner.text.jss.value,
                  title: banner.title.jss.value,
                };

                return <CarouselItem key={i} banner={data} sliderClass="swiper-slide" />;
              })}
          </Swiper>
        </div>
      </div>
    );
  }
}
