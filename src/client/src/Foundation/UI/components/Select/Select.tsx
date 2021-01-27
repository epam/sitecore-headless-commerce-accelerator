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

import { cnSelect } from './cn';

import './Select.scss';

export type SelectProps = HTMLProps<HTMLSelectElement> & {
  controlSize?: 'm';
  error?: boolean;
  fullWidth?: boolean;
};

export const Select: FC<SelectProps> = ({
  children,
  className,
  disabled = false,
  controlSize = 'm',
  error = false,
  fullWidth = false,
  ...rest
}) => {
  return (
    <select
      {...rest}
      className={cnSelect({ controlSize, disabled, error, fullWidth }, [className])}
      disabled={disabled}
    >
      {children}
    </select>
  );
};
