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

// tslint:disable:no-empty-interface

import * as HeadlessDefinitions from 'Foundation/ReactJss';

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Cart
// ID: d0466420-eeca-46a4-93b6-b17cc198e87b
export interface CartDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Cart
// ID: d0466420-eeca-46a4-93b6-b17cc198e87b
export interface CartRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Cart
// ID: d0466420-eeca-46a4-93b6-b17cc198e87b
export const CartTemplate = {
  templateId: 'd0466420-eeca-46a4-93b6-b17cc198e87b',
};

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Category
// ID: 0cbbb134-4025-4a51-9b46-1ab7021ccb0f
export interface CategoryDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Category
// ID: 0cbbb134-4025-4a51-9b46-1ab7021ccb0f
export interface CategoryRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Category
// ID: 0cbbb134-4025-4a51-9b46-1ab7021ccb0f
export const CategoryTemplate = {
  templateId: '0cbbb134-4025-4a51-9b46-1ab7021ccb0f',
};

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Checkout
// ID: d5440345-d25e-4a9b-b8e4-5c2aef3c9178
export interface CheckoutDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Checkout
// ID: d5440345-d25e-4a9b-b8e4-5c2aef3c9178
export interface CheckoutRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Checkout
// ID: d5440345-d25e-4a9b-b8e4-5c2aef3c9178
export const CheckoutTemplate = {
  templateId: 'd5440345-d25e-4a9b-b8e4-5c2aef3c9178',
};

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Data Folder
// ID: 5c07e9e8-6ca9-43ad-9cec-2988a77f6d70
export interface DataFolderDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Data Folder
// ID: 5c07e9e8-6ca9-43ad-9cec-2988a77f6d70
export interface DataFolderRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Data Folder
// ID: 5c07e9e8-6ca9-43ad-9cec-2988a77f6d70
export const DataFolderTemplate = {
  templateId: '5c07e9e8-6ca9-43ad-9cec-2988a77f6d70',
};

// Path: /sitecore/templates/HCA/Project/HCA/Scaffolding/Rendering Parameters/Grid Layout
// ID: 61ed6a03-bbdb-4937-87fa-db5c53fc870c
export interface GridLayoutDataSource extends HeadlessDefinitions.BaseDataSourceItem {
  /// The First Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88
  firstColumnClass: HeadlessDefinitions.TextField;

  /// The Second Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: 525bb442-fc9e-49dd-b557-52b833f0855d
  secondColumnClass: HeadlessDefinitions.TextField;

  /// The Third Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: bfed443f-3ac3-460d-be12-5759b8294233
  thirdColumnClass: HeadlessDefinitions.TextField;

  /// The Wrapper Class field.
  /// Field Type: Single-Line Text
  /// Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52
  wrapperClass: HeadlessDefinitions.TextField;
}

// Path: /sitecore/templates/HCA/Project/HCA/Scaffolding/Rendering Parameters/Grid Layout
// ID: 61ed6a03-bbdb-4937-87fa-db5c53fc870c
export interface GridLayoutRenderingParams extends HeadlessDefinitions.BaseRenderingParam {
  /// The First Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88
  firstColumnClass: string;

  /// The Second Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: 525bb442-fc9e-49dd-b557-52b833f0855d
  secondColumnClass: string;

  /// The Third Column Class field.
  /// Field Type: Single-Line Text
  /// Field ID: bfed443f-3ac3-460d-be12-5759b8294233
  thirdColumnClass: string;

  /// The Wrapper Class field.
  /// Field Type: Single-Line Text
  /// Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52
  wrapperClass: string;
}

// Path: /sitecore/templates/HCA/Project/HCA/Scaffolding/Rendering Parameters/Grid Layout
// ID: 61ed6a03-bbdb-4937-87fa-db5c53fc870c
export const GridLayoutTemplate = {
  templateId: '61ed6a03-bbdb-4937-87fa-db5c53fc870c',

  /// The First Column Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88</para>
  firstColumnClassFieldId: 'a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88',
  firstColumnClassFieldName: 'First Column Class',

  /// The Second Column Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 525bb442-fc9e-49dd-b557-52b833f0855d</para>
  secondColumnClassFieldId: '525bb442-fc9e-49dd-b557-52b833f0855d',
  secondColumnClassFieldName: 'Second Column Class',

  /// The Third Column Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: bfed443f-3ac3-460d-be12-5759b8294233</para>
  thirdColumnClassFieldId: 'bfed443f-3ac3-460d-be12-5759b8294233',
  thirdColumnClassFieldName: 'Third Column Class',

  /// The Wrapper Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52</para>
  wrapperClassFieldId: 'b4386ac8-1f1b-49f3-adcb-264f5f595e52',
  wrapperClassFieldName: 'Wrapper Class',
};

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Home
// ID: 83883836-8f50-4bb8-bf62-369b7661e815
export interface HomeDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Home
// ID: 83883836-8f50-4bb8-bf62-369b7661e815
export interface HomeRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Home
// ID: 83883836-8f50-4bb8-bf62-369b7661e815
export const HomeTemplate = {
  templateId: '83883836-8f50-4bb8-bf62-369b7661e815',
};

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Page
// ID: 9ca4a8c4-0295-49c1-a879-e008933d0a4f
export interface PageDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Page
// ID: 9ca4a8c4-0295-49c1-a879-e008933d0a4f
export interface PageRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Page
// ID: 9ca4a8c4-0295-49c1-a879-e008933d0a4f
export const PageTemplate = {
  templateId: '9ca4a8c4-0295-49c1-a879-e008933d0a4f',
};

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Product
// ID: 1daeff25-b075-4c13-b41a-72b553b22542
export interface ProductDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Product
// ID: 1daeff25-b075-4c13-b41a-72b553b22542
export interface ProductRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Pages/Product
// ID: 1daeff25-b075-4c13-b41a-72b553b22542
export const ProductTemplate = {
  templateId: '1daeff25-b075-4c13-b41a-72b553b22542',
};

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder
// ID: ff3f660a-3d15-4efc-9873-113aaf71b44e
export interface SettingsFolderDataSource extends HeadlessDefinitions.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder
// ID: ff3f660a-3d15-4efc-9873-113aaf71b44e
export interface SettingsFolderRenderingParams extends HeadlessDefinitions.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder
// ID: ff3f660a-3d15-4efc-9873-113aaf71b44e
export const SettingsFolderTemplate = {
  templateId: 'ff3f660a-3d15-4efc-9873-113aaf71b44e',
};
