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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { GlobalFooterProps, GlobalFooterState } from './models';

import './styles.scss';

class GlobalFooterComponent extends JSS.SafePureComponent<
    GlobalFooterProps,
    GlobalFooterState
    > {
    protected safeRender() {
        return (
            <footer id="footer-main" className="footer-main">
                <div className="footer-wrap color-bar">
                    <Placeholder name="footer-content" rendering={this.props.rendering} />
                </div>
            </footer>
        );
    }
}

export const GlobalFooter = JSS.rendering(GlobalFooterComponent);