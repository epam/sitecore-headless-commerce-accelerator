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

import { Input } from 'Foundation/ReactJss/Form';

import { Icon } from 'components';

import { cnRegister } from '../cn';
import '../Register.scss';

export type PasswordFieldProps = {
  label: string;
  id: string;
  name: string;
  disabled: boolean;
  showPassword?: boolean;
  error: boolean;
  helperText: string;
  fullWidth?: boolean;
  onClickAdornment: () => void;
  handlerFocusField?: () => void;
};

export const PasswordField: FC<PasswordFieldProps> = ({
  label,
  id,
  name,
  disabled,
  showPassword = false,
  error,
  helperText,
  fullWidth = true,
  onClickAdornment,
  handlerFocusField,
}) => {
  return (
    <>
      <label htmlFor={id}>{label}</label>
      <Input
        id={id}
        type={showPassword ? 'text' : 'password'}
        name={name}
        disabled={disabled}
        fullWidth={fullWidth}
        error={error}
        helperText={helperText}
        handlerFocusField={handlerFocusField}
        adornment={
          <div onClick={onClickAdornment}>
            <Icon icon={showPassword ? 'icon-look-slash' : 'icon-look'} className={cnRegister('FaEyeIcon')} size="l" />
          </div>
        }
      />
    </>
  );
};
