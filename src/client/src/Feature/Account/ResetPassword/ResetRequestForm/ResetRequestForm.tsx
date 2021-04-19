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

import React, { FC, FormEvent, useState } from 'react';

import { LoadingStatus } from 'Foundation/Integration';
import { Form } from 'Foundation/ReactJss/Form';
import { Button } from 'Foundation/UI/components/Button';
import { Input } from 'Foundation/UI/components/Input';
import { SpinnerIcon } from 'Foundation/UI/components/SpinnerIcon';
import { validateEmail } from 'Foundation/utils/validation';

import { ResetRequestFormProps } from './models';

import { cnResetRequestForm } from './cn';
import './ResetRequestForm.scss';

export const ResetRequestFormComponent: FC<ResetRequestFormProps> = ({
  confirmPasswordRecovery,
  requestPasswordResetState,
}) => {
  const [isEmailValid, setIsEmailValid] = useState(true);
  const [email, setEmail] = useState('');

  const handleInputChange = (e: FormEvent<HTMLInputElement>) => {
    const newEmail = e.currentTarget.value;

    setEmail(newEmail);
    setIsEmailValid(validateEmail(newEmail));
  };

  const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    confirmPasswordRecovery(email);

    setEmail('');
    setIsEmailValid(true);
  };

  return (
    <div className={cnResetRequestForm()}>
      <h1 className={cnResetRequestForm('Title')}>Reset Password</h1>
      <div className={cnResetRequestForm('FormContainer')}>
        <Form className={cnResetRequestForm('Form')} onSubmit={handleFormSubmit}>
          <Input
            className={cnResetRequestForm('Input', { error: !isEmailValid })}
            error={!isEmailValid}
            helperText={!isEmailValid && 'Please enter a valid email'}
            fullWidth={true}
            placeholder="Email"
            type="text"
            value={email}
            onChange={handleInputChange}
            onFocus={() => setIsEmailValid(true)}
            onBlur={() => setIsEmailValid(validateEmail(email))}
          />
          <Button
            className={cnResetRequestForm('Submit', { disabled: !isEmailValid })}
            disabled={!isEmailValid}
            buttonType="submit"
            buttonTheme="grey"
          >
            {requestPasswordResetState.status === LoadingStatus.Loading && <SpinnerIcon />}
            Reset my password
          </Button>
          <div className={cnResetRequestForm('SubmitMessage')}>
            {requestPasswordResetState.status === LoadingStatus.Loaded &&
              'Password reset link has successfully been sent to your email'}
            {requestPasswordResetState.status === LoadingStatus.Failure &&
              `Sorry, something went wrong: ${requestPasswordResetState.error}`}
          </div>
        </Form>
      </div>
    </div>
  );
};
