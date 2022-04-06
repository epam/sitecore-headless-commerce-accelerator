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

import React from 'react';

import { storiesOf } from '@storybook/react';

import { Select } from 'components/Select';

storiesOf('Select', module)
  .add('Default', () => (
    <Select id="country" name="country">
      <option value="">Not Selected</option>
      <option value="CA">Canada</option>
      <option value="US">United States</option>
    </Select>
  ))
  .add('With error', () => (
    <Select id="country" name="country" error={true}>
      <option value="">Not Selected</option>
      <option value="CA">Canada</option>
      <option value="US">United States</option>
    </Select>
  ))
  .add('Full width', () => (
    <Select id="country" name="country" fullWidth={true}>
      <option value="">Not Selected</option>
      <option value="CA">Canada</option>
      <option value="US">United States</option>
    </Select>
  ))
  .add('Disabled', () => (
    <Select id="country" name="country" disabled={true}>
      <option value="">Not Selected</option>
    </Select>
  ));
