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
import InputMask from 'react-input-mask';

import { Form, Input, Submit } from 'Foundation/ReactJss/Form';
import { isMobile } from 'react-device-detect';
import { get } from 'lodash';

import { PersonalInformationFormProps } from './models';
import { FORM_FIELDS_KEYS } from './utils';

import { cnPersonalInformationForm } from './cn';
import './PersonalInformationForm.scss';

const PERSONAL_INFORMATION_FORM_CONTENT = {
  FIRST_NAME: 'First name',
  LAST_NAME: 'Last name',
  DATE_OF_BIRTH: 'Date of birth',
  PHONE_NUMBER: 'Phone',
  SAVE: 'Save',
  DATE_OF_BIRTH_PLACEHOLDER: 'DD/MM/YYYY',
  PHONE_NUMBER_PLACEHOLDER: '+XXXXXXXXXXX',
  DEFAULT_ERROR_MESSAGE: 'Please, specify the value',
};

export const PersonalInformationForm = (props: PersonalInformationFormProps) => {
  const { handleSubmit, firstName, lastName, dateOfBirth, phoneNumber, isLoading, stateFormFields, resetErrors } =
    props;
  const [isFirstNameValid, setIsFirstNameValid] = useState(true);
  const [isLastNameValid, setIsLastNameValid] = useState(true);

  return (
    <Form className={cnPersonalInformationForm()}>
      <div className={cnPersonalInformationForm('FormField')}>
        <label className={cnPersonalInformationForm('Label', { required: true })}>
          {PERSONAL_INFORMATION_FORM_CONTENT.FIRST_NAME}
        </label>
        <Input
          className={cnPersonalInformationForm('Input')}
          controlSize={'l'}
          name="firstName"
          type="text"
          required={true}
          defaultValue={firstName ? firstName : ''}
          disabled={isLoading}
          maxLength={255}
          error={get(stateFormFields, [FORM_FIELDS_KEYS.FIRST_NAME, 'hasError'], false) || !isFirstNameValid}
          helperText={
            get(stateFormFields, [FORM_FIELDS_KEYS.FIRST_NAME, 'message']) ||
            (!isFirstNameValid && PERSONAL_INFORMATION_FORM_CONTENT.DEFAULT_ERROR_MESSAGE)
          }
          onBlur={(e) => setIsFirstNameValid(!!e.target.value)}
        />
      </div>
      <div className={cnPersonalInformationForm('FormField')}>
        <label className={cnPersonalInformationForm('Label', { required: true })}>
          {PERSONAL_INFORMATION_FORM_CONTENT.LAST_NAME}
        </label>
        <Input
          className={cnPersonalInformationForm('Input')}
          controlSize={'l'}
          name="lastName"
          type="text"
          required={true}
          defaultValue={lastName ? lastName : ''}
          disabled={isLoading}
          maxLength={255}
          error={get(stateFormFields, [FORM_FIELDS_KEYS.LAST_NAME, 'hasError'], false) || !isLastNameValid}
          helperText={
            get(stateFormFields, [FORM_FIELDS_KEYS.LAST_NAME, 'message']) ||
            (!isLastNameValid && PERSONAL_INFORMATION_FORM_CONTENT.DEFAULT_ERROR_MESSAGE)
          }
          onBlur={(e) => setIsLastNameValid(!!e.target.value)}
        />
      </div>
      <div className={cnPersonalInformationForm('FormField')}>
        <label className={cnPersonalInformationForm('Label')}>{PERSONAL_INFORMATION_FORM_CONTENT.DATE_OF_BIRTH}</label>
        <InputMask mask="99/99/9999" maskChar={null} disabled={isLoading} defaultValue={dateOfBirth ? dateOfBirth : ''}>
          {(inputProps: any) => (
            <Input
              {...inputProps}
              className={cnPersonalInformationForm('Input', { date: true })}
              controlSize={isMobile ? 'l' : 'm'}
              name="dateOfBirth"
              type="text"
              placeholder={PERSONAL_INFORMATION_FORM_CONTENT.DATE_OF_BIRTH_PLACEHOLDER}
              error={get(stateFormFields, [FORM_FIELDS_KEYS.DATE_OF_BIRTH, 'hasError'], false)}
              helperText={get(stateFormFields, [FORM_FIELDS_KEYS.DATE_OF_BIRTH, 'message'])}
              handlerFocusField={() => resetErrors()}
            />
          )}
        </InputMask>
      </div>
      <div className={cnPersonalInformationForm('FormField')}>
        <label className={cnPersonalInformationForm('Label')}>{PERSONAL_INFORMATION_FORM_CONTENT.PHONE_NUMBER}</label>
        <InputMask
          mask="+999999999999999"
          maskChar={null}
          disabled={isLoading}
          defaultValue={phoneNumber ? phoneNumber : ''}
        >
          {(inputProps: any) => (
            <Input
              {...inputProps}
              className={cnPersonalInformationForm('Input')}
              controlSize={'l'}
              name="phoneNumber"
              type="text"
              placeholder={PERSONAL_INFORMATION_FORM_CONTENT.PHONE_NUMBER_PLACEHOLDER}
              maxLength={15}
              error={get(stateFormFields, [FORM_FIELDS_KEYS.PHONE_NUMBER, 'hasError'], false)}
              helperText={get(stateFormFields, [FORM_FIELDS_KEYS.PHONE_NUMBER, 'message'])}
            />
          )}
        </InputMask>
      </div>
      <div className={cnPersonalInformationForm('ButtonsContainer')}>
        <Submit
          className={cnPersonalInformationForm('Button')}
          buttonTheme="darkGrey"
          buttonSize="default"
          fullWidth={isMobile}
          disabled={false}
          onSubmitHandler={(formValues) => handleSubmit(formValues)}
        >
          <span>Save</span>
        </Submit>
      </div>
    </Form>
  );
};
