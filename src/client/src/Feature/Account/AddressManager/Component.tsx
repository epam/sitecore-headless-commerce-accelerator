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

import React, { MouseEvent } from 'react';

import { Address } from 'Foundation/Commerce';
import { LoadingStatus } from 'Foundation/Integration';

import { Button } from 'components';
import { SafePureComponent } from 'Foundation/ReactJss';

import { AddressForm } from './components';
import { AddressManagerOwnState, AddressManagerProps } from './models';

import './styles.scss';

export default class AddressManager extends SafePureComponent<AddressManagerProps, AddressManagerOwnState> {
  public constructor(props: AddressManagerProps) {
    super(props);

    this.state = {
      editForm: false,
      isListFormVisible: true,
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
    const { savedAddressList, savedAddressListStatus } = this.props;
    const { selectedAddressId, editForm, isListFormVisible } = this.state;
    const selectedAddress = savedAddressList.find((a) => a.externalId === selectedAddressId) || savedAddressList[0];
    const isExistAddressItems = savedAddressList && savedAddressList.length > 0;
    return (
      <div className="account-details-container">
        <div className="account-details-form">
          {savedAddressListStatus === LoadingStatus.Loading && (
            <div className="address-manager-loading-overlay">
              <div className="loading" />
            </div>
          )}
          <div className="account-details-form_header header-accordion" onClick={(e) => this.toggleFormByAnchorClick()}>
            <h3 className="header-title">
              <span className="header-title_number">2. </span>
              <span>SAVED ADDRESSES</span>
              <i className="fa fa-angle-down" aria-hidden="true" />
            </h3>
          </div>
          <div className="account-details-form_main address-management-body">
            {isListFormVisible ? (
              <div className="address_body">
                <div className="myaccount-info-wrapper">
                  {isExistAddressItems && (
                    <div className="account-info-wrapper">
                      <h4>Address Book Entries</h4>
                    </div>
                  )}
                  <div className="entries-wrapper-container">
                    {isExistAddressItems ? (
                      savedAddressList.map((address, index) => {
                        return (
                          <div className="entries-wrapper" key={index}>
                            <div className="row entries_row_wrapper">
                              <div className="col-lg-6 col-md-6 entries_col_wrapper">
                                <div className="entries-info text-center">
                                  <p>
                                    {address.firstName} {address.lastName}
                                  </p>
                                  <p>{address.address1}</p>
                                  <p>{address.address2}</p>
                                  <p>
                                    {address.city}, {address.state}
                                  </p>
                                  <p>
                                    {address.country}, {address.countryCode}
                                  </p>
                                </div>
                              </div>
                              <div className="col-lg-6 col-md-6 entries_col_wrapper">
                                <div className="entries-edit-delete text-center">
                                  <Button
                                    className="AddressManager-EditButton"
                                    buttonTheme="orange"
                                    onClick={(e: MouseEvent<HTMLButtonElement>) =>
                                      this.editAddressByButtonClick(true, e)
                                    }
                                  >
                                    Edit
                                  </Button>
                                  <Button
                                    className="AddressManager-DeleteButton"
                                    buttonTheme="black"
                                    onClick={(e: MouseEvent<HTMLButtonElement>) =>
                                      this.onDeleteButtonClick(e, address.externalId)
                                    }
                                  >
                                    Delete
                                  </Button>
                                </div>
                              </div>
                            </div>
                          </div>
                        );
                      })
                    ) : (
                      <div className="address_container-empty">
                        <div className="address-icon">
                          <i className="pe-7s-way" />
                        </div>
                        <label className="address_label-empty">No saved address</label>
                        <div className="add-address-btn-container">
                          <Button
                            buttonType="submit"
                            buttonTheme="grey"
                            onClick={(e: MouseEvent<HTMLButtonElement>) => this.editAddressByButtonClick(false, e)}
                          >
                            Add
                          </Button>
                        </div>
                      </div>
                    )}
                  </div>
                  {isExistAddressItems && (
                    <div className="add-address-btn-container">
                      <Button
                        buttonType="submit"
                        buttonTheme="grey"
                        onClick={(e: MouseEvent<HTMLButtonElement>) => this.editAddressByButtonClick(false, e)}
                      >
                        Add
                      </Button>
                    </div>
                  )}
                </div>
              </div>
            ) : (
              <AddressForm
                countries={this.props.fields.countries}
                defaultValues={editForm && selectedAddress}
                SubmitAction={editForm ? this.onUpdateAdress : this.onAddAdress}
                ToggleForm={() => this.editAddress(false)}
              />
            )}
          </div>
        </div>
      </div>
    );
  }

  // private onSavedAddressChange(e: React.ChangeEvent<HTMLSelectElement>) {
  //   const { value } = e.target;
  //   this.setState({
  //     selectedAddressId: value,
  //   });
  // }

  private editAddressByButtonClick(editForm: boolean, e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();
    this.editAddress(editForm);
  }
  private editAddress(editForm: boolean) {
    this.setState({
      editForm,
      isListFormVisible: !this.state.isListFormVisible,
    });
  }
  private toggleFormByAnchorClick() {
    const lstNodeToogle = document.querySelectorAll('.account-details-form_main');
    lstNodeToogle.forEach((item) => {
      if (item.classList.contains('active') && !item.classList.contains('address-management-body')) {
        item.classList.remove('active');
      } else if (item.classList.contains('active') && item.classList.contains('address-management-body')) {
        item.classList.remove('active');
      } else if (!item.classList.contains('active') && item.classList.contains('address-management-body')) {
        item.classList.add('active');
      }
    });
  }

  private onDeleteButtonClick(e: React.MouseEvent<HTMLButtonElement>, externalId: string) {
    e.preventDefault();
    if (!confirm('Are you sure you want to delete item?')) {
      return;
    }
    this.props.RemoveAddress(externalId);
  }

  private onUpdateAdress(address: Address) {
    this.props.UpdateAddress(address);
    this.setState({ isListFormVisible: true, editForm: false });
  }

  private onAddAdress(address: Address) {
    this.props.AddAddress(address);
    this.setState({ isListFormVisible: true, editForm: false });
  }
}
