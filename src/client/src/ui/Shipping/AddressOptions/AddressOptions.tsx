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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import React, { FC, HTMLProps, useCallback } from 'react';

import { Radio } from 'Foundation/ReactJss/Form';

import { cnShipping } from '../cn';
import { ADDRESS_TYPE, FIELDS } from '../constants';

import './AddressOptions.scss';

export type AddressOptionsProps = HTMLProps<HTMLUListElement> & {
  isLoggedIn: boolean;
  selectedAddressOption: string;
  onAddressOptionChange: (value: string) => void;
};

export const AddressOptions: FC<AddressOptionsProps> = ({
  isLoggedIn,
  selectedAddressOption,
  onAddressOptionChange,
}) => {
  const handleChange = useCallback((e) => {
    onAddressOptionChange(e.target.value);
  }, []);

  const radioButtonIds = {
    newAddress: 'r1',
    savedAddress: 'r2',
  };

  return (
    <ul className={cnShipping('AddressOptions')}>
      <li className={cnShipping('AddressOption')}>
        <Radio
          id={radioButtonIds.newAddress}
          name={FIELDS.ADDRESS_TYPE}
          value={ADDRESS_TYPE.NEW}
          checked={selectedAddressOption === ADDRESS_TYPE.NEW}
          onChange={handleChange}
        />
        <Text className={cnShipping('AddressOptionLabel')} field={{ value: 'A New Address' }} tag="label" htmlFor={radioButtonIds.newAddress} />
      </li>

      {isLoggedIn && (
        <li className={cnShipping('AddressOption')}>
          <Radio
            id={radioButtonIds.savedAddress}
            name={FIELDS.ADDRESS_TYPE}
            value={ADDRESS_TYPE.SAVED}
            checked={selectedAddressOption === ADDRESS_TYPE.SAVED}
            onChange={handleChange}
          />
          <Text className={cnShipping('AddressOptionLabel')} field={{ value: 'A Saved Address' }} tag="label" htmlFor={radioButtonIds.savedAddress} />
        </li>
      )}
    </ul>
  );
};
