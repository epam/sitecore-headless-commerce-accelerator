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

import React, { FC, useCallback } from 'react';
import { useDispatch } from 'react-redux';

import { openHamburgerMenu } from '../NavigationMenu/Integration';

import './MenuButton.scss';

export const MenuButton: FC = () => {
  const dispatch = useDispatch();

  const handleHamburgerMenuOpen = useCallback(() => {
    dispatch(openHamburgerMenu());
  }, [dispatch]);

  return (
    <div className="navigation-buttons_item menu-button">
      <a onClick={handleHamburgerMenuOpen}>
        <i className="pe-7s-menu" />
      </a>
    </div>
  );
};
