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

import React, { ButtonHTMLAttributes, FC, HTMLProps } from 'react';
import { cnButton } from './cn';

import './Button.scss';

type ThemeTypes = 'default' | 'default-reversed' | 'grey' | 'grey-reversed' | 'orange' | 'black';
type Types = 'link' | 'submit' | 'button' | 'reset';

export type ButtonProps = {
  buttonSize?: 's' | 'm' | 'l';
  buttonTheme?: ThemeTypes;
  buttonType?: Types;
  rounded?: boolean;
  fullWidth?: boolean;
} & HTMLProps<HTMLButtonElement> &
  HTMLProps<HTMLAnchorElement> &
  ButtonHTMLAttributes<HTMLButtonElement>;

export const Button: FC<ButtonProps> = ({
  children,
  className,
  buttonSize = 'm',
  buttonTheme = 'default',
  buttonType = 'button',
  rounded = false,
  fullWidth = false,
  ...restProps
}) => {
  return buttonType === 'link' ? (
    <a className={cnButton({ buttonSize, buttonTheme, rounded, fullWidth }, [className])} {...restProps}>
      {children}
    </a>
  ) : (
    <button
      type={buttonType}
      className={cnButton({ buttonSize, buttonTheme, rounded, fullWidth }, [className])}
      {...restProps}
    >
      {children}
    </button>
  );
};
