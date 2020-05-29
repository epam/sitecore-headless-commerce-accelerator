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
import * as React from 'react';

import { LoadingStatus } from 'Foundation/Integration/client';
import * as Jss from 'Foundation/ReactJss/client';
import { NavigationLink } from 'Foundation/UI/client';

import { NavUserProps, NavUserState } from './models';
import './styles.scss';

export default class NavUser extends Jss.SafePureComponent<NavUserProps, NavUserState> {
  private signInformRef: React.RefObject<HTMLFormElement>;
  private emailRef: React.RefObject<HTMLInputElement>;
  private passwordRef: React.RefObject<HTMLInputElement>;
  private popupWrapperRef: React.RefObject<HTMLLIElement>;

  constructor(props: NavUserProps) {
    super(props);

    this.state = {
      email: '',
      formValid: true,
      userFormVisible: false,
    };

    this.signInformRef = React.createRef<HTMLFormElement>();
    this.emailRef = React.createRef<HTMLInputElement>();
    this.passwordRef = React.createRef<HTMLInputElement>();
    this.popupWrapperRef = React.createRef<HTMLLIElement>();
    this.togglePopup = this.togglePopup.bind(this);
    this.handleOutsidePopupClick = this.handleOutsidePopupClick.bind(this);
  }

  public componentDidUpdate() {
    const { authProcess } = this.props;
    if (authProcess.status === LoadingStatus.Loaded && authProcess.hasValidCredentials) {
      this.signInformRef.current.submit();
    }
  }

  protected safeRender() {
    const { cartQuantity, commerceUser } = this.props;
    const { userFormVisible } = this.state;
    return (
      <nav id="nav-user">
        <ul>
          <li>
            <NavigationLink to="/wishlist">
              <i className="fa fa-list-ul" />
              <span>Wishlist</span>
            </NavigationLink>
          </li>
          <li>
            <NavigationLink to="/cart">
              <i className="fa fa-shopping-cart">
                {cartQuantity > 0 && <span className="quantity">{cartQuantity}</span>}
              </i>
              <span>Shopping Cart</span>
            </NavigationLink>
          </li>
          <li ref={this.popupWrapperRef}>
            <a onClick={this.togglePopup}>
              <i className="fa fa-user" />
              <span>My Account</span>
            </a>
            {userFormVisible && (
              <div className="login-form">
                {!!commerceUser && commerceUser.customerId ? this.renderUserInfoForm() : this.renderSignInFrom()}
              </div>
            )}
          </li>
        </ul>
      </nav>
    );
  }

  private renderUserInfoForm() {
    return (
      <form method="POST" action="/apix/client/commerce/auth/logout" className="sign-out-form">
        <NavigationLink to="/account" className="btn btn-main">
          <Jss.Text tag="span" field={{ value: 'My Account', editable: 'My Account' }} />
        </NavigationLink>
        <NavigationLink to="/account/order-history" className="btn btn-main no-margin-bottom">
          <Jss.Text tag="span" field={{ value: 'Order History', editable: 'Order History' }} />
        </NavigationLink>
        <hr />
        <button type="submit" className="btn btn-focus">
          Sign Out
        </button>
      </form>
    );
  }

  private renderSignInFrom() {
    const { authProcess, returnUrl } = this.props;
    const { email, formValid } = this.state;

    const isLoading = authProcess.status === LoadingStatus.Loading;
    const isError = authProcess.status === LoadingStatus.Failure;
    return (
      <form
        ref={this.signInformRef}
        className={classnames('form-member-sign-in', { 'invalid-form': !formValid })}
        method="POST"
        action={`/apix/client/commerce/auth/login?returnUrl=${returnUrl}`}
      >
        <Jss.Text tag="h2" field={{ value: 'Welcome to HCA', editable: 'Welcome to HCA' }} />
        <input
          ref={this.emailRef}
          type="email"
          name="email"
          placeholder="Email Address"
          required={true}
          onChange={(e) => this.setState({ email: e.target.value })}
          disabled={isLoading}
          value={email}
        />
        <input
          ref={this.passwordRef}
          type="password"
          name="password"
          placeholder="Password"
          required={true}
          disabled={isLoading}
        />
        {isError && <h5>The email or password you entered is incorrect</h5>}
        {!formValid && <h5>Please fill the form</h5>}
        <button className="btn btn-outline-white" onClick={(e) => this.handleSignInButtonClick(e)} disabled={isLoading}>
          {isLoading && <i className="fa fa-spinner fa-spin" />} Sign in
        </button>
        <NavigationLink to="/account/sign-up" className="sign-up">
          <Jss.Text tag="span" field={{ value: 'Create Account', editable: 'Create Account' }} />
        </NavigationLink>
      </form>
    );
  }

  private handleOutsidePopupClick(e: any) {
    if (!this.popupWrapperRef.current || this.popupWrapperRef.current.contains(e.target)) {
      return;
    }

    this.togglePopup(e);
  }

  private togglePopup(e: React.MouseEvent<HTMLElement>) {
    e.preventDefault();

    if (!this.state.userFormVisible) {
      document.addEventListener('click', this.handleOutsidePopupClick, false);
    } else {
      document.removeEventListener('click', this.handleOutsidePopupClick, false);
    }

    this.setState({ userFormVisible: !this.state.userFormVisible });
  }

  private handleSignInButtonClick(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();

    const { StartAuth } = this.props;

    if (!this.emailRef.current.validity.valid || !this.passwordRef.current.validity.valid) {
      this.setState({
        formValid: false,
      });
      return;
    }

    if (!this.state.formValid) {
      this.setState({
        formValid: true,
      });
    }

    const { email } = this.state;

    StartAuth(email, this.passwordRef.current.value);
  }
}
