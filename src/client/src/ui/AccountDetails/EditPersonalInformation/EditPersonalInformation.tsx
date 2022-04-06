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
import { isEmpty } from 'lodash';

import { Button } from 'components';
import { UpdateAccount, updateError as UpdateError, updateStatus as UpdateStatus } from 'services/account';
import { LoadingStatus } from 'models';

import { EditPersonalInformationProps } from './models';
import { Avatar } from '../../Account/Avatar/Avatar';
import { PersonalInformationForm } from '../PersonalInformationForm';
import { PersonalInformationStatic } from '../PersonalInformationStatic';
import { isPhoneNumberEmpty, setServerErrors, validate } from '../PersonalInformationForm/utils';

import { cnEditPersonalInformation } from './cn';
import './EditPersonalInformation.scss';

const EDIT_PERSONAL_INFORMATION_CONTENT = {
  TITLE: 'Personal details',
  FIRST_NAME: 'First name',
  LAST_NAME: 'Last name',
  EMAIL: 'Email/login: ',
  EDIT: 'Edit',
};

export const EditPersonalInformation = (props: EditPersonalInformationProps) => {
  const dispatch = useDispatch();
  const updateStatus = useSelector(UpdateStatus);
  const updateError = useSelector(UpdateError);

  const { firstName, lastName, email, phoneNumber, imageUrl, dateOfBirth } = props;
  const [isEditingMode, setIsEditingMode] = useState(false);
  const [stateFormFields, setStateFormFields] = useState({});

  const isLoading = updateStatus === LoadingStatus.Loading;
  const isFailure = updateStatus === LoadingStatus.Failure;
  const Loaded = updateStatus === LoadingStatus.Loaded;

  useEffect(() => {
    if (isFailure) {
      const formFields = setServerErrors(updateError);

      if (!isEmpty(formFields)) {
        setStateFormFields(formFields);
      }
    }

    if (Loaded && isEditingMode) {
      setIsEditingMode(false);
    }
  }, [isFailure, Loaded]);

  const resetErrors = () => {
    setStateFormFields({});
  };

  const handleSubmit = (formValues: any) => {
    const { firstName, lastName, dateOfBirth, phoneNumber } = formValues;
    const formFields = validate(formValues);

    if (isEmpty(formFields)) {
      const correctPhoneNumber = isPhoneNumberEmpty(phoneNumber) ? '' : phoneNumber;
      dispatch(UpdateAccount(firstName, lastName, dateOfBirth, correctPhoneNumber));
    }

    setStateFormFields(formFields);
  };

  const handleEditClick = () => {
    setIsEditingMode(true);
  };

  return (
    <div className={cnEditPersonalInformation()}>
      <div className={cnEditPersonalInformation('TitleWrapper')}>
        <div className={cnEditPersonalInformation('Title')}>{EDIT_PERSONAL_INFORMATION_CONTENT.TITLE}</div>
        {!isEditingMode && (
          <Button
            className={cnEditPersonalInformation('Edit')}
            onClick={() => handleEditClick()}
            buttonType="link"
            buttonTheme="link"
          >
            {EDIT_PERSONAL_INFORMATION_CONTENT.EDIT}
          </Button>
        )}
      </div>
      <div className={cnEditPersonalInformation('Content')}>
        <div className={cnEditPersonalInformation('ContentWrapper')}>
          <Avatar imageUrl={imageUrl} isAccountDetails={true} />
          <div className={cnEditPersonalInformation('Details')}>
            <div className={cnEditPersonalInformation('Item')}>
              <span className={cnEditPersonalInformation('Value', { firstName: true })}>
                {firstName + ' ' + lastName}
              </span>
            </div>
            <div className={cnEditPersonalInformation('Item')}>
              <div className={cnEditPersonalInformation('Field')}>{EDIT_PERSONAL_INFORMATION_CONTENT.EMAIL}</div>
              <div className={cnEditPersonalInformation('Value')}>{email}</div>
            </div>
          </div>
        </div>
      </div>
      {isEditingMode || isLoading ? (
        <PersonalInformationForm
          firstName={firstName}
          lastName={lastName}
          email={email}
          phoneNumber={phoneNumber}
          dateOfBirth={dateOfBirth}
          handleSubmit={handleSubmit}
          isLoading={isLoading}
          stateFormFields={stateFormFields}
          resetErrors={resetErrors}
        />
      ) : (
        <PersonalInformationStatic
          firstName={firstName}
          lastName={lastName}
          email={email}
          phoneNumber={phoneNumber}
          dateOfBirth={dateOfBirth}
          isLoading={isLoading}
        />
      )}
    </div>
  );
};
