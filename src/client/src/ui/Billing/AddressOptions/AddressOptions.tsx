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

import { cnBilling } from '../cn';
import { ADDRESS_TYPE, FIELDS } from '../constants';

import './AddressOptions.scss';

export type AddressOptionsProps = HTMLProps<HTMLUListElement> & {
  selectedAddressOption: string;
  onAddressOptionChange: (value: string) => void;
};

export const AddressOptions: FC<AddressOptionsProps> = ({ selectedAddressOption, onAddressOptionChange }) => {
  const handleChange = useCallback((e) => {
    onAddressOptionChange(e.target.value);
  }, []);

  const radioButtonIds = {
    newAddress: 'r11',
    sameAsShippingAddress: 'r22',
  };

  return (
    <ul className={cnBilling('AddressOptions')}>
      <li className={cnBilling('AddressOption')}>
        <Radio
          className={cnBilling('RadioButton')}
          id={radioButtonIds.newAddress}
          name={FIELDS.ADDRESS_TYPE}
          value={ADDRESS_TYPE.NEW}
          checked={selectedAddressOption === ADDRESS_TYPE.NEW}
          onChange={handleChange}
        />
        <label className={cnBilling('AddressOptionLabel')} htmlFor={radioButtonIds.newAddress}>
          A New Address
        </label>
      </li>

      <li className={cnBilling('AddressOption')}>
        <Radio
          className={cnBilling('RadioButton')}
          id={radioButtonIds.sameAsShippingAddress}
          name={FIELDS.ADDRESS_TYPE}
          value={ADDRESS_TYPE.SAME_AS_SHIPPING}
          checked={selectedAddressOption === ADDRESS_TYPE.SAME_AS_SHIPPING}
          onChange={handleChange}
        />
        <Text
          className={cnBilling('AddressOptionLabel')}
          field={{ value: 'Same As Shipping Address' }}
          tag="label"
          htmlFor={radioButtonIds.sameAsShippingAddress}
        />
      </li>
    </ul>
  );
};
