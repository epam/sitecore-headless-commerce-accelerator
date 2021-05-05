//    Copyright 2021 EPAM Systems, Inc.
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
import { isMobile } from 'react-device-detect';
import { toast, TypeOptions } from 'react-toastify';

import { Product } from 'Foundation/Commerce';

import { SubscriptionMessage } from './SubscriptionMessage';
import { ToastContent } from './ToastContent';

export const notify = (type: TypeOptions, message: string) => {
  return toast(() => <ToastContent type={type} message={message} />, isMobile && { position: 'bottom-right' });
};

export const notifySubscribed = (product: Product) => {
  return toast(
    () => <SubscriptionMessage product={product} configuration={{ includeDetails: !isMobile }} />,
    isMobile && { position: 'bottom-right' },
  );
};
