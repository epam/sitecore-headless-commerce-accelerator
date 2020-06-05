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

import * as JSS from 'Foundation/ReactJss/client';

export interface CommerceCategory extends JSS.BaseDataSourceItem {
  name: string;
}

export interface MenuItemDataSource extends JSS.BaseDataSourceItem {
  title: JSS.GraphQLField<JSS.TextField>;
  image: JSS.GraphQLField<JSS.ImageField>;
  commerceCategories: JSS.GraphQLListField<CommerceCategory>;
}

export interface NavigationMenuDataSource extends JSS.BaseDataSourceItem {
  menuItems: JSS.GraphQLListField<MenuItemDataSource>;
}

export interface NavigationMenuProps extends JSS.GraphQLRendering<NavigationMenuDataSource> {}

export interface NavigationMenuState extends JSS.SafePureComponentState {}
