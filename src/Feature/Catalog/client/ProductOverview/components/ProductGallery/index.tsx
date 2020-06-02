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

import classnames from 'classnames';
import * as React from 'react';

import { Carousel } from 'Foundation/UI/client';

import * as JSS from 'Foundation/ReactJss/client';

import { ProductGalleryProps, ProductGalleryState } from './models';
import './styles.scss';

export class ProductGallery extends JSS.SafePureComponent<ProductGalleryProps, ProductGalleryState> {
  constructor(props: ProductGalleryProps) {
    super(props);

    this.state = {
      activeImageIndex: 0,
    };

    this.setImage = this.setImage.bind(this);
  }

  protected safeRender() {
    const { images } = this.props;
    const { activeImageIndex } = this.state;

    return (
      <div className="product-gallery">
        <div className="product-gallery-header">
          <img src={images[activeImageIndex]} alt="1" className="gallery-image" />
        </div>
        <Carousel
          className="product-gallery-carousel"
          buttonPreviousText={<i className="fa fa-angle-left" />}
          buttonNextText={<i className="fa fa-angle-right" />}
          options={{
            breakpoints: {
              1350: {
                slidesPerView: 3,
              },
            },
            slidesPerView: 4,
            spaceBetween: 10,
          }}
        >
          {images &&
            images.map((src, index) => (
              <div
                key={index}
                onClick={() => this.setImage(index)}
                className={classnames('swiper-slide', 'item', { active: activeImageIndex === index })}
              >
                <img src={src} alt={index.toString()} className="thumbs-img" />
              </div>
            ))}
        </Carousel>
      </div>
    );
  }

  private setImage(index: number) {
    this.setState({
      activeImageIndex: index,
    });
  }
}
