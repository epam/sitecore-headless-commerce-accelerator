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

import { LOCATION_CHANGE, LocationChangeAction } from 'connected-react-router';
import { Store } from 'redux';

import { Action } from 'Foundation/Integration/client';

import * as selectors from '../selectors';
import { ChangeRoute } from './actions';

const hasNewRoute = (loadedUrl: string = '', pathname: string = '', search: string = '') => {
  const [loadedPathname, loadedSearch] = loadedUrl.split('?');
  return (
    loadedPathname.toLocaleLowerCase() !== pathname.toLocaleLowerCase() ||
    (loadedSearch || '').toLocaleLowerCase() !== search.slice(search.indexOf('?') + 1).toLocaleLowerCase()
  );
};

export default (store: Store<any>) => (next: any) => (action: Action) => {
  // we have to intercept location change here, in order add some custom handling for default LOCATION_CHANGE event
  if (action.type === LOCATION_CHANGE) {
    const { payload } = action as LocationChangeAction;
    const loadedUrl = selectors.loadedUrl(store.getState());

    if (loadedUrl) {
      const { pathname, search } = payload.location;
      if (hasNewRoute(loadedUrl, pathname, search)) {
        next(action);
        const newUrl = search ? `${pathname}${search}` : pathname;
        return store.dispatch(ChangeRoute(newUrl, false));
      }
    }

    return next(action);
  }

  next(action);
};
