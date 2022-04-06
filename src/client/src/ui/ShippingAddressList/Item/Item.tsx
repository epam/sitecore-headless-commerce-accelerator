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

import React, { useState } from 'react';
import { useDispatch } from 'react-redux';

import { Button, Dialog, Icon } from 'components';
import { RemoveAddress } from 'services/account';
import { Address } from 'services/commerce';

import { cnItem } from './cn';
import './Item.scss';

type ItemProps = {
  item: Address;
  setIsOpenForm: (value: boolean) => void;
  setEditForm: (value: boolean) => void;
  setSelectedItem: (value: {}) => void;
};

const ITEM_CONTENT = {
  BUTTON: 'Edit',
};
export const Item = (props: ItemProps) => {
  const { item, setIsOpenForm, setEditForm, setSelectedItem } = props;
  const dispatch = useDispatch();
  const [dialogOpen, setDialogOpen] = useState(false);

  const onDeleteButtonClick = (e: React.MouseEvent<HTMLButtonElement>, externalId: string) => {
    e.preventDefault();
    dispatch(RemoveAddress(externalId));
    handleToggleDialogClick();
  };

  const handleToggleDialogClick = () => {
    setDialogOpen(!dialogOpen);
  };

  return (
    <div className={cnItem()}>
      <div className={cnItem('AddressInfo')}>
        <div className={cnItem('FullName')}>
          {item.firstName}, {item.lastName}
        </div>
        <div className={cnItem('AddressLine')}>{item.address1}</div>
        <div className={cnItem('City')}>{item.city}</div>
        <div className={cnItem('Country')}>{item.country}</div>
        <div className={cnItem('State')}>{item.state}</div>
        <div className={cnItem('PostalCode')}>{item.zipPostalCode}</div>
      </div>
      <div className={cnItem('EditDeleteWrap')}>
        <button
          title={'Delete shipping address'}
          className={cnItem('DeleteBtn')}
          onClick={() => handleToggleDialogClick()}
        >
          <Icon icon="icon-close" size="xl" />
        </button>

        <button
          className={cnItem('EditBtn')}
          onClick={() => {
            setSelectedItem(item);
            setIsOpenForm(true);
            setEditForm(true);
          }}
        >
          {ITEM_CONTENT.BUTTON}
        </button>
      </div>
      <Dialog isOpen={dialogOpen} toggleDialog={() => handleToggleDialogClick()}>
        <div className="CartDialog ContentDialog">
          <div className="CartDialog HeaderDialog">
            <h4 className="CartDialog TitleDialog">Are you sure you want to delete this shipping address?</h4>
          </div>
          <form>
            <div className="CartDialog SubmitContainerDialog">
              <Button
                className="CartDialog ButtonDialog"
                buttonTheme="default"
                onClick={(e: React.MouseEvent<HTMLButtonElement>) => onDeleteButtonClick(e, item.externalId)}
              >
                Yes
              </Button>
              <Button
                className="CartDialog ButtonDialog"
                buttonTheme="default"
                onClick={() => handleToggleDialogClick()}
              >
                No
              </Button>
            </div>
          </form>
        </div>
      </Dialog>
    </div>
  );
};
