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

import { cnSelect } from './cn';

import './Select.scss';

export type SelectProps = HTMLProps<HTMLSelectElement> & {
  controlSize?: 'm';
  error?: boolean;
  fullWidth?: boolean;
  helperText?: string;
};

export const Select: FC<SelectProps> = forwardRef(
  (
    { children, className, disabled = false, helperText, controlSize = 'm', error = false, fullWidth = false, ...rest },
    ref,
  ) => {
    return (
      <div className={cnSelect({ error })}>
        <select
          {...rest}
          className={cnSelect('SelectWrapper', { controlSize, error, disabled, fullWidth }, [className])}
          disabled={disabled}
          ref={ref}
        >
          {children}
        </select>
        {helperText && <p className={cnSelect('HelperText')}>{helperText}</p>}
      </div>
    );
  },
);
