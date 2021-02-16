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

import React, { FC, FocusEventHandler, KeyboardEventHandler, MouseEventHandler } from 'react';
import { cnButton } from './cn';

import './Button.scss';

type ThemeTypes =
  | 'default'
  | 'defaultReversed'
  | 'defaultSlide'
  | 'transparentSlide'
  | 'grey'
  | 'greyReversed'
  | 'orange'
  | 'black';
type ButtonTypes = 'link' | 'submit' | 'button' | 'reset';

type ContainerElement = HTMLButtonElement | HTMLAnchorElement;

export type ButtonProps = {
  id?: string;
  className?: string;
  disabled?: boolean;
  href?: string;
  target?: string;
  buttonType?: ButtonTypes;
  title?: string;
  buttonTheme?: ThemeTypes;
  buttonSize?: 's' | 'm' | 'l';
  rounded?: boolean;
  fullWidth?: boolean;

  onKeyDown?: KeyboardEventHandler<ContainerElement>;
  onKeyUp?: KeyboardEventHandler<ContainerElement>;
  onClick?: MouseEventHandler<ContainerElement>;
  onMouseDown?: MouseEventHandler<ContainerElement>;
  onMouseUp?: MouseEventHandler<ContainerElement>;
  onMouseLeave?: MouseEventHandler<ContainerElement>;
  onBlur?: FocusEventHandler<ContainerElement>;
};

export const Button: FC<ButtonProps> = ({
  className,
  children,
  buttonType = 'button',

  buttonTheme = 'default',
  buttonSize = 'm',
  href,
  target,
  rounded,
  fullWidth,
  ...rest
}) => {
  return buttonType === 'link' ? (
    <a
      {...rest}
      className={cnButton({ buttonTheme, buttonSize, rounded, fullWidth }, [className])}
      href={href}
      target={target}
    >
      {children}
    </a>
  ) : (
    <button
      {...rest}
      className={cnButton({ buttonTheme, buttonSize, rounded, fullWidth }, [className])}
      type={buttonType}
    >
      {children}
    </button>
  );
};
