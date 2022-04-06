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

import { savedAddressList as SavedAddressList, GetAddressList, AddAddress } from 'services/account';
import { Address, CountryRegion } from 'services/commerce';

import { ShippingAddressForm } from 'ui/ShippingAddress/ShippingAddressForm';

import { cnShippingAddress } from './cn';
import './ShippingAddress.scss';

export interface ShippingAddressProps {
  rendering: {
    fields: {
      countries: CountryRegion[];
    };
  };
}

export const ShippingAddress = ({ rendering }: ShippingAddressProps) => {
  const countries = rendering.fields.countries;
  const savedAddressListItems = useSelector(SavedAddressList);
  const [isOpenForm, setIsOpenForm] = useState(false);
  const dispatch = useDispatch();

  const savedAddressList = Object.keys(savedAddressListItems.items).map(
    (externalId) => savedAddressListItems.items[externalId],
  );
  const selectedAddressId = '';
  const selectedAddress = savedAddressList.find((a) => a.externalId === selectedAddressId) || savedAddressList[0];

  useEffect(() => {
    dispatch(GetAddressList());
  }, []);

  const onAddAdress = (address: Address) => {
    dispatch(AddAddress(address));
    setIsOpenForm(false);
  };
  const lastItem = savedAddressList[savedAddressList.length - 1];

  return (
    <div className={cnShippingAddress()}>
      {lastItem ? (
        <>
          <div className={cnShippingAddress('FullName')}>
            {lastItem.firstName}, {lastItem.lastName}
          </div>
          <div className={cnShippingAddress('AddressLine')}>{lastItem.address1}</div>
          <div className={cnShippingAddress('City')}>{lastItem.city}</div>
          <div className={cnShippingAddress('State')}>{lastItem.state}</div>
          <div className={cnShippingAddress('Country')}>
            {lastItem.country}, {lastItem.countryCode}
          </div>
        </>
      ) : (
        <button
          className={cnShippingAddress('AddShippingAddress', { hidden: isOpenForm })}
          onClick={() => setIsOpenForm(true)}
        >
          + add shipping address
        </button>
      )}

      {isOpenForm && (
        <ShippingAddressForm
          submitAction={onAddAdress}
          toggleForm={() => setIsOpenForm(false)}
          countries={countries}
          defaultValues={selectedAddress}
        />
      )}
    </div>
  );
};
