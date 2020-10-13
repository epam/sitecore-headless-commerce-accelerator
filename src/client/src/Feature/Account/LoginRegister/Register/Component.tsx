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

import { LoadingStatus } from 'Foundation/Integration';
import * as Jss from 'Foundation/ReactJss';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/Form';

import { FORM_FIELDS } from './constants';
import { SignUpOwnState, SignUpProps } from './models';

import './styles.scss';

export class RegisterComponent extends Jss.SafePureComponent<SignUpProps, SignUpOwnState> {
  public constructor(props: SignUpProps) {
    super(props);

    this.state = {
      isSignUp: false,
    };
  }
  public componentWillUnmount() {
    this.props.ResetValidation();
  }

  // tslint:disable-next-line:cognitive-complexity
  protected safeRender() {
    const { loading, accountValidation, AccountValidation } = this.props;
    const submitButtonDisabled =
      loading || accountValidation.invalid || accountValidation.status === LoadingStatus.Loading;
    return (
      <div className="register_form">
        <Form>
          <div className="form-field">
            <Input type="text" name={FORM_FIELDS.FIRST_NAME} required={true} placeholder="First Name" />
          </div>
          <div className="form-field">
            <Input type="text" name={FORM_FIELDS.LAST_NAME} required={true} placeholder="Last Name" />
          </div>
          <div className="form-field">
            <Input
              type="email"
              name={FORM_FIELDS.EMAIL}
              required={true}
              onBlur={(e) => {
                if (e.target.validity.valid) {
                  AccountValidation(e.target.value);
                }
              }}
              placeholder="Email"
            />
            {accountValidation.status === LoadingStatus.Loading && (
              <span className="form-field-async-validation">
                <i className="fa fa-spinner fa-spin" />
              </span>
            )}
            {accountValidation.status === LoadingStatus.Loaded && (
              <span className="form-field-error-message">
                {accountValidation.inUse && 'Email is already in use!'}
                {accountValidation.invalid && 'Email is invalid!'}
              </span>
            )}
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.PASSWORD}
              required={true}
              customValidator={(formValues) => this.passwordCustomValidator(formValues)}
              placeholder="Password"
            />
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.CONFIRM_PASSWORD}
              required={true}
              customValidator={(formValues) => this.passwordCustomValidator(formValues)}
              placeholder="Confirm Password"
            />
          </div>
          <Submit
            disabled={submitButtonDisabled}
            className="btn-login-register"
            onSubmitHandler={(formValues) => this.handleFormSubmit(formValues)}
          >
            {loading && <i className="fa fa-spinner fa-spin" />}
            <span>Register</span>
          </Submit>
        </Form>
      </div>
    );
  }

  private handleFormSubmit(formValues: FormValues) {
    const { CreateAccount, returnUrl } = this.props;

    const createAccountDto = {
      email: formValues[FORM_FIELDS.EMAIL] as string,
      firstName: formValues[FORM_FIELDS.FIRST_NAME] as string,
      lastName: formValues[FORM_FIELDS.LAST_NAME] as string,
      password: formValues[FORM_FIELDS.PASSWORD] as string,
    };

    CreateAccount(createAccountDto, returnUrl);
  }

  private passwordCustomValidator(formValues: FormValues) {
    return formValues[FORM_FIELDS.PASSWORD] === formValues[FORM_FIELDS.CONFIRM_PASSWORD];
  }
}
