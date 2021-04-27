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

import React, { memo } from 'react';

import { StarIcon } from 'components/StarIcon';

import { cnRating } from './cn';
import './Rating.scss';

export type RatingProps = {
  className?: string;
  maxRating?: number;
  rating?: number;
  theme?: 'default';
  size?: 'm';
};

export const Rating = memo<RatingProps>(({ className, size = 'm', maxRating = 5, rating = 0, theme = 'default' }) => {
  const Stars = [];

  for (let i = 0; i < maxRating; i++) {
    Stars.push(<StarIcon key={i} className={cnRating('Star', { active: i < rating })} />);
  }

  return <div className={cnRating({ theme, size }, [className])}>{Stars}</div>;
});
