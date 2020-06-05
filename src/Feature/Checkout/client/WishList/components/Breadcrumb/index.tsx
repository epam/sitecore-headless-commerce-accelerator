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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { BreadcrumbProps, BreadcrumbState } from './models';

import './styles.scss';

export class Breadcrumb extends JSS.SafePureComponent<BreadcrumbProps, BreadcrumbState> {
  public safeRender() {
    return (
      <>
        <nav className="breadcrumbs">
            <ul>
                <li>
                    <Text tag="a" href="/" field={{ value: 'Root' }} />
                </li>
                <li>
                    <Text tag="a" href="/Category" field={{ value: 'Category' }} />
                </li>
                <li>
                    <Text tag="a" href="/Category/Subcategory" field={{ value: 'Subcategory' }} />
                </li>
                <li>
                    <Text tag="span" field={{ value: 'Product' }} />
                </li>
            </ul>
        </nav>
      </>
    );
  }
}
