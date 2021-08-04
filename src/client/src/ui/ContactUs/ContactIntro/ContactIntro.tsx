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

import React, { FC } from 'react';

import { RenderingWithContext } from 'Foundation/ReactJss';
import { ContactIntroDataSource } from 'services/pageContent/models/generated';
import { Image, Text } from '@sitecore-jss/sitecore-jss-react';

import '../styles.scss';

type ContactIntroProps = RenderingWithContext<ContactIntroDataSource>;

export const ContactIntro: FC<ContactIntroProps> = (props) => {
  const { image, introLine } = props.fields;
  image.value.height = '100%';
  image.value.width = '100%';

  return (
    <div className="contact-area_content">
      <div className="contact-area_text">
        <Text tag="p" field={introLine} />
      </div>
      <div className="contact-intro_image">
        <Image media={image} alt="contact-us" />
      </div>
    </div>
  );
};
