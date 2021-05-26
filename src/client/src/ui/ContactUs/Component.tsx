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

import { Link, Placeholder, Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';
import {
  ContactAddressDataSource,
  ContactLinkDataSource,
  ContactPhoneDataSource,
  ContactUsProps,
  ContactUsState,
} from './models';

import * as JSS from 'Foundation/ReactJss';
import './styles.scss';

export class ContactUsComponent extends JSS.SafePureComponent<ContactUsProps, ContactUsState> {
  public constructor(props: ContactUsProps) {
    super(props);
    this.state = {
      errors: {},
      radiuses: [],
    };
  }
  public componentDidMount() {
    this.props.GetStores();
  }

  protected safeRender() {
    const { datasource } = this.props.fields.data;

    return (
      <div className="contact-area">
        <div className="contact-intro-area">
          <Placeholder name="contact-intro" rendering={this.props.rendering} />
        </div>
        <div className="contact-area_content_container row">
          <div className="contact-area_content col-lg-4 col-md-5">
            <div className="contact-area_content_info">
              <div className="single-contact-info">
                <div className="contact-icon">
                  <i className="fa fa-phone" />
                </div>
                <div className="contact-info-dec">
                  {datasource &&
                    datasource.phones &&
                    datasource.phones.items &&
                    datasource.phones.items.map((contactPhone: ContactPhoneDataSource, index: number) => (
                      <p key={contactPhone.id}>
                        <Text field={contactPhone.phone.jss} />
                      </p>
                    ))}
                </div>
              </div>
              <div className="single-contact-info">
                <div className="contact-icon">
                  <i className="fa fa-globe" />
                </div>
                <div className="contact-info-dec">
                  {datasource &&
                    datasource.links &&
                    datasource.links.items &&
                    datasource.links.items.map((contactLink: ContactLinkDataSource, index: number) => (
                      <p key={contactLink.id}>
                        <Link field={contactLink.uri.jss} />
                      </p>
                    ))}
                </div>
              </div>
              <div className="single-contact-info">
                <div className="contact-icon">
                  <i className="fa fa-map-marker" />
                </div>
                <div className="contact-info-dec">
                  {datasource &&
                    datasource.addresses &&
                    datasource.addresses.items &&
                    datasource.addresses.items.map((contactAddress: ContactAddressDataSource, index: number) => (
                      <p key={contactAddress.id}>
                        <Text field={contactAddress.address.jss} />
                      </p>
                    ))}
                </div>
              </div>
              <div className="contact-social text-center">
                <Placeholder name="contact-social-networks" rendering={this.props.rendering} />
              </div>
            </div>
          </div>
          <div className="contact-area_content col-lg-8 col-md-7">
            <Placeholder name="get-in-touch-form" rendering={this.props.rendering} />
          </div>
        </div>
      </div>
    );
  }
}
