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

import * as JSS from 'Foundation/ReactJss/client';

import { BannerGridProps, BannerGridState } from './models';

import './styles.scss';

class BannerGridComponent extends JSS.SafePureComponent<BannerGridProps, BannerGridState> {
    public safeRender() {
        return (
            <section className="hero-home-wrap">
                <div className="promo-row">
                    <div className="promo-lg-half">
                        <Placeholder name="banner-item-main" rendering={this.props.rendering} />
                    </div>
                    <div className="promo-lg-half">
                        <div className="promo-row">
                            <div className="promo-sm-full">
                                <Placeholder name="banner-item-wide" rendering={this.props.rendering} />
                            </div>
                        </div>
                        <div className="promo-row">
                            <div className="promo-sm-half">
                                <Placeholder name="banner-item-a" rendering={this.props.rendering} />
                            </div>
                            <div className="promo-sm-half">
                                <Placeholder name="banner-item-b" rendering={this.props.rendering} />
                            </div>
                        </div>
                        <div className="promo-row">
                            <div className="promo-sm-half">
                                <Placeholder name="banner-item-c" rendering={this.props.rendering} />
                            </div>
                            <div className="promo-sm-half">
                                <Placeholder name="banner-item-d" rendering={this.props.rendering} />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}

export const BannerGrid = JSS.rendering(BannerGridComponent);
