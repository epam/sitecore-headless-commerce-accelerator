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

import React, { FC } from 'react';

import { Button } from 'components';

import { cnRegister } from '../cn';
import '../Register.scss';

export const RegisterOptions: FC = () => (
  <div className={cnRegister('RegisterOptions')}>
    <p>Sign up with</p>
    <div className={cnRegister('SocialButtons')}>
      <Button className={cnRegister('SocialButton', { first: true })} buttonTheme="transparentSlide" fullWidth={true}>
        Facebook
      </Button>
      <Button className={cnRegister('SocialButton')} buttonTheme="transparentSlide" fullWidth={true}>
        Google
      </Button>
      <Button className={cnRegister('SocialButton', { last: true })} buttonTheme="transparentSlide" fullWidth={true}>
        Apple
      </Button>
    </div>
    <p className={cnRegister('RegisterOptionsDescription')}>
      Signing up with social is super quick. Don't worry, we'd never share any of your data or post anything on your
      behalf
    </p>
  </div>
);
