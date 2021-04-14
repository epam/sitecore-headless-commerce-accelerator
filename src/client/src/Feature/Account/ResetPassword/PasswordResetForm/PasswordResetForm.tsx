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

import qs from 'query-string';
import React, { FC, useState } from 'react';

import { LoadingStatus } from 'Foundation/Integration';
import * as JSS from 'Foundation/ReactJss';
import { Form, FormValues, Input, Submit } from 'Foundation/ReactJss/Form';
import { SpinnerIcon } from 'Foundation/UI/components/SpinnerIcon';

import { PasswordResetFormProps } from './models';

import { cnPasswordResetForm } from './cn';
import './PasswordResetForm.scss';

const FORM_FIELDS = JSS.keyMirror(
  {
    NEW_PASSWORD: null,
    NEW_PASSWORD_CONFIRM: null,
  },
  'PASSWORD_RESET_FORM',
);

export const PasswordResetFormComponent: FC<PasswordResetFormProps> = ({
  history,
  recoverPassword,
  resetPasswordState,
}) => {
  const [valid, setValid] = useState(false);
  const [showValidationMessage, setShowValidationMessage] = useState(false);
  const { username, token } = qs.parse(history.location.search) as { username: string; token: string };

  const validateForm = (formValues: FormValues) => {
    const formIsValid = formValues[FORM_FIELDS.NEW_PASSWORD] === formValues[FORM_FIELDS.NEW_PASSWORD_CONFIRM];

    setValid(formIsValid);
    setShowValidationMessage(
      formValues[FORM_FIELDS.NEW_PASSWORD] && formValues[FORM_FIELDS.NEW_PASSWORD_CONFIRM] && !formIsValid,
    );

    return formIsValid;
  };

  const handleSubmit = (formValues: FormValues) => {
    recoverPassword(username, formValues[FORM_FIELDS.NEW_PASSWORD] as string, token);
  };

  return (
    <div className={cnPasswordResetForm()}>
      <h1 className={cnPasswordResetForm('Title')}>Enter New Password</h1>
      <div className={cnPasswordResetForm('FormContainer')}>
        <Form className={cnPasswordResetForm('Form')}>
          <Input
            type="password"
            className={cnPasswordResetForm('Input')}
            name={FORM_FIELDS.NEW_PASSWORD}
            placeholder="New Password"
            required={true}
            customValidator={(formValues) => validateForm(formValues)}
            disabled={false}
          />
          <Input
            type="password"
            className={cnPasswordResetForm('Input')}
            name={FORM_FIELDS.NEW_PASSWORD_CONFIRM}
            placeholder="Confirm New Password"
            required={true}
            customValidator={(formValues) => validateForm(formValues)}
            disabled={false}
          />
          {showValidationMessage && (
            <div className={cnPasswordResetForm('ValidationMessage')}>Both passwords should be identical</div>
          )}
          <Submit
            onSubmitHandler={(formValues) => handleSubmit(formValues)}
            disabled={!valid || resetPasswordState.status === LoadingStatus.Loading}
          >
            {resetPasswordState.status === LoadingStatus.Loading && <SpinnerIcon />}
            Reset my password
          </Submit>
          <div className={cnPasswordResetForm('SubmitMessage')}>
            {resetPasswordState.status === LoadingStatus.Loaded && 'Your password has been successfully updated'}
            {resetPasswordState.status === LoadingStatus.Failure &&
              `Sorry, something went wrong: ${resetPasswordState.error}`}
          </div>
        </Form>
      </div>
    </div>
  );
};
