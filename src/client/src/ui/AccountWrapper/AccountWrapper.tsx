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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import { NavigationLink } from 'ui/NavigationLink';

import { AccountWrapperProps } from './models';

import { cnAccountWrapper } from './cn';
import './AccountWrapper.scss';

const TITLE_ALIGN = {
  LEFT: 'left',
  CENTER: 'center',
};

export const AccountWrapper = (props: AccountWrapperProps) => {
  const {
    children,
    title,
    isAccountDetails,
    isLeaveLink,
    leaveLinkText,
    leaveLinkPath,
    rendering,
    isTitleHiddenOnMobile,
  } = props;

  const titleAlign = isAccountDetails ? TITLE_ALIGN.CENTER : TITLE_ALIGN.LEFT;

  return (
    <div className={cnAccountWrapper()}>
      {isLeaveLink && (
        <span className={cnAccountWrapper('LeaveLink')}>
          <NavigationLink to={leaveLinkPath}>{leaveLinkText}</NavigationLink>
        </span>
      )}
      <div className={cnAccountWrapper('Title', { isHiddenOnMobile: isTitleHiddenOnMobile, align: titleAlign })}>
        {title}
      </div>
      <div className={cnAccountWrapper('Content')}>
        {rendering && (
          <div className={cnAccountWrapper('LeftMenu')}>
            <Placeholder name="left-menu" rendering={rendering} />
          </div>
        )}
        <div className={cnAccountWrapper('Main', { top: isAccountDetails })}>{children}</div>
      </div>
    </div>
  );
};
