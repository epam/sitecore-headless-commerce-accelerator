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
import { PromoDControlProps, PromoDControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

export class PromoD extends Jss.SafePureComponent<PromoDControlProps, PromoDControlState> {
    public safeRender() {
        const { image } = this.props;
        return (
            <figure className="promo-d">
                <a href="#">
                    <Image media={image} />
                    <figcaption>
                        <h2 className="promo-d-text">
                            <Text
                                field={{ value: 'Track' }}
                                tag="span"
                                className="text-style1"
                            />
                            <Text
                                field={{ value: 'The' }}
                                tag="span"
                                className="text-style2"
                            />
                            <Text
                                field={{ value: 'Game' }}
                                tag="span"
                                className="text-style3"
                            />
                            <Text
                                field={{ value: 'Get Fitted' }}
                                tag="span"
                                className="text-style4"
                            />
                        </h2>
                    </figcaption>
                </a>
            </figure>
        );
    }
}