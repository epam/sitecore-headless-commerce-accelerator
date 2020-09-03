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

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Countries Folder
// ID: d3c19476-fc89-461f-9997-38446a4aa64f
export interface CountriesFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Countries Folder
// ID: d3c19476-fc89-461f-9997-38446a4aa64f
export interface CountriesFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Countries Folder
// ID: d3c19476-fc89-461f-9997-38446a4aa64f
export const CountriesFolderTemplate = {
  templateId: 'd3c19476-fc89-461f-9997-38446a4aa64f',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Country
// ID: 4b54de2d-7fd8-40e7-b64d-b38e24e156a3
export interface CountryDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Code field.
  /// Field Type: Single-Line Text
  /// Field ID: 709185a9-dfbb-40b0-985d-0f4f73eda570
  code: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: cd8a9527-e002-46b4-8237-eca7caf7140a
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Country
// ID: 4b54de2d-7fd8-40e7-b64d-b38e24e156a3
export interface CountryRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Code field.
  /// Field Type: Single-Line Text
  /// Field ID: 709185a9-dfbb-40b0-985d-0f4f73eda570
  code: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: cd8a9527-e002-46b4-8237-eca7caf7140a
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Country
// ID: 4b54de2d-7fd8-40e7-b64d-b38e24e156a3
export const CountryTemplate = {
  templateId: '4b54de2d-7fd8-40e7-b64d-b38e24e156a3',

  /// The Code field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 709185a9-dfbb-40b0-985d-0f4f73eda570</para>
  codeFieldId: '709185a9-dfbb-40b0-985d-0f4f73eda570',
  codeFieldName: 'Code',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: cd8a9527-e002-46b4-8237-eca7caf7140a</para>
  titleFieldId: 'cd8a9527-e002-46b4-8237-eca7caf7140a',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radius
// ID: 846425b7-2427-4ba2-ab81-38e895355621
export interface RadiusDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Value field.
  /// Field Type: Number
  /// Field ID: 5a040c2d-afc8-41de-aeb9-63a16e889e50
  value: ReactJssModule.Field<number>;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radius
// ID: 846425b7-2427-4ba2-ab81-38e895355621
export interface RadiusRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Value field.
  /// Field Type: Number
  /// Field ID: 5a040c2d-afc8-41de-aeb9-63a16e889e50
  value: string;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radius
// ID: 846425b7-2427-4ba2-ab81-38e895355621
export const RadiusTemplate = {
  templateId: '846425b7-2427-4ba2-ab81-38e895355621',

  /// The Value field.
  /// <para>Field Type: Number</para>
  /// <para>Field ID: 5a040c2d-afc8-41de-aeb9-63a16e889e50</para>
  valueFieldId: '5a040c2d-afc8-41de-aeb9-63a16e889e50',
  valueFieldName: 'Value',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radiuses Folder
// ID: 7d6a3104-0c1d-42f6-951f-45aa19a8b07b
export interface RadiusesFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radiuses Folder
// ID: 7d6a3104-0c1d-42f6-951f-45aa19a8b07b
export interface RadiusesFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Radiuses Folder
// ID: 7d6a3104-0c1d-42f6-951f-45aa19a8b07b
export const RadiusesFolderTemplate = {
  templateId: '7d6a3104-0c1d-42f6-951f-45aa19a8b07b',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export interface StoreDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export interface StoreRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store
// ID: 7360d0b3-5400-4dcc-913d-80a4f0d7539e
export const StoreTemplate = {
  templateId: '7360d0b3-5400-4dcc-913d-80a4f0d7539e',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export interface StoreLocatorDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Countries field.
  /// Field Type: Multilist
  /// Field ID: ba1bbc85-7fbe-4beb-a8e2-e36b739e2f42
  countries: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Default Latitude field.
  /// Field Type: Number
  /// Field ID: 42265054-df48-49c4-be6d-4eb68f4f632e
  defaultLatitude: ReactJssModule.Field<number>;

  /// The Default Longitude field.
  /// Field Type: Number
  /// Field ID: 8c68f0d1-5e42-4e71-a220-2ce4b0cf651c
  defaultLongitude: ReactJssModule.Field<number>;

  /// The Default Radius field.
  /// Field Type: Droplink
  /// Field ID: b8df8215-ceb2-4f11-b725-4f5080f4ab18
  defaultRadius: ReactJssModule.Item<ReactJssModule.BaseDataSourceItem>;

  /// The Description field.
  /// Field Type: Multi-Line Text
  /// Field ID: 50365886-475f-4656-a7d9-bac8bea0c413
  description: ReactJssModule.TextField;

  /// The Radiuses field.
  /// Field Type: Multilist
  /// Field ID: f0d8d855-8257-4096-907f-752b50a62744
  radiuses: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Stores field.
  /// Field Type: Multilist
  /// Field ID: 43aa9312-0033-41de-b127-59379956428f
  stores: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0f5d3b8e-badd-4617-9a6d-9102cea617e4
  title: ReactJssModule.TextField;

  /// The Unit Of Length field.
  /// Field Type: Droplink
  /// Field ID: 506a4f5b-4c6e-466b-8709-b212231977e5
  unitOfLength: ReactJssModule.Item<ReactJssModule.BaseDataSourceItem>;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export interface StoreLocatorRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Countries field.
  /// Field Type: Multilist
  /// Field ID: ba1bbc85-7fbe-4beb-a8e2-e36b739e2f42
  countries: string;

  /// The Default Latitude field.
  /// Field Type: Number
  /// Field ID: 42265054-df48-49c4-be6d-4eb68f4f632e
  defaultLatitude: string;

  /// The Default Longitude field.
  /// Field Type: Number
  /// Field ID: 8c68f0d1-5e42-4e71-a220-2ce4b0cf651c
  defaultLongitude: string;

  /// The Default Radius field.
  /// Field Type: Droplink
  /// Field ID: b8df8215-ceb2-4f11-b725-4f5080f4ab18
  defaultRadius: string;

  /// The Description field.
  /// Field Type: Multi-Line Text
  /// Field ID: 50365886-475f-4656-a7d9-bac8bea0c413
  description: string;

  /// The Radiuses field.
  /// Field Type: Multilist
  /// Field ID: f0d8d855-8257-4096-907f-752b50a62744
  radiuses: string;

  /// The Stores field.
  /// Field Type: Multilist
  /// Field ID: 43aa9312-0033-41de-b127-59379956428f
  stores: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0f5d3b8e-badd-4617-9a6d-9102cea617e4
  title: string;

  /// The Unit Of Length field.
  /// Field Type: Droplink
  /// Field ID: 506a4f5b-4c6e-466b-8709-b212231977e5
  unitOfLength: string;
}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Store Locator
// ID: dc35377d-4d05-416a-9785-e8e380c75baf
export const StoreLocatorTemplate = {
  templateId: 'dc35377d-4d05-416a-9785-e8e380c75baf',

  /// The Countries field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: ba1bbc85-7fbe-4beb-a8e2-e36b739e2f42</para>
  countriesFieldId: 'ba1bbc85-7fbe-4beb-a8e2-e36b739e2f42',
  countriesFieldName: 'Countries',

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

  /// The Default Radius field.
  /// <para>Field Type: Droplink</para>
  /// <para>Field ID: b8df8215-ceb2-4f11-b725-4f5080f4ab18</para>
  defaultRadiusFieldId: 'b8df8215-ceb2-4f11-b725-4f5080f4ab18',
  defaultRadiusFieldName: 'Default Radius',

  /// The Description field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 50365886-475f-4656-a7d9-bac8bea0c413</para>
  descriptionFieldId: '50365886-475f-4656-a7d9-bac8bea0c413',
  descriptionFieldName: 'Description',

  /// The Radiuses field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: f0d8d855-8257-4096-907f-752b50a62744</para>
  radiusesFieldId: 'f0d8d855-8257-4096-907f-752b50a62744',
  radiusesFieldName: 'Radiuses',

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

  /// The Unit Of Length field.
  /// <para>Field Type: Droplink</para>
  /// <para>Field ID: 506a4f5b-4c6e-466b-8709-b212231977e5</para>
  unitOfLengthFieldId: '506a4f5b-4c6e-466b-8709-b212231977e5',
  unitOfLengthFieldName: 'Unit Of Length',
};

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Stores Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export interface StoresFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Stores Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export interface StoresFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/StoreLocator/Stores Folder
// ID: fe957ab9-71b5-4c9f-a34c-1e640acb0abb
export const StoresFolderTemplate = {
  templateId: 'fe957ab9-71b5-4c9f-a34c-1e640acb0abb',
};
