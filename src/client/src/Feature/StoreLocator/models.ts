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

import * as Jss from 'Foundation/ReactJss';

export interface Coordinates {
  latitude: number;
  longitude: number;
}
export interface Store {
  title: Jss.GraphQLField<Jss.TextField>;
  description: Jss.GraphQLField<Jss.TextField>;
  latitude: Jss.GraphQLField<Jss.TextField>;
  longitude: Jss.GraphQLField<Jss.TextField>;
}

export interface StoreLocatorDataSource extends Jss.BaseDataSourceItem {
  title: Jss.GraphQLField<Jss.TextField>;
  description: Jss.GraphQLField<Jss.TextField>;
  defaultLatitude: Jss.GraphQLField<Jss.TextField>;
  defaultLongitude: Jss.GraphQLField<Jss.TextField>;
  stores: Jss.GraphQLListField<Store>;
}

export interface StoreLocatorProps extends Jss.GraphQLRendering<StoreLocatorDataSource> {}

export interface StoreLocatorOwnState extends Jss.SafePureComponentState {}