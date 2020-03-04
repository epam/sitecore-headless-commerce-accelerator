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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import {
    HeroHome,
    SliderHome,
} from './components';
import { HomeControlProps, HomeControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

class HomeControl extends Jss.SafePureComponent<HomeControlProps, HomeControlState> {
    public safeRender() {
        return (
            <div className="container">
                <HeroHome {...this.props} />
                <SliderHome {...this.props} />
            </div>
        );
    }
}

export const Home = withExperienceEditorChromes(HomeControl);
