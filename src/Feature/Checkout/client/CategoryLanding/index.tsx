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
import {
    HeroPanel,
    NavCategory,
    PromoProduct2Up,
    PromoProductFullA,
    PromoProductFullB,
    Slider,
    Video,
} from './components';
import { CategoryLandingControlProps, CategoryLandingControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

class CategoryLandingControl extends Jss.SafePureComponent<CategoryLandingControlProps, CategoryLandingControlState> {
  public safeRender() {
    return (
        <div className="container relative">
            <NavCategory/>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <HeroPanel/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <Slider/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <PromoProductFullA/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <PromoProduct2Up/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <Video/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12 no-padding">
                    <PromoProductFullB/>
                </div>
            </div>
        </div>
    );
  }
}

export const CategoryLanding = withExperienceEditorChromes(CategoryLandingControl);
