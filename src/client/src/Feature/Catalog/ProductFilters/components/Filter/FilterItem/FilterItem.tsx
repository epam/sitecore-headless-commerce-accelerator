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

import React, { ChangeEvent, FC, useCallback, useState } from 'react';

import { Checkbox } from 'components';

import { cnFilterItem } from './cn';
import './FilterItem.scss';

type Props = {
  checked: boolean;
  onChange: (e: ChangeEvent<HTMLInputElement>) => void;
  title: string;
  total: number;
};

export const FilterItem: FC<Props> = ({ checked, title, total, onChange }) => {
  const [hovered, setHovered] = useState(false);

  const handleMouseEnter = useCallback(() => {
    setHovered(true);
  }, []);

  const handleMouseLeave = useCallback(() => {
    setHovered(false);
  }, []);

  return (
    <label className={cnFilterItem()} onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}>
      <Checkbox controlSize="m" id={`${title}${total}`} checked={checked} hovered={hovered} onChange={onChange} />
      <label className={cnFilterItem('Name')} htmlFor={`${title}${total}`} title={title}>
        {title}
      </label>
      <span className={cnFilterItem('Total', { hovered })}>{total}</span>
    </label>
  );
};
