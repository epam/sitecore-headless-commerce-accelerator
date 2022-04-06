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
import { NavigationLink } from 'ui/NavigationLink';

import { PersonalInformationProps } from './models';
import { Avatar } from '../Avatar/Avatar';
import { ClubPoints } from '../ClubPoints/ClubPoints';

import { cnPersonalInformation } from './cn';
import './PersonalInformation.scss';

const PERSONAL_INFORMATION_CONTENT = {
  TITLE: 'Welcome, ',
  NAME: 'Name: ',
  EMAIL: 'Email/login: ',
  ACCOUNT_DETAILS: 'Account details',
  PATH: '/MyAccount/AccountDetails',
};

export const PersonalInformation = (props: PersonalInformationProps) => {
  const { firstName, lastName, email, imageUrl, clubPoints } = props;

  return (
    <div className={cnPersonalInformation()}>
      <div className={cnPersonalInformation('Title')}>{PERSONAL_INFORMATION_CONTENT.TITLE + firstName}</div>
      <div className={cnPersonalInformation('Content')}>
        <div className={cnPersonalInformation('ContentWrapper')}>
          <Avatar imageUrl={imageUrl} />
          <div className={cnPersonalInformation('Details')}>
            <div className={cnPersonalInformation('Item')}>
              <span className={cnPersonalInformation('Field')}>{PERSONAL_INFORMATION_CONTENT.NAME}</span>
              <span className={cnPersonalInformation('Value', { firstName: true })}>{firstName + ' ' + lastName}</span>
            </div>
            <div className={cnPersonalInformation('Item')}>
              <span className={cnPersonalInformation('Field')}>{PERSONAL_INFORMATION_CONTENT.EMAIL}</span>
              <span className={cnPersonalInformation('Value')}>{email}</span>
            </div>
            <NavigationLink
              className={cnPersonalInformation('Link', { marginTop: true })}
              to={PERSONAL_INFORMATION_CONTENT.PATH}
            >
              {PERSONAL_INFORMATION_CONTENT.ACCOUNT_DETAILS}
            </NavigationLink>
          </div>
        </div>
        <ClubPoints clubPoints={clubPoints} />
      </div>
      <div className={cnPersonalInformation('Divider')} />
    </div>
  );
};
