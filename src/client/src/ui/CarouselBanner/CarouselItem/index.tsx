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

import * as JSS from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';

import { CarouselItemProps, CarouselItemState } from './models';
import './styles.scss';

export class CarouselItem extends JSS.SafePureComponent<CarouselItemProps, CarouselItemState> {
  public safeRender() {
    const { banner, sliderClass } = this.props;
    return (
      <div
        className={`single-slider slider-height d-flex align-items-center bg-img ${sliderClass ? sliderClass : ''}`}
        style={{ backgroundImage: `url(${banner.image})` }}
      >
        <div className="banner-container">
          <div className="col-xl-6 col-lg-7 col-md-8 col-12 ml-auto slider-content_container">
            <div className="slider-content slider-animated text-center">
              <h3 className="animated">{banner.title}</h3>
              <h1 className="animated">{banner.subtitle}</h1>
              <p className="animated">{banner.text}</p>
              <div className="slider-btn btn-hover">
                <NavigationLink className="animated" to={banner.link}>
                  <span>SHOP NOW</span>
                </NavigationLink>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
