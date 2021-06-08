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

import { cnIcon } from './cn';
import './Icon.scss';

export type IconProps = {
  className?: string;
  // tslint:disable:max-union-size
  icon:
    | 'icon-print'
    | 'icon-quality'
    | 'icon-delivery'
    | 'icon-support'
    | 'icon-sales'
    | 'icon-minus'
    | 'icon-plus'
    | 'icon-angle-down'
    | 'icon-angle-double-up'
    | 'icon-angle-double-right'
    | 'icon-angle-double-left'
    | 'icon-long-arrow'
    | 'icon-note2'
    | 'icon-cart'
    | 'icon-spinner-solid'
    | 'icon-google-4'
    | 'icon-google-1'
    | 'icon-google-2'
    | 'icon-google-3'
    | 'icon-instagram-brands'
    | 'icon-facebook-f-brands'
    | 'icon-youtube-brands'
    | 'icon-dribbble-brands'
    | 'icon-pinterest-brands'
    | 'icon-linkedin-brands'
    | 'icon-twitter-brands'
    | 'icon-apple-brands'
    | 'icon-facebook-brands'
    | 'icon-global'
    | 'icon-angle-left'
    | 'icon-angle-right'
    | 'icon-angle-up'
    | 'icon-call-solid'
    | 'icon-camera'
    | 'icon-way'
    | 'icon-chat'
    | 'icon-check'
    | 'icon-close'
    | 'icon-cup'
    | 'icon-error'
    | 'icon-expand1'
    | 'icon-heart-solid'
    | 'icon-heart'
    | 'icon-light'
    | 'icon-look'
    | 'icon-look-slash'
    | 'icon-like'
    | 'icon-map-marker'
    | 'icon-map-marker-solid'
    | 'icon-map-search'
    | 'icon-menu'
    | 'icon-portfolio'
    | 'icon-search'
    | 'icon-shopbag'
    | 'icon-shuffle'
    | 'icon-smile'
    | 'icon-star'
    | 'icon-star-solid'
    | 'icon-user-simple';
  size?: 'xs' | 's' | 'm' | 'l' | 'xl' | 'xxl' | 'xxxl';
};

export const Icon: FC<IconProps> = ({
  className,
  icon,
  size,

  ...rest
}) => {
  return <i {...rest} className={cnIcon({ size }, [icon, className])} />;
};
