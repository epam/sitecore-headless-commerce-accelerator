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

import { SignIn } from './SignIn';
import { SignIn2 } from './SignIn2';
import { SignOut } from './SignOut';

import { UserButtonProps, UserButtonState } from './models';
import './styles.scss';

export class UserButtonComponent extends JSS.SafePureComponent<UserButtonProps, UserButtonState> {
  private wrapperRef: React.MutableRefObject<HTMLDivElement>;

  constructor(props: UserButtonProps) {
    super(props);

    this.state = {
      userFormVisible: false,
    };

    this.wrapperRef = React.createRef<HTMLDivElement>();
  }

  public componentDidMount() {
    document.addEventListener('click', this.handleOutsidePopupClick.bind(this), false);
  }

  protected safeRender() {
    const { commerceUser } = this.props;
    const { userFormVisible } = this.state;
    const isHome2 = window.location.pathname.includes('home2');
    return (
      <div ref={this.wrapperRef} className="user-button" data-autotests="userButton">
        <a className="user-navigation-btn" onClick={this.togglePopup}>
          <i className="fa fa-user" />
          <span>My Account</span>
        </a>
        {userFormVisible && (
          <div className={isHome2 ? 'login-form-2' : 'login-form'}>
            {!!commerceUser && commerceUser.customerId ? (
              <SignOut onLoaded={this.togglePopup} />
            ) : isHome2 ? (
              <SignIn2 onLoaded={this.togglePopup} />
            ) : (
              <SignIn onLoaded={this.togglePopup} />
            )}
          </div>
        )}
      </div>
    );
  }

  private handleOutsidePopupClick(e: MouseEvent) {
    if (this.wrapperRef.current && !this.wrapperRef.current.contains(e.target as Node) && this.state.userFormVisible) {
      this.togglePopup();
    }
  }

  private togglePopup = () => this.setState({ userFormVisible: !this.state.userFormVisible });
}
