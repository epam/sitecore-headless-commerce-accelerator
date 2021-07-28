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

import React, { FC, ReactNode } from 'react';

import { Icon } from 'components';

import { cnAdvantages } from '../cn';

export type ItemProps = {
  item?: any;
  children?: ReactNode;
};

export const Item: FC<ItemProps> = ({ item }) => {
  const iconName: any = `icon-${item.image.jss.value}`;
  return (
    <li className={cnAdvantages('Item')}>
      <Icon icon={iconName} size="xxxl" />
      <div className={cnAdvantages('Title')}>{item.title.jss.value}</div>
      <div className={cnAdvantages('Description')}>{item.description.jss.value}</div>
    </li>
  );
};
