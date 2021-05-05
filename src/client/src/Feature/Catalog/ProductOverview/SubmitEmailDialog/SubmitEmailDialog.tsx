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

import React, { ChangeEvent, FC, useCallback, useState } from 'react';

import { Button, Dialog, Input } from 'components';

import { cnSubmitEmailDialog } from './cn';
import './SubmitEmailDialog.scss';

type Props = {
  dialogOpen: boolean;
  toggleDialog: (open: boolean) => void;
  submitDialogData: (email: string) => void;
};

export const SubmitEmailDialog: FC<Props> = ({ dialogOpen, toggleDialog, submitDialogData }) => {
  const [email, setEmail] = useState('');

  const handleToggleDialogClick = useCallback(() => {
    toggleDialog(!dialogOpen);
  }, [dialogOpen]);

  const handleInputValueChange = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  }, []);

  return (
    <Dialog isOpen={dialogOpen} toggleDialog={handleToggleDialogClick}>
      <div className={cnSubmitEmailDialog('Content')}>
        <div className={cnSubmitEmailDialog('Header')}>
          <h4 className={cnSubmitEmailDialog('Title')}>Submit your email</h4>
          <h6>And we will send a notification when the product is available again</h6>
        </div>
        <form>
          <Input
            name="email"
            value={email}
            required={true}
            onChange={handleInputValueChange}
            type="email"
            placeholder="Email*"
            fullWidth={true}
          />
          <div className={cnSubmitEmailDialog('SubmitContainer')}>
            <Button
              buttonType="submit"
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.preventDefault();

                submitDialogData(email);
                toggleDialog(false);
              }}
            >
              Submit
            </Button>
          </div>
        </form>
      </div>
    </Dialog>
  );
};
