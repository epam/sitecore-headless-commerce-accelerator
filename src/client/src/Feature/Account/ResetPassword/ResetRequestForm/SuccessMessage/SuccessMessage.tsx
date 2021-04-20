//    Copyright 2020 EPAM Systems, Inc.
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

import { faCheckCircle } from '@fortawesome/free-regular-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { FC } from 'react';

import { cnResetRequestForm } from '../cn';
import './SuccessMessage.scss';

export const SuccessMessage: FC = () => (
  <>
    <div className={cnResetRequestForm('SuccessMessage')}>
      <div className={cnResetRequestForm('FaCheckCircleIcon')}>
        <FontAwesomeIcon icon={faCheckCircle} size="4x" />
      </div>
      <p>
        You should receive an email to reset your password within the next hour. If you don't, your account may be under
        a different email address.
      </p>
    </div>
    <hr />
  </>
);
