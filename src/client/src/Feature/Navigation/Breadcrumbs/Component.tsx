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

import * as JSS from 'Foundation/ReactJss';

import { BreadcrumbsProps, BreadcrumbsState } from './models';

import './styles.scss';

const homeName = 'Home';
export default class BreadcrumbsComponent extends JSS.SafePureComponent<BreadcrumbsProps, BreadcrumbsState> {
  public constructor(props: BreadcrumbsProps) {
    super(props);
    this.state = {
      showBreadcrumbs: true,
    };
  }

  public componentDidMount() {
    if (((this.props.currentPageName as unknown) as string) === homeName) {
      this.setState({
        showBreadcrumbs: false,
      });
    }
  }

  protected safeRender() {
    const { currentPageName } = this.props;
    return (
      <div className="header_breadcrumbs">
        <span>
          <span>
            <a aria-current="page" className="active" href="/">
              {homeName}
            </a>
            <span className="slash">/</span>
          </span>
          <span>{currentPageName}</span>
        </span>
      </div>
    );
  }
}
