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

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import classnames from 'classnames';

import { breadcrumbs } from './constant';
import { HeaderProps, HeaderState } from './models';

import './styles.scss';

class HeaderComponent extends JSS.SafePureComponent<HeaderProps, HeaderState> {
  private readonly headerRef: React.RefObject<HTMLDivElement>;
  constructor(props: HeaderProps) {
    super(props);

    this.state = {
      headerTop: 0,
      pageName: '',
      scroll: 0,
    };

    this.headerRef = React.createRef();
  }

  public componentDidUpdate(prevProps: Readonly<HeaderProps>, prevState: Readonly<HeaderState>, snapshot?: any) {
    this.setState({ headerTop: this.headerRef.current.offsetTop });
  }

  public componentDidMount() {
    const pageResult = this.getPageName();
    if (pageResult) {
      this.setState({ pageName: pageResult.pageName });
    }
    document.addEventListener('scroll', this.handleScroll.bind(this));

  }

  public componentWillUnmount() {
    document.removeEventListener('scroll', this.handleScroll.bind(this));
  }

  public handleScroll() {
    this.setState({ scroll: window.scrollY });
  }

  public handleClick() {
    const header = document.querySelector('[data-el="header"]');
    header.classList.remove('header--active');

    if (!header.classList.contains('header--inactive')) {
      header.classList.add('header--inactive');
    }
  }
  public getPageName() {
    const currentUrl = document.location.pathname;
    return breadcrumbs.find((element) => currentUrl.includes(element.url));
  }
  protected safeRender() {
    const { pageName } = this.state;
    return (
      <header
        ref={this.headerRef}
        className={classnames('header', 'header--inactive', {
          'header--sticky': this.state.scroll > this.state.headerTop,
        })}
        data-el="header"
      >
        <div className="header-desktop">
          <Placeholder name="header-content" rendering={this.props.rendering} />
        </div>

        <div className={classnames('header_mobile mobile-menu')}>
          <button className="mobile-menu_close" onClick={this.handleClick}>
            <i className="pe-7s-close" />
          </button>
          <div className="mobile-menu_container">
            <div className="mobile-menu_content">
              <Placeholder name="header-content" rendering={this.props.rendering} />
            </div>
          </div>
        </div>
        {pageName && (
          <div className="header_breadcrumbs">
            <span>
              <span>
                <a aria-current="page" className="active" href="/">
                  Home
                </a>
                <span className="slash">/</span>
              </span>
              <span>{pageName}</span>
            </span>
          </div>
        )}
      </header>
    );
  }
}

export const Header2 = JSS.rendering(HeaderComponent);
