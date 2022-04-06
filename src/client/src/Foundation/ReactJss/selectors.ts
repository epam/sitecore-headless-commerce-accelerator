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

import { RoutingState, SitecoreState } from './headlessDefinitions';

export const sitecore = <TContext, TRoute>(state: SitecoreState<TContext, TRoute>) => state.sitecore;
export const sitecoreContext = <TContext>(state: SitecoreState<TContext>) => sitecore(state).context;
export const sitecoreRoute = <TRoute>(state: SitecoreState<{}, TRoute>) => sitecore(state).route;
export const routing = (state: RoutingState) => state.router;
export const routingLocation = (state: RoutingState) => routing(state).location;
export const routingLocationPathname = (state: RoutingState) => routingLocation(state).pathname;
export const routingLocationSearch = (state: RoutingState) => routingLocation(state).search;
