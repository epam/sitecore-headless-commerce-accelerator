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

import { RouteData } from '@sitecore-jss/sitecore-jss-react';
import { RouteComponentProps } from 'react-router-dom';
import { Store } from 'redux';

import * as Commerce from 'Foundation/Commerce/client';
import * as Jss from 'Foundation/ReactJss/client';

import * as ProductSearch from 'Feature/Catalog/client/Integration/ProductsSearch';
import * as Order from 'Feature/Checkout/client/Integration/Order';
import * as OrderHistory from 'Feature/Checkout/client/Integration/OrderHistory';
import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

import { NormalizedCacheObject } from 'apollo-cache-inmemory';
import ApolloClient from 'apollo-client';

export interface SitecoreContext
  extends Commerce.ProductContext,
    Commerce.CategoryContext,
    Commerce.UserContext,
    Commerce.ProductColorsContext {
  site: {
    name: string;
  };
}

export interface SitecoreRoute {
  name: string;
  displayName: string;
  fields: object;
  placeholders: object;
}

export interface SitecoreState extends Jss.SitecoreState<SitecoreContext, RouteData> {}

export interface Dictionary {
  dictionary: {
    [index: string]: string;
  };
}

export interface ViewBag extends Dictionary {
  language: string;
}

export interface ViewBagState extends Jss.ViewBagState<ViewBag> {}

export interface RoutingState extends Jss.RoutingState {}

export interface AppState
  extends SitecoreState,
    ViewBagState,
    ProductSearch.GlobalProductSearchState,
    ShoppingCart.GlobalShoppingCartState,
    Order.GlobalCurrentOrderState,
    OrderHistory.GlobalCurrentOrderHistoryState,
    RoutingState {}

export interface ServerSideState extends SitecoreState, ViewBagState {}

export interface ViewRenderResults {
  html: string;
  redirect: null | string;
  status: number;
}

export interface AppStateProps extends Dictionary {
  rendering: RouteData;
  routeFields: object;
  sitecoreContext: SitecoreContext;
  language: string;
  isLoading: boolean;
}

export interface AppDispatchProps {
  InitAuthentication: () => void;
}

export interface AppProps extends AppStateProps, AppDispatchProps {}

export interface LayoutProps extends AppProps, RouteComponentProps<any> {}

export interface RootProps {
  store: Store<AppState>;
  graphQLClient: ApolloClient<NormalizedCacheObject>;
}
