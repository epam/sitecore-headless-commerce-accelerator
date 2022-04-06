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

import { Icon } from 'components';
import React from 'react';
import { useDispatch } from 'react-redux';

import { useLocation } from 'react-router';
import { Logout } from 'services/authentication';

import { NavigationLink } from 'ui/NavigationLink';
import { LeftMenuLink, LeftMenuProps } from './models';

import { cnLeftMenu } from './cn';
import './LeftMenu.scss';

export const LeftMenu = ({ rendering }: LeftMenuProps) => {
  const menuLinks = rendering.fields.links;
  const { pathname } = useLocation();
  const dispatch = useDispatch();

  const handleClickLogout = (path: string) => {
    dispatch(Logout(path));
  };

  return (
    <div className={cnLeftMenu()}>
      <ul className={cnLeftMenu('Block')}>
        {menuLinks.map((item: LeftMenuLink) => {
          const myAccountLink = item.fields.uri.value.href === pathname;
          return item.fields.isPrimary ? (
            <div className={cnLeftMenu('Title')} key={item.id}>
              {item.fields.uri.value.text}
            </div>
          ) : item.fields.uri.value.text === 'Log out' ? (
            <NavigationLink
              className={cnLeftMenu('LogOut')}
              to={item.fields.uri.value.href}
              key={item.id}
              onClick={() => handleClickLogout(item.fields.uri.value.href)}
            >
              {item.fields.uri.value.text}
            </NavigationLink>
          ) : (
            <NavigationLink
              className={cnLeftMenu('Wrap', { hidden: myAccountLink })}
              key={item.id}
              to={item.fields.uri.value.href}
            >
              <li className={cnLeftMenu('Link', { active: pathname === item.fields.uri.value.href })}>
                {item.fields.uri.value.text}
              </li>
              <Icon icon="icon-angle-right" size="xl"></Icon>
            </NavigationLink>
          );
        })}
      </ul>
    </div>
  );
};
