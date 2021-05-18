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

import * as commonModels from 'Feature/Catalog/Integration/common/models';
import * as JSS from 'Foundation/ReactJss';
import { GlobalSearchState } from 'services/search';

export interface NavigationSearchProps extends JSS.Rendering {
  ChangeRoute: (newRoute: string) => void;
  closeHamburgerMenu: () => void;
  requestSuggestions: (search: string) => void;
  resetSuggestionsState: () => void;
  searchSuggestionsStatus: string;
}

export interface NavigationSearchState extends JSS.SafePureComponentState {
  isOpen: boolean;
}

export type AppState = GlobalSearchState & commonModels.AppState;
