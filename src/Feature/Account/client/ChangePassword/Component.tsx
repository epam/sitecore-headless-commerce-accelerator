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

import * as Jss from 'Foundation/ReactJss/client';

import { LoadingStatus } from 'Foundation/Integration/client';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/client/Form';

import { CHANGE_PASSWORD_FORM_FIELDS } from './constants';
import { ChangePasswordOwnState, ChangePasswordProps } from './models';

import './styles.scss';

export default class ChangePasswordComponent extends Jss.SafePureComponent<
  ChangePasswordProps,
  ChangePasswordOwnState
> {
  public componentDidMount() {
    if (!this.props.sitecoreContext.pageEditing) {
      this.props.VerifyCommerceUser();
    }
  }

  protected safeRender() {
    const { changePasswordState } = this.props;

    const isLoading = changePasswordState.status === LoadingStatus.Loading;
    const isChanged = changePasswordState.status === LoadingStatus.Loaded;
    const isError = changePasswordState.status === LoadingStatus.Failure;

    return (
      <div className="change-password-form">
        <div className="change-password-form__header">
          <Jss.Text tag="h2" field={{ value: 'Change Password', editable: 'Change Password' }} />
        </div>
        <Form className="change-password-form__main">
          <Jss.Text tag="label" field={{ value: 'Old Password', editable: 'Old Password' }} />
          <Input type="password" name={CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD} required={true} disabled={isLoading} />
          <Jss.Text tag="label" field={{ value: 'New Password', editable: 'New Password' }} />
          <Input
            type="password"
            name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD}
            required={true}
            customValidator={(formValues) => this.passwordCustomValidator(formValues)}
            disabled={isLoading}
          />
          <Jss.Text tag="label" field={{ value: 'Confirm New Password', editable: 'Confirm New Password' }} />
          <Input
            type="password"
            name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD_CONFIRM}
            required={true}
            customValidator={(formValues) => this.passwordCustomValidator(formValues)}
            disabled={isLoading}
          />
          {isError && (
            <Jss.Text
              tag="p"
              className="error-message"
              field={{ value: 'Change password failed', editable: 'Change password failed' }}
            />
          )}
          {isChanged && (
            <Jss.Text
              tag="p"
              className="success-message"
              field={{
                editable: 'Password was successfully changed',
                value: 'Password was successfully changed',
              }}
            />
          )}
          <Submit
            disabled={isLoading}
            type="button"
            className="btn btn-outline-main"
            onSubmitHandler={(formValues) => this.handleChangePasswordSubmit(formValues)}
          >
            {isLoading && <i className="fa fa-spinner fa-spin" />}
            Change Password
          </Submit>
        </Form>
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
