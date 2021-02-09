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

import * as JSS from 'Foundation/ReactJss';
import React, { ChangeEvent } from 'react';

import { validateEmail } from 'Foundation/utils/validation';

import { GetInTouchFormProps, GetInTouchFormState } from './models';
import './styles.scss';

export class GetInTouchFormComponent extends JSS.SafePureComponent<GetInTouchFormProps, GetInTouchFormState> {
  public constructor(props: GetInTouchFormProps) {
    super(props);

    this.state = {
      email: '',
      formSubmission: false,
      message: '',
      name: '',
      subject: '',
    };
  }

  public handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const name = e.currentTarget.name;
    this.setState({ ...this.state, [name]: e.currentTarget.value.trim(), formSubmission: false });
  };

  protected safeRender() {
    const { datasource } = this.props.fields.data;
    const { email, formSubmission, message, name, subject } = this.state;
    const areFieldsFilled = message && email && name && subject && validateEmail(email);
    return (
      <div className="contact-area_content_form">
        <div className="contact-area_content_form_title">
          <JSS.Text tag="h2" field={datasource.formTitle.jss} />
        </div>
        <form className="contact-area_content_form_style">
          <div className="row">
            <div className="col-lg-6">
              <input
                name="name"
                placeholder={datasource.namePlaceholder.jss.value}
                type="text"
                value={name}
                onChange={this.handleChange}
              />
            </div>
            <div className="col-lg-6">
              <input
                name="email"
                placeholder={datasource.emailPlaceholder.jss.value}
                type="text"
                value={email}
                onChange={this.handleChange}
              />
            </div>
            <div className="col-lg-12">
              <input
                name="subject"
                placeholder={datasource.subjectPlaceholder.jss.value}
                type="text"
                value={subject}
                onChange={this.handleChange}
              />
            </div>
            <div className="col-lg-12">
              <textarea
                name="message"
                placeholder={datasource.subjectPlaceholder.jss.value}
                defaultValue={''}
                value={message}
                onChange={this.handleChange}
              />
              <div className="Button-Container">
                <button
                  className="submit"
                  type="submit"
                  disabled={!areFieldsFilled}
                  onClick={(e) => this.submitContactForm(e)}
                >
                  <JSS.Text field={datasource.submitButtonText.jss} />
                </button>
                <span className="Status_Message">
                  {formSubmission && 'Message is successfully sent! We will be in touch'}
                </span>
              </div>
            </div>
          </div>
        </form>
        <p className="form-message" />
      </div>
    );
  }

  private submitContactForm(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();
    this.setState({ name: '', email: '', subject: '', message: '' });

    setTimeout(() => this.setState({ formSubmission: true }), 1000);
  }
}
