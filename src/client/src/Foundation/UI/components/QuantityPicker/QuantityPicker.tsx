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

import React, { FC, useCallback } from 'react';

import { Button } from 'Foundation/UI/components/Button';

import { cnQuantityPicker } from './cn';
import './QuantityPicker.scss';

export type QuantityPickerProps = {
  className?: string;
  value?: number;
  step?: number;
  disabled?: boolean;
  theme?: 'withBorder';
  size?: 'm';
  min?: number;
  max?: number;

  onChange?: (value: number) => void;
};

export const QuantityPicker: FC<QuantityPickerProps> = ({
  className,
  value = 0,
  step = 1,
  disabled = false,
  theme,
  size = 'm',

  min = 0,
  max = Number.POSITIVE_INFINITY,

  onChange,
}) => {
  const handleDecreaseClick = useCallback(() => {
    onChange(value - step);
  }, [value, step, onChange]);

  const handleIncreaseClick = useCallback(() => {
    onChange(value + step);
  }, [value, step, onChange]);

  const disabledDecrease = disabled || value <= min;
  const disabledIncrease = disabled || value >= max;

  return (
    <div className={cnQuantityPicker({ theme, disabled, size }, [className])}>
      <Button
        buttonTheme="clear"
        className={cnQuantityPicker('DecreaseButton')}
        onClick={handleDecreaseClick}
        disabled={disabledDecrease}
      >
        -
      </Button>
      <div className={cnQuantityPicker('Field')}>{value}</div>
      <Button
        buttonTheme="clear"
        className={cnQuantityPicker('IncreaseButton')}
        onClick={handleIncreaseClick}
        disabled={disabledIncrease}
      >
        +
      </Button>
    </div>
  );
};
