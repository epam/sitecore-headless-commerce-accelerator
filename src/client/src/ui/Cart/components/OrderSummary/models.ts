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

import * as Commerce from 'services/commerce';
import * as JSS from 'Foundation/ReactJss';
import * as ShoppingCart from 'services/shoppingCart';

export interface OrderSummaryDataSource extends JSS.BaseDataSourceItem {
  countries: Commerce.CountryRegion[];
}
export interface OrderSummaryOwnProps {
  price: ShoppingCart.ShoppingCartPrice;
  rendering: any;
  countries: Commerce.CountryRegion[];
}

export interface OrderSummaryProps extends OrderSummaryOwnProps {}

export interface AppState extends ShoppingCart.GlobalShoppingCartState {}