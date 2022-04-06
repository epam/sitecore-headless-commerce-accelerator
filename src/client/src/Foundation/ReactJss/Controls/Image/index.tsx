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

import * as ScJss from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';
import * as Models from './models';

const mapStyleToCollection = (s: string) =>
  s
    .split(';')
    .map((style) => style.split(':'))
    .reduce((acc, prop) => {
      const propertyName = prop[0].replace(/-([a-z])/g, (g) => g[1].toUpperCase()).trim();
      acc[propertyName] = prop[1].trim();
      return acc;
    }, {});

const getStyleRemoved = (strValue: string) => {
  if (!strValue) {
    return strValue;
  }

  const mediaUrlPrefixRegex = /style="[^"]+"/i;
  return strValue.replace(mediaUrlPrefixRegex, '');
};

export const Image = ({ media, ...props }: Models.ImageProps) => {
  if (!media) {
    console.warn('Media is required');
    return null;
  }

  if (media && media.value && media.value.style && typeof media.value.style === 'string') {
    media.value.style = mapStyleToCollection(media.value.style);
    media.value.style = undefined;
  }

  media.editable = getStyleRemoved(media.editable);

  return <ScJss.Image media={media} {...props} />;
};

export default Image;
