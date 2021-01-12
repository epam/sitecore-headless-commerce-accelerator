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

import { NavigationLink } from 'Foundation/UI';

import { AboutUsBannerProps, AboutUsBannerState } from './models';

import { AboutUsBannerMockData } from '../mocks';

import BannerPlaceholder from 'Foundation/UI/common/media/images/product-placeholder.jpg';

import '../styles.scss';

export class AboutUsBanner extends JSS.SafePureComponent<AboutUsBannerProps, AboutUsBannerState> {
  protected safeRender() {
    return (
      <div className="banner-area">
        <div className="container">
          <div className="row">
            { AboutUsBannerMockData.map((data, key) => {
                return (
                        <div className="col-lg-4 col-md-4" key={key}>
                          <div
                            className="single-banner"
                          >
                            <NavigationLink to={data.link}>
                              <img src={data.img ? data.img : BannerPlaceholder} alt="" />
                            </NavigationLink>
                            <div className="banner-content">
                              <h3>{data.title}</h3>
                              <h4>
                                {data.subtitle} <span>{data.price}</span>
                              </h4>
                              <NavigationLink to={data.link}>
                                <i className="fa fa-long-arrow-right" />
                              </NavigationLink>
                            </div>
                          </div>
                      </div>
                    );
                })
            }
        </div>
      </div>
    </div>
    );
  }
}
