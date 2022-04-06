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

import { PaymentCardForm } from './PaymentCardForm';

import { cnPaymentCard } from './cn';
import './PaymentCard.scss';

import Visa from 'static/images/cc-visa-straight-32px.png';

const cardList = [
  {
    cardOwner: 'John Wick',
    cardNumber: '4000123412341234',
    dateOfExpiration: '12/2025',
    type: Visa,
    id: '1',
  },
  {
    cardOwner: 'Albert Einstein',
    cardNumber: '4099909900099876',
    dateOfExpiration: '1/2028',
    type: Visa,
    id: '2',
  },
  {
    cardOwner: 'Abraham Lincoln',
    cardNumber: '4000123412349797',
    dateOfExpiration: '12/2025',
    type: Visa,
    id: '3',
  },
];

export const PaymentCard = () => {
  const [isOpenForm, setIsOpenForm] = useState(false);

  const cardNumber = cardList[0].cardNumber.replace(/.(?=.{4})/g, '*');

  return (
    <div className={cnPaymentCard()}>
      {cardList.length > 0 ? (
        <>
          <div className={cnPaymentCard('CardOwner')}>{cardList[0].cardOwner}</div>
          <div className={cnPaymentCard('CardNumber')}>{cardNumber}</div>
          <div className={cnPaymentCard('DateOfExpiration')}>{'Exp.' + ' ' + cardList[0].dateOfExpiration}</div>
          <img className={cnPaymentCard('CardType')} src={cardList[0].type} />
        </>
      ) : isOpenForm ? (
        <PaymentCardForm
          toggleForm={() => {
            setIsOpenForm(false);
          }}
        />
      ) : (
        <button className={cnPaymentCard('AddCard', { hidden: isOpenForm })} onClick={() => setIsOpenForm(true)}>
          + add payment card
        </button>
      )}
    </div>
  );
};
