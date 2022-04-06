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

import { ClubPointsProps } from './models';

import { cnClubPoints } from './cn';
import { cnPersonalInformation } from '../PersonalInformation/cn';
import './ClubPoints.scss';

const CLUB_POINTS_CONTENT = {
  TITLE: 'Club points',
  LINK: 'how it works?',
};

export const ClubPoints = (props: ClubPointsProps) => {
  const { clubPoints } = props;
  const path = 'MyAccount/clubPoints';

  return (
    <div className={cnClubPoints()}>
      <div className={cnClubPoints('Title')}>{CLUB_POINTS_CONTENT.TITLE}</div>
      <div className={cnClubPoints('Count')}>{clubPoints}</div>
      <NavigationLink className={cnPersonalInformation('Link', { clubPoints: true })} to={path}>
        {CLUB_POINTS_CONTENT.LINK}
      </NavigationLink>
    </div>
  );
};
