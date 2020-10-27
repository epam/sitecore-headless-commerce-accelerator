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

import { LightgalleryItem, LightgalleryProvider } from 'react-lightgallery';

import * as JSS from 'Foundation/ReactJss';

import Swiper, { SwiperRefNode } from 'react-id-swiper';
import { ProductGalleryProps, ProductGalleryState } from './models';
import './styles.scss';

export class ProductGallery extends JSS.SafePureComponent<ProductGalleryProps, ProductGalleryState> {
  private readonly imageSwiperRef: React.MutableRefObject<SwiperRefNode>;
  private readonly carouselSwiperRef: React.MutableRefObject<SwiperRefNode>;
  constructor(props: ProductGalleryProps) {
    super(props);

    this.imageSwiperRef = React.createRef();
    this.carouselSwiperRef = React.createRef();

    this.state = {
    };

    this.goPrev = this.goPrev.bind(this);
    this.goNext = this.goNext.bind(this);
    this.changeIndex = this.changeIndex.bind(this);
  }
  public goPrev() {
    const { images } = this.props;
    const thumbnailSwiper = this.carouselSwiperRef.current.swiper;
    if (thumbnailSwiper) {
      const currentIndex = thumbnailSwiper.activeIndex;
      (currentIndex === 1)
      ? thumbnailSwiper.slideTo(images.length)
      : thumbnailSwiper.slideTo(currentIndex - 1);
    }
  }
  public goNext() {
    const { images } = this.props;
    const thumbnailSwiper = this.carouselSwiperRef.current.swiper;
    if (thumbnailSwiper) {
      const currentIndex = thumbnailSwiper.activeIndex;
      (currentIndex === images.length)
      ? thumbnailSwiper.slideTo(1)
      : thumbnailSwiper.slideTo(currentIndex + 1);
    }
  }

  public changeIndex(e: React.MouseEvent<HTMLDivElement>) {
    const thumbnailSwiper = this.carouselSwiperRef.current.swiper;
    const index = parseInt(e.currentTarget.getAttribute('data-swiper-slide-index'), 10);
    if (thumbnailSwiper) {
      thumbnailSwiper.slideToLoop(index);
    }
  }

  public componentDidUpdate() {
    const gallerySwiper = this.imageSwiperRef.current.swiper;
    const thumbnailSwiper = this.carouselSwiperRef.current.swiper;
    if (gallerySwiper && thumbnailSwiper
    ) {
      gallerySwiper.controller.control = thumbnailSwiper;
      thumbnailSwiper.controller.control = gallerySwiper;
      thumbnailSwiper.height = 140;

      const listOfSlides = thumbnailSwiper.wrapperEl.children;
      for (const slide of listOfSlides as any) {
        slide.addEventListener('click', this.changeIndex);
      }
    }
  }

  protected safeRender() {
    const { images } = this.props;

    const gallerySwiperParams = {
      freeMode: false,
      loop: true,
      loopedSlides: (images && (images.length % 4) === 0 ? 0 : images.length * 3),
      slidesPerView: 1,
      spaceBetween: 10,
    };

    const thumbnailSwiperParams = {
      freeMode: false,
      loop: true,
      loopedSlides: (images && (images.length % 4) === 0 ? 0 : images.length * 3),
      slidesPerView: (images && images.length < 4 ? images.length : 4),
      spaceBetween: 10,
      touchRatio: 0.2,
    };

    return (
      <div className="product-gallery">
        <div className="product-gallery-header">
          <LightgalleryProvider>
            <Swiper {...gallerySwiperParams} effect="fade" ref={this.imageSwiperRef}>
              {images &&
                images.map((src, index) => (
                  <div key={index}>
                    <LightgalleryItem group="any" src={src} thumb={src}>
                      <button>
                        <i className="pe-7s-expand1" />
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
        <div className="product-gallery-carousel">
          <Swiper {...thumbnailSwiperParams} ref={this.carouselSwiperRef} activeSlideKey="0">
            {images &&
              images.map((src, index) => (
                <div key={index} onClick={(e) => this.changeIndex(e)}>
                  <div className="single-image">
                    <img
                      src={src}
                      className="img-fluid"
                      alt=""
                    />
                  </div>
                </div>
              )
            )}
          </Swiper>
          <button className="product-gallery-carousel-prev" onClick={this.goPrev}>
            <i className="pe-7s-angle-left" />
          </button>
          <button className="product-gallery-carousel-next" onClick={this.goNext}>
            <i className="pe-7s-angle-right" />
          </button>
        </div>
      </div>
    );
  }
}
