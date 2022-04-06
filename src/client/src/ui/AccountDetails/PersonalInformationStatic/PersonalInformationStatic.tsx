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

import React from 'react';
import { Spinner } from 'components';

import { PersonalInformationStaticProps } from './models';

import { cnPersonalInformationStatic } from './cn';
import './PersonalInformationStatic.scss';

const PERSONAL_INFORMATION_STATIC_CONTENT = {
  FIRST_NAME: 'First name',
  LAST_NAME: 'Last name',
  DATE_OF_BIRTH: 'Date of birth',
  DATE_OF_BIRTH_PLACEHOLDER: 'DD/MM/YYYY',
  PHONE_NUMBER: 'Phone',
  SAVE: 'Save',
};

export const PersonalInformationStatic = (props: PersonalInformationStaticProps) => {
  const { firstName, lastName, phoneNumber, dateOfBirth, isLoading } = props;

  return (
    <>
      {isLoading && <Spinner data-autotests="loading_spinner" />}
      {!isLoading && (
        <div className={cnPersonalInformationStatic()}>
          <div className={cnPersonalInformationStatic('Item')}>
            <div className={cnPersonalInformationStatic('Field')}>{PERSONAL_INFORMATION_STATIC_CONTENT.FIRST_NAME}</div>
            <div className={cnPersonalInformationStatic('Value')}>{firstName}</div>
          </div>
          <div className={cnPersonalInformationStatic('Item')}>
            <div className={cnPersonalInformationStatic('Field')}>{PERSONAL_INFORMATION_STATIC_CONTENT.LAST_NAME}</div>
            <div className={cnPersonalInformationStatic('Value')}>{lastName}</div>
          </div>
          <div className={cnPersonalInformationStatic('Item')}>
            <div className={cnPersonalInformationStatic('Field')}>
              {PERSONAL_INFORMATION_STATIC_CONTENT.DATE_OF_BIRTH}
            </div>
            {dateOfBirth ? (
              <div className={cnPersonalInformationStatic('Value')}>{dateOfBirth}</div>
            ) : (
              <div className={cnPersonalInformationStatic('Placeholder')}>
                {PERSONAL_INFORMATION_STATIC_CONTENT.DATE_OF_BIRTH_PLACEHOLDER}
              </div>
            )}
          </div>
          <div className={cnPersonalInformationStatic('Item')}>
            <div className={cnPersonalInformationStatic('Field')}>
              {PERSONAL_INFORMATION_STATIC_CONTENT.PHONE_NUMBER}
            </div>
            <div className={cnPersonalInformationStatic('Value')}>{phoneNumber}</div>
          </div>
        </div>
      )}
    </>
  );
};
