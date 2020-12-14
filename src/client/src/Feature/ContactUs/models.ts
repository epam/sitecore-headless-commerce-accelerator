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

import { GlobalCurrentStoreLocatorState } from 'Feature/StoreLocator/Integration/StoreLocator';
import * as JSS from 'Foundation/ReactJss';

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

export interface ContactUsDataSource extends JSS.BaseDataSourceItem {
  stores: JSS.GraphQLListField<Store>;
}

export interface ContactUsStateProps {
  stores: Store[];
  isLoading: boolean;
}

export interface ContactUsDispatchProps {
  GetStores: () => Store[];
  FindStores: (zipCode: string, countryCode: string, radius: number) => Store[];
}

export interface ContactUsProps
  extends JSS.RenderingWithContext<JSS.BaseDataSourceItem, ContactUsDataSource>,
    ContactUsStateProps,
    ContactUsDispatchProps {}

export interface ContactUsState extends JSS.SafePureComponentState {
  errors: {
    [key: string]: string;
  };
  radiuses: string[];
}

export interface AppState extends GlobalCurrentStoreLocatorState, JSS.RoutingState {}
