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
import { Button } from 'components';

import { COOKIE_BANNER_DATA } from './constants';
import { useCookiesState } from './hooks';

import './CookieBanner.scss';
import { cnCookieBanner } from './cn';

interface CookieBannerProps {
  isLoaded: boolean;
}

export const CookieBanner: FC<CookieBannerProps> = ({ isLoaded }) => {
  const [isCookiesAccepted, setIsCookiesAccepted] = useCookiesState(false);

  const handleAcceptCookies = useCallback(() => {
    setIsCookiesAccepted(true);
  }, []);

  return (
    isLoaded && (
      <div className={cnCookieBanner({ visible: !isCookiesAccepted })}>
        <div className="CookieBanner-Text">{COOKIE_BANNER_DATA.TEXT}</div>
        <Button className="CookieBanner-Button" onClick={handleAcceptCookies}>
          {COOKIE_BANNER_DATA.BUTTON}
        </Button>
      </div>
    )
  );
};
