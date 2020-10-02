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
import { GlobalCurrentStoreLocatorState } from './Integration/StoreLocator';

export interface Coordinates {
  latitude: number;
  longitude: number;
}
export interface Store {
  title: string;
  description: string;
  latitude: number;
  longitude: number;
}

export interface Radius {
  value: Jss.GraphQLField<Jss.TextField>;
}

export interface Country {
  title: Jss.GraphQLField<Jss.TextField>;
  code: Jss.GraphQLField<Jss.TextField>;
}

export interface StoreLocatorDataSource extends Jss.BaseDataSourceItem {
  title: Jss.GraphQLField<Jss.TextField>;
  description: Jss.GraphQLField<Jss.TextField>;
  defaultLatitude: Jss.GraphQLField<Jss.TextField>;
  defaultLongitude: Jss.GraphQLField<Jss.TextField>;
  stores: Jss.GraphQLListField<Store>;
  radiuses: Jss.GraphQLListField<Radius>;
  countries: Jss.GraphQLListField<Country>;
}

export interface StoreLocatorDispatchProps {
  GetStores: () => Store[];
  FindStores: (zipCode: string, countryCode: string, radius: number) => Store[];
}

export interface StoreLocatorStateProps {
  stores: Store[];
  isLoading: boolean;
}

export interface StoreLocatorProps
  extends Jss.GraphQLRendering<StoreLocatorDataSource>,
    StoreLocatorStateProps,
    StoreLocatorDispatchProps {}
export interface StoreLocatorState extends Jss.SafePureComponentState {
  errors: {
    [key: string]: string;
  };
}

export interface AppState extends GlobalCurrentStoreLocatorState, Jss.RoutingState {}
