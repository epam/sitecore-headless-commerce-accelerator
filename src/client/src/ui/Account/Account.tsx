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

import { PersonalInformation } from './PersonalInformation';
import { AccountWrapper } from '../AccountWrapper';
import { ActiveOrder } from './ActiveOrder';
import { PaymentAndShippingInfo } from './PaymentAndShippingInfo';

import './Account.scss';

const ACCOUNT_CONTENT = {
  TITLE: 'My account',
};

const defaultProps = {
  clubPoints: 0,
};

export const Account: FC<GraphQLRenderingWithParams<BaseDataSourceItem, BaseRenderingParam>> = ({ rendering }) => {
  const commerceUser = useSelector(CommerceUser);
  const { firstName, lastName, imageUrl, email } = commerceUser;
  const { clubPoints } = defaultProps;

  return (
    <AccountWrapper title={ACCOUNT_CONTENT.TITLE} isTitleHiddenOnMobile={true} rendering={rendering}>
      <PersonalInformation
        firstName={firstName}
        lastName={lastName}
        email={email}
        imageUrl={imageUrl}
        clubPoints={clubPoints}
      />
      <ActiveOrder />
      <PaymentAndShippingInfo rendering={rendering} />
    </AccountWrapper>
  );
};
