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

import classnames from 'classnames';
import { LoadingStatus } from 'Foundation/Integration';
import * as JSS from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';
import * as React from 'react';
import { UserButtonProps, UserButtonState } from './models';
import './styles.scss';

export class UserButtonComponent extends JSS.SafePureComponent<UserButtonProps, UserButtonState> {
  private wrapperRef: React.MutableRefObject<HTMLDivElement>;

  constructor(props: UserButtonProps) {
    super(props);

    this.state = {
      isDropdownVisible: false,
    };

    this.wrapperRef = React.createRef<HTMLDivElement>();
  }

  public componentDidMount() {
    document.addEventListener('click', this.handleOutsidePopupClick.bind(this), false);
  }

  public componentDidUnMount() {
    document.removeEventListener('click', this.handleOutsidePopupClick.bind(this), false);
  }

  public componentDidUpdate() {
    if (this.props.logoutProcess.status === LoadingStatus.Loaded) {
      location.reload();
    }
  }

  protected safeRender() {
    const { isDropdownVisible } = this.state;
    const { commerceUser, returnUrl, Logout } = this.props;

    return (
      <div ref={this.wrapperRef} className="navigation-buttons_item account">
        <a onClick={this.toggleDropdown}>
          <i className="pe-7s-user-female" />
        </a>
        <ul className={classnames('account_dropdown', { 'account_dropdown--visible': isDropdownVisible })}>
          {commerceUser && commerceUser.customerId ? (
            <>
              <li className="account_dropdown-item">
                <NavigationLink className="account_link" to="/account">
                  My account
                </NavigationLink>
              </li>
              <li className="account_dropdown-item">
                <NavigationLink className="account_link" to="/account/order-history">
                  Order History
                </NavigationLink>
              </li>
              <li className="account_dropdown-item">
                <a href="#" className="account_link" onClick={() => Logout(returnUrl)}>
                  Sign Out
                </a>
              </li>
            </>
          ) : (
            <>
              <li className="account_dropdown-item">
                <NavigationLink className="account_link" to="/account/login-register?form=login">
                  Login
                </NavigationLink>
              </li>
              <li className="account_dropdown-item">
                <NavigationLink className="account_link" to="/account/login-register?form=register">
                  Register
                </NavigationLink>
              </li>
            </>
          )}
        </ul>
      </div>
    );
  }

  private handleOutsidePopupClick(e: MouseEvent) {
    if (
      this.wrapperRef.current &&
      !this.wrapperRef.current.contains(e.target as Node) &&
      this.state.isDropdownVisible
    ) {
      this.toggleDropdown();
    }
  }

  private toggleDropdown = () => this.setState({ isDropdownVisible: !this.state.isDropdownVisible });
}
