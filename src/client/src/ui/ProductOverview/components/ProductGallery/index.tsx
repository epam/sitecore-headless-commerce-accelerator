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

import React, { createRef, MouseEvent } from 'react';
import Swiper, { SwiperRefNode } from 'react-id-swiper';
import { LightgalleryItem, LightgalleryProvider } from 'react-lightgallery';

import * as JSS from 'Foundation/ReactJss';

import { Icon } from 'components';

import { ProductGalleryProps, ProductGalleryState } from './models';
import { calculateNumberOfSlidesToShow } from './utils';

import { cnProductGallery } from './cn';
import './styles.scss';

const BUTTON_TYPES = {
  PREV: 'prev',
  NEXT: 'next',
};

const BUTTON_SIZES = {
  SMALL: 'small',
  BIG: 'big',
};

export class ProductGallery extends JSS.SafePureComponent<ProductGalleryProps, ProductGalleryState> {
  private readonly gallerySwiperRef: React.MutableRefObject<SwiperRefNode>;
  private readonly carouselSwiperRef: React.MutableRefObject<SwiperRefNode>;
  constructor(props: ProductGalleryProps) {
    super(props);

    this.gallerySwiperRef = createRef();
    this.carouselSwiperRef = createRef();

    this.state = {
      carouselSwiper: null,
      gallerySwiper: null,
    };

    this.goPrev = this.goPrev.bind(this);
    this.goNext = this.goNext.bind(this);
    this.onGallerySlideChange = this.onGallerySlideChange.bind(this);
    this.onCarouselSlideClick = this.onCarouselSlideClick.bind(this);
  }

  public goPrev() {
    const { gallerySwiper, carouselSwiper } = this.state;

    if (gallerySwiper && carouselSwiper) {
      gallerySwiper.slidePrev();
      carouselSwiper.slidePrev();
    }
  }

  public goNext() {
    const { gallerySwiper, carouselSwiper } = this.state;

    if (gallerySwiper && carouselSwiper) {
      gallerySwiper.slideNext();
      carouselSwiper.slideNext();
    }
  }

  public onCarouselSlideClick(e: MouseEvent<HTMLDivElement>) {
    const slideElement = (e.target as Element).closest('.swiper-slide');

    if (slideElement) {
      const { gallerySwiper, carouselSwiper } = this.state;
      const index = parseInt(slideElement.getAttribute('data-swiper-slide-index'), 10);

      if (gallerySwiper && carouselSwiper) {
        gallerySwiper.slideToLoop(index);
        carouselSwiper.slideToLoop(index);
      }
    }
  }

  public onGallerySlideChange() {
    const { gallerySwiper, carouselSwiper } = this.state;

    if (gallerySwiper && carouselSwiper) {
      carouselSwiper.slideToLoop(gallerySwiper.realIndex);
    }
  }

  public componentDidUpdate() {
    const gallerySwiper = this.gallerySwiperRef.current && this.gallerySwiperRef.current.swiper;
    const carouselSwiper = this.carouselSwiperRef.current && this.carouselSwiperRef.current.swiper;

    if (gallerySwiper && carouselSwiper) {
      this.setState({
        carouselSwiper,
        gallerySwiper,
      });
    }
  }

  protected safeRender() {
    const { images } = this.props;

    const gallerySwiperParams = {
      freeMode: false,
      loop: true,
      loopedSlides: images && images.length % 4 === 0 ? 0 : images.length * 3,
      on: {
        slideChange: this.onGallerySlideChange,
      },
      slidesPerView: 1,
      spaceBetween: 10,
    };

    const carouselSwiperParams = {
      breakpoints: calculateNumberOfSlidesToShow(images),
      freeMode: false,
      height: 140,
      loop: true,
      loopedSlides: images && images.length % 4 === 0 ? 0 : images.length * 3,
      slidesPerView: images && images.length < 4 ? images.length : 4,
      spaceBetween: 10,
      touchRatio: 0.2,
    };

    return (
      <div className={cnProductGallery()}>
        <div className={cnProductGallery('Header')}>
          <LightgalleryProvider>
            {images && images.length > 1 && (
              <>
                <button
                  className={cnProductGallery('Button', { type: BUTTON_TYPES.PREV, size: BUTTON_SIZES.BIG })}
                  onClick={this.goPrev}
                >
                  <Icon icon="icon-angle-left" />
                </button>
                <button
                  className={cnProductGallery('Button', { type: BUTTON_TYPES.NEXT, size: BUTTON_SIZES.BIG })}
                  onClick={this.goNext}
                >
                  <Icon icon="icon-angle-right" />
                </button>
              </>
            )}
            <Swiper {...gallerySwiperParams} effect="fade" ref={this.gallerySwiperRef}>
              {images &&
                images.map((src, index) => (
                  <div key={index}>
                    <LightgalleryItem group="any" src={src} thumb={src}>
                      <button>
                        <Icon icon="icon-expand1" size="xl" />
                      </button>
                    </LightgalleryItem>
                    <div className="single-image">
                      <img src={src} alt="1" className="gallery-image" />
                    </div>
                  </div>
                ))}
            </Swiper>
          </LightgalleryProvider>
        </div>
        {images && images.length > 1 && (
          <div className={cnProductGallery('Carousel')} onClick={this.onCarouselSlideClick}>
            <button
              className={cnProductGallery('Button', { type: BUTTON_TYPES.PREV, size: BUTTON_SIZES.SMALL })}
              onClick={this.goPrev}
            >
              <Icon icon="icon-angle-left" />
            </button>
            <Swiper {...carouselSwiperParams} ref={this.carouselSwiperRef} activeSlideKey="0">
              {images.map((src, index) => (
                <div key={index}>
                  <div className="single-image">
                    <img src={src} className="img-fluid" alt="" />
                  </div>
                </div>
              ))}
            </Swiper>
            <button
              className={cnProductGallery('Button', { type: BUTTON_TYPES.NEXT, size: BUTTON_SIZES.SMALL })}
              onClick={this.goNext}
            >
              <Icon icon="icon-angle-right" />
            </button>
          </div>
        )}
      </div>
    );
  }
}
