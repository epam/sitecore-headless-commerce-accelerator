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

import { OperationVariables, QueryHookOptions, QueryResult, useQuery } from '@apollo/client';
import { getMainDefinition } from '@apollo/client/utilities';
import { DocumentNode } from 'graphql';
import { get, set } from 'lodash';

import { useSitecoreContext } from './useSitecoreContext';

export const useQueryWithSitecore = (
  query: DocumentNode,
  options: QueryHookOptions<any, OperationVariables> = {},
  rendering?: any,
): QueryResult => {
  const sitecoreContext = useSitecoreContext();
  const newOptions: QueryHookOptions = { ...options };
  const definition = getMainDefinition(query);

  if (definition.kind === 'OperationDefinition' && definition.operation === 'subscription') {
    newOptions['ssr'] = false;
  }

  if (sitecoreContext.itemId) {
    set(newOptions, ['variables', 'contextItem'], sitecoreContext.itemId);
  }

  const dataSource = get(rendering, 'dataSource');

  if (dataSource) {
    set(newOptions, ['variables', 'datasource'], dataSource);
  }

  return useQuery(query, newOptions);
};
