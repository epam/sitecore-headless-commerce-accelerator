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

import React, { FC, useCallback, useEffect, useState, useRef } from 'react';

import { useDispatch, useSelector } from 'react-redux';

import { useLocation } from 'react-router';

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import { BaseDataSourceItem, BaseRenderingParam, GraphQLRenderingWithParams } from 'Foundation/ReactJss';
import { closeHamburgerMenu, selectHamburgerMenuVisibility } from 'services/navigationMenu';

import { Icon } from 'components';

import classnames from 'classnames';

import './Header.scss';

const HEADER_TOP = 0;

export const Header: FC<GraphQLRenderingWithParams<BaseDataSourceItem, BaseRenderingParam>> = ({ rendering }) => {
  const dispatch = useDispatch();

  const isHamburgerMenuVisible = useSelector(selectHamburgerMenuVisibility);

  const hamburgerMenuRef = useRef(null);

  const [scroll, setScroll] = useState<number>(0);

  const { pathname } = useLocation();

  const handleScroll = () => {
    setScroll(document.documentElement.scrollTop);
  };

  const handleHamburgerMenuClose = useCallback(() => {
    dispatch(closeHamburgerMenu());
  }, [dispatch]);

  const handleOutsideClick = useCallback(
    (e: MouseEvent) => {
      if (hamburgerMenuRef.current && !hamburgerMenuRef.current.contains(e.target) && isHamburgerMenuVisible) {
        dispatch(closeHamburgerMenu());
      }
    },
    [hamburgerMenuRef, isHamburgerMenuVisible],
  );

  useEffect(() => {
    dispatch(closeHamburgerMenu());
  }, [pathname]);

  useEffect(() => {
    window.addEventListener('scroll', handleScroll);
    window.addEventListener('click', handleOutsideClick, false);
    return () => {
      window.removeEventListener('scroll', handleScroll);
      window.removeEventListener('click', handleOutsideClick, false);
    };
  });

  return (
    <>
      <header
        className={classnames('header', {
          'header--active': isHamburgerMenuVisible,
          'header--inactive': !isHamburgerMenuVisible,
          'header--sticky': scroll > HEADER_TOP,
        })}
        data-el="header"
      >
        <div className="header-desktop">
          <Placeholder name="header-content" rendering={rendering} />
        </div>

        <div ref={hamburgerMenuRef} className={classnames('header_mobile mobile-menu')}>
          <button className="mobile-menu_close" onClick={handleHamburgerMenuClose}>
            <Icon icon="icon-close" />
          </button>
          <div className="mobile-menu_container">
            <div className="mobile-menu_content">
              <Placeholder name="header-content" rendering={rendering} />
            </div>
          </div>
        </div>
      </header>
      <Placeholder name="breadcrumb" rendering={rendering} />
    </>
  );
};
