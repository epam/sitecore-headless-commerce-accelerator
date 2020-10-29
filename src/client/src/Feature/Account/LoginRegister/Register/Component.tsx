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

      isConfirmPasswordEmpty: false,
      isEmailValid: true,
      isFirstNameEmpty: false,
      isLastNameEmpty: false,
      isPasswordEmpty: false,
      isPasswordsValid: true,
    };
  }
  public componentWillUnmount() {
    this.props.ResetValidation();
  }

  // tslint:disable-next-line:cognitive-complexity
  protected safeRender() {
    const { loading, accountValidation } = this.props;
    const { isConfirmPasswordEmpty, isEmailValid, isFirstNameEmpty, isLastNameEmpty, isPasswordEmpty, isPasswordsValid } = this.state;
    return (
      <div className="register_form">
        <Form>
          <div className="form-field">
            <Input type="text" name={FORM_FIELDS.FIRST_NAME} placeholder="First Name" />
            {isFirstNameEmpty && (
              <div className="form-field-error-message">First Name field is required!</div>
            )}
          </div>
          <div className="form-field">
            <Input type="text" name={FORM_FIELDS.LAST_NAME} placeholder="Last Name" />
            {isLastNameEmpty && (
              <div className="form-field-error-message">Last Name field is required!</div>
            )}
          </div>
          <div className="form-field">
            <Input
              type="email"
              name={FORM_FIELDS.EMAIL}
              placeholder="Email"
            />
            {(!isEmailValid || accountValidation.inUse || accountValidation.invalid) && (
              <div className="form-field-error-message">
                {!isEmailValid
                  ? 'Email field is required!'
                  : accountValidation.inUse
                    ? 'Email is already in use!'
                    : 'Email is invalid!'
                }
              </div>
            )}
            {/* {accountValidation.status === LoadingStatus.Loaded
              && ((isEmailValid || accountValidation.inUse || accountValidation.invalid) && (
              <div className="form-field-error-message">
                {isEmailValid
                  ? 'Email field is required!'
                  : accountValidation.inUse
                    ? 'Email is already in use!'
                    : 'Email is invalid!'
                }
              </div>
            ))} */}
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.PASSWORD}
              placeholder="Password"
            />
            {isPasswordEmpty && (
              <div className="form-field-error-message">Password field is required!</div>
            )}
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.CONFIRM_PASSWORD}
              placeholder="Confirm Password"
            />
            {(isConfirmPasswordEmpty || !isPasswordsValid) && (
              <div className="form-field-error-message">
                {
                  isConfirmPasswordEmpty
                  ? 'Confirm Password field is required!'
                  : 'Passwords do not match!'
                }
              </div>
            )}
          </div>
          <Submit
            disabled={false}
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
    const { accountValidation } = this.props;

    const isFirstNameValid = this.validateFirstName(formValues);
    const isLastNameValid = this.validateLastName(formValues);
    const isEmailValid = this.validateEmail(formValues);
    const isPasswordValid = this.validatePassword(formValues);
    const isConfirmPasswordValid = this.validateConfirmPassword(formValues);
    let isPasswordsMatch = false;
    if (isPasswordValid && isConfirmPasswordValid) {
      isPasswordsMatch = this.passwordValidator(formValues);
    }

    if (isFirstNameValid
      && isLastNameValid
      && isEmailValid
      && isPasswordsMatch
      && (accountValidation.status === LoadingStatus.Loaded
        && accountValidation.inUse === false
        && accountValidation.invalid === false)
      ) {
        const createAccountDto = {
          email: formValues[FORM_FIELDS.EMAIL] as string,
          firstName: formValues[FORM_FIELDS.FIRST_NAME] as string,
          lastName: formValues[FORM_FIELDS.LAST_NAME] as string,
          password: formValues[FORM_FIELDS.PASSWORD] as string,
        };
        CreateAccount(createAccountDto, returnUrl);
    }
  }

  private passwordValidator(formValues: FormValues) {
    if (formValues[FORM_FIELDS.PASSWORD] === formValues[FORM_FIELDS.CONFIRM_PASSWORD]) {
      this.setState({ isPasswordsValid: true });
      return true;
    } else {
      this.setState({ isPasswordsValid: false });
      return false;
    }
  }
  private validateFirstName(form: FormValues) {
    const fName = form[FORM_FIELDS.FIRST_NAME] as string;
    if (form && fName) {
      this.setState({ isFirstNameEmpty: false });
      return true;
    } else {
      this.setState({ isFirstNameEmpty: true });
      return false;
    }
  }

  private validateLastName(form: FormValues) {
    const lName = form[FORM_FIELDS.LAST_NAME] as string;
    if (form && lName) {
      this.setState({ isLastNameEmpty: false });
      return true;
    } else {
      this.setState({ isLastNameEmpty: true });
      return false;
    }
  }

  private validateEmail(form: FormValues) {
    const { AccountValidation } = this.props;
    const email = form[FORM_FIELDS.EMAIL] as string;

    if (email && (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email))) {
      AccountValidation(email);
      this.setState({ isEmailValid: true });
      return true;
    } else {
      this.setState({ isEmailValid: false });
      return false;
    }
  }

  private validatePassword(form: FormValues) {
    const password = form[FORM_FIELDS.PASSWORD] as string;
    if (form && password) {
      this.setState({ isPasswordEmpty: false });
      return true;
    } else {
      this.setState({ isPasswordEmpty: true });
      return false;
    }
  }

  private validateConfirmPassword(form: FormValues) {
    const confirmPassword = form[FORM_FIELDS.CONFIRM_PASSWORD] as string;
    if (form && confirmPassword) {
      this.setState({ isConfirmPasswordEmpty: false });
      return true;
    } else {
      this.setState({ isConfirmPasswordEmpty: true });
      return false;
    }
  }
}
