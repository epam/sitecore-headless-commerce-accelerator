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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';
import { ContactUsProps, ContactUsState } from './models';

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

  protected safeRender() {
    return (
      <div className="contact-area">
        <div className="contact-area_content_container row">
          <div className="contact-area_content col-lg-4 col-md-5">
              <div className="contact-area_content_info">
                <div className="single-contact-info">
                  <div className="contact-icon">
                    <i className="fa fa-phone" />
                  </div>
                  <div className="contact-info-dec">
                    <p>+012 345 678 102</p>
                    <p>+012 345 678 102</p>
                  </div>
                </div>
                <div className="single-contact-info">
                  <div className="contact-icon">
                    <i className="fa fa-globe" />
                  </div>
                  <div className="contact-info-dec">
                    <p>
                      <a href="mailto:urname@email.com">urname@email.com</a>
                    </p>
                    <p>
                      <a href="//urwebsitenaem.com">urwebsitenaem.com</a>
                    </p>
                  </div>
                </div>
                <div className="single-contact-info">
                  <div className="contact-icon">
                    <i className="fa fa-map-marker" />
                  </div>
                  <div className="contact-info-dec">
                    <p>Address goes here, </p>
                    <p>street, Crossroad 123.</p>
                  </div>
                </div>
                <div className="contact-social text-center">
                  <h3>Follow Us</h3>
                  <Placeholder name="contact-social_lists" rendering={this.props.rendering} />
                </div>
              </div>
          </div>
          <div className="contact-area_content col-lg-8 col-md-7">
            <div className="contact-area_content_form">
              <div className="contact-area_content_form_title">
                <h2>Get In Touch</h2>
              </div>
              <form className="contact-area_content_form_style">
                <div className="row">
                  <div className="col-lg-6">
                    <input name="name" placeholder="Name*" type="text" />
                  </div>
                  <div className="col-lg-6">
                    <input name="email" placeholder="Email*" type="email" />
                  </div>
                  <div className="col-lg-12">
                    <input
                      name="subject"
                      placeholder="Subject*"
                      type="text"
                    />
                  </div>
                  <div className="col-lg-12">
                    <textarea
                      name="message"
                      placeholder="Your Message*"
                      defaultValue={''}
                    />
                    <button className="submit" type="submit">
                      SEND
                    </button>
                  </div>
                </div>
              </form>
              <p className="form-messege" />
            </div>
          </div>
        </div>
      </div>
    );
  }
}