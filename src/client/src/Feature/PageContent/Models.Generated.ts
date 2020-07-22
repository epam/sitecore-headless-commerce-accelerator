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

import * as ReactJssModule from 'Foundation/ReactJss';

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner
// ID: aca0aaff-3317-4535-8bc5-504256e08255
export interface BannerDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Button Text field.
  /// Field Type: Single-Line Text
  /// Field ID: 42ec7cba-9200-49fe-b5f1-03824e1dc337
  buttonText: ReactJssModule.TextField;

  /// The Image field.
  /// Field Type: Image
  /// Field ID: cfb54c8b-6f8e-4bdb-9a07-7df0871196c7
  image: ReactJssModule.ImageField;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: ed641b03-6844-4ad0-ae83-ba9d351a2660
  link: ReactJssModule.LinkField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner
// ID: aca0aaff-3317-4535-8bc5-504256e08255
export interface BannerRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Button Text field.
  /// Field Type: Single-Line Text
  /// Field ID: 42ec7cba-9200-49fe-b5f1-03824e1dc337
  buttonText: string;

  /// The Image field.
  /// Field Type: Image
  /// Field ID: cfb54c8b-6f8e-4bdb-9a07-7df0871196c7
  image: string;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: ed641b03-6844-4ad0-ae83-ba9d351a2660
  link: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner
// ID: aca0aaff-3317-4535-8bc5-504256e08255
export const BannerTemplate = {
  templateId: 'aca0aaff-3317-4535-8bc5-504256e08255',

  /// The Button Text field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 42ec7cba-9200-49fe-b5f1-03824e1dc337</para>
  buttonTextFieldId: '42ec7cba-9200-49fe-b5f1-03824e1dc337',
  buttonTextFieldName: 'Button Text',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: cfb54c8b-6f8e-4bdb-9a07-7df0871196c7</para>
  imageFieldId: 'cfb54c8b-6f8e-4bdb-9a07-7df0871196c7',
  imageFieldName: 'Image',

  /// The Link field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: ed641b03-6844-4ad0-ae83-ba9d351a2660</para>
  linkFieldId: 'ed641b03-6844-4ad0-ae83-ba9d351a2660',
  linkFieldName: 'Link',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner Folder
// ID: 60d1329d-bfbf-4c2a-a6be-426f1c700ba9
export interface BannerFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner Folder
// ID: 60d1329d-bfbf-4c2a-a6be-426f1c700ba9
export interface BannerFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Banner/Banner Folder
// ID: 60d1329d-bfbf-4c2a-a6be-426f1c700ba9
export const BannerFolderTemplate = {
  templateId: '60d1329d-bfbf-4c2a-a6be-426f1c700ba9',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products
// ID: 02303189-7dc9-41c9-914a-4784878a6a7d
export interface RecommendedProductsDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Header field.
  /// Field Type: Single-Line Text
  /// Field ID: ba9cd774-93ea-490e-b978-38efe2c648ed
  header: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products
// ID: 02303189-7dc9-41c9-914a-4784878a6a7d
export interface RecommendedProductsRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Header field.
  /// Field Type: Single-Line Text
  /// Field ID: ba9cd774-93ea-490e-b978-38efe2c648ed
  header: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products
// ID: 02303189-7dc9-41c9-914a-4784878a6a7d
export const RecommendedProductsTemplate = {
  templateId: '02303189-7dc9-41c9-914a-4784878a6a7d',

  /// The Header field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: ba9cd774-93ea-490e-b978-38efe2c648ed</para>
  headerFieldId: 'ba9cd774-93ea-490e-b978-38efe2c648ed',
  headerFieldName: 'Header',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products Folder
// ID: 2ef7b0f6-a5e5-43e6-acec-0f1c2eed1731
export interface RecommendedProductsFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products Folder
// ID: 2ef7b0f6-a5e5-43e6-acec-0f1c2eed1731
export interface RecommendedProductsFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Recommended Products/Recommended Products Folder
// ID: 2ef7b0f6-a5e5-43e6-acec-0f1c2eed1731
export const RecommendedProductsFolderTemplate = {
  templateId: '2ef7b0f6-a5e5-43e6-acec-0f1c2eed1731',
};
