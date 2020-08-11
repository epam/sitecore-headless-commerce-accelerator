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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import * as JSS from 'Foundation/ReactJss';

import { BannerGridProps, BannerGridState } from './models';

import './styles.scss';

class BannerGridComponent extends JSS.SafePureComponent<BannerGridProps, BannerGridState> {
  public safeRender() {
    const bannerNames = [
      'banner-item-main',
      'banner-item-wide',
      'banner-item-a',
      'banner-item-b',
      'banner-item-c',
      'banner-item-d',
    ];
    const { rendering } = this.props;
    return (
      <section className="hero-home-wrap">
        <div className="section-title text-center mb-55 ">
          <h2>OUR BANNERS</h2>
        </div>
        <div className="banners-row row">
          {bannerNames.map((item, index) => (
            <div className="col-xl-3 col-md-6 col-lg-4 col-sm-6 banner-item" key={index}>
              <Placeholder name={item} rendering={rendering} />
            </div>
          ))}
        </div>
      </section>
    );
  }
}

export const BannerGrid2 = JSS.rendering(BannerGridComponent);
