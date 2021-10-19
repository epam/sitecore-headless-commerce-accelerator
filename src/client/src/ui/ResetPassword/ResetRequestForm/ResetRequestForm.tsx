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

import { LoadingStatus } from 'models';
import { validateValue, EMAIL_REGEX } from 'utils';
import { sitecore } from 'Foundation/ReactJss';

import { Button, Icon, Input, Spinner } from 'components';
import { Form } from 'Foundation/ReactJss/Form';
import { NavigationLink } from 'ui/NavigationLink';

import { ErrorMessage } from './ErrorMessage';
import { SuccessMessage } from './SuccessMessage';

import { ResetRequestFormProps } from './models';

import { cnResetRequestForm } from './cn';
import './ResetRequestForm.scss';
import { useSelector } from 'react-redux';

export const ResetRequestFormComponent: FC<ResetRequestFormProps> = ({
  confirmPasswordRecovery,
  requestPasswordResetState,
}) => {
  const [displaySubmitMessage, setDisplaySubmitMessage] = useState(false);
  const [isEmailValid, setIsEmailValid] = useState(true);
  const [email, setEmail] = useState('');
  const sitecoreState = useSelector(sitecore);

  const isLoading = sitecoreState.status === LoadingStatus.Loading;

  const handleInputChange = (e: FormEvent<HTMLInputElement>) => {
    const newEmail = e.currentTarget.value;

    setEmail(newEmail);
    setIsEmailValid(validateValue(EMAIL_REGEX, newEmail));
  };

  const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    confirmPasswordRecovery(email);
    setDisplaySubmitMessage(true);
  };

  return isLoading ? (
    <Spinner data-autotests="loading_spinner" />
  ) : (
    <div className={cnResetRequestForm()}>
      <h1 className={cnResetRequestForm('Title')}>Reset your password</h1>
      <div className={cnResetRequestForm('FormContainer')}>
        <Form className={cnResetRequestForm('Form')} onSubmit={handleFormSubmit}>
          {displaySubmitMessage && requestPasswordResetState.status === LoadingStatus.Loaded && <SuccessMessage />}
          {displaySubmitMessage && requestPasswordResetState.status === LoadingStatus.Failure && <ErrorMessage />}
          <p>Submit your email and we’ll send you a link to reset your password</p>
          <label htmlFor="email-address">Email address</label>
          <Input
            id="email-address"
            className={cnResetRequestForm('Input')}
            error={!isEmailValid}
            helperText={!isEmailValid && 'Please enter a valid email'}
            fullWidth={true}
            type="text"
            value={email}
            onChange={handleInputChange}
            onFocus={() => setIsEmailValid(true)}
            onBlur={() => setIsEmailValid(validateValue(EMAIL_REGEX, email))}
            disabled={requestPasswordResetState.status === LoadingStatus.Loading}
          />
          <div className={cnResetRequestForm('Actions')}>
            <Button
              className={cnResetRequestForm('Submit', {
                disabled: !isEmailValid || requestPasswordResetState.status === LoadingStatus.Loading,
              })}
              disabled={!isEmailValid || requestPasswordResetState.status === LoadingStatus.Loading}
              buttonType="submit"
              buttonTheme="grey"
            >
              {requestPasswordResetState.status === LoadingStatus.Loading && <Icon icon="icon-spinner-solid" />}
              Reset password
            </Button>
            <div className={cnResetRequestForm('BackToSignIn')}>
              <NavigationLink to="/login-register?form=login">Back to sign in</NavigationLink>
            </div>
          </div>
        </Form>
      </div>
    </div>
  );
};
