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

import { LoadingStatus } from 'Foundation/Integration/client';
import * as Jss from 'Foundation/ReactJss/client';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/client/Form';
import { NavigationLink } from 'Foundation/UI/client';

import { FORM_FIELDS } from './constants';
import { SignUpOwnState, SignUpProps } from './models';

import './styles.scss';

export default class SignUpFormComponent extends Jss.SafePureComponent<SignUpProps, SignUpOwnState> {
  public componentWillUnmount() {
    this.props.ResetValidation();
  }

  // tslint:disable-next-line:cognitive-complexity
  protected safeRender() {
    const { loading, accountValidation, AccountValidation, commerceUser } = this.props;

    const submitButtonDisabled =
      loading || accountValidation.invalid || accountValidation.status === LoadingStatus.Loading;
    return (
      <section className="sign-up-form">
        <div className="sign-up-form-header">
          <Jss.Text tag="h1" field={{ value: 'Sign up', editable: 'Sign up' }} />
        </div>
        {!!commerceUser && commerceUser.customerId ? (
          <div className="row sign-up-form-content">
            <div className="col-md-12">
              <div className="account-created-message">
                <h2>Congratulations!</h2>
                <h2>Your Account was successfully created!</h2>
                <h4>Email: {commerceUser.email}</h4>
                <NavigationLink className="btn btn-outline-main" to="/account">
                  Go to Account Settings
                </NavigationLink>
              </div>
            </div>
          </div>
        ) : (
          <div className="row sign-up-form-content">
            <div className="col-md-4 col-xs-12">
              <Form>
                <div className="form-field">
                  <Jss.Text tag="label" field={{ value: 'First Name', editable: 'First Name' }} />
                  <Input type="text" name={FORM_FIELDS.FIRST_NAME} required={true} />
                </div>
                <div className="form-field">
                  <Jss.Text tag="label" field={{ value: 'Last Name', editable: 'Last Name' }} />
                  <Input type="text" name={FORM_FIELDS.LAST_NAME} required={true} />
                </div>
                <div className="form-field">
                  <Jss.Text tag="label" field={{ value: 'Email', editable: 'Email' }} />
                  <Input
                    type="email"
                    name={FORM_FIELDS.EMAIL}
                    required={true}
                    onBlur={(e) => {
                      if (e.target.validity.valid) {
                        AccountValidation(e.target.value);
                      }
                    }}
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
                  <Jss.Text tag="label" field={{ value: 'Password', editable: 'Password' }} />
                  <Input
                    type="password"
                    name={FORM_FIELDS.PASSWORD}
                    required={true}
                    customValidator={(formValues) => this.passwordCustomValidator(formValues)}
                  />
                </div>
                <div className="form-field">
                  <Jss.Text tag="label" field={{ value: 'Confirm Password', editable: 'Confirm Password' }} />
                  <Input
                    type="password"
                    name={FORM_FIELDS.CONFIRM_PASSWORD}
                    required={true}
                    customValidator={(formValues) => this.passwordCustomValidator(formValues)}
                  />
                </div>
                <Submit
                  disabled={submitButtonDisabled}
                  className="btn btn-outline-main"
                  onSubmitHandler={(formValues) => this.handleFormSubmit(formValues)}
                >
                  {loading && <i className="fa fa-spinner fa-spin" />} Sign up!
                </Submit>
              </Form>
            </div>
            <div className="col-md-8 col-xs-12">
              <p>
                Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the
                industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and
                scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap
                into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the
                release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing
                software like Aldus PageMaker including versions of Lorem Ipsum.
              </p>
              <p>
                Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the
                industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and
                scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap
                into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the
                release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing
                software like Aldus PageMaker including versions of Lorem Ipsum.
              </p>
            </div>
          </div>
        )}
      </section>
    );
  }

  private handleFormSubmit(formValues: FormValues) {
    const { CreateAccount } = this.props;

    const createAccountDto = {
      email: formValues[FORM_FIELDS.EMAIL] as string,
      firstName: formValues[FORM_FIELDS.FIRST_NAME] as string,
      lastName: formValues[FORM_FIELDS.LAST_NAME] as string,
      password: formValues[FORM_FIELDS.PASSWORD] as string,
    };

    CreateAccount(createAccountDto);
  }

  private passwordCustomValidator(formValues: FormValues) {
    return formValues[FORM_FIELDS.PASSWORD] === formValues[FORM_FIELDS.CONFIRM_PASSWORD];
  }
}
