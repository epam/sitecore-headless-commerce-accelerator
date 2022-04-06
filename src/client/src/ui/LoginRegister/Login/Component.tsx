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

import { LoadingStatus } from 'models';
import { NavigationLink } from 'ui/NavigationLink';
import { validateValue, EMAIL_REGEX } from 'utils';

import { Icon } from 'components';

import { LogInProps, LogInStates, LogInValues } from './models';
import { cnLogin } from './cn';
import './styles.scss';

const FIELD_TYPES = {
  EMAIL: 'email',
  PASSWORD: 'password',
};

export class LogInComponent extends JSS.SafePureComponent<LogInProps, LogInStates> {
  public constructor(props: LogInProps) {
    super(props);

    this.state = {
      isPasswordEmpty: false,
      isUsernameValid: true,
      showPassword: false,
    };
  }
  public componentWillUnmount() {
    this.props.ResetState();
  }
  public validateUser(form: LogInValues) {
    if (validateValue(EMAIL_REGEX, form.email)) {
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

  public handleToggleShowPassword() {
    const { showPassword } = this.state;
    this.setState({ showPassword: !showPassword });
  }

  protected safeRender() {
    const { authenticationProcess, onLoaded } = this.props;
    const { isUsernameValid, isPasswordEmpty, showPassword } = this.state;

    const isLoading = authenticationProcess.status === LoadingStatus.Loading;
    let isError = false;
    if (authenticationProcess.status === LoadingStatus.Failure) {
      isError = true;
      authenticationProcess.status = LoadingStatus.Loaded;
    }

    if (authenticationProcess.status === LoadingStatus.Loaded && onLoaded) {
      onLoaded();
    }

    const handlerFocusField = (field: string) => {
      if (field === FIELD_TYPES.EMAIL) {
        this.setState({ isUsernameValid: true });
      }
      if (field === FIELD_TYPES.PASSWORD) {
        this.setState({ isPasswordEmpty: false });
        this.props.ResetState();
      }
    };

    return (
      <Form className={cnLogin()}>
        <div className={cnLogin('FormGroup')}>
          <div className={cnLogin('FormField')}>
            <Input
              name="email"
              type="text"
              placeholder="Email"
              disabled={isLoading}
              error={!isUsernameValid}
              helperText={!isUsernameValid && 'Enter valid email'}
              handlerFocusField={() => handlerFocusField(FIELD_TYPES.EMAIL)}
            />
          </div>
          <div className={cnLogin('FormField')}>
            <Input
              name="password"
              type={showPassword ? 'text' : 'password'}
              placeholder="Password"
              disabled={isLoading}
              error={isError || isPasswordEmpty}
              helperText={
                isError ? 'The email or password you entered is incorrect' : isPasswordEmpty && 'Enter valid password'
              }
              handlerFocusField={() => handlerFocusField(FIELD_TYPES.PASSWORD)}
              adornment={
                <div onClick={() => this.handleToggleShowPassword()}>
                  <Icon
                    icon={showPassword ? 'icon-look-slash' : 'icon-look'}
                    className={cnLogin('FaEyeIcon')}
                    size="l"
                  />
                </div>
              }
            />
          </div>
        </div>
        <div className={cnLogin('Actions')}>
          <div className={cnLogin('ToggleBtn')}>
            <NavigationLink to="/reset-password">Forgot Password?</NavigationLink>
          </div>
          <Submit
            className={cnLogin('Button')}
            buttonTheme="default"
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
