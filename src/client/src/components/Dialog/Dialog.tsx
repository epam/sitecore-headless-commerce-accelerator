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

import React, { FC } from 'react';

import Modal, { Props as ModalProps } from 'react-modal';

import { Button, Icon } from 'components';

import { cnDialog } from './cn';
import './Dialog.scss';

if (process.env.NODE_ENV !== 'test') {
  Modal.setAppElement('#app');
}

type Props = ModalProps & {
  isHeader?: boolean;
  toggleDialog: () => void;
};

export const Dialog: FC<Props> = ({ isOpen, isHeader = true, toggleDialog, children, ...props }) => {
  return (
    <Modal isOpen={isOpen} className={cnDialog('Content')} overlayClassName={cnDialog('Overlay')} {...props}>
      {isHeader && (
        <div className={cnDialog('Header')}>
          <Button buttonTheme="clear" className={cnDialog('CloseButton')} onClick={toggleDialog}>
            <Icon icon="icon-close" />
          </Button>
        </div>
      )}
      {children}
    </Modal>
  );
};
