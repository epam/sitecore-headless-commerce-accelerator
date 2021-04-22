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

import { Action } from 'Foundation/Integration';
import { keyMirrorReducer } from 'Foundation/ReactJss';

const ACTION_TYPES_NAMESPACE = 'HAMBURGER_MENU';

export const reducerActionTypes = keyMirrorReducer(
  {
    CLOSE_HAMBURGER_MENU: null,
    OPEN_HAMBURGER_MENU: null,
  },
  ACTION_TYPES_NAMESPACE,
);

type CloseHamburgerMenuType = () => Action;
type OpenHamburgerMenuType = () => Action;

export const closeHamburgerMenu: CloseHamburgerMenuType = () => ({
  type: reducerActionTypes.CLOSE_HAMBURGER_MENU,
});

export const openHamburgerMenu: OpenHamburgerMenuType = () => ({
  type: reducerActionTypes.OPEN_HAMBURGER_MENU,
});
