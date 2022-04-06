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

import React, { useEffect, useState } from 'react';

import { useDispatch, useSelector } from 'react-redux';

import { savedAddressList as SavedAddressList, GetAddressList, UpdateAddress, AddAddress } from 'services/account';
import { Address } from 'services/commerce';

import { ShippingAddressForm } from 'ui/ShippingAddress/ShippingAddressForm';
import { Dialog } from 'components';

import { Item } from './Item';

import { cnShippingAddressList } from './cn';
import './ShippingAddressList.scss';

const SHIPPING_ADDRESS_LIST_COMPONENT_CONTENT = {
  BUTTON: ' + add shipping address',
};

export const ShippingAddressListComponent = ({ countries }: any) => {
  const [editForm, setEditForm] = useState(false);
  const [isOpenForm, setIsOpenForm] = useState(false);
  const [selectedItem, setSelectedItem] = useState();
  const savedAddressListItems = useSelector(SavedAddressList);
  const dispatch = useDispatch();

  const savedAddressList = Object.keys(savedAddressListItems.items).map(
    (externalId) => savedAddressListItems.items[externalId],
  );

  useEffect(() => {
    dispatch(GetAddressList());
  }, []);

  const onUpdateAddress = (address: Address) => {
    dispatch(UpdateAddress(address));
    setIsOpenForm(false);
    setEditForm(false);
  };

  const onAddAddress = (address: Address) => {
    dispatch(AddAddress(address));
    setIsOpenForm(false);
  };

  return (
    <div className={cnShippingAddressList()}>
      <div className={cnShippingAddressList('Header')}>
        <button className={cnShippingAddressList('AddShippingAddress')} onClick={() => setIsOpenForm(true)}>
          {SHIPPING_ADDRESS_LIST_COMPONENT_CONTENT.BUTTON}
        </button>
      </div>

      {savedAddressList.length > 0 &&
        savedAddressList.map((item, index) => {
          return (
            <div className={cnShippingAddressList('Address')} key={index}>
              <Item
                setSelectedItem={setSelectedItem}
                setEditForm={setEditForm}
                setIsOpenForm={setIsOpenForm}
                item={item}
                key={index}
              />
            </div>
          );
        })}
      <Dialog isOpen={isOpenForm} toggleDialog={() => setIsOpenForm(!isOpenForm)}>
        <ShippingAddressForm
          submitAction={editForm ? onUpdateAddress : onAddAddress}
          toggleForm={() => {
            setIsOpenForm(false);
            setEditForm(false);
          }}
          setEditForm={setEditForm}
          countries={countries}
          defaultValues={editForm && selectedItem}
        />
      </Dialog>
    </div>
  );
};
