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

import { cnRadioButton } from './cn';

import './RadioButton.scss';

export type RadioButtonProps = HTMLProps<HTMLInputElement> & {
  controlSize?: 'm';
};

export const RadioButton: FC<RadioButtonProps> = forwardRef(
  ({ checked, className, controlSize = 'm', ...rest }, ref) => {
    return (
      <div className={cnRadioButton({ controlSize }, [className])} tabIndex={0}>
        <input {...rest} checked={checked} className={cnRadioButton('Input')} type="radio" ref={ref} />
        <div className={cnRadioButton('Circle', { checked })} tabIndex={-1} />
      </div>
    );
  },
);
