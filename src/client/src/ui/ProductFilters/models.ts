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

import { RouterProps } from 'react-router';

import * as Jss from 'Foundation/ReactJss';
import * as Search from 'services/search';

export interface ProductFiltersOwnProps extends RouterProps {}
export interface ProductFiltersStateProps {
  search: string;
  facets: Search.Facets[];
  isLoading: boolean;
  items: Search.Product[];
  totalItemCount: number;
}
export interface ProductFiltersDispatchProps {
  ApplyFacet: (name: string, value: string, search: string) => void;
  DiscardFacet: (name: string, value: string, search: string) => void;
}
export interface ProductFiltersProps
  extends ProductFiltersOwnProps,
    ProductFiltersStateProps,
    ProductFiltersDispatchProps {}

export interface ProductFiltersOwnState extends Jss.SafePureComponentState {}

export interface AppState extends Search.GlobalSearchState {}