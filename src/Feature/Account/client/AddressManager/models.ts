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

import * as Account from 'Feature/Account/client/Integration/Account';
import * as Commerce from 'Foundation/Commerce/client';
import { LoadingStatus } from 'Foundation/Integration/client';
import * as Jss from 'Foundation/ReactJss/client';

export interface AddressDataSource extends Jss.BaseDataSourceItem {
  countries: Commerce.CountryRegionModel[];
}

export interface AddressManagerOwnProps extends Jss.Rendering<AddressDataSource> {}

export interface AddressManagerStateProps {
  savedAddressList: Commerce.AddressModel[];
  savedAddressListStatus: LoadingStatus;
}

export interface AddressManagerDispatchProps {
  GetAddressList: () => void;
  UpdateAddress: (address: Commerce.AddressModel) => void;
  AddAddress: (address: Commerce.AddressModel) => void;
  RemoveAddress: (address: Commerce.AddressModel) => void;
}

export interface AddressManagerProps
  extends AddressManagerOwnProps,
    AddressManagerStateProps,
    AddressManagerDispatchProps {}

export interface AddressManagerOwnState extends Jss.SafePureComponentState {
  selectedAddressId: string;
  formVisible: boolean;
  editForm: boolean;
}

export interface AppState extends Account.GlobalAccountState {}
