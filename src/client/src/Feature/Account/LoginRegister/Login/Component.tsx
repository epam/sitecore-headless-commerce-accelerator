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

import { LogInProps, LogInValues } from './models';
import './styles.scss';

export class LogInComponent extends JSS.SafePureComponent<LogInProps, {}> {
  public componentWillUnmount() {
    this.props.ResetState();
  }

  protected safeRender() {
    const { Authentication, authenticationProcess, returnUrl, onLoaded } = this.props;

    const isLoading = authenticationProcess.status === LoadingStatus.Loading;
    const isError = authenticationProcess.status === LoadingStatus.Failure;

    if (authenticationProcess.status === LoadingStatus.Loaded && onLoaded) {
      onLoaded();
    }

    return (
      <Form className="login">
        <div className="login_form-group">
          <div className="form-field">
            <Input
              className="login_input"
              name="email"
              type="email"
              placeholder="Email Address"
              required={true}
              disabled={isLoading}
            />
          </div>
          <div className="form-field">
            <Input name="password" type="password" placeholder="Password" required={true} disabled={isLoading} />
          </div>
        </div>

        {isError && <div className="login_invalid-msg">The email or password you entered is incorrect</div>}
        <div className="login_buttons">
          <Submit
            className="btn-log-in"
            onSubmitHandler={(form: LogInValues) => Authentication(form.email, form.password, returnUrl)}
            disabled={isLoading}
          >
            {isLoading && <i className="fa fa-spinner fa-spin" />}
            <span>Login</span>
          </Submit>
        </div>
      </Form>
    );
  }
}
