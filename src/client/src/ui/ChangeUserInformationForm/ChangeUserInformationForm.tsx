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

import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { get, isEmpty } from 'lodash';

import * as Jss from 'Foundation/ReactJss';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/Form';
import { LoadingStatus } from 'models';
import { commerceUser as CommerceUser, updateStatus as UpdateStatus, UpdateAccount } from 'services/account';
import { Icon } from 'components';

import { validate } from './utils';
import { ACCOUNT_DETAILS_FORM_FIELDS } from './constants';

import './ChangeUserInformationForm.scss';

export const ChangeUserInformationForm = () => {
  const dispatch = useDispatch();
  const commerceUser = useSelector(CommerceUser);
  const updateStatus = useSelector(UpdateStatus);
  const [stateFormFields, setStateFormFields] = useState({});

  const isLoading = updateStatus === LoadingStatus.Loading;

  const toggleAccordion = () => {
    const lstNodeToogle = document.querySelectorAll('.account-details-form_main');
    lstNodeToogle.forEach((item) => {
      const isActive = item.classList.contains('active');
      const hasContent = item.classList.contains('user-information-body');

      !isActive && hasContent ? item.classList.add('active') : item.classList.remove('active');
    });
  };

  const handleSaveChangesClick = (formValues: FormValues) => {
    const formFields = validate(formValues);
    const firstName = formValues[ACCOUNT_DETAILS_FORM_FIELDS.FIRST_NAME] as string;
    const lastName = formValues[ACCOUNT_DETAILS_FORM_FIELDS.LAST_NAME] as string;

    if (isEmpty(formFields)) {
      dispatch(UpdateAccount(firstName, lastName));
    }

    setStateFormFields(formFields);
  };

  return (
    <div className="account-details-container">
      <div className="account-details-form">
        <div className="account-details-form_header header-accordion" onClick={() => toggleAccordion()}>
          <h3 className="header-title">
            <span className="header-title_number">1. </span>
            <span>EDIT YOUR ACCOUNT INFORMATION</span>
            <Icon icon="icon-angle-down" aria-hidden="true" />
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
                    fullWidth={true}
                    error={get(stateFormFields, ['firstName', 'hasError'], false)}
                    helperText={get(stateFormFields, ['firstName', 'message'])}
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
                    fullWidth={true}
                    error={get(stateFormFields, ['lastName', 'hasError'], false)}
                    helperText={get(stateFormFields, ['lastName', 'message'])}
                  />
                </div>
              </div>
              <div className="submit-container">
                <Submit
                  className="SubmitButton"
                  buttonTheme="default"
                  disabled={isLoading}
                  onSubmitHandler={(formValues) => handleSaveChangesClick(formValues)}
                >
                  {isLoading && <Icon icon="icon-spinner-solid" />}
                  Save Changes
                </Submit>
              </div>
            </Form>
          </div>
        </div>
      </div>
    </div>
  );
};
