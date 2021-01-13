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

import './styles.scss';

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';

import { BreadcrumbProps, BreadcrumbState, PageLink } from './models';

export default class BreadcrumbComponent extends JSS.SafePureComponent<BreadcrumbProps, BreadcrumbState> {
  protected safeRender() {
    const { breadcrumb } = this.props.sitecoreContext;
    const pageLinks = breadcrumb && breadcrumb.pageLinks;

    return (
      <div className="header_breadcrumbs">
        <span>
          {pageLinks &&
            pageLinks.map((pageLink: PageLink, index: number, array: PageLink[]) => (
              <span key={pageLink.title}>
                <a aria-current="page" className="active" href={pageLink.link}>
                  {pageLink.title}
                </a>
                {index < array.length - 1 && <span className="slash">/</span>}
              </span>
            ))}
        </span>
      </div>
    );
  }
}
