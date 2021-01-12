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
import Swiper from 'react-id-swiper';
import { AboutUsBrandMockData } from '../mocks';
import { BrandLogosProps, BrandLogosState } from './models';

import BrandPlaceholder from 'Foundation/UI/common/media/images/brand-placeholder.png';

import '../styles.scss';

const settings = {
  autoplay: {
    delay: 3000,
    disableOnInteraction: false
  },
  breakpoints: {
    1024: {
      slidesPerView: 5
    },
    768: {
      slidesPerView: 4
    },
    640: {
      slidesPerView: 3
    },
    320: {
      slidesPerView: 2
    }
  },
  grabCursor: true,
  loop: true,
};

export class BrandLogos extends JSS.SafePureComponent<BrandLogosProps, BrandLogosState> {
  protected safeRender() {
    return (
      <div className="brand-logo-area container">
        <Swiper {...settings}>
            {AboutUsBrandMockData &&
              AboutUsBrandMockData.map((data, key) => {
                return (
                  <div key={key} className={`single-brand-logo swiper-slide`}>
                    <img src={data.imgSrc ? data.imgSrc : BrandPlaceholder} alt={data.brandName} />
                  </div>
                );
              })}
          </Swiper>
    </div>
    );
  }
}
