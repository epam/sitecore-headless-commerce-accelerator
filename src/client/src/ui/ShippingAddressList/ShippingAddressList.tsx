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

import { BaseDataSourceItem, BaseRenderingParam, GraphQLRenderingWithParams } from 'Foundation/ReactJss';

import { AccountWrapper } from '../AccountWrapper';
import { ShippingAddressListComponent } from './ShippingAddressListComponent';

const SHIPPING_ADDRESS_LIST_CONTENT = {
  TITLE: 'My shipping address book',
  BACK: 'Back to My account',
  PATH: '/MyAccount',
  BUTTON: ' + add shipping address',
};

export const ShippingAddressList: FC<GraphQLRenderingWithParams<BaseDataSourceItem, BaseRenderingParam>> = ({
  rendering,
}) => {
  const { countries } = rendering.fields;

  return (
    <AccountWrapper
      title={SHIPPING_ADDRESS_LIST_CONTENT.TITLE}
      leaveLinkText={SHIPPING_ADDRESS_LIST_CONTENT.BACK}
      leaveLinkPath={SHIPPING_ADDRESS_LIST_CONTENT.PATH}
      isLeaveLink={true}
      rendering={rendering}
    >
      <ShippingAddressListComponent countries={countries} />
    </AccountWrapper>
  );
};
