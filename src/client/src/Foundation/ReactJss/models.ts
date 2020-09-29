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

import { ComponentRendering, LayoutServiceContextData } from '@sitecore-jss/sitecore-jss-react';
import * as Router from 'connected-react-router';

import { Status } from 'Foundation/Integration';

export interface BaseDataSourceItem {
  checkoutStepName: any;
  id: string;
}

export interface GraphQLDataSourceItem<TDataSourceItem> {
  data: {
    datasource: TDataSourceItem;
  };
}

export interface BaseRenderingParam {
  caching?: string;
}

export interface Item<TDataSourceItem> {
  fields: TDataSourceItem;
}

export interface GraphQLField<TFieldValue> {
  jss: TFieldValue;
}

export interface GraphQLListField<TFieldValue> {
  items: TFieldValue[];
}

export interface ItemList<TDataSourceItem> extends Array<Item<TDataSourceItem>> {}

export interface DictionaryItem {
  dictionary: { [key: string]: string };
}

export interface Rendering<TDataSourceItem extends BaseDataSourceItem = BaseDataSourceItem>
  extends Item<TDataSourceItem> {
  rendering: ComponentRendering;
}

export interface GraphQLRendering<TDataSourceItem extends BaseDataSourceItem>
  extends Item<GraphQLDataSourceItem<TDataSourceItem>> {}

export interface RenderingWithParams<
  TDataSourceItem extends BaseDataSourceItem,
  TParameters extends BaseRenderingParam
> extends Rendering<TDataSourceItem> {
  params: TParameters;
}

export interface RouteFields<TRouteFields extends BaseDataSourceItem = BaseDataSourceItem> {
  routeFields: TRouteFields;
}

export interface RenderingWithContext<
  TDataSourceItem extends BaseDataSourceItem = BaseDataSourceItem,
  TCustomData = {},
  TRouteFields extends BaseDataSourceItem = BaseDataSourceItem
> extends Rendering<TDataSourceItem> {
  sitecoreContext: LayoutServiceContextData['context'] & RouteFields<TRouteFields> & TCustomData;
}

export interface Field<TFieldValue> {
  value: TFieldValue;
  editable: string;
}

export interface SitecoreContext<TContext> {
  context: TContext;
}

export interface SitecoreRoute<TRoute> {
  route: TRoute;
}

export interface Sitecore<TContext, TRoute> extends SitecoreContext<TContext>, SitecoreRoute<TRoute>, Status {
  loadedUrl?: string;
}

export interface SitecorePayload extends Partial<Sitecore<{}, {}>> {}

export interface ChangeRoutePayload {
  newRoute: string;
  shouldPushNewRoute: boolean;
}

export interface SitecoreState<TContext = {}, TRoute = {}> {
  sitecore: Sitecore<TContext, TRoute>;
}

export interface ViewBagState<TViewBag = {}> {
  viewBag: TViewBag;
}

export interface RoutingState {
  router: Router.RouterState;
}
