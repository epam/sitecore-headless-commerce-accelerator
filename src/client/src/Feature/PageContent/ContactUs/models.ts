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

export interface ContactPhoneDataSource extends JSS.BaseDataSourceItem {
  phone: JSS.GraphQLField<JSS.TextField>;
}

export interface ContactLinkDataSource extends JSS.BaseDataSourceItem {
  uri: JSS.GraphQLField<JSS.LinkField>;
}

export interface ContactAddressDataSource extends JSS.BaseDataSourceItem {
  address: JSS.GraphQLField<JSS.TextField>;
}

export interface ContactUsDataSource extends JSS.BaseDataSourceItem {
  stores: JSS.GraphQLListField<Store>;
  phones: JSS.GraphQLListField<ContactPhoneDataSource>;
  links: JSS.GraphQLListField<ContactLinkDataSource>;
  addresses: JSS.GraphQLListField<ContactAddressDataSource>;
}

export interface ContactUsOwnProps extends JSS.GraphQLRendering<ContactUsDataSource> {}

export interface ContactUsStateProps {
  stores: Store[];
  isLoading: boolean;
}

export interface ContactUsDispatchProps {
  GetStores: () => Store[];
  FindStores: (zipCode: string, countryCode: string, radius: number) => Store[];
}

export interface ContactUsProps
  extends JSS.GraphQLRendering<ContactUsDataSource>,
    ContactUsStateProps,
    ContactUsDispatchProps {}

export interface ContactUsState extends JSS.SafePureComponentState {
  errors: {
    [key: string]: string;
  };
  radiuses: string[];
}

export interface AppState extends GlobalCurrentStoreLocatorState, JSS.RoutingState {}
