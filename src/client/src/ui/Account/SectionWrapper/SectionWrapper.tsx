//    Copyright 2022 EPAM Systems, Inc.
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

import React from 'react';

import { NavigationLink } from 'ui/NavigationLink';

import { cnSectionWrapper } from './cn';
import './SectionWrapper.scss';

type SectionWrapperProps = {
  title: string;
  path: string;
  children: JSX.Element;
  contentType?: string;
  itemsCount?: number;
  linkText?: string;
  isLinkVisible?: boolean;
};

export const SectionWrapper = (props: SectionWrapperProps) => {
  const { title, children, path, contentType, itemsCount, linkText, isLinkVisible = true } = props;
  const sectionTitle = itemsCount ? title + ' (' + itemsCount + ') ' : title;

  return (
    <div className={cnSectionWrapper()}>
      <div className={cnSectionWrapper('Header')}>
        <div className={cnSectionWrapper('Title')}>{sectionTitle}</div>
        {isLinkVisible && (
          <NavigationLink className={cnSectionWrapper('Link', { marginTop: true })} to={path}>
            {linkText}
          </NavigationLink>
        )}
      </div>
      <div className={cnSectionWrapper('Content', { contentType })}>{children}</div>
    </div>
  );
};
