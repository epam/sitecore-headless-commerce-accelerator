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

import { Form, Input, Submit } from 'Foundation/ReactJss/Form';

import { LoadingStatus } from 'Foundation/Integration';
import { NavigationLink } from 'Foundation/UI';
import { validateEmail } from 'Foundation/utils/validation';

import { Icon } from 'components';

import { LogInProps, LogInStates, LogInValues } from './models';
import './styles.scss';

export class LogInComponent extends JSS.SafePureComponent<LogInProps, LogInStates> {
  public constructor(props: LogInProps) {
    super(props);

    this.state = {
      isPasswordEmpty: false,
      isUsernameValid: true,
    };
  }
  public componentWillUnmount() {
    this.props.ResetState();
  }
  public validateUser(form: LogInValues) {
    if (validateEmail(form.email)) {
      this.setState({ isUsernameValid: true });
      return true;
    } else {
      this.setState({ isUsernameValid: false });
      return false;
    }
  }
  public validatePassword(form: LogInValues) {
    if (form && form.password) {
      this.setState({ isPasswordEmpty: false });
      return true;
    } else {
      this.setState({ isPasswordEmpty: true });
      return false;
    }
  }
  public validateForm(form: LogInValues) {
    const { Authentication } = this.props;
    const passwordSuccess = this.validatePassword(form);
    const userSuccess = this.validateUser(form);
    if (passwordSuccess && userSuccess) {
      Authentication({ email: form.email, password: form.password }, '/');
    }
  }

  protected safeRender() {
    const { authenticationProcess, onLoaded } = this.props;
    const { isUsernameValid, isPasswordEmpty } = this.state;

    const isLoading = authenticationProcess.status === LoadingStatus.Loading;
    let isError = false;
    if (authenticationProcess.status === LoadingStatus.Failure) {
      isError = true;
      authenticationProcess.status = LoadingStatus.Loaded;
    }

    if (authenticationProcess.status === LoadingStatus.Loaded && onLoaded) {
      onLoaded();
    }

    return (
      <Form className="login">
        <div className="login_form-group">
          <div className="form-field">
            <Input
              name="email"
              type="text"
              placeholder="Email"
              disabled={isLoading}
              error={!isUsernameValid}
              helperText={!isUsernameValid && 'Enter valid email'}
            />
          </div>
          <div className="form-field">
            <Input
              name="password"
              type="password"
              placeholder="Password"
              disabled={isLoading}
              error={isError || isPasswordEmpty}
              helperText={
                isError ? 'The email or password you entered is incorrect' : isPasswordEmpty && 'Enter valid password'
              }
            />
          </div>
        </div>
        <div className="login_actions">
          <div className="actions_toggle_btn">
            <NavigationLink to="/reset-password">Forgot Password?</NavigationLink>
          </div>
          <Submit
            className="Login-Button"
            buttonTheme="grey"
            buttonSize="s"
            onSubmitHandler={(form: LogInValues) => {
              isError = false;
              this.validateForm(form);
            }}
            disabled={false}
          >
            {isLoading && <Icon icon="icon-spinner-solid" />}
            <span>Login</span>
          </Submit>
        </div>
      </Form>
    );
  }
}
