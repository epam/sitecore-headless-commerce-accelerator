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

import React, { ChangeEvent, FC, HTMLProps, KeyboardEvent, useCallback } from 'react';

import { Button, Icon, Input } from 'components';

import { cnSidebarSearch } from './cn';
import './SidebarSearch.scss';

export type SidebarSearchProps = HTMLProps<HTMLDivElement> & {
  disabled: boolean;
  value: string;
  onChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onClear: () => void;
  onSubmit: () => void;
};

export const SidebarSearch: FC<SidebarSearchProps> = ({ className, disabled, value, onChange, onClear, onSubmit }) => {
  const onKeyDown = useCallback(
    (e: KeyboardEvent<HTMLInputElement>) => {
      if (e.key === 'Enter') {
        onSubmit();
      }
    },
    [onSubmit],
  );

  return (
    <div className={cnSidebarSearch(null, [className])}>
      <h4 className={cnSidebarSearch('Title')}>Search</h4>
      <div className={cnSidebarSearch('FormContainer')}>
        <Input
          fullWidth={true}
          onChange={onChange}
          onKeyDown={onKeyDown}
          className="Input_search"
          value={value}
          disabled={disabled}
          placeholder="Search"
        />
        <Button buttonType="reset" className={cnSidebarSearch('Button')} onClick={onClear}>
          <Icon icon="icon-close" size="xl" />
        </Button>
      </div>
    </div>
  );
};
