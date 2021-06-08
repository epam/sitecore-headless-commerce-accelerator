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

import { Link, Text } from '@sitecore-jss/sitecore-jss-react';

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';

import { Icon } from 'components';

import { AboutUsBannerProps, AboutUsBannerState } from './models';

import '../styles.scss';

export class AboutUsBanner extends JSS.SafePureComponent<AboutUsBannerProps, AboutUsBannerState> {
  protected safeRender() {
    const { items } = this.props.fields;

    return (
      <div className="banner-area">
        <div className="container">
          <div className="row">
            {items &&
              items.map((data, key) => {
                return (
                  <div className="col-lg-4 col-md-4" key={key}>
                    <div className="single-banner">
                      <Link field={data.fields.uri}>
                        <img src={data.fields.image.value.src} />
                      </Link>
                      <div className="banner-content">
                        <Text tag="h3" field={data.fields.title} />
                        <h4>
                          <Text field={data.fields.subtitle} />
                          &nbsp;
                          <Text tag="span" field={data.fields.price} />
                        </h4>
                        <Link field={data.fields.uri}>
                          <Icon icon="icon-long-arrow" />
                        </Link>
                      </div>
                    </div>
                  </div>
                );
              })}
          </div>
        </div>
      </div>
    );
  }
}
