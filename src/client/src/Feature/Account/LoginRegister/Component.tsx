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

import * as Jss from 'Foundation/ReactJss';
import { NavigationLink } from 'Foundation/UI';

import { Login } from './Login';
import { LoginRegisterOwnState, LoginRegisterProps } from './models';
import { Register } from './Register';

import './styles.scss';

export class LoginRegisterFormComponent extends Jss.SafePureComponent<LoginRegisterProps, LoginRegisterOwnState> {
  public constructor(props: LoginRegisterProps) {
    super(props);

    this.state = {
      isSignUp: false,
    };
  }

  public componentDidUpdate() {
    const { currentForm } = this.props;
    (currentForm === 'login')
      ? this.setState({ isSignUp: false })
      : this.setState({ isSignUp: true });
  }

  // tslint:disable-next-line:cognitive-complexity
  protected safeRender() {
    const { commerceUser } = this.props;
    const { isSignUp } = this.state;
    return (
      <section className="login-register-form">
        {!!commerceUser && commerceUser.customerId ? (
          <div className="row login-register-form-content">
            <div className="col-md-12">
              <div className="account-created-message">
                <div className="account-created-message_inform">Congratulations!</div>
                <div className="account-created-message_inform">Your Account was successfully created!</div>
                <div className="account-created-message_email">Email: {commerceUser.email}</div>
                <NavigationLink className="btn btn-new-design-active" to="/account">
                  Go to Account Settings
                </NavigationLink>
              </div>
            </div>
          </div>
        ) : (
          <div>
            <div className="login-register-form-header">
              <div className={`form-title ${!isSignUp && 'active'}`} onClick={(e) => this.handleFormChange(e, 'login')}>
                <Jss.Text tag="span" field={{ value: 'Login', editable: 'Login' }} />
              </div>
              <div className={`form-title ${isSignUp && 'active'}`} onClick={(e) => this.handleFormChange(e, 'register')}>
                <Jss.Text tag="span" field={{ value: 'Register', editable: 'Register' }} />
              </div>
            </div>
            <div className="row login-register-form-content">
              <div className="col-lg-8 col-md-12 ml-auto mr-auto field_wrapper">{isSignUp ? <Register /> : <Login />}</div>
            </div>
          </div>
        )}
      </section>
    );
  }

  private handleFormChange(e: React.MouseEvent<HTMLDivElement>, queryParam: string) {
    const { pathname, ChangeRoute } = this.props;
    ChangeRoute(`${pathname}?form=${queryParam}`);
  }
}
