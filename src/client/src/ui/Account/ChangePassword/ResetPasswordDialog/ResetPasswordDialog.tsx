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
import { Button, Dialog } from 'components';
import { ResetPasswordDialogProps } from './models';

import { cnChangePassword } from '../cn';

const RESET_PASSWORD_DIALOG_CONTENT = {
  RESET_PASSWORD_TITLE: 'Reset your password',
  RESET_PASSWORD_TEXT: 'We’ll send you a link to reset your password. Please check your email.',
  RESET_PASSWORD: 'Reset password',
  CANCEL: 'Cancel',
};

export const ResetPasswordDialog = (props: ResetPasswordDialogProps) => {
  const { resetPasswordDialogOpen, handleToggleResetPasswordDialog, handleResetPassword } = props;

  return (
    <Dialog isOpen={resetPasswordDialogOpen} isHeader={false} toggleDialog={handleToggleResetPasswordDialog}>
      <div className={cnChangePassword('ResetPasswordModal')}>
        <div className={cnChangePassword('Header')}>
          <h4 className={cnChangePassword('Title')}>{RESET_PASSWORD_DIALOG_CONTENT.RESET_PASSWORD_TITLE}</h4>
          <div className={cnChangePassword('Text')}>{RESET_PASSWORD_DIALOG_CONTENT.RESET_PASSWORD_TEXT}</div>
        </div>
        <div className={cnChangePassword('ButtonsContainer')}>
          <Button
            className={cnChangePassword('ButtonDialog')}
            buttonType="submit"
            buttonTheme="transparent"
            onClick={() => handleToggleResetPasswordDialog()}
          >
            {RESET_PASSWORD_DIALOG_CONTENT.CANCEL}
          </Button>
          <Button
            className={cnChangePassword('ButtonDialog')}
            buttonType="submit"
            buttonTheme="darkGrey"
            onClick={() => handleResetPassword()}
          >
            {RESET_PASSWORD_DIALOG_CONTENT.RESET_PASSWORD}
          </Button>
        </div>
      </div>
    </Dialog>
  );
};
