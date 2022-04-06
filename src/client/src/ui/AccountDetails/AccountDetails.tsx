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

import React, { FC } from 'react';

import { useSelector } from 'react-redux';
import { commerceUser as CommerceUser } from 'services/account';
import { BaseDataSourceItem, BaseRenderingParam, GraphQLRenderingWithParams } from 'Foundation/ReactJss';

import { AccountWrapper } from '../AccountWrapper';
import { EditPersonalInformation } from './EditPersonalInformation';
import { ChangePassword } from '../Account/ChangePassword';

import './AccountDetails.scss';

const ACCOUNT_DETAILS_CONTENT = {
  TITLE: 'Account details',
  BACK: 'Back to My account',
  PATH: '/MyAccount',
};

export const AccountDetails: FC<GraphQLRenderingWithParams<BaseDataSourceItem, BaseRenderingParam>> = ({
  rendering,
}) => {
  const commerceUser = useSelector(CommerceUser);
  const { firstName, lastName, email, dateOfBirth, phoneNumber, imageUrl } = commerceUser;

  return (
    <AccountWrapper
      title={ACCOUNT_DETAILS_CONTENT.TITLE}
      isAccountDetails={true}
      isLeaveLink={true}
      leaveLinkText={ACCOUNT_DETAILS_CONTENT.BACK}
      leaveLinkPath={ACCOUNT_DETAILS_CONTENT.PATH}
      rendering={rendering}
    >
      <EditPersonalInformation
        firstName={firstName}
        lastName={lastName}
        email={email}
        dateOfBirth={dateOfBirth}
        phoneNumber={phoneNumber}
        imageUrl={imageUrl}
      />
      <ChangePassword />
      {/* <DeleteAccount /> */}
    </AccountWrapper>
  );
};
