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

import React from 'react';
import { get } from 'lodash';
import { Form, Input, Submit } from 'Foundation/ReactJss/Form';

import { Button, Dialog, Icon } from 'components';

import { ChangePasswordDialogProps } from './models';
import { CHANGE_PASSWORD_FORM_FIELDS } from '../../../ChangePassword/constants';
import { CHANGE_PASSWORD_KEYS } from '../utils';

import { cnChangePassword } from '../cn';
import { cnLogin } from '../../../LoginRegister/Login/cn';

const CHANGE_PASSWORD_DIALOG_CONTENT = {
  TITLE: 'Change password',
  CHANGE_PASSWORD_LINK: 'Change password',
  OLD_PASSWORD: 'Old password',
  NEW_PASSWORD: 'New password',
  CONFIRM_NEW_PASSWORD: 'Confirm new password',
  FORGOT_YOUR_PASSWORD: 'Forgot your password?',
  CANCEL: 'Cancel',
  SAVE: 'Save',
};

export const ChangePasswordDialog = (props: ChangePasswordDialogProps) => {
  const {
    dialogOpen,
    handleToggleDialogClick,
    showOldPassword,
    showNewPassword,
    showConfirmNewPassword,
    isLoading,
    stateFormFields,
    handlerFocusField,
    handleToggleShowOldPassword,
    handleToggleShowNewPassword,
    handleToggleShowConfirmNewPassword,
    handleResetPasswordLinkClick,
    handleChangePasswordSubmit,
  } = props;

  return (
    <Dialog isOpen={dialogOpen} isHeader={false} toggleDialog={handleToggleDialogClick}>
      <div className={cnChangePassword('Modal')}>
        <div className={cnChangePassword('Header')}>
          <h4 className={cnChangePassword('Title')}>{CHANGE_PASSWORD_DIALOG_CONTENT.TITLE}</h4>
        </div>
        <Form className={cnChangePassword('Form')}>
          <div className={cnChangePassword('FormField')}>
            <label className={cnChangePassword('Label')}>{CHANGE_PASSWORD_DIALOG_CONTENT.OLD_PASSWORD}</label>
            <Input
              type={showOldPassword ? 'text' : 'password'}
              name={CHANGE_PASSWORD_FORM_FIELDS.OLD_PASSWORD}
              required={true}
              disabled={isLoading}
              fullWidth={true}
              error={get(stateFormFields, [CHANGE_PASSWORD_KEYS.OLD_PASSWORD, 'hasError'], false)}
              helperText={get(stateFormFields, [CHANGE_PASSWORD_KEYS.OLD_PASSWORD, 'message'])}
              handlerFocusField={() => handlerFocusField(CHANGE_PASSWORD_KEYS.OLD_PASSWORD)}
              adornment={
                <div onClick={() => handleToggleShowOldPassword()}>
                  <Icon
                    icon={showOldPassword ? 'icon-look-slash' : 'icon-look'}
                    className={cnLogin('FaEyeIcon')}
                    size="l"
                  />
                </div>
              }
            />
            <Button
              buttonTheme="link"
              className={cnChangePassword('ResetPasswordLink')}
              onClick={() => handleResetPasswordLinkClick()}
            >
              {CHANGE_PASSWORD_DIALOG_CONTENT.FORGOT_YOUR_PASSWORD}
            </Button>
          </div>
          <div className={cnChangePassword('FormField')}>
            <label className={cnChangePassword('Label')}>{CHANGE_PASSWORD_DIALOG_CONTENT.NEW_PASSWORD}</label>
            <Input
              type={showNewPassword ? 'text' : 'password'}
              name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD}
              required={true}
              disabled={isLoading}
              fullWidth={true}
              error={get(stateFormFields, [CHANGE_PASSWORD_KEYS.NEW_PASSWORD, 'hasError'], false)}
              helperText={get(stateFormFields, [CHANGE_PASSWORD_KEYS.NEW_PASSWORD, 'message'])}
              handlerFocusField={() => handlerFocusField(CHANGE_PASSWORD_KEYS.NEW_PASSWORD)}
              adornment={
                <div onClick={() => handleToggleShowNewPassword()}>
                  <Icon
                    icon={showNewPassword ? 'icon-look-slash' : 'icon-look'}
                    className={cnLogin('FaEyeIcon')}
                    size="l"
                  />
                </div>
              }
            />
          </div>
          <div className={cnChangePassword('FormField')}>
            <label className={cnChangePassword('Label')}>{CHANGE_PASSWORD_DIALOG_CONTENT.CONFIRM_NEW_PASSWORD}</label>
            <Input
              type={showConfirmNewPassword ? 'text' : 'password'}
              name={CHANGE_PASSWORD_FORM_FIELDS.NEW_PASSWORD_CONFIRM}
              required={true}
              disabled={isLoading}
              fullWidth={true}
              error={get(stateFormFields, [CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD, 'hasError'], false)}
              helperText={get(stateFormFields, [CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD, 'message'])}
              handlerFocusField={() => handlerFocusField(CHANGE_PASSWORD_KEYS.CONFIRM_NEW_PASSWORD)}
              adornment={
                <div onClick={() => handleToggleShowConfirmNewPassword()}>
                  <Icon
                    icon={showConfirmNewPassword ? 'icon-look-slash' : 'icon-look'}
                    className={cnLogin('FaEyeIcon')}
                    size="l"
                  />
                </div>
              }
            />
          </div>
          <div className={cnChangePassword('ButtonsContainer')}>
            <Button
              className={cnChangePassword('ButtonDialog')}
              buttonType="submit"
              buttonTheme="transparent"
              onClick={() => handleToggleDialogClick()}
            >
              {CHANGE_PASSWORD_DIALOG_CONTENT.CANCEL}
            </Button>
            <Submit
              className={cnChangePassword('ButtonDialog')}
              disabled={isLoading}
              buttonType="submit"
              buttonTheme="darkGrey"
              onSubmitHandler={(formValues) => handleChangePasswordSubmit(formValues)}
            >
              {isLoading && <Icon icon="icon-spinner-solid" />}
              {CHANGE_PASSWORD_DIALOG_CONTENT.SAVE}
            </Submit>
          </div>
        </Form>
      </div>
    </Dialog>
  );
};
