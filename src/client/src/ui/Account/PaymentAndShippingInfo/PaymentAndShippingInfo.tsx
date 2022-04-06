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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import { SectionWrapper } from '../SectionWrapper';

import { PaymentAndShippingInfoProps } from './models';

import './PaymentAndShippingInfo.scss';

const SHIPPING_INFO_CONTENT = {
  TITLE: 'Shipping address',
  PATH: '/MyAccount/ShippingAddress',
  LINK_TEXT: 'view all',
};

// US4248-TA4771 due to bloc of Integration path
// const PAYMENT_INFO_CONTENT = {
//   TITLE: 'Payment card',
//   PATH: '/MyAccount/PaymentCards',
//   LINK_TEXT: 'view all',
// };

export const PaymentAndShippingInfo = (props: PaymentAndShippingInfoProps) => {
  return (
    <div className="PaymentAndShippingInfo row">
      {/* <div className="col-md-6">
        <SectionWrapper
          title={PAYMENT_INFO_CONTENT.TITLE}
          path={PAYMENT_INFO_CONTENT.PATH}
          linkText={PAYMENT_INFO_CONTENT.LINK_TEXT}
        >
          <PaymentCard />
        </SectionWrapper>
      </div> */}
      <div className="col-md-6">
        <SectionWrapper
          title={SHIPPING_INFO_CONTENT.TITLE}
          path={SHIPPING_INFO_CONTENT.PATH}
          linkText={SHIPPING_INFO_CONTENT.LINK_TEXT}
        >
          <Placeholder name="shipping-address" rendering={props.rendering} />
        </SectionWrapper>
      </div>
    </div>
  );
};
