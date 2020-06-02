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

import * as JSS from 'Foundation/ReactJss/client';
import * as React from 'react';

import classnames from 'classnames';

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import { HeaderProps, HeaderState } from './models';
import './styles.scss';

class HeaderComponent extends JSS.SafePureComponent<HeaderProps, HeaderState> {
  constructor(props: HeaderProps) {
    super(props);

    this.state = {
      isClosed: true,
    };
  }

  protected safeRender() {
    return (
      <>
        <header className={classnames('main-header', { 'toggle-closed': this.state.isClosed })}>
          <div className="flex-container">
            <Placeholder name="header-content" rendering={this.props.rendering} />
          </div>
        </header>
        <header className="mobile-header">
          <a panel-toggle="panel-toggle" className="toggle" onClick={(e) => this.toggleMenu()}>
            <i className="fa fa-bars" />
          </a>
          <Placeholder name="navigation-content" rendering={this.props.rendering} />
        </header>
        <div
          className={classnames('overlay', { 'toggle-closed': this.state.isClosed })}
          onClick={(e) => this.toggleMenu()}
        />
      </>
    );
  }

  protected toggleMenu() {
    this.setState({
      isClosed: !this.state.isClosed,
    });
  }
}

export const Header = JSS.rendering(HeaderComponent);
