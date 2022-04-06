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

import React, { FC, useCallback, useState, useEffect, useRef } from 'react';
import { useLocation } from 'react-router';

import { cnNavigation } from '../cn';
import { DesktopItemContent } from '../DesktopItemContent';
import { MobileItemContent } from '../MobileItemContent';
import { MenuCommerceItemDataSource } from '../NavigationMenu';

import './Item.scss';

export const Item: FC<MenuCommerceItemDataSource> = ({ commerceCategories, image, title }) => {
  const {
    jss: { value: commerceItemName },
  } = title;

  const { jss: commerceItemImage } = image;

  const commerceItemNavigationLinks = commerceCategories.items.map(({ name }) => {
    return {
      title: name,
      url: `/shop/${name}`,
    };
  });

  const { pathname } = useLocation();
  const ref = useRef(null);
  const [hovered, setHovered] = useState(false);
  const [isTouch, setIsTouch] = useState(false);

  const handleClickOutside = (e: MouseEvent) => {
    if (ref.current && !ref.current.contains(e.target) && hovered) {
      setHovered(!hovered);
    }
  };

  useEffect(() => {
    window.addEventListener('click', handleClickOutside, false);
    return () => {
      window.removeEventListener('click', handleClickOutside, false);
    };
  });

  useEffect(() => {
    setHovered(false);
  }, [pathname]);

  const handleTouch = () => {
    setIsTouch(true);
  };

  const handleMouseEnter = useCallback(() => {
    setHovered(true);
  }, []);

  const handleMouseLeave = useCallback(() => {
    setHovered(false);
  }, []);

  const handleClick = (e: any) => {
    setHovered(!hovered);
  };

  return (
    <li
      ref={ref}
      className={cnNavigation('Item')}
      onMouseEnter={isTouch ? null : handleMouseEnter}
      onMouseLeave={isTouch ? null : handleMouseLeave}
      onTouchStart={handleTouch}
      onClick={handleClick}
    >
      <DesktopItemContent
        commerceItemImage={commerceItemImage}
        commerceItemName={commerceItemName}
        commerceItemNavigationLinks={commerceItemNavigationLinks}
        isOpen={hovered}
      />
      <MobileItemContent
        commerceItemName={commerceItemName}
        commerceItemNavigationLinks={commerceItemNavigationLinks}
      />
    </li>
  );
};
