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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { HeroHomeControlProps, HeroHomeControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import {
    MainPromoSlider,
    PromoA,
    PromoB,
    PromoC,
    PromoD,
    WidePromo,
} from './components';

import './styles.scss';

class HeroHomeControl extends Jss.SafePureComponent<HeroHomeControlProps, HeroHomeControlState> {
    public safeRender() {
        const { fields, sitecoreContext } = this.props;
        const isEditing = sitecoreContext.pageEditing;
        return (
            <section className="hero-home-wrap">
                <div className="promo-row">
                    <div className="promo-lg-half">
                        <MainPromoSlider image={fields.mainPromoImage} text={fields.mainPromoText} isEditing={isEditing} />
                    </div>
                    <div className="promo-lg-half">
                        <div className="promo-row">
                            <div className="promo-sm-full">
                                <WidePromo image={fields.widePromoImage} text={fields.widePromoText}  isEditing={isEditing} />
                            </div>
                        </div>
                        <div className="promo-row">
                            <div className="promo-sm-half">
                                <PromoA image={fields.promoAImage} text={fields.promoAText} isEditing={isEditing} />
                            </div>
                            <div className="promo-sm-half">
                                <PromoB image={fields.promoBImage} isEditing={isEditing} />
                            </div>
                        </div>
                        <div className="promo-row">
                            <div className="promo-sm-half">
                                <PromoC image={fields.promoCImage} isEditing={isEditing} />
                            </div>
                            <div className="promo-sm-half">
                                <PromoD image={fields.promoDImage} isEditing={isEditing} />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}

export const HeroHome = withExperienceEditorChromes(HeroHomeControl);
