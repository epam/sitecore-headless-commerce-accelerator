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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import { get, isEmpty } from 'lodash';

import { Form, FormValues, Input, Select, Submit } from 'Foundation/ReactJss/Form';
import { Card } from 'services/commerce';
import { Button } from 'components';
import { CreditCardTypes } from '../CreditCardTypes';

import { FIELDS, FIELD_TYPES, DEFAULT_MONTH, MONTH_LIST } from './constants';
import { validate } from './utils';
import { cnPaymentCardForm } from './cn';
import './PaymentCardForm.scss';

export interface PaymentCardFormProps {
  submitAction?: (cardData: Card) => void;
  defaultValues?: Card;

  toggleForm: () => void;
}

export const PaymentCardForm = (props: PaymentCardFormProps) => {
  const { submitAction, defaultValues, toggleForm } = props;
  const [stateFormFields, setStateFormFields] = useState({});
  const [selectedCardType, setSelectedCardType] = useState('');

  const cardNumber = defaultValues ? defaultValues.cardNumber : '';
  const cardOwner = defaultValues ? defaultValues.cardOwner : '';
  const cardType = defaultValues ? defaultValues.cardType : '';
  const expiresMonth = defaultValues ? defaultValues.expiresMonth : DEFAULT_MONTH;
  const expiresYear = defaultValues ? defaultValues.expiresYear : (new Date().getFullYear() + 1).toString();

  const handleAddCardClick = (formValues: FormValues) => {
    formValues[FIELDS.CARD_TYPE] = selectedCardType;
    const formFields = validate(formValues);

    if (isEmpty(formFields)) {
      const creditCardData = {
        ...defaultValues,
        cardOwner: formValues[FIELDS.CARD_OWNER] as string,
        cardNumber: formValues[FIELDS.CARD_NUMBER] as string,
        expiresMonth: formValues[FIELDS.EXPIRES_MONTH] as string,
        expiresYear: formValues[FIELDS.EXPIRES_YEAR] as string,
        securityCode: formValues[FIELDS.SECURITY_CODE] as string,
      };
      submitAction(creditCardData);
    }
    setStateFormFields(formFields);
  };

  const expireYears = new Array<number>(10).fill(new Date().getFullYear()).map((date, index) => `${date + index}`);

  return (
    <Form className={cnPaymentCardForm()}>
      <Text field={{ value: 'Card Number' }} tag="label" className="required" />
      <Input
        type="text"
        className={cnPaymentCardForm('CardNumber')}
        fullWidth={true}
        defaultValue={cardNumber}
        name={FIELDS.CARD_NUMBER}
        maxLength={16}
        minLength={13}
        error={get(stateFormFields, [FIELD_TYPES.CARD_NUMBER, 'hasError'], false)}
        helperText={get(stateFormFields, [FIELD_TYPES.CARD_NUMBER, 'message'])}
      />

      <Text field={{ value: 'Card Owner' }} tag="label" className="required" />
      <Input
        type="text"
        className={cnPaymentCardForm('CardOwner')}
        fullWidth={true}
        defaultValue={cardOwner}
        name={FIELDS.CARD_OWNER}
        maxLength={175}
        error={get(stateFormFields, [FIELD_TYPES.CARD_OWNER, 'hasError'], false)}
        helperText={get(stateFormFields, [FIELD_TYPES.CARD_OWNER, 'message'])}
      />

      <CreditCardTypes
        name={FIELDS.CARD_TYPE}
        setSelectedCardType={setSelectedCardType}
        defaultValue={selectedCardType ? selectedCardType : cardType}
        error={get(stateFormFields, [FIELD_TYPES.CARD_TYPE, 'hasError'], false)}
        helperText={get(stateFormFields, [FIELD_TYPES.CARD_TYPE, 'message'])}
      />

      <Text field={{ value: 'Expires' }} tag="label" className="required" />
      <div className={cnPaymentCardForm('Expires')}>
        <Select fullWidth={true} name={FIELDS.EXPIRES_MONTH} defaultValue={expiresMonth}>
          {MONTH_LIST.map((monthName, index) => (
            <option value={index + 1} key={monthName}>
              {monthName}
            </option>
          ))}
        </Select>
        <span className={cnPaymentCardForm('Slashy')}>/</span>

        <Select fullWidth={true} name={FIELDS.EXPIRES_YEAR} defaultValue={expiresYear}>
          {expireYears.map((year) => (
            <option value={year} key={year}>
              {year}
            </option>
          ))}
        </Select>
      </div>

      <div className={cnPaymentCardForm('SecurityCode')}>
        <Text field={{ value: 'Security Code' }} tag="label" className="required" />
        <Input
          type="text"
          className={cnPaymentCardForm('InputSecurityCode')}
          name={FIELDS.SECURITY_CODE}
          fullWidth={true}
          maxLength={3}
          minLength={3}
          error={get(stateFormFields, [FIELD_TYPES.SECURITY_CODE, 'hasError'], false)}
          helperText={get(stateFormFields, [FIELD_TYPES.SECURITY_CODE, 'message'])}
        />
      </div>
      <div className={cnPaymentCardForm('SubmitButton')}>
        <Submit
          buttonTheme="darkGrey"
          fullWidth={true}
          onSubmitHandler={(formValues) => handleAddCardClick(formValues)}
        >
          <Text field={defaultValues ? { value: 'Save' } : { value: 'Add' }} tag="span" />
        </Submit>
      </div>
      <Button
        className={cnPaymentCardForm('CancelButton')}
        fullWidth={true}
        buttonTheme="darkGrey"
        onClick={() => toggleForm()}
      >
        <Text field={{ value: 'CANCEL' }} tag="span" />
      </Button>
    </Form>
  );
};
