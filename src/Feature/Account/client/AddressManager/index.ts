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

import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import { rendering } from 'Foundation/ReactJss/client';

import * as Account from 'Feature/Account/client/Integration/Account';

import Component from './Component';
import { AddressManagerDispatchProps, AddressManagerOwnProps, AddressManagerStateProps, AppState } from './models';

const mapStateToProps = (state: AppState) => {
  const savedAddressListStatus = Account.savedAddressList(state).status;
  const savedAddressListItems = Account.savedAddressList(state).items;
  const savedAddressList = Object.keys(savedAddressListItems).map((externalId) => savedAddressListItems[externalId]);

  return {
    savedAddressList,
    savedAddressListStatus,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      AddAddress: Account.AddAddress,
      GetAddressList: Account.GetAddressList,
      RemoveAddress: Account.RemoveAddress,
      UpdateAddress: Account.UpdateAddress,
    },
    dispatch
  );

const connectedToStore = connect<AddressManagerStateProps, AddressManagerDispatchProps, AddressManagerOwnProps>(
  mapStateToProps,
  mapDispatchToProps
);

export const AddressManager = compose(connectedToStore, rendering)(Component);
