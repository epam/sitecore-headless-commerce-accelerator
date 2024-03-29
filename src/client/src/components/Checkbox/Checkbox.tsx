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

import React, { FC, forwardRef, HTMLProps } from 'react';

import { cnCheckbox } from './cn';

import './Checkbox.scss';

export type CheckboxProps = {
  checked?: boolean;
  hovered?: boolean;
  className?: string;
  controlSize?: 's' | 'm' | 'l';
  error?: boolean;
} & HTMLProps<HTMLInputElement>;

export const Checkbox: FC<CheckboxProps> = forwardRef(
  ({ error = false, checked = false, hovered = false, className, controlSize = 'm', ...restProps }, ref) => {
    return (
      <span className={cnCheckbox({ error, controlSize, checked, hovered }, [className])}>
        <input type="checkbox" className={cnCheckbox('Control')} checked={checked} {...restProps} ref={ref} />
        <span className={cnCheckbox('Checkmark', { controlSize })} />
      </span>
    );
  },
);
