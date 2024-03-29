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

export interface AnalyticsEventArgs {
  action: string;
  category: string;
  label?: string;
  value?: number;
  nonInteraction?: boolean;
}

export interface CartLine {
  id: string;
  name: string;
  categoty?: string;
  brand?: string;
  price?: number;
  variant?: string;
  quantity?: number;
}

export interface ProductImpression {
  id: string;
  name: string;
  brand?: string;
  price?: number;
  variant?: string;
  list?: string;
}

export interface ProductDetailsView {
  id: string;
  name: string;
  brand?: string;
  category?: string;
  variant?: string;
  price?: number;
}

export interface Product {
  id: string;
  name: string;
  category?: string;
  brand?: string;
  price?: number;
  position?: number;
  variant?: string;
  list?: string;
}
