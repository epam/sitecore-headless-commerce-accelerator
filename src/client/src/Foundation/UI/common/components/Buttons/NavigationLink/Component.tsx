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

import React, { MouseEvent } from 'react';
import { Link } from 'react-router-dom';

import * as Jss from 'Foundation/ReactJss';

import { NavigationLinkProps } from './models';

export default class NavigationLinkComponent extends Jss.SafePureComponent<NavigationLinkProps, {}> {
  public safeRender() {
    const { to, className, children } = this.props;
    const autotests = this.props['data-autotests'];

    return (
      <Link to={to} onClick={(e) => this.handleLinkOnClick(e)} className={className} data-autotests={autotests}>
        {children}
      </Link>
    );
  }

  private handleLinkOnClick(e: MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();

    const { to, ChangeRoute } = this.props;
    ChangeRoute(to);

    if (this.props.onClick) {
      this.props.onClick(e);
    }
  }
}
