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

import React, { useCallback, useState } from 'react';

import { useDispatch } from 'react-redux';

import { Button, Dialog } from 'components';
import { DeleteAccount as deleteAccountAction } from 'services/account';

import { cnDeleteAccount } from './cn';
import './DeleteAccount.scss';

export const DeleteAccount = () => {
  const dispatch = useDispatch();
  const [dialogOpen, setDialogOpen] = useState(false);

  const handleToggleDialogClick = useCallback(() => {
    setDialogOpen(!dialogOpen);
  }, [dialogOpen]);

  const handleDeleteAccountSubmit = () => {
    handleToggleDialogClick();
    dispatch(deleteAccountAction());
  };

  return (
    <div className={cnDeleteAccount()}>
      <Button
        className={cnDeleteAccount('Button')} buttonType="submit"
        buttonTheme="grey"
        onClick={() => handleToggleDialogClick()}
      >
        Delete Account
      </Button>
      <Dialog isOpen={dialogOpen} toggleDialog={handleToggleDialogClick}>
        <div className={cnDeleteAccount('Content')}>
          <div className={cnDeleteAccount('Header')}>
            <h4 className={cnDeleteAccount('Title')}>Confirm account deletion</h4>
          </div>
          <form>
            <div className={cnDeleteAccount('SubmitContainer')}>
              <Button
                className={cnDeleteAccount('Button-dialog')} buttonType="submit"
                buttonTheme="grey"
                onClick={() => handleDeleteAccountSubmit()}
              >
                DELETE
              </Button>
              <Button
                className={cnDeleteAccount('Button-dialog')} buttonType="submit"
                buttonTheme="grey"
                onClick={() => handleToggleDialogClick()}
              >
                CANCEL
              </Button>
            </div>
          </form>
        </div>
      </Dialog>
    </div>
  );
};
