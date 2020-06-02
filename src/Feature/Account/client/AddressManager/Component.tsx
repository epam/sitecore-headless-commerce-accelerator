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

import * as React from 'react';

import { LoadingStatus } from 'Foundation/Integration/client';
import * as Jss from 'Foundation/ReactJss/client';

import { AddressForm } from './components';
import { AddressManagerOwnState, AddressManagerProps } from './models';

import * as Commerce from 'Foundation/Commerce/client';

import './styles.scss';

export default class AddressManager extends Jss.SafePureComponent<AddressManagerProps, AddressManagerOwnState> {
  public constructor(props: AddressManagerProps) {
    super(props);

    this.state = {
      editForm: false,
      formVisible: false,
      selectedAddressId: '',
    };

    this.onUpdateAdress = this.onUpdateAdress.bind(this);
    this.onAddAdress = this.onAddAdress.bind(this);
  }

  public componentDidMount() {
    const { GetAddressList } = this.props;

    GetAddressList();
  }

  protected safeRender() {
    const { savedAddressListStatus } = this.props;
    const { formVisible } = this.state;
    return (
      <section className="address-manager">
        {savedAddressListStatus === LoadingStatus.Loading && (
          <div className="address-manager-loading-overlay">
            <div className="loading" />
          </div>
        )}
        <div className="address-manager__header">
          <h2>Saved Addresses</h2>
          {!formVisible && (
            <a className="add-link" onClick={(e) => this.toggleFormByAnchorClick(false, e)}>
              Add
            </a>
          )}
        </div>
        {this.renderMain()}
      </section>
    );
  }

  private renderMain() {
    const { savedAddressList } = this.props;
    const { selectedAddressId, formVisible, editForm } = this.state;

    const selectedAddress = savedAddressList.find((a) => a.externalId === selectedAddressId) || savedAddressList[0];

    return (
      <div className="address-manager__main">
        {formVisible ? (
          <AddressForm
            countries={this.props.fields.countries}
            defaultValues={editForm && selectedAddress}
            SubmitAction={editForm ? this.onUpdateAdress : this.onAddAdress}
            ToggleForm={() => this.toggleForm(true)}
          />
        ) : (
          <>
            {savedAddressList && savedAddressList.length > 0 ? (
              <>
                <div className="address-manager__select">
                  <Jss.Text tag="label" field={{ value: 'Addresses', editable: 'Addresses' }} />
                  <select
                    onChange={(e) => this.onSavedAddressChange(e)}
                    value={selectedAddress ? selectedAddress.externalId : savedAddressList[0].externalId}
                  >
                    {savedAddressList.map((savedAddress, index) => (
                      <option value={savedAddress.externalId} key={index}>
                        {`${savedAddress.firstName} ${savedAddress.lastName}, ${savedAddress.address1}`}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="address-manager__card">
                  <Jss.Text className="card-name" tag="span" field={{ value: 'Address', editable: 'Address' }} />
                  <span>{`${selectedAddress.firstName} ${selectedAddress.lastName}`}</span>
                  <span className="address-manager__card-address">{selectedAddress.address1}</span>
                  <span className="address-manager__card-address">
                    {selectedAddress.city}, {selectedAddress.state}, {selectedAddress.country}
                  </span>
                  <span className="address-manager__card-address">{selectedAddress.zipPostalCode}</span>
                  {!formVisible && (
                    <div className="address-manager__card-actions">
                      <a className="edit-link" onClick={(e) => this.toggleFormByAnchorClick(true, e)}>
                        Edit
                      </a>
                      <a
                        className="delete-link"
                        onClick={(e) => this.onDeleteButtonClick(e, selectedAddress.externalId)}
                      >
                        Delete
                      </a>
                    </div>
                  )}
                </div>
              </>
            ) : (
              <Jss.Text tag="h4" field={{ value: 'List is empty', editable: 'List is empty' }} />
            )}
          </>
        )}
      </div>
    );
  }

  private onSavedAddressChange(e: React.ChangeEvent<HTMLSelectElement>) {
    const { value } = e.target;

    this.setState({
      selectedAddressId: value,
    });
  }

  private toggleFormByAnchorClick(editForm: boolean, e: React.MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();

    this.toggleForm(editForm);
  }

  private toggleForm(editForm: boolean) {
    this.setState({
      editForm,
      formVisible: !this.state.formVisible,
    });
  }

  private onDeleteButtonClick(e: React.MouseEvent<HTMLAnchorElement>, externalId: string) {
    e.preventDefault();

    if (!confirm('Are you sure you want to delete item?')) {
      return;
    }

    this.props.RemoveAddress(externalId);
  }

  private onUpdateAdress(address: Commerce.AddressModel) {
    this.props.UpdateAddress(address);
    this.setState({ formVisible: false });
  }

  private onAddAdress(address: Commerce.AddressModel) {
    this.props.AddAddress(address);
    this.setState({ formVisible: false });
  }
}
