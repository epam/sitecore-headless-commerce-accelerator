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

import { Button, Dialog, Icon } from 'components';
import { Card } from 'services/commerce';

import AmericanExpress from 'static/images/cc-american-express-straight-32px.png';
import Discover from 'static/images/cc-discover-straight-32px.png';
import MasterCard from 'static/images/cc-mastercard-straight-32px.png';
import Visa from 'static/images/cc-visa-straight-32px.png';

import { cnItem } from './cn';
import './Item.scss';

const ITEM_CONTENT = {
  BUTTON: 'Edit',
  DIALOG_TITLE: 'Are you sure you want to delete this card?',
  DELETE: 'Delete',
  CANCEL: 'Cancel',
};
const CARD_TYPES = {
  VISA: 'Visa',
  AMERICAN_EXPRESS: 'AmericanExpress',
  MASTER_CARD: 'MasterCard',
  DISCOVER: 'Discover',
};
export interface ItemProps {
  item: Card;
  setSelectedItem: (value: {}) => void;
  setEditCard: (value: boolean) => void;
  setIsOpenForm: (value: boolean) => void;
}
export const Item = (props: ItemProps) => {
  const { item, setEditCard, setSelectedItem, setIsOpenForm } = props;
  const cardNumber = item.cardNumber.replace(/.(?=.{4})/g, '*');
  const [dialogOpen, setDialogOpen] = useState(false);
  const cardType =
    item.cardType === CARD_TYPES.VISA
      ? Visa
      : item.cardType === CARD_TYPES.AMERICAN_EXPRESS
      ? AmericanExpress
      : item.cardType === CARD_TYPES.MASTER_CARD
      ? MasterCard
      : Discover;

  const onDeleteButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    //dispatch delete
    handleToggleDialogClick();
  };

  const handleToggleDialogClick = () => {
    setDialogOpen(!dialogOpen);
  };

  return (
    <div className={cnItem()}>
      <div className={cnItem('CardInfo')}>
        <div className={cnItem('CardOwner')}>{item.cardOwner}</div>
        <div className={cnItem('CardNumber')}>{cardNumber}</div>
        <div className={cnItem('DateOfExpiration')}>{`Exp. ${item.expiresMonth} / ${item.expiresYear}`}</div>
        <img className={cnItem('CardType')} src={cardType} />
      </div>
      <div className={cnItem('EditDeleteWrap')}>
        <button title={'Delete payment card'} className={cnItem('DeleteBtn')} onClick={() => handleToggleDialogClick()}>
          <Icon icon="icon-close" size="xl" />
        </button>

        <button
          className={cnItem('EditBtn')}
          onClick={() => {
            setSelectedItem(item);
            setIsOpenForm(true);
            setEditCard(true);
          }}
        >
          {ITEM_CONTENT.BUTTON}
        </button>
      </div>

      <Dialog isOpen={dialogOpen} toggleDialog={() => handleToggleDialogClick()}>
        <div className="CartDialog ContentDialog">
          <div className="CartDialog HeaderDialog">
            <h4 className="CartDialog TitleDialog">{ITEM_CONTENT.DIALOG_TITLE}</h4>
          </div>
          <form>
            <div className="CartDialog SubmitContainerDialog">
              <Button
                className="CartDialog ButtonDialog"
                buttonTheme="default"
                onClick={(e: React.MouseEvent<HTMLButtonElement>) => onDeleteButtonClick(e)}
              >
                {ITEM_CONTENT.DELETE}
              </Button>
              <Button
                className="CartDialog ButtonDialog"
                buttonTheme="default"
                onClick={() => handleToggleDialogClick()}
              >
                {ITEM_CONTENT.CANCEL}
              </Button>
            </div>
          </form>
        </div>
      </Dialog>
    </div>
  );
};
