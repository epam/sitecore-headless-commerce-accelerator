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

import * as JSS from 'Foundation/ReactJss/client';
import * as React from 'react';

import { Form, Input, Submit } from 'Foundation/ReactJss/client/Form';
import { NavigationLink } from 'Foundation/UI/client';

import { LoadingStatus } from 'Foundation/Integration/client';

import { SingInProps, SingInValues } from './models';
import './styles.scss';

export class SingInComponent extends JSS.SafePureComponent<SingInProps, {}> {
  public componentWillUnmount() {
    this.props.ResetState();
  }

  protected safeRender() {
    const { authenticationProcess: authProcess, returnUrl } = this.props;

    const isLoading = authProcess.status === LoadingStatus.Loading;
    const isError = authProcess.status === LoadingStatus.Failure;
    return (
      <Form className="form-sign-in">
        <JSS.Text tag="h2" field={{ value: 'Welcome to HCA', editable: 'Welcome to HCA' }} />
        <Input name="email" type="email" placeholder="Email Address" required={true} disabled={isLoading} />
        <Input name="password" type="password" placeholder="Password" required={true} disabled={isLoading} />
        {isError && <h5>The email or password you entered is incorrect</h5>}
        <Submit
          className="btn btn-outline-white"
          onSubmitHandler={(form: SingInValues) => this.props.Authentication(form.email, form.password, returnUrl)}
          disabled={isLoading}
        >
          {isLoading && <i className="fa fa-spinner fa-spin" />}
          <span>Sign in</span>
        </Submit>
        <NavigationLink to="/account/sign-up" className="sign-up">
          <JSS.Text tag="span" field={{ value: 'Create Account', editable: 'Create Account' }} />
        </NavigationLink>
      </Form>
    );
  }
}
