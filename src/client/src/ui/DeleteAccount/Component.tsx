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

import React, { MouseEvent } from 'react';

import * as Jss from 'Foundation/ReactJss';

import { Button } from 'components';

import { DeleteAccountOwnState, DeleteAccountProps } from './models';

import './styles.scss';

export default class DeleteAccountComponent extends Jss.SafePureComponent<DeleteAccountProps, DeleteAccountOwnState> {
  public constructor(props: DeleteAccountProps) {
    super(props);
  }

  protected safeRender() {
    return (
      <div className="submit-container">
        <Button
          buttonType="submit"
          buttonTheme="grey"
          onClick={(e: MouseEvent<HTMLButtonElement>) => this.handleDeleteAccountSubmit(e)}
        >
          Delete Account
        </Button>
      </div>
    );
  }

  protected toggleAccordion() {
    const lstNodeToogle = document.querySelectorAll('.account-details-form_main');
    lstNodeToogle.forEach((item) => {
      const isActive = item.classList.contains('active');
      const isDeleteAccountItem = item.classList.contains('delete-account-body');

      !isActive && isDeleteAccountItem ? item.classList.add('active') : item.classList.remove('active');
    });
  }

  private handleDeleteAccountSubmit(e: React.MouseEvent<HTMLButtonElement>) {
    const { DeleteAccount } = this.props;

    DeleteAccount();
  }
}
