//    Copyright 2019 EPAM Systems, Inc.
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

import { Image, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { PromoBControlProps, PromoBControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

class PromoBControl extends Jss.SafePureComponent<PromoBControlProps, PromoBControlState> {
    public safeRender() {
        const { image, isEditing } = this.props;
        return (
            <figure className={isEditing ? '' : 'promo-b'}>
                <a href="#">
                    {isEditing
                        ? <Image media={image} />
                        : <img src={image.value.src} alt={image.value.alt} />}
                    <figcaption>
                        <h2 className="promo-b-text">
                            <span className="text-style1" />
                        </h2>
                    </figcaption>
                </a>
            </figure>
        );
    }
}

export const PromoB = withExperienceEditorChromes(PromoBControl);
