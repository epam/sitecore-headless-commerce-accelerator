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
import { validateEmail } from 'Foundation/utils/validation';

import { FORM_FIELDS } from './constants';
import { SignUpOwnState, SignUpProps } from './models';

import './styles.scss';

export class RegisterComponent extends Jss.SafePureComponent<SignUpProps, SignUpOwnState> {
  public formValues?: FormValues;

  public constructor(props: SignUpProps) {
    super(props);

    this.state = {
      isSignUp: false,

      isConfirmPasswordEmpty: false,
      isEmailEmpty: false,
      isEmailValid: true,
      isFirstNameEmpty: false,
      isLastNameEmpty: false,
      isPasswordEmpty: false,
      isPasswordsValid: true,
    };
  }

  public componentDidUpdate(prevProps: SignUpProps) {
    const {
      accountValidation: { inUse, invalid, status },
      returnUrl,
      CreateAccount,
    } = this.props;
    const {
      accountValidation: { status: prevStatus },
    } = prevProps;

    if (status === LoadingStatus.Loaded && prevStatus === LoadingStatus.Loading && !inUse && !invalid) {
      const isValid = this.validate(this.formValues);

      if (isValid) {
        const createAccountDto = {
          email: this.formValues[FORM_FIELDS.EMAIL] as string,
          firstName: this.formValues[FORM_FIELDS.FIRST_NAME] as string,
          lastName: this.formValues[FORM_FIELDS.LAST_NAME] as string,
          password: this.formValues[FORM_FIELDS.PASSWORD] as string,
        };

        CreateAccount(createAccountDto, returnUrl);
      }
    }
  }

  public componentWillUnmount() {
    this.props.ResetValidation();
  }
  // tslint:disable-next-line:cognitive-complexity
  protected safeRender() {
    const { loading, accountValidation, createAccount } = this.props;
    const {
      isConfirmPasswordEmpty,
      isEmailEmpty,
      isEmailValid,
      isFirstNameEmpty,
      isLastNameEmpty,
      isPasswordEmpty,
      isPasswordsValid,
    } = this.state;
    const formDisabled =
      createAccount.status === LoadingStatus.Loading || accountValidation.status === LoadingStatus.Loading;

    return (
      <div className="register_form">
        <Form>
          <div className="form-field">
            <Input
              type="text"
              name={FORM_FIELDS.FIRST_NAME}
              placeholder="First Name"
              disabled={formDisabled}
              fullWidth={true}
            />
            {isFirstNameEmpty && <div className="form-field-error-message">First Name field is required!</div>}
          </div>
          <div className="form-field">
            <Input
              type="text"
              name={FORM_FIELDS.LAST_NAME}
              placeholder="Last Name"
              disabled={formDisabled}
              fullWidth={true}
            />
            {isLastNameEmpty && <div className="form-field-error-message">Last Name field is required!</div>}
          </div>
          <div className="form-field">
            <Input type="text" name={FORM_FIELDS.EMAIL} placeholder="Email" disabled={formDisabled} fullWidth={true} />
            {(!isEmailValid || accountValidation.inUse || accountValidation.invalid || isEmailEmpty) && (
              <div className="form-field-error-message">{this.checkEmailValidation()}</div>
            )}
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.PASSWORD}
              placeholder="Password"
              disabled={formDisabled}
              fullWidth={true}
            />
            {isPasswordEmpty && <div className="form-field-error-message">Password field is required!</div>}
          </div>
          <div className="form-field">
            <Input
              type="password"
              name={FORM_FIELDS.CONFIRM_PASSWORD}
              placeholder="Confirm Password"
              disabled={formDisabled}
              fullWidth={true}
            />
            {(isConfirmPasswordEmpty || !isPasswordsValid) && (
              <div className="form-field-error-message">
                {isConfirmPasswordEmpty ? 'Confirm Password field is required!' : 'Passwords do not match!'}
              </div>
            )}
          </div>
          <Submit
            disabled={formDisabled}
            className="btn-login-register"
            onSubmitHandler={(formValues) => this.handleFormSubmit(formValues)}
          >
            {(loading || formDisabled) && <i className="fa fa-spinner fa-spin" />}
            <span>Register</span>
          </Submit>
        </Form>
      </div>
    );
  }

  private checkEmailValidation() {
    const { accountValidation } = this.props;
    const { isEmailEmpty, isEmailValid } = this.state;

    if (
      accountValidation.inUse &&
      accountValidation.status !== LoadingStatus.Loading &&
      !isEmailEmpty &&
      isEmailValid
    ) {
      return 'Email is already in use!';
    }

    if (isEmailEmpty) {
      return 'Email field is required!';
    }

    if (!isEmailValid) {
      return 'Email is invalid!';
    }

    return '';
  }

  private validate(formValues: FormValues) {
    const isFirstNameValid = this.validateFirstName(formValues);
    const isLastNameValid = this.validateLastName(formValues);
    const isPasswordValid = this.validatePassword(formValues);
    const isConfirmPasswordValid = this.validateConfirmPassword(formValues);
    let isPasswordsMatch = false;

    if (isPasswordValid && isConfirmPasswordValid) {
      isPasswordsMatch = this.passwordValidator(formValues);
    }

    if (isFirstNameValid && isLastNameValid && isPasswordsMatch) {
      return true;
    }

    return false;
  }

  private handleFormSubmit(formValues: FormValues) {
    const { AccountValidation } = this.props;
    this.validate(formValues);
    const isEmailValid = this.validateEmail(formValues);

    if (isEmailValid) {
      const email = formValues[FORM_FIELDS.EMAIL] as string;
      this.formValues = formValues;
      AccountValidation(email);
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
    const email = form[FORM_FIELDS.EMAIL] as string;

    if (email === '') {
      this.setState({ isEmailEmpty: true, isEmailValid: false });
      return false;
    }

    const isValid = validateEmail(email);

    if (isValid) {
      this.setState({ isEmailEmpty: false, isEmailValid: true });
      return true;
    }

    this.setState({ isEmailEmpty: false, isEmailValid: false });

    return false;
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
