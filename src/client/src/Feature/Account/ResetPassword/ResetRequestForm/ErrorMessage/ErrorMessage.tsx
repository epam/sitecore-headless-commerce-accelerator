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

import { faCircle } from '@fortawesome/free-regular-svg-icons';
import { faExclamation } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { FC } from 'react';

import { NavigationLink } from 'Foundation/UI';
import { Button } from 'Foundation/UI/components/Button';

import { cnResetRequestForm } from '../cn';
import './ErrorMessage.scss';

export const ErrorMessage: FC = () => (
  <>
    <div className={cnResetRequestForm('ErrorMessage')}>
      <div className={cnResetRequestForm('FaExclamationCircleIcon')}>
        <span className="fa-stack fa-2x">
          <FontAwesomeIcon icon={faCircle} className={cnResetRequestForm('FaCircleIcon', ['fa-stack-2x'])} />
          <FontAwesomeIcon icon={faExclamation} className={'fa-stack-1x'} />
        </span>
      </div>
      <p>
        Sorry, we didn’t recognize that email address. Want to try another? <br />
        If you’d like some extra help, please{' '}
        <NavigationLink to="/ContactUs">
          <Button buttonTheme="text" className={cnResetRequestForm('ContactSupportButton')}>
            contact support
          </Button>
        </NavigationLink>
        .
      </p>
    </div>
    <hr />
  </>
);
