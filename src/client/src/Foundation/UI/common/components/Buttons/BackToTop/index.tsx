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
import { animateScroll } from 'react-scroll';

import * as Jss from 'Foundation/ReactJss';

import { Icon } from 'components';

import { BackToTopProps, BackToTopState } from './models';

import './styles.scss';

export class BackToTop extends Jss.SafePureComponent<BackToTopProps, BackToTopState> {
  public constructor(props: BackToTopProps) {
    super(props);
    this.state = {
      scrollTop: 0,
    };
  }
  public componentDidMount() {
    document.addEventListener('scroll', this.handleScroll.bind(this));
  }
  public componentWillUnmount() {
    document.removeEventListener('scroll', this.handleScroll.bind(this));
  }
  public handleScroll() {
    this.setState({ scrollTop: document.documentElement.scrollTop });
  }
  public handleClick() {
    animateScroll.scrollToTop();
  }
  public safeRender() {
    const offsetToDisplayButton = 20;
    const { disabled } = this.props;
    const { scrollTop } = this.state;
    return (
      <button
        onClick={this.handleClick}
        className={`scroll-top ${scrollTop > offsetToDisplayButton ? 'show' : ''}`}
        disabled={disabled}
      >
        <Icon icon="icon-angle-double-up" />
      </button>
    );
  }
}
