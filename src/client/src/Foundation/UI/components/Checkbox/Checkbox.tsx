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
import { cnCheckbox } from './cn';

import './Checkbox.scss';

export type CheckboxProps = {
  checked?: boolean;
  className?: string;
  size?: string;
} & HTMLProps<HTMLInputElement>;

export const Checkbox: FC<CheckboxProps> = ({
  checked = false,
  className,
  size = 'm',
  ...restProps
}) => {
  return (
    <span className={cnCheckbox({size, checked}, [className])}>
      <input className={cnCheckbox('Control')} type="checkbox" checked={checked} {...restProps}/>
      <span className={cnCheckbox('Checkmark', {size})} />
    </span>
  );
};