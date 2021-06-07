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

import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { Input } from 'Foundation/ReactJss/Form';

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
        adornment={
          <FontAwesomeIcon
            icon={showPassword ? faEyeSlash : faEye}
            className={cnRegister('FaEyeIcon')}
            size="lg"
            onClick={onClickAdornment}
          />
        }
      />
    </>
  );
};
