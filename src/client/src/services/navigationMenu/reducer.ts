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

import { Action } from 'models';

import { reducerActionTypes } from './actions';

type HamburgerMenuState = {
  isHamburgerMenuOpen: boolean;
};

export type GlobalHamburgerMenuState = {
  hamburgerMenu: HamburgerMenuState;
};

const initialState: HamburgerMenuState = {
  isHamburgerMenuOpen: false,
};

export default (state: HamburgerMenuState = { ...initialState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.CLOSE_HAMBURGER_MENU: {
      return {
        ...state,
        isHamburgerMenuOpen: false,
      };
    }

    case reducerActionTypes.OPEN_HAMBURGER_MENU: {
      return {
        ...state,
        isHamburgerMenuOpen: true,
      };
    }

    default:
      return state;
  }
};
