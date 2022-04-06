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

import * as React from 'react';

import AmericanExpress from 'static/images/cc-american-express-straight-32px.png';
import Discover from 'static/images/cc-discover-straight-32px.png';
import MasterCard from 'static/images/cc-mastercard-straight-32px.png';
import Visa from 'static/images/cc-visa-straight-32px.png';

import { cnCreditCardTypes } from './cn';
import './CreditCardTypes.scss';

const CREDIT_CARD_TYPES = [
  { SRC: Visa, VALUE: 'Visa' },
  { SRC: MasterCard, VALUE: 'MasterCard' },
  { SRC: AmericanExpress, VALUE: 'AmericanExpress' },
  { SRC: Discover, VALUE: 'Discover' },
];

export interface CreditCardTypesProps {
  name: string;
  error?: boolean;
  helperText?: string;
  defaultValue: string;

  setSelectedCardType: (value: string) => void;
}

export const CreditCardTypes = (props: CreditCardTypesProps) => {
  const { setSelectedCardType, helperText, defaultValue } = props;

  const onCardTypeClick = (cardType: string) => {
    defaultValue === cardType ? setSelectedCardType('') : setSelectedCardType(cardType);
  };

  return (
    <div className={cnCreditCardTypes()}>
      {CREDIT_CARD_TYPES.map((item, index) => {
        return (
          <img
            className={cnCreditCardTypes('CardType', { selected: defaultValue === item.VALUE })}
            key={index}
            src={item.SRC}
            alt={item.VALUE}
            onClick={() => onCardTypeClick(item.VALUE)}
          />
        );
      })}
      {helperText && <p className={cnCreditCardTypes('HelperText')}>{helperText}</p>}
    </div>
  );
};
