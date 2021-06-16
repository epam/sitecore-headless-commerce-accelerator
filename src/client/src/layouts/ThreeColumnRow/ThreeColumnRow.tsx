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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import { get } from 'lodash';

import { getRenderingSitecoreProps } from 'utils';

import { GridProps } from '../models';

import classnames from 'classnames';

export const ThreeColumnRow: FC<GridProps> = (props) => {
  const rendering = getRenderingSitecoreProps(props);
  const wrapperClass = get(props, ['params', 'wrapperClass']);
  const firstColumnClass = get(props, ['params', 'firstColumnClass']);
  const secondColumnClass = get(props, ['params', 'secondColumnClass']);
  const thirdColumnClass = get(props, ['params', 'thirdColumnClass']);

  return (
    <div className={classnames('row', { [wrapperClass]: !!wrapperClass })}>
      <div className={firstColumnClass || 'col-xs-4'}>
        <Placeholder name="w-col-1" rendering={rendering} />
      </div>
      <div className={secondColumnClass || 'col-xs-4'}>
        <Placeholder name="w-col-2" rendering={rendering} />
      </div>
      <div className={thirdColumnClass || 'col-xs-4'}>
        <Placeholder name="w-col-3" rendering={rendering} />
      </div>
    </div>
  );
};
