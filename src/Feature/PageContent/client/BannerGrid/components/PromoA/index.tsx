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

import { Image, Text } from '@sitecore-jss/sitecore-jss-react';
import { PromoAControlProps, PromoAControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

export class PromoA extends Jss.SafePureComponent<PromoAControlProps, PromoAControlState> {
    public safeRender() {
        const { image, text } = this.props;
        return (
            <figure className="promo-a">
                <a href="#">
                    <Image media={image} />
                    <figcaption>
                        <h2 className="main-a-text">
                            <span className="text-style1" />
                        </h2>
                        <Text
                            field={text}
                            tag="button"
                            className="btn-promo-a"
                        />
                    </figcaption>
                </a>
            </figure>
        );
    }
}