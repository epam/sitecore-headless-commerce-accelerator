//    Copyright 2022 EPAM Systems, Inc.
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

import React, { useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { isEmpty } from 'lodash';

import { FormValues } from 'Foundation/ReactJss/Form';

import { LoadingStatus } from 'models';
import * as Account from 'services/account';
import { commerceUser as CommerceUser, RequestPasswordReset as confirmPasswordRecovery } from 'services/account';

import { CHANGE_PASSWORD_FORM_FIELDS } from '../../ChangePassword/constants';
import { CHANGE_PASSWORD_KEYS, validate, setServerErrors } from './utils';
import { ConfirmationDialog } from './ConfirmationDialog';
import { ResetPasswordDialog } from './ResetPasswordDialog';
import { ChangePasswordDialog } from './ChangePasswordDialog';

import { cnChangePassword } from './cn';
import './ChangePassword.scss';

const CHANGE_PASSWORD_CONTENT = {
  CHANGE_PASSWORD_LINK: 'Change password',
  PASSWORD_CHANGED: 'Password was successfully changed',
  PASSWORD_RESET:
    "You should receive an email to reset your password within the next hour. If you don't, your account may be under a different email address.",
  OK: 'Ok',
};

export const ChangePassword = () => {
  const dispatch = useDispatch();
  const commerceUser = useSelector(CommerceUser);
  const { email } = commerceUser;

  const [dialogOpen, setDialogOpen] = useState(false);
  const [confirmationDialogOpen, setConfirmationDialogOpen] = useState(false);
  const [resetPasswordDialogOpen, setResetPasswordDialogOpen] = useState(false);
  const [resetPasswordConfirmationDialogOpen, setResetPasswordConfirmationDialogOpen] = useState(false);

  const [stateFormFields, setStateFormFields] = useState({});

  const changePasswordState = useSelector(Account.changePassword);
  const changePasswordError = useSelector(Account.changePasswordError);

  const isLoading = changePasswordState.status === LoadingStatus.Loading;
  const isChanged = changePasswordState.status === LoadingStatus.Loaded;
  const isFailure = changePasswordState.status === LoadingStatus.Failure;

  const [firstRender, setFirstRender] = useState(true);

  const [showOldPassword, setShowOldPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [showConfirmNewPassword, setShowConfirmNewPassword] = useState(false);

  const handleToggleShowOldPassword = useCallback(() => {
    setShowOldPassword(!showOldPassword);
  }, [showOldPassword]);

  const handleToggleShowNewPassword = useCallback(() => {
    setShowNewPassword(!showNewPassword);
  }, [showNewPassword]);

  const handleToggleShowConfirmNewPassword = useCallback(() => {
    setShowConfirmNewPassword(!showConfirmNewPassword);
  }, [showConfirmNewPassword]);

  useEffect(() => {
    if (isFailure) {
      const formFields = setServerErrors(changePasswordError);

      if (!isEmpty(formFields)) {
        setStateFormFields(formFields);
      }
    }
  }, [isFailure]);

  useEffect(() => {
    if (firstRender) {
      setFirstRender(false);
    }
    if (!firstRender) {
      if (isChanged) {
        setDialogOpen(false);
        setConfirmationDialogOpen(true);
      }
    }
  }, [isChanged]);

  const handleToggleDialogClick = useCallback(() => {
    setDialogOpen(!dialogOpen);
  }, [dialogOpen]);

  const handleToggleConfirmationDialog = useCallback(() => {
    setConfirmationDialogOpen(!confirmationDialogOpen);
  }, [confirmationDialogOpen]);

  const handleToggleResetPasswordDialog = useCallback(() => {
    setResetPasswordDialogOpen(!resetPasswordDialogOpen);
  }, [resetPasswordDialogOpen]);

  const handleToggleResetPasswordConfirmationDialog = useCallback(() => {
    setResetPasswordConfirmationDialogOpen(!resetPasswordConfirmationDialogOpen);
  }, [resetPasswordConfirmationDialogOpen]);

  const handleResetPasswordLinkClick = () => {
    setDialogOpen(false);
    setResetPasswordDialogOpen(true);
  };

  const handleResetPassword = () => {
    dispatch(confirmPasswordRecovery(email));
    setResetPasswordDialogOpen(false);
    setResetPasswordConfirmationDialogOpen(true);
  };

  const handleChangePasswordSubmit = (formValues: FormValues) => {
    const formFields = validate(formValues);

    if (isEmpty(formFields)) {
      const oldPassword = formValues[CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD] as string;
      const newPassword = formValues[CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD] as string;

      dispatch(Account.ChangePassword(oldPassword, newPassword));
    }
    setStateFormFields(formFields);
  };

  const handlerFocusField = (field: string) => {
    setStateFormFields((value) => ({
      ...value,
      [field]: {
        hasError: false,
      },
    }));

    if (field === CHANGE_PASSWORD_KEYS.NEW_PASSWORD || field === CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD) {
      setStateFormFields((value) => ({
        ...value,
        [CHANGE_PASSWORD_KEYS.NEW_PASSWORD]: {
          hasError: false,
        },
        [CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD]: {
          hasError: false,
        },
      }));
    }
  };

  return (
    <div className={cnChangePassword()}>
      <span className={cnChangePassword('Link')} onClick={() => handleToggleDialogClick()}>
        {CHANGE_PASSWORD_CONTENT.CHANGE_PASSWORD_LINK}
      </span>

      <ChangePasswordDialog
        dialogOpen={dialogOpen}
        showOldPassword={showOldPassword}
        showNewPassword={showNewPassword}
        showConfirmNewPassword={showConfirmNewPassword}
        isLoading={isLoading}
        stateFormFields={stateFormFields}
        handleToggleDialogClick={handleToggleDialogClick}
        handlerFocusField={handlerFocusField}
        handleToggleShowOldPassword={handleToggleShowOldPassword}
        handleToggleShowNewPassword={handleToggleShowNewPassword}
        handleToggleShowConfirmNewPassword={handleToggleShowConfirmNewPassword}
        handleResetPasswordLinkClick={handleResetPasswordLinkClick}
        handleChangePasswordSubmit={handleChangePasswordSubmit}
      />

      <ConfirmationDialog
        title={CHANGE_PASSWORD_CONTENT.PASSWORD_CHANGED}
        buttonText={CHANGE_PASSWORD_CONTENT.OK}
        confirmationDialogOpen={confirmationDialogOpen}
        handleToggleConfirmationDialog={handleToggleConfirmationDialog}
      />

      <ResetPasswordDialog
        resetPasswordDialogOpen={resetPasswordDialogOpen}
        handleToggleResetPasswordDialog={handleToggleResetPasswordDialog}
        handleResetPassword={handleResetPassword}
      />

      <ConfirmationDialog
        title={CHANGE_PASSWORD_CONTENT.PASSWORD_RESET}
        buttonText={CHANGE_PASSWORD_CONTENT.OK}
        confirmationDialogOpen={resetPasswordConfirmationDialogOpen}
        handleToggleConfirmationDialog={handleToggleResetPasswordConfirmationDialog}
      />
    </div>
  );
};
