//    Copyright 2021 EPAM Systems, Inc.
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

import React, { FC, useCallback } from 'react';
import { useDispatch } from 'react-redux';

import { NavigationLink } from 'ui/NavigationLink';
import { closeHamburgerMenu } from 'services/navigationMenu';

import { cnNavigation } from '../cn';
import { MenuLinkDataSource } from '../NavigationMenu';

import './MenuItem.scss';

export const MenuItem: FC<MenuLinkDataSource> = ({ uri }) => {
  const dispatch = useDispatch();

  const handleLinkClick = useCallback(() => {
    dispatch(closeHamburgerMenu());
  }, [dispatch]);

  const {
    jss: {
      value: { href, text },
    },
  } = uri;
  const link = `${href}?q=`;

  return (
    <li className={cnNavigation('MenuItem')}>
      <NavigationLink className={cnNavigation('MenuItemLink')} to={link} onClick={handleLinkClick}>
        {text}
      </NavigationLink>
    </li>
  );
};
