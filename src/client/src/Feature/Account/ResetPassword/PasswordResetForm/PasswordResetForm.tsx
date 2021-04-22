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

import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import qs from 'query-string';
import React, { FC, FormEvent, useCallback, useEffect, useState } from 'react';
import { isMobile } from 'react-device-detect';

import { LoadingStatus } from 'Foundation/Integration';
import { Form } from 'Foundation/ReactJss/Form';
import { notify } from 'Foundation/services/notificationsService';
import { Button } from 'Foundation/UI/components/Button';
import { Input } from 'Foundation/UI/components/Input';
import { SpinnerIcon } from 'Foundation/UI/components/SpinnerIcon';

import { PasswordResetFormProps } from './models';

import { cnPasswordResetForm } from './cn';
import './PasswordResetForm.scss';

export const PasswordResetFormComponent: FC<PasswordResetFormProps> = ({
  history,
  recoverPassword,
  resetPasswordState,
}) => {
  const { username, token } = qs.parse(history.location.search) as { username: string; token: string };

  const [formIsValid, setFormIsValid] = useState(true);
  const [passwordValue, setPasswordValue] = useState('');
  const [showPasswordValue, setShowPasswordValue] = useState(false);
  const [confirmPasswordValue, setConfirmPasswordValue] = useState('');
  const [showConfirmPasswordValue, setShowConfirmPasswordValue] = useState(false);

  useEffect(() => {
    if (resetPasswordState.status === LoadingStatus.Loaded) {
      notify('success', 'Your password has been succesfully changed', isMobile && { position: 'bottom-right' });
    }
    if (resetPasswordState.status === LoadingStatus.Failure) {
      notify('error', 'Sorry, something went wrong', isMobile && { position: 'bottom-right' });
    }
  }, [resetPasswordState.status]);

  useEffect(() => {
    if (passwordValue && confirmPasswordValue) {
      setFormIsValid(passwordValue === confirmPasswordValue);
    }
  }, [passwordValue, confirmPasswordValue]);

  const toggleShowPasswordValue = useCallback(() => {
    setShowPasswordValue((value) => !value);
  }, [setShowPasswordValue, showPasswordValue]);

  const toggleShowConfirmPasswordValue = useCallback(() => {
    setShowConfirmPasswordValue((value) => !value);
  }, [setShowConfirmPasswordValue, confirmPasswordValue]);

  const handlePasswordValueChange = useCallback(
    (e: FormEvent<HTMLInputElement>) => {
      setPasswordValue(e.currentTarget.value);
    },
    [setPasswordValue],
  );

  const handleConfirmPasswordValueChange = useCallback(
    (e: FormEvent<HTMLInputElement>) => {
      setConfirmPasswordValue(e.currentTarget.value);
    },
    [setConfirmPasswordValue],
  );

  const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    recoverPassword(username, passwordValue, token);
  };

  return (
    <div className={cnPasswordResetForm()}>
      <h1 className={cnPasswordResetForm('Title')}>Change your password</h1>
      <div className={cnPasswordResetForm('FormContainer')}>
        <Form className={cnPasswordResetForm('Form')} onSubmit={handleFormSubmit}>
          <p>Enter your new password</p>
          <label htmlFor="password">New password</label>
          <Input
            id="password"
            type={showPasswordValue ? 'text' : 'password'}
            className={cnPasswordResetForm('Input')}
            error={!formIsValid}
            helperText={!formIsValid ? 'Both passwords should be identical' : 'Must be at least 6 characters'}
            required={true}
            minLength={6}
            disabled={resetPasswordState.status === LoadingStatus.Loading}
            fullWidth={true}
            onChange={handlePasswordValueChange}
            adornment={
              <FontAwesomeIcon
                icon={showPasswordValue ? faEyeSlash : faEye}
                className={cnPasswordResetForm('FaEyeIcon')}
                size="lg"
                onClick={toggleShowPasswordValue}
              />
            }
          />
          <label htmlFor="confirm-password">Confirm new password</label>
          <Input
            id="confirm-password"
            type={showConfirmPasswordValue ? 'text' : 'password'}
            className={cnPasswordResetForm('Input')}
            error={!formIsValid}
            helperText={!formIsValid && 'Both passwords should be identical'}
            required={true}
            minLength={6}
            disabled={resetPasswordState.status === LoadingStatus.Loading}
            fullWidth={true}
            onChange={handleConfirmPasswordValueChange}
            adornment={
              <FontAwesomeIcon
                icon={showConfirmPasswordValue ? faEyeSlash : faEye}
                className={cnPasswordResetForm('FaEyeIcon')}
                size="lg"
                onClick={toggleShowConfirmPasswordValue}
              />
            }
          />
          <Button
            className={cnPasswordResetForm('Submit', { disabled: !formIsValid })}
            disabled={!formIsValid}
            buttonType="submit"
            buttonTheme="grey"
          >
            {resetPasswordState.status === LoadingStatus.Loading && <SpinnerIcon />}
            Change password
          </Button>
        </Form>
      </div>
    </div>
  );
};
