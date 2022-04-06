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

import React, { FC, useCallback, useState } from 'react';

import { Button } from 'components/Button';

import { cnQuantityPicker } from './cn';
import './QuantityPicker.scss';

export type QuantityPickerProps = {
  className?: string;
  value?: number;
  step?: number;
  disabled?: boolean;
  theme?: 'withBorder' | 'grey';
  size?: 'm' | 'l';
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
  const [valueInput, setValueInput] = useState(value);
  const handleDecreaseClick = useCallback(() => {
    onChange(valueInput - step);
    setValueInput((v) => v - step);
  }, [valueInput, step, onChange]);

  const handleIncreaseClick = useCallback(() => {
    onChange(valueInput + step);
    setValueInput((v) => v + step);
  }, [valueInput, step, onChange]);

  const disabledDecrease = disabled || valueInput <= min;
  const disabledIncrease = disabled || valueInput >= max;

  const setCount = (e: any) => {
    onChange(+e.target.value);
    setValueInput(+e.target.value);
    e.target.blur();
  };

  const handleInputValueChange = useCallback((e: any) => {
    e.target.value = e.target.value.replace(/[^0-9]/g, '');
    setValueInput(+e.target.value);

    if (+e.target.value > max) {
      setValueInput(max);
    }

    if (e.target.value.charAt(0) === '0') {
      const num = e.target.value.slice(1);
      +num > max ? setValueInput(max) : setValueInput(+num);
    }

    if (e.key === 'Enter') {
      if (+e.target.value < min) {
        setValueInput(min);
        e.target.value = min;
      }
      setCount(e);
    }
  }, []);

  const handleBlur = useCallback((e) => {
    if (+e.target.value < min) {
      setValueInput(min);
      e.target.value = min;
    }

    setCount(e);
  }, []);

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
      <input
        className={cnQuantityPicker('Field')}
        type="text"
        value={valueInput}
        onKeyDown={(e) => handleInputValueChange(e)}
        onChange={handleInputValueChange}
        onBlur={handleBlur}
      />
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
