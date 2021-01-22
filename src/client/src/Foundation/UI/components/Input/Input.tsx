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

import React, { FC, HTMLProps } from 'react';

import { cnInput } from './cn';

import './Input.scss';

export type InputType = 'text' | 'password' | 'number' | 'email';

export type InputProps = HTMLProps<HTMLInputElement> & {
  error?: boolean;
  fullWidth?: boolean;
  controlSize?: 'm';
  type?: InputType;
};

export const Input: FC<InputProps> = ({
  className,
  error = false,
  fullWidth = false,
  controlSize = 'm',
  type = 'text',
  ...rest
}) => {
  return (
    <input
      {...rest}
      className={cnInput({ error, fullWidth, controlSize, type }, [className])}
      type={type}
    />
  );
};
