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

import React, { FC } from 'react';
import { BaseDataSourceItem, ItemList, RenderingWithContext, TextField } from 'Foundation/ReactJss';
import { Text } from '@sitecore-jss/sitecore-jss-react';

export interface NoResultsFoundData extends BaseDataSourceItem {
  headerLine: TextField;
  tipsHeader: TextField;
  tips: ItemList<Tip>;
}

export interface Tip extends BaseDataSourceItem {
  tip: TextField;
}

export type NoResultsFoundProps = RenderingWithContext<NoResultsFoundData>;

export const NoResultsFound: FC<NoResultsFoundProps> = (props) => {
  const { headerLine, tipsHeader, tips } = props.fields;

  return (
    <div className="not-found">
      <Text tag="h3" field={headerLine} />
      <Text tag="h4" field={tipsHeader} />
      <ul>
        {tips.map((tip: any) => {
          return <Text key={tip.id} tag="li" field={tip.fields.tip} />;
        })}
      </ul>
    </div>
  );
};
