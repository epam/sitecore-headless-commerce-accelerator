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

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export interface StoreDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Description field.
  /// Field Type: Single-Line Text
  /// Field ID: 7b75c619-ead2-46fe-b684-8f9dec31a791
  description: ReactJssModule.TextField;

  /// The Latitude field.
  /// Field Type: Number
  /// Field ID: 2eb66e67-f3f2-44ac-9654-7f26a5fd52fe
  latitude: ReactJssModule.Field<number>;

  /// The Longitude field.
  /// Field Type: Number
  /// Field ID: 5e72c2b0-9340-4dde-b8a0-d51b6b1a9960
  longitude: ReactJssModule.Field<number>;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 35a0e97b-3284-44d6-9578-49b14e66f4ec
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export interface StoreRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Description field.
  /// Field Type: Single-Line Text
  /// Field ID: 7b75c619-ead2-46fe-b684-8f9dec31a791
  description: string;

  /// The Latitude field.
  /// Field Type: Number
  /// Field ID: 2eb66e67-f3f2-44ac-9654-7f26a5fd52fe
  latitude: string;

  /// The Longitude field.
  /// Field Type: Number
  /// Field ID: 5e72c2b0-9340-4dde-b8a0-d51b6b1a9960
  longitude: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 35a0e97b-3284-44d6-9578-49b14e66f4ec
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export const StoreTemplate = {
  templateId: '7360d0b3-5400-4dcc-913d-80a4f0d7539e',

  /// The Description field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 7b75c619-ead2-46fe-b684-8f9dec31a791</para>
  descriptionFieldId: '7b75c619-ead2-46fe-b684-8f9dec31a791',
  descriptionFieldName: 'Description',

  /// The Latitude field.
  /// <para>Field Type: Number</para>
  /// <para>Field ID: 2eb66e67-f3f2-44ac-9654-7f26a5fd52fe</para>
  latitudeFieldId: '2eb66e67-f3f2-44ac-9654-7f26a5fd52fe',
  latitudeFieldName: 'Latitude',

  /// The Longitude field.
  /// <para>Field Type: Number</para>
  /// <para>Field ID: 5e72c2b0-9340-4dde-b8a0-d51b6b1a9960</para>
  longitudeFieldId: '5e72c2b0-9340-4dde-b8a0-d51b6b1a9960',
  longitudeFieldName: 'Longitude',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 35a0e97b-3284-44d6-9578-49b14e66f4ec</para>
  titleFieldId: '35a0e97b-3284-44d6-9578-49b14e66f4ec',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export interface StoreLocatorDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Default Latitude field.
  /// Field Type: Number
  /// Field ID: 42265054-df48-49c4-be6d-4eb68f4f632e
  defaultLatitude: ReactJssModule.Field<number>;

  /// The Default Longitude field.
  /// Field Type: Number
  /// Field ID: 8c68f0d1-5e42-4e71-a220-2ce4b0cf651c
  defaultLongitude: ReactJssModule.Field<number>;

  /// The Description field.
  /// Field Type: Multi-Line Text
  /// Field ID: 50365886-475f-4656-a7d9-bac8bea0c413
  description: ReactJssModule.TextField;

  /// The Radius field.
  /// Field Type: Multilist
  /// Field ID: f0d8d855-8257-4096-907f-752b50a62744
  radius: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Stores field.
  /// Field Type: Multilist
  /// Field ID: 43aa9312-0033-41de-b127-59379956428f
  stores: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0f5d3b8e-badd-4617-9a6d-9102cea617e4
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export interface StoreLocatorRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Default Latitude field.
  /// Field Type: Number
  /// Field ID: 42265054-df48-49c4-be6d-4eb68f4f632e
  defaultLatitude: string;

  /// The Default Longitude field.
  /// Field Type: Number
  /// Field ID: 8c68f0d1-5e42-4e71-a220-2ce4b0cf651c
  defaultLongitude: string;

  /// The Description field.
  /// Field Type: Multi-Line Text
  /// Field ID: 50365886-475f-4656-a7d9-bac8bea0c413
  description: string;

  /// The Radius field.
  /// Field Type: Multilist
  /// Field ID: f0d8d855-8257-4096-907f-752b50a62744
  radius: string;

  /// The Stores field.
  /// Field Type: Multilist
  /// Field ID: 43aa9312-0033-41de-b127-59379956428f
  stores: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0f5d3b8e-badd-4617-9a6d-9102cea617e4
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export const StoreLocatorTemplate = {
  templateId: 'dc35377d-4d05-416a-9785-e8e380c75baf',

  /// The Default Latitude field.
  /// <para>Field Type: Number</para>
  /// <para>Field ID: 42265054-df48-49c4-be6d-4eb68f4f632e</para>
  defaultLatitudeFieldId: '42265054-df48-49c4-be6d-4eb68f4f632e',
  defaultLatitudeFieldName: 'Default Latitude',

  /// The Default Longitude field.
  /// <para>Field Type: Number</para>
  /// <para>Field ID: 8c68f0d1-5e42-4e71-a220-2ce4b0cf651c</para>
  defaultLongitudeFieldId: '8c68f0d1-5e42-4e71-a220-2ce4b0cf651c',
  defaultLongitudeFieldName: 'Default Longitude',

  /// The Description field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 50365886-475f-4656-a7d9-bac8bea0c413</para>
  descriptionFieldId: '50365886-475f-4656-a7d9-bac8bea0c413',
  descriptionFieldName: 'Description',

  /// The Radius field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: f0d8d855-8257-4096-907f-752b50a62744</para>
  radiusFieldId: 'f0d8d855-8257-4096-907f-752b50a62744',
  radiusFieldName: 'Radius',

  /// The Stores field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: 43aa9312-0033-41de-b127-59379956428f</para>
  storesFieldId: '43aa9312-0033-41de-b127-59379956428f',
  storesFieldName: 'Stores',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 0f5d3b8e-badd-4617-9a6d-9102cea617e4</para>
  titleFieldId: '0f5d3b8e-badd-4617-9a6d-9102cea617e4',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export interface StoreLocatorFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export interface StoreLocatorFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export const StoreLocatorFolderTemplate = {
  templateId: 'fe957ab9-71b5-4c9f-a34c-1e640acb0abb',
};
