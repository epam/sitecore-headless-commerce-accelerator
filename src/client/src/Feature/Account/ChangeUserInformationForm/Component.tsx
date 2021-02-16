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

import { ACCOUNT_DETAILS_FORM_FIELDS } from './constants';
import { ChangeUserInformationFormOwnState, ChangeUserInformationFormProps } from './models';

import './styles.scss';

export default class ChangeUserInformationForm extends Jss.SafePureComponent<
  ChangeUserInformationFormProps,
  ChangeUserInformationFormOwnState
> {
  public constructor(props: ChangeUserInformationFormProps) {
    super(props);
  }
  protected toggleAccordion() {
    const lstNodeToogle = document.querySelectorAll('.account-details-form_main');
    lstNodeToogle.forEach((item) => {
      if (item.classList.contains('active') && !item.classList.contains('user-information-body')) {
        item.classList.remove('active');
      } else if (item.classList.contains('active') && item.classList.contains('user-information-body')) {
        item.classList.remove('active');
      } else if (!item.classList.contains('active') && item.classList.contains('user-information-body')) {
        item.classList.add('active');
      }
    });
  }
  protected safeRender() {
    const { commerceUser, updateStatus } = this.props;
    const isLoading = updateStatus === LoadingStatus.Loading;
    return (
      <div className="account-details-container">
        <div className="account-details-form">
          <div className="account-details-form_header header-accordion" onClick={() => this.toggleAccordion()}>
            <h3 className="header-title">
              <span className="header-title_number">1. </span>
              <span>EDIT YOUR ACCOUNT INFORMATION</span>
              <i className="fa fa-angle-down" aria-hidden="true" />
            </h3>
          </div>
          <div className="account-details-form_main user-information-body">
            <div className="account-details-form_main_container">
              <div className="form-title">
                <h4>MY ACCOUNT INFORMATION</h4>
                <h5>Your Personal Details</h5>
              </div>
              <Form>
                <div className="row">
                  <div className="col-lg-6 col-md-6">
                    <Jss.Text tag="label" field={{ value: 'First Name', editable: 'First Name' }} />
                    <Input
                      name={ACCOUNT_DETAILS_FORM_FIELDS.FIRST_NAME}
                      type="text"
                      required={true}
                      disabled={isLoading}
                      defaultValue={commerceUser && commerceUser.firstName ? commerceUser.firstName : ''}
                    />
                  </div>
                  <div className="col-lg-6 col-md-6">
                    <Jss.Text tag="label" field={{ value: 'Last Name', editable: 'Last Name' }} />
                    <Input
                      name={ACCOUNT_DETAILS_FORM_FIELDS.LAST_NAME}
                      type="text"
                      required={true}
                      disabled={isLoading}
                      defaultValue={commerceUser && commerceUser.lastName ? commerceUser.lastName : ''}
                    />
                  </div>
                </div>
                <div className="submit-container">
                  <Submit
                    buttonTheme="grey"
                    disabled={isLoading}
                    onSubmitHandler={(formValues) => this.handleSaveChangesClick(formValues)}
                  >
                    {isLoading && <i className="fa fa-spinner fa-spin" />}
                    Save Changes
                  </Submit>
                </div>
              </Form>
            </div>
          </div>
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
