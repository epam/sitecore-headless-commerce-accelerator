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

import { ACCOUNT_DETAILS_FORM_FIELDS } from './constants';
import { ChangeUserInformationFormOwnState, ChangeUserInformationFormProps } from './models';

import './styles.scss';

export default class ChangeUserInformationForm extends Jss.SafePureComponent<
  ChangeUserInformationFormProps,
  ChangeUserInformationFormOwnState
> {
  protected safeRender() {
    const { commerceUser, updateStatus } = this.props;

    const isLoading = updateStatus === LoadingStatus.Loading;
    return (
      <div className="account-details-form">
        <div className="account-details-form__header">
          <h2>Account Details</h2>
        </div>
        <div className="account-details-form__main">
          <Form>
            <Jss.Text tag="label" field={{ value: 'First Name', editable: 'First Name' }} />
            <Input
              name={ACCOUNT_DETAILS_FORM_FIELDS.FIRST_NAME}
              type="text"
              required={true}
              disabled={isLoading}
              defaultValue={commerceUser.firstName}
            />
            <Jss.Text tag="label" field={{ value: 'Last Name', editable: 'Last Name' }} />
            <Input
              name={ACCOUNT_DETAILS_FORM_FIELDS.LAST_NAME}
              type="text"
              required={true}
              disabled={isLoading}
              defaultValue={commerceUser.lastName}
            />
            <Submit
              className="btn btn-outline-main"
              disabled={isLoading}
              onSubmitHandler={(formValues) => this.handleSaveChangesClick(formValues)}
            >
              {isLoading && <i className="fa fa-spinner fa-spin" />}
              Save Changes
            </Submit>
          </Form>
        </div>
      </div>
    );
  }

  private handleSaveChangesClick(formValues: FormValues) {
    const { UpdateAccount } = this.props;

    const firstName = formValues[ACCOUNT_DETAILS_FORM_FIELDS.FIRST_NAME] as string;
    const lastName = formValues[ACCOUNT_DETAILS_FORM_FIELDS.LAST_NAME] as string;

    UpdateAccount(firstName, lastName);
  }
}
