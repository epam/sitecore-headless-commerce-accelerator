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
import { NavigationLink } from 'Foundation/UI';

import { LoadingStatus } from 'Foundation/Integration';

import { SignInProps, SignInValues } from './models';
import './styles.scss';

export class SignInComponent extends JSS.SafePureComponent<SignInProps, {}> {
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
        <Form className="form-sign-in" data-autotests="signInForm">
        <JSS.Text tag="h2" field={{ value: 'Welcome to HCA', editable: 'Welcome to HCA' }} />
            <Input name="email" type="email" placeholder="Email Address" required={true} disabled={isLoading} data-autotests="emailField" />
            <Input name="password" type="password" placeholder="Password" required={true} disabled={isLoading} data-autotests="passwordField" />
        {isError && <h5>The email or password you entered is incorrect</h5>}
        <Submit
          className="btn btn-outline-white"
          onSubmitHandler={(form: SignInValues) => Authentication(form.email, form.password, returnUrl)}
                disabled={isLoading}
                data-autotests="signInButton"
        >
          {isLoading && <i className="fa fa-spinner fa-spin" />}
          <span>Sign in</span>
            </Submit>
            <div data-autotests="signUpButton" >
            <NavigationLink to="/account/sign-up" className="sign-up">
          <JSS.Text tag="span" field={{ value: 'Create Account', editable: 'Create Account' }} />
                </NavigationLink>
                </div>
      </Form>
    );
  }
}
