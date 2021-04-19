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

import { cnInput } from './cn';

import './Input.scss';

export type InputType = 'text' | 'password' | 'number' | 'email' | 'search';

export type InputProps = HTMLProps<HTMLInputElement> & {
  error?: boolean;
  helperText?: string;
  fullWidth?: boolean;
  controlSize?: 'm';
  type?: InputType;
  adornment?: React.ReactElement;
};

export const Input: FC<InputProps> = forwardRef(
  (
    { className, error = false, helperText, fullWidth = false, controlSize = 'm', type = 'text', adornment, ...rest },
    ref,
  ) => {
    return (
      <div className={cnInput({ error, fullWidth, controlSize }, [className])}>
        <div className={cnInput('ControlWrapper')}>
          <input {...rest} className={cnInput('Control')} type={type} ref={ref} />
          {adornment && <div className={cnInput('Adornment')}>{adornment}</div>}
        </div>
        {helperText && <p className={cnInput('HelperText')}>{helperText}</p>}
      </div>
    );
  },
);
