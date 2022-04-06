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

import { Card } from 'services/commerce';
import { UpdateCardData, AddCard } from 'services/account';
import { Dialog } from 'components';
import { NavigationLink } from 'ui/NavigationLink';

import { PaymentCardForm } from 'ui/PaymentCard/PaymentCardForm';
import { Item } from 'ui/PaymentCardList/Item';

import { cnPaymentCardList } from './cn';
import './PaymentCardList.scss';

const cardList = [
  {
    expiresMonth: '5',
    expiresYear: '2035',
    securityCode: '123',
    cardOwner: 'John Wick',
    cardNumber: '4000123412341234',
    cardType: 'Visa',
    id: '1',
  },
  {
    expiresMonth: '7',
    expiresYear: '2024',
    securityCode: '123',
    cardOwner: 'Albert Einstein',
    cardNumber: '4099909900099876',
    cardType: 'MasterCard',
    id: '2',
  },
  {
    expiresMonth: '6',
    expiresYear: '2025',
    securityCode: '123',
    cardOwner: 'Abraham Lincoln',
    cardNumber: '4000123412349797',
    cardType: 'AmericanExpress',
    id: '3',
  },
];

const PAYMENT_CARD_LIST_CONTENT = {
  TITLE: 'My payment card list',
  BACK: 'Back to My account',
  PATH: '/MyAccount',
  BUTTON: ' + add payment card',
};

export const PaymentCardList = () => {
  const [isOpenForm, setIsOpenForm] = useState(false);
  const [editCard, setEditCard] = useState(false);
  const [selectedItem, setSelectedItem] = useState();

  const dispatch = useDispatch();

  const onUpdateCardData = (cardData: Card) => {
    dispatch(UpdateCardData(cardData));
    setIsOpenForm(false);
    setEditCard(false);
  };

  const onAddCard = (cardData: Card) => {
    dispatch(AddCard(cardData));
    setIsOpenForm(false);
  };

  return (
    <div className={cnPaymentCardList()}>
      <div className={cnPaymentCardList('LeaveLink')}>
        <NavigationLink to={PAYMENT_CARD_LIST_CONTENT.PATH}>{PAYMENT_CARD_LIST_CONTENT.BACK}</NavigationLink>
      </div>
      <div className={cnPaymentCardList('Header')}>
        <span className={cnPaymentCardList('Title')}>{PAYMENT_CARD_LIST_CONTENT.TITLE}</span>
        <button className={cnPaymentCardList('AddCard')} onClick={() => setIsOpenForm(true)}>
          {PAYMENT_CARD_LIST_CONTENT.BUTTON}
        </button>
      </div>
      {cardList.length > 0 &&
        cardList.map((item: Card, index: number) => {
          return (
            <div className={cnPaymentCardList('Card')} key={index}>
              <Item
                item={item}
                setSelectedItem={setSelectedItem}
                setEditCard={setEditCard}
                setIsOpenForm={setIsOpenForm}
              />
            </div>
          );
        })}

      <Dialog
        isOpen={isOpenForm}
        toggleDialog={() => {
          setIsOpenForm(!isOpenForm);
        }}
      >
        <PaymentCardForm
          submitAction={editCard ? onUpdateCardData : onAddCard}
          defaultValues={editCard && selectedItem}
          toggleForm={() => {
            setIsOpenForm(false);
            setEditCard(false);
          }}
        />
      </Dialog>
    </div>
  );
};
