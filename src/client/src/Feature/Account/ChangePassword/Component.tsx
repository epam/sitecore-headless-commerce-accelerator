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

import { LoadingStatus } from 'Foundation/Integration';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/Form';

import { CHANGE_PASSWORD_FORM_FIELDS } from './constants';
import { ChangePasswordOwnState, ChangePasswordProps } from './models';

import './styles.scss';

export default class ChangePasswordComponent extends Jss.SafePureComponent<
  ChangePasswordProps,
  ChangePasswordOwnState
> {
  public constructor(props: ChangePasswordProps) {
    super(props);
  }
  public componentDidMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.VerifyCommerceUser();
    }
  }

  protected toggleAccordion() {
    const lstNodeToogle = document.querySelectorAll('.account-details-form_main');
    lstNodeToogle.forEach((item) => {
      if (item.classList.contains('active') && !item.classList.contains('change-password-body')) {
        item.classList.remove('active');
      } else if (item.classList.contains('active') && item.classList.contains('change-password-body')) {
        item.classList.remove('active');
      } else if (!item.classList.contains('active') && item.classList.contains('change-password-body')) {
        item.classList.add('active');
      }
    });
  }

  protected safeRender() {
    const { changePasswordState } = this.props;

    const isLoading = changePasswordState.status === LoadingStatus.Loading;
    const isChanged = changePasswordState.status === LoadingStatus.Loaded;
    const isError = changePasswordState.status === LoadingStatus.Failure;

    return (
      <div className="account-details-container">
        <div className="account-details-form">
          <div className="account-details-form_header header-accordion" onClick={() => this.toggleAccordion()}>
            <h3 className="header-title">
              <span className="header-title_number">3. </span>
              <span>CHANGE YOUR PASSWORD</span>
              <i className="fa fa-angle-down" aria-hidden="true" />
            </h3>
          </div>
          <div className="account-details-form_main change-password-body">
            <div className="account-details-form_main_container">
              <div className="form-title">
                <h4>CHANGE PASSWORD</h4>
              </div>
              <Form>
                <div className="row">
                  <div className="col-lg-12 col-md-12">
                    <Jss.Text tag="label" field={{ value: 'Old Password', editable: 'Old Password' }} />
                    <Input
                      type="password"
                      name={CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD}
                      required={true}
                      customValidator={(formValues) => this.passwordCustomValidator(formValues)}
                      disabled={isLoading}
                    />
                  </div>
                  <div className="col-lg-12 col-md-12">
                    <Jss.Text tag="label" field={{ value: 'New Password', editable: 'New Password' }} />
                    <Input
                      type="password"
                      name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD}
                      required={true}
                      customValidator={(formValues) => this.passwordCustomValidator(formValues)}
                      disabled={isLoading}
                    />
                  </div>
                  <div className="col-lg-12 col-md-12">
                    <Jss.Text tag="label" field={{ value: 'Confirm New Password', editable: 'Confirm New Password' }} />
                    <Input
                      type="password"
                      name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD_CONFIRM}
                      required={true}
                      customValidator={(formValues) => this.passwordCustomValidator(formValues)}
                      disabled={isLoading}
                    />
                  </div>
                  {isError && (
                    <div className="col-lg-12 col-md-12">
                      <Jss.Text
                        tag="p"
                        className="error-message"
                        field={{ value: 'Change password failed', editable: 'Change password failed' }}
                      />
                    </div>
                  )}
                  {isChanged && (
                    <div className="col-lg-12 col-md-12">
                      <Jss.Text
                        tag="p"
                        className="success-message"
                        field={{
                          editable: 'Password was successfully changed',
                          value: 'Password was successfully changed',
                        }}
                      />
                    </div>
                  )}
                </div>
                <div className="submit-container">
                  <Submit
                    disabled={isLoading}
                    type="button"
                    className="btn btn-outline-main"
                    onSubmitHandler={(formValues) => this.handleChangePasswordSubmit(formValues)}
                  >
                    {isLoading && <i className="fa fa-spinner fa-spin" />}
                    Change Password
                  </Submit>
                </div>
              </Form>
            </div>
          </div>
        </div>
      </div>
    );
  }

  private handleChangePasswordSubmit(formValues: FormValues) {
    const { ChangePassword } = this.props;

    const oldPassword = formValues[CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD] as string;
    const newPassword = formValues[CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD] as string;

    ChangePassword(oldPassword, newPassword);
  }

  private passwordCustomValidator(formValues: FormValues) {
    return (
      formValues[CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD] ===
      formValues[CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD_CONFIRM]
    );
  }
}
