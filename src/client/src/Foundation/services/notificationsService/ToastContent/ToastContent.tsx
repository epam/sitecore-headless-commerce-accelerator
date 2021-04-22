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

import { faCheckCircle, faCircle } from '@fortawesome/free-regular-svg-icons';
import { faExclamation } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { FC } from 'react';
import { TypeOptions } from 'react-toastify';

import { cnToastContent } from './cn';
import './ToastContent.scss';

type Props = {
  type: TypeOptions;
  message: string;
};

export const ToastContent: FC<Props> = ({ type, message }) => (
  <div className={cnToastContent()}>
    {type === 'dark' && null}
    {type === 'default' && null}
    {type === 'error' && (
      <span className={cnToastContent('ExclamationCircleIcon', ['fa-stack fa-lg'])}>
        <FontAwesomeIcon icon={faCircle} className={cnToastContent('CircleIcon', ['fa-stack-2x'])} />
        <FontAwesomeIcon icon={faExclamation} className={'fa-stack-1x'} />
      </span>
    )}
    {type === 'info' && null}
    {type === 'success' && <FontAwesomeIcon icon={faCheckCircle} size="3x" className={cnToastContent('CheckIcon')} />}
    {type === 'warning' && null}
    <p>{message}</p>
  </div>
);
