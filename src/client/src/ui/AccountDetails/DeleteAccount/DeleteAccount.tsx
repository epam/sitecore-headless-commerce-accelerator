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

import React, { useCallback, useState } from 'react';

import { useDispatch } from 'react-redux';

import { Button, Dialog } from 'components';
import { DeleteAccount as deleteAccountAction } from 'services/account';

import { cnDeleteAccount } from './cn';
import './DeleteAccount.scss';

const DELETE_ACCOUNT_CONTENT = {
  TITLE: 'Are you sure you want to delete your account?',
  DELETE: 'Delete Account',
  CANCEL: 'Cancel',
  DELETE_ACCONUT_LINK: ' Delete account',
  MEMBER_ADVANTAGES: {
    TITLE: 'As a member you currently get:',
    ADVANTAGES: ['feature 1', 'feature 2', 'feature 3'],
  },
  MEMBER_DISADVANTAGES: {
    TITLE: 'By deleting your profile you will lose:',
    DISADVANTAGES: ['feature 1', 'feature 2', 'feature 3'],
  },
};

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
      <span className={cnDeleteAccount('DeleteAccountLink')} onClick={() => handleToggleDialogClick()}>
        {DELETE_ACCOUNT_CONTENT.DELETE_ACCONUT_LINK}
      </span>

      <Dialog isOpen={dialogOpen} toggleDialog={handleToggleDialogClick}>
        <div className={cnDeleteAccount('Content')}>
          <div className={cnDeleteAccount('Header')}>
            <h4 className={cnDeleteAccount('Title')}>{DELETE_ACCOUNT_CONTENT.TITLE}</h4>
          </div>
          <div className={cnDeleteAccount('Wrapp')}>
            <p className={cnDeleteAccount('TitleAdvantages')}>{DELETE_ACCOUNT_CONTENT.MEMBER_ADVANTAGES.TITLE}</p>
            <ul className={cnDeleteAccount('List')}>
              {DELETE_ACCOUNT_CONTENT.MEMBER_ADVANTAGES.ADVANTAGES.map((item, index) => {
                return (
                  <li className={cnDeleteAccount('ItemList')} key={index}>
                    {item}
                  </li>
                );
              })}
            </ul>
            <p className={cnDeleteAccount('TitleDisadvantages')}>{DELETE_ACCOUNT_CONTENT.MEMBER_DISADVANTAGES.TITLE}</p>
            <ul className={cnDeleteAccount('List')}>
              {DELETE_ACCOUNT_CONTENT.MEMBER_DISADVANTAGES.DISADVANTAGES.map((item, index) => {
                return (
                  <li className={cnDeleteAccount('ItemList')} key={index}>
                    {item}
                  </li>
                );
              })}
            </ul>
          </div>
          <div className={cnDeleteAccount('SubmitContainer')}>
            <Button
              className={cnDeleteAccount('ButtonDialog')}
              buttonType="submit"
              buttonTheme="darkGrey"
              onClick={() => handleToggleDialogClick()}
            >
              {DELETE_ACCOUNT_CONTENT.CANCEL}
            </Button>
            <Button
              className={cnDeleteAccount('ButtonDialog')}
              buttonType="submit"
              buttonTheme="darkGrey"
              onClick={() => handleDeleteAccountSubmit()}
            >
              {DELETE_ACCOUNT_CONTENT.DELETE}
            </Button>
          </div>
        </div>
      </Dialog>
    </div>
  );
};
