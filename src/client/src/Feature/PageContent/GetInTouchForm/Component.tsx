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
import React, { useState } from 'react';

import { Button } from 'Foundation/UI/components/Button';
import { Input } from 'Foundation/UI/components/Input';
import { validateEmail } from 'Foundation/utils/validation';

import { GetInTouchFormProps } from './models';
import './styles.scss';

export const GetInTouchFormComponent: React.FC<GetInTouchFormProps> = (props) => {
  const [values, setValues] = useState({ email: '', message: '', name: '', subject: '' });
  const [showStatusMessage, setShowStatusMessage] = useState(false);

  const { datasource } = props.fields.data;

  const handleChange = ({ target }: React.ChangeEvent<HTMLInputElement>) => {
    setShowStatusMessage(false);
    setValues({ ...values, [target.name]: target.value });
  };

  const handleBlur = ({ target }: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [target.name]: target.value.trim() });
  };

  const handleSubmit = (e: React.MouseEvent<HTMLButtonElement | HTMLAnchorElement>) => {
    e.preventDefault();
    setValues({ name: '', email: '', subject: '', message: '' });

    setTimeout(() => setShowStatusMessage(true), 1000);
  };

  return (
    <div className="contact-area_content_form">
      <div className="contact-area_content_form_title">
        <JSS.Text tag="h2" field={datasource.formTitle.jss} />
      </div>
      <form className="contact-area_content_form_style">
        <div className="row">
          <div className="col-lg-6">
            <Input
              name="name"
              placeholder={datasource.namePlaceholder.jss.value}
              type="text"
              value={values.name}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          </div>
          <div className="col-lg-6">
            <Input
              name="email"
              placeholder={datasource.emailPlaceholder.jss.value}
              type="text"
              value={values.email}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          </div>
          <div className="col-lg-12">
            <Input
              name="subject"
              placeholder={datasource.subjectPlaceholder.jss.value}
              type="text"
              value={values.subject}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          </div>
          <div className="col-lg-12">
            <textarea
              name="message"
              placeholder={datasource.subjectPlaceholder.jss.value}
              defaultValue={''}
              value={values.message}
              onChange={(e: any) => handleChange(e)}
              onBlur={(e: any) => handleBlur(e)}
            />
            <div className="Button-Container">
              <Button
                buttonType="submit"
                disabled={
                  !(
                    values.email.trim() &&
                    values.message.trim() &&
                    values.name.trim() &&
                    values.subject.trim() &&
                    validateEmail(values.email)
                  )
                }
                onClick={(e: React.MouseEvent<HTMLButtonElement | HTMLAnchorElement>) => handleSubmit(e)}
              >
                <JSS.Text field={datasource.submitButtonText.jss} />
              </Button>
              <span className="Status_Message">
                {showStatusMessage && 'Message is successfully sent! We will be in touch'}
              </span>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
};
