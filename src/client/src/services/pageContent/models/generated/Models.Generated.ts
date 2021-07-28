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

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission
// ID: 829bfcfc-d56b-4025-affa-3967aa246356
export interface AboutMissionDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission
// ID: 829bfcfc-d56b-4025-affa-3967aa246356
export interface AboutMissionRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission
// ID: 829bfcfc-d56b-4025-affa-3967aa246356
export const AboutMissionTemplate = {
  templateId: '829bfcfc-d56b-4025-affa-3967aa246356',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission Paragraph
// ID: 3a856aa5-4790-4e47-b46b-babc49c34f3b
export interface AboutMissionParagraphDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 8e352f92-e46b-4019-98ba-de8932b91479
  text: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 303f54bb-3bd0-4172-81a5-bc6bcf0a6b6b
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission Paragraph
// ID: 3a856aa5-4790-4e47-b46b-babc49c34f3b
export interface AboutMissionParagraphRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 8e352f92-e46b-4019-98ba-de8932b91479
  text: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 303f54bb-3bd0-4172-81a5-bc6bcf0a6b6b
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Mission/About Mission Paragraph
// ID: 3a856aa5-4790-4e47-b46b-babc49c34f3b
export const AboutMissionParagraphTemplate = {
  templateId: '3a856aa5-4790-4e47-b46b-babc49c34f3b',

  /// The Text field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 8e352f92-e46b-4019-98ba-de8932b91479</para>
  textFieldId: '8e352f92-e46b-4019-98ba-de8932b91479',
  textFieldName: 'Text',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 303f54bb-3bd0-4172-81a5-bc6bcf0a6b6b</para>
  titleFieldId: '303f54bb-3bd0-4172-81a5-bc6bcf0a6b6b',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner
// ID: db5f5088-5e5b-4480-a7ac-9560eb0f7210
export interface AboutUsBannerDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner
// ID: db5f5088-5e5b-4480-a7ac-9560eb0f7210
export interface AboutUsBannerRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner
// ID: db5f5088-5e5b-4480-a7ac-9560eb0f7210
export const AboutUsBannerTemplate = {
  templateId: 'db5f5088-5e5b-4480-a7ac-9560eb0f7210',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner Item
// ID: 19fac856-e53a-4b70-8283-e01b995f0921
export interface AboutUsBannerItemDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: aa21d98d-9b76-45ec-9526-5de099730af5
  image: ReactJssModule.ImageField;

  /// The Price field.
  /// Field Type: Single-Line Text
  /// Field ID: 3d9b2ee7-fba8-4d5e-a511-f05465c7f5b7
  price: ReactJssModule.TextField;

  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: abf631e7-0fb3-4b26-8c57-399e2c57a830
  subtitle: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: e82aad0f-7170-4659-ac5e-418b9b95d461
  title: ReactJssModule.TextField;

  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: a6dce57c-2e1d-46f0-9041-6faa6dcdd476
  uri: ReactJssModule.LinkField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner Item
// ID: 19fac856-e53a-4b70-8283-e01b995f0921
export interface AboutUsBannerItemRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: aa21d98d-9b76-45ec-9526-5de099730af5
  image: string;

  /// The Price field.
  /// Field Type: Single-Line Text
  /// Field ID: 3d9b2ee7-fba8-4d5e-a511-f05465c7f5b7
  price: string;

  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: abf631e7-0fb3-4b26-8c57-399e2c57a830
  subtitle: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: e82aad0f-7170-4659-ac5e-418b9b95d461
  title: string;

  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: a6dce57c-2e1d-46f0-9041-6faa6dcdd476
  uri: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Banner/About Us Banner Item
// ID: 19fac856-e53a-4b70-8283-e01b995f0921
export const AboutUsBannerItemTemplate = {
  templateId: '19fac856-e53a-4b70-8283-e01b995f0921',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: aa21d98d-9b76-45ec-9526-5de099730af5</para>
  imageFieldId: 'aa21d98d-9b76-45ec-9526-5de099730af5',
  imageFieldName: 'Image',

  /// The Price field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 3d9b2ee7-fba8-4d5e-a511-f05465c7f5b7</para>
  priceFieldId: '3d9b2ee7-fba8-4d5e-a511-f05465c7f5b7',
  priceFieldName: 'Price',

  /// The Subtitle field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: abf631e7-0fb3-4b26-8c57-399e2c57a830</para>
  subtitleFieldId: 'abf631e7-0fb3-4b26-8c57-399e2c57a830',
  subtitleFieldName: 'Subtitle',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: e82aad0f-7170-4659-ac5e-418b9b95d461</para>
  titleFieldId: 'e82aad0f-7170-4659-ac5e-418b9b95d461',
  titleFieldName: 'Title',

  /// The Uri field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: a6dce57c-2e1d-46f0-9041-6faa6dcdd476</para>
  uriFieldId: 'a6dce57c-2e1d-46f0-9041-6faa6dcdd476',
  uriFieldName: 'Uri',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Us Folder
// ID: 82ff551a-fb69-4e09-befa-1bec040f5a62
export interface AboutUsFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Us Folder
// ID: 82ff551a-fb69-4e09-befa-1bec040f5a62
export interface AboutUsFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/About Us Folder
// ID: 82ff551a-fb69-4e09-befa-1bec040f5a62
export const AboutUsFolderTemplate = {
  templateId: '82ff551a-fb69-4e09-befa-1bec040f5a62',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages
// ID: 41e8abef-3803-47b0-adc6-d225a8d91ffb
export interface AdvantagesDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Description field.
  /// Field Type: Single-Line Text
  /// Field ID: 44a66b2a-190e-4b2a-b42a-caf3eac0107d
  description: ReactJssModule.TextField;

  /// The Image field.
  /// Field Type: Single-Line Text
  /// Field ID: ff3f4d26-a588-421b-b17c-3aa70552e6da
  image: ReactJssModule.TextField;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: 80968376-a314-4a17-8182-9123a819536b
  link: ReactJssModule.LinkField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 205f79a5-c5bf-498c-8863-6d669f3f6341
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages
// ID: 41e8abef-3803-47b0-adc6-d225a8d91ffb
export interface AdvantagesRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Description field.
  /// Field Type: Single-Line Text
  /// Field ID: 44a66b2a-190e-4b2a-b42a-caf3eac0107d
  description: string;

  /// The Image field.
  /// Field Type: Single-Line Text
  /// Field ID: ff3f4d26-a588-421b-b17c-3aa70552e6da
  image: string;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: 80968376-a314-4a17-8182-9123a819536b
  link: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 205f79a5-c5bf-498c-8863-6d669f3f6341
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages
// ID: 41e8abef-3803-47b0-adc6-d225a8d91ffb
export const AdvantagesTemplate = {
  templateId: '41e8abef-3803-47b0-adc6-d225a8d91ffb',

  /// The Description field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 44a66b2a-190e-4b2a-b42a-caf3eac0107d</para>
  descriptionFieldId: '44a66b2a-190e-4b2a-b42a-caf3eac0107d',
  descriptionFieldName: 'Description',

  /// The Image field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: ff3f4d26-a588-421b-b17c-3aa70552e6da</para>
  imageFieldId: 'ff3f4d26-a588-421b-b17c-3aa70552e6da',
  imageFieldName: 'Image',

  /// The Link field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: 80968376-a314-4a17-8182-9123a819536b</para>
  linkFieldId: '80968376-a314-4a17-8182-9123a819536b',
  linkFieldName: 'Link',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 205f79a5-c5bf-498c-8863-6d669f3f6341</para>
  titleFieldId: '205f79a5-c5bf-498c-8863-6d669f3f6341',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages Folder
// ID: 48571882-2196-4e19-a0fa-673cede42b56
export interface AdvantagesFolderDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Advantages field.
  /// Field Type: Multilist
  /// Field ID: 0e23c1d8-37c8-420a-90bf-b669bdbd01b1
  advantages: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages Folder
// ID: 48571882-2196-4e19-a0fa-673cede42b56
export interface AdvantagesFolderRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Advantages field.
  /// Field Type: Multilist
  /// Field ID: 0e23c1d8-37c8-420a-90bf-b669bdbd01b1
  advantages: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Advantages Folder
// ID: 48571882-2196-4e19-a0fa-673cede42b56
export const AdvantagesFolderTemplate = {
  templateId: '48571882-2196-4e19-a0fa-673cede42b56',

  /// The Advantages field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: 0e23c1d8-37c8-420a-90bf-b669bdbd01b1</para>
  advantagesFieldId: '0e23c1d8-37c8-420a-90bf-b669bdbd01b1',
  advantagesFieldName: 'Advantages',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Rendering Parameters/Advantages Rendering Parameters
// ID: e01b4fdd-75cf-4632-84b1-74a75dba4a4f
export interface AdvantagesRenderingParametersDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Rendering Parameters/Advantages Rendering Parameters
// ID: e01b4fdd-75cf-4632-84b1-74a75dba4a4f
export interface AdvantagesRenderingParametersRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Advantages/Rendering Parameters/Advantages Rendering Parameters
// ID: e01b4fdd-75cf-4632-84b1-74a75dba4a4f
export const AdvantagesRenderingParametersTemplate = {
  templateId: 'e01b4fdd-75cf-4632-84b1-74a75dba4a4f',
};

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

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logo
// ID: c451dcea-1dc8-4de8-9e1d-475633ba3d4d
export interface BrandLogoDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: 0f9d658b-1cfa-4e70-ac2d-a541841ace2b
  image: ReactJssModule.ImageField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logo
// ID: c451dcea-1dc8-4de8-9e1d-475633ba3d4d
export interface BrandLogoRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: 0f9d658b-1cfa-4e70-ac2d-a541841ace2b
  image: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logo
// ID: c451dcea-1dc8-4de8-9e1d-475633ba3d4d
export const BrandLogoTemplate = {
  templateId: 'c451dcea-1dc8-4de8-9e1d-475633ba3d4d',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: 0f9d658b-1cfa-4e70-ac2d-a541841ace2b</para>
  imageFieldId: '0f9d658b-1cfa-4e70-ac2d-a541841ace2b',
  imageFieldName: 'Image',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logos
// ID: 42ac7b43-aa41-407c-bbb8-92f61642eb17
export interface BrandLogosDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logos
// ID: 42ac7b43-aa41-407c-bbb8-92f61642eb17
export interface BrandLogosRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Brand Logos/Brand Logos
// ID: 42ac7b43-aa41-407c-bbb8-92f61642eb17
export const BrandLogosTemplate = {
  templateId: '42ac7b43-aa41-407c-bbb8-92f61642eb17',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner
// ID: f3e176b7-c8d9-4c1f-bbe9-e83ce643e0d7
export interface CarouselBannerDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: f335bcc8-a000-4246-b7a5-ebb11e5ffc77
  image: ReactJssModule.ImageField;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: 4d22d3fa-f785-4f8c-92eb-b75ab4cf6e4f
  link: ReactJssModule.LinkField;

  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: 1751fa11-d5ca-42c7-b63a-da23a47f37db
  subtitle: ReactJssModule.TextField;

  /// The Text field.
  /// Field Type: Single-Line Text
  /// Field ID: ab878a5d-cfad-471c-949e-1f590caa233c
  text: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: b2b0d2d9-a822-4647-80c9-0b221b8cb569
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner
// ID: f3e176b7-c8d9-4c1f-bbe9-e83ce643e0d7
export interface CarouselBannerRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: f335bcc8-a000-4246-b7a5-ebb11e5ffc77
  image: string;

  /// The Link field.
  /// Field Type: General Link
  /// Field ID: 4d22d3fa-f785-4f8c-92eb-b75ab4cf6e4f
  link: string;

  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: 1751fa11-d5ca-42c7-b63a-da23a47f37db
  subtitle: string;

  /// The Text field.
  /// Field Type: Single-Line Text
  /// Field ID: ab878a5d-cfad-471c-949e-1f590caa233c
  text: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: b2b0d2d9-a822-4647-80c9-0b221b8cb569
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner
// ID: f3e176b7-c8d9-4c1f-bbe9-e83ce643e0d7
export const CarouselBannerTemplate = {
  templateId: 'f3e176b7-c8d9-4c1f-bbe9-e83ce643e0d7',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: f335bcc8-a000-4246-b7a5-ebb11e5ffc77</para>
  imageFieldId: 'f335bcc8-a000-4246-b7a5-ebb11e5ffc77',
  imageFieldName: 'Image',

  /// The Link field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: 4d22d3fa-f785-4f8c-92eb-b75ab4cf6e4f</para>
  linkFieldId: '4d22d3fa-f785-4f8c-92eb-b75ab4cf6e4f',
  linkFieldName: 'Link',

  /// The Subtitle field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 1751fa11-d5ca-42c7-b63a-da23a47f37db</para>
  subtitleFieldId: '1751fa11-d5ca-42c7-b63a-da23a47f37db',
  subtitleFieldName: 'Subtitle',

  /// The Text field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: ab878a5d-cfad-471c-949e-1f590caa233c</para>
  textFieldId: 'ab878a5d-cfad-471c-949e-1f590caa233c',
  textFieldName: 'Text',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: b2b0d2d9-a822-4647-80c9-0b221b8cb569</para>
  titleFieldId: 'b2b0d2d9-a822-4647-80c9-0b221b8cb569',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner Folder
// ID: 764f7c1b-1d67-43fb-9074-df87a0deb60e
export interface CarouselBannerFolderDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Banners field.
  /// Field Type: Multilist
  /// Field ID: e7bbd25a-3f42-4199-9e19-19a87b7020e7
  banners: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner Folder
// ID: 764f7c1b-1d67-43fb-9074-df87a0deb60e
export interface CarouselBannerFolderRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Banners field.
  /// Field Type: Multilist
  /// Field ID: e7bbd25a-3f42-4199-9e19-19a87b7020e7
  banners: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Carousel Banner/Carousel Banner Folder
// ID: 764f7c1b-1d67-43fb-9074-df87a0deb60e
export const CarouselBannerFolderTemplate = {
  templateId: '764f7c1b-1d67-43fb-9074-df87a0deb60e',

  /// The Banners field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: e7bbd25a-3f42-4199-9e19-19a87b7020e7</para>
  bannersFieldId: 'e7bbd25a-3f42-4199-9e19-19a87b7020e7',
  bannersFieldName: 'Banners',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Address
// ID: 4336232f-c7d3-4820-9784-11323307c054
export interface ContactAddressDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Address field.
  /// Field Type: Single-Line Text
  /// Field ID: 988886e4-c5bf-4fd0-803f-61d1cae54112
  address: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Address
// ID: 4336232f-c7d3-4820-9784-11323307c054
export interface ContactAddressRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Address field.
  /// Field Type: Single-Line Text
  /// Field ID: 988886e4-c5bf-4fd0-803f-61d1cae54112
  address: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Address
// ID: 4336232f-c7d3-4820-9784-11323307c054
export const ContactAddressTemplate = {
  templateId: '4336232f-c7d3-4820-9784-11323307c054',

  /// The Address field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 988886e4-c5bf-4fd0-803f-61d1cae54112</para>
  addressFieldId: '988886e4-c5bf-4fd0-803f-61d1cae54112',
  addressFieldName: 'Address',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Addresses
// ID: e31ddc89-fefd-47d4-8f57-240af742ec35
export interface ContactAddressesDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Addresses
// ID: e31ddc89-fefd-47d4-8f57-240af742ec35
export interface ContactAddressesRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Addresses
// ID: e31ddc89-fefd-47d4-8f57-240af742ec35
export const ContactAddressesTemplate = {
  templateId: 'e31ddc89-fefd-47d4-8f57-240af742ec35',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Intro
// ID: 252becf7-cdf0-4cf7-970c-313d1e98c0a1
export interface ContactIntroDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: 31f05fc6-a229-406d-b57d-14c13a4a7150
  image: ReactJssModule.ImageField;

  /// The Intro Line field.
  /// Field Type: Single-Line Text
  /// Field ID: 12c05cc3-2307-49b2-9230-4c9816463bb1
  introLine: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Intro
// ID: 252becf7-cdf0-4cf7-970c-313d1e98c0a1
export interface ContactIntroRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Image field.
  /// Field Type: Image
  /// Field ID: 31f05fc6-a229-406d-b57d-14c13a4a7150
  image: string;

  /// The Intro Line field.
  /// Field Type: Single-Line Text
  /// Field ID: 12c05cc3-2307-49b2-9230-4c9816463bb1
  introLine: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Intro
// ID: 252becf7-cdf0-4cf7-970c-313d1e98c0a1
export const ContactIntroTemplate = {
  templateId: '252becf7-cdf0-4cf7-970c-313d1e98c0a1',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: 31f05fc6-a229-406d-b57d-14c13a4a7150</para>
  imageFieldId: '31f05fc6-a229-406d-b57d-14c13a4a7150',
  imageFieldName: 'Image',

  /// The Intro Line field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 12c05cc3-2307-49b2-9230-4c9816463bb1</para>
  introLineFieldId: '12c05cc3-2307-49b2-9230-4c9816463bb1',
  introLineFieldName: 'Intro Line',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Link
// ID: c140817d-2718-4c74-8f0f-46d220b0fa91
export interface ContactLinkDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: a32b6e94-4bd9-4456-a9bf-40a1fc46c242
  uri: ReactJssModule.LinkField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Link
// ID: c140817d-2718-4c74-8f0f-46d220b0fa91
export interface ContactLinkRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: a32b6e94-4bd9-4456-a9bf-40a1fc46c242
  uri: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Link
// ID: c140817d-2718-4c74-8f0f-46d220b0fa91
export const ContactLinkTemplate = {
  templateId: 'c140817d-2718-4c74-8f0f-46d220b0fa91',

  /// The Uri field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: a32b6e94-4bd9-4456-a9bf-40a1fc46c242</para>
  uriFieldId: 'a32b6e94-4bd9-4456-a9bf-40a1fc46c242',
  uriFieldName: 'Uri',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Links
// ID: 1e890498-c6cc-440a-9fbc-397d0e998b59
export interface ContactLinksDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Links
// ID: 1e890498-c6cc-440a-9fbc-397d0e998b59
export interface ContactLinksRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Links
// ID: 1e890498-c6cc-440a-9fbc-397d0e998b59
export const ContactLinksTemplate = {
  templateId: '1e890498-c6cc-440a-9fbc-397d0e998b59',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phone
// ID: ba4cab76-aa33-465b-a385-a8b1d045b3cf
export interface ContactPhoneDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Phone field.
  /// Field Type: Single-Line Text
  /// Field ID: eb187aa2-499d-4881-8284-4d77690fac60
  phone: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phone
// ID: ba4cab76-aa33-465b-a385-a8b1d045b3cf
export interface ContactPhoneRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Phone field.
  /// Field Type: Single-Line Text
  /// Field ID: eb187aa2-499d-4881-8284-4d77690fac60
  phone: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phone
// ID: ba4cab76-aa33-465b-a385-a8b1d045b3cf
export const ContactPhoneTemplate = {
  templateId: 'ba4cab76-aa33-465b-a385-a8b1d045b3cf',

  /// The Phone field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: eb187aa2-499d-4881-8284-4d77690fac60</para>
  phoneFieldId: 'eb187aa2-499d-4881-8284-4d77690fac60',
  phoneFieldName: 'Phone',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phones
// ID: 9300407f-67f7-459c-ae3d-ba2680058e21
export interface ContactPhonesDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phones
// ID: 9300407f-67f7-459c-ae3d-ba2680058e21
export interface ContactPhonesRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Phones
// ID: 9300407f-67f7-459c-ae3d-ba2680058e21
export const ContactPhonesTemplate = {
  templateId: '9300407f-67f7-459c-ae3d-ba2680058e21',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Us Folder
// ID: 6a683694-25c8-48da-9577-f9964fd58f17
export interface ContactUsFolderDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Us Folder
// ID: 6a683694-25c8-48da-9577-f9964fd58f17
export interface ContactUsFolderRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contact Us Folder
// ID: 6a683694-25c8-48da-9577-f9964fd58f17
export const ContactUsFolderTemplate = {
  templateId: '6a683694-25c8-48da-9577-f9964fd58f17',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contacts
// ID: 1b87628c-ea15-4c9e-8f6f-339bbcf222ee
export interface ContactsDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Addresses field.
  /// Field Type: Multilist
  /// Field ID: 657f4729-c214-4c33-919a-936ca4012273
  addresses: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Links field.
  /// Field Type: Multilist
  /// Field ID: 11b3488a-1d66-42fb-a92d-0e9030d3ff46
  links: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

  /// The Phones field.
  /// Field Type: Multilist
  /// Field ID: e27ca199-3309-4618-bc8a-fcb867ffc15c
  phones: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contacts
// ID: 1b87628c-ea15-4c9e-8f6f-339bbcf222ee
export interface ContactsRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Addresses field.
  /// Field Type: Multilist
  /// Field ID: 657f4729-c214-4c33-919a-936ca4012273
  addresses: string;

  /// The Links field.
  /// Field Type: Multilist
  /// Field ID: 11b3488a-1d66-42fb-a92d-0e9030d3ff46
  links: string;

  /// The Phones field.
  /// Field Type: Multilist
  /// Field ID: e27ca199-3309-4618-bc8a-fcb867ffc15c
  phones: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Contacts
// ID: 1b87628c-ea15-4c9e-8f6f-339bbcf222ee
export const ContactsTemplate = {
  templateId: '1b87628c-ea15-4c9e-8f6f-339bbcf222ee',

  /// The Addresses field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: 657f4729-c214-4c33-919a-936ca4012273</para>
  addressesFieldId: '657f4729-c214-4c33-919a-936ca4012273',
  addressesFieldName: 'Addresses',

  /// The Links field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: 11b3488a-1d66-42fb-a92d-0e9030d3ff46</para>
  linksFieldId: '11b3488a-1d66-42fb-a92d-0e9030d3ff46',
  linksFieldName: 'Links',

  /// The Phones field.
  /// <para>Field Type: Multilist</para>
  /// <para>Field ID: e27ca199-3309-4618-bc8a-fcb867ffc15c</para>
  phonesFieldId: 'e27ca199-3309-4618-bc8a-fcb867ffc15c',
  phonesFieldName: 'Phones',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Fact
// ID: 465438d2-f2d3-4034-a2f5-38fe8bd243cc
export interface FunFactDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Count field.
  /// Field Type: Single-Line Text
  /// Field ID: fe79f45e-da7a-4924-ba49-9d3b73774a59
  count: ReactJssModule.TextField;

  /// The Icon Class field.
  /// Field Type: Single-Line Text
  /// Field ID: c6d1bff6-7e74-44f8-ab16-f494c1de52f9
  iconClass: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0c30560c-6b97-4505-818d-21f2c5316b55
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Fact
// ID: 465438d2-f2d3-4034-a2f5-38fe8bd243cc
export interface FunFactRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Count field.
  /// Field Type: Single-Line Text
  /// Field ID: fe79f45e-da7a-4924-ba49-9d3b73774a59
  count: string;

  /// The Icon Class field.
  /// Field Type: Single-Line Text
  /// Field ID: c6d1bff6-7e74-44f8-ab16-f494c1de52f9
  iconClass: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 0c30560c-6b97-4505-818d-21f2c5316b55
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Fact
// ID: 465438d2-f2d3-4034-a2f5-38fe8bd243cc
export const FunFactTemplate = {
  templateId: '465438d2-f2d3-4034-a2f5-38fe8bd243cc',

  /// The Count field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: fe79f45e-da7a-4924-ba49-9d3b73774a59</para>
  countFieldId: 'fe79f45e-da7a-4924-ba49-9d3b73774a59',
  countFieldName: 'Count',

  /// The Icon Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: c6d1bff6-7e74-44f8-ab16-f494c1de52f9</para>
  iconClassFieldId: 'c6d1bff6-7e74-44f8-ab16-f494c1de52f9',
  iconClassFieldName: 'Icon Class',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 0c30560c-6b97-4505-818d-21f2c5316b55</para>
  titleFieldId: '0c30560c-6b97-4505-818d-21f2c5316b55',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Facts
// ID: b05afe71-d5f0-42d7-8f4f-ff4bcf446aee
export interface FunFactsDataSource extends ReactJssModule.BaseDataSourceItem {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Facts
// ID: b05afe71-d5f0-42d7-8f4f-ff4bcf446aee
export interface FunFactsRenderingParams extends ReactJssModule.BaseRenderingParam {}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Fun Facts/Fun Facts
// ID: b05afe71-d5f0-42d7-8f4f-ff4bcf446aee
export const FunFactsTemplate = {
  templateId: 'b05afe71-d5f0-42d7-8f4f-ff4bcf446aee',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Get In Touch Form
// ID: 611e79bd-6fbf-4fee-8f66-94b27238c033
export interface GetInTouchFormDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Email Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: cd7f5292-5278-40d1-b004-5ad19595c96e
  emailPlaceholder: ReactJssModule.TextField;

  /// The Form Title field.
  /// Field Type: Single-Line Text
  /// Field ID: b22d3283-8235-4521-a321-958dae1d0403
  formTitle: ReactJssModule.TextField;

  /// The Message Placeholder field.
  /// Field Type: Multi-Line Text
  /// Field ID: 5ffe7eee-73a0-4320-be91-777b6f42457e
  messagePlaceholder: ReactJssModule.TextField;

  /// The Name Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: 61f745b2-2e40-48af-90b3-ace97cb8f17d
  namePlaceholder: ReactJssModule.TextField;

  /// The Subject Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: 5d7813fd-a32d-47d6-a690-369da5851958
  subjectPlaceholder: ReactJssModule.TextField;

  /// The Submit Button Text field.
  /// Field Type: Single-Line Text
  /// Field ID: 1ff5310c-3ed4-4c5b-ac20-79257e8d608e
  submitButtonText: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Get In Touch Form
// ID: 611e79bd-6fbf-4fee-8f66-94b27238c033
export interface GetInTouchFormRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Email Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: cd7f5292-5278-40d1-b004-5ad19595c96e
  emailPlaceholder: string;

  /// The Form Title field.
  /// Field Type: Single-Line Text
  /// Field ID: b22d3283-8235-4521-a321-958dae1d0403
  formTitle: string;

  /// The Message Placeholder field.
  /// Field Type: Multi-Line Text
  /// Field ID: 5ffe7eee-73a0-4320-be91-777b6f42457e
  messagePlaceholder: string;

  /// The Name Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: 61f745b2-2e40-48af-90b3-ace97cb8f17d
  namePlaceholder: string;

  /// The Subject Placeholder field.
  /// Field Type: Single-Line Text
  /// Field ID: 5d7813fd-a32d-47d6-a690-369da5851958
  subjectPlaceholder: string;

  /// The Submit Button Text field.
  /// Field Type: Single-Line Text
  /// Field ID: 1ff5310c-3ed4-4c5b-ac20-79257e8d608e
  submitButtonText: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/Contact Us/Get In Touch Form
// ID: 611e79bd-6fbf-4fee-8f66-94b27238c033
export const GetInTouchFormTemplate = {
  templateId: '611e79bd-6fbf-4fee-8f66-94b27238c033',

  /// The Email Placeholder field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: cd7f5292-5278-40d1-b004-5ad19595c96e</para>
  emailPlaceholderFieldId: 'cd7f5292-5278-40d1-b004-5ad19595c96e',
  emailPlaceholderFieldName: 'Email Placeholder',

  /// The Form Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: b22d3283-8235-4521-a321-958dae1d0403</para>
  formTitleFieldId: 'b22d3283-8235-4521-a321-958dae1d0403',
  formTitleFieldName: 'Form Title',

  /// The Message Placeholder field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 5ffe7eee-73a0-4320-be91-777b6f42457e</para>
  messagePlaceholderFieldId: '5ffe7eee-73a0-4320-be91-777b6f42457e',
  messagePlaceholderFieldName: 'Message Placeholder',

  /// The Name Placeholder field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 61f745b2-2e40-48af-90b3-ace97cb8f17d</para>
  namePlaceholderFieldId: '61f745b2-2e40-48af-90b3-ace97cb8f17d',
  namePlaceholderFieldName: 'Name Placeholder',

  /// The Subject Placeholder field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 5d7813fd-a32d-47d6-a690-369da5851958</para>
  subjectPlaceholderFieldId: '5d7813fd-a32d-47d6-a690-369da5851958',
  subjectPlaceholderFieldName: 'Subject Placeholder',

  /// The Submit Button Text field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 1ff5310c-3ed4-4c5b-ac20-79257e8d608e</para>
  submitButtonTextFieldId: '1ff5310c-3ed4-4c5b-ac20-79257e8d608e',
  submitButtonTextFieldName: 'Submit Button Text',
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

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team
// ID: 3921f0ca-20fb-4c66-bf7b-3d936e97d936
export interface TeamDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 28913ab4-ede0-4201-b070-b3e376729d0e
  text: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: e4c9c92f-23af-4cb0-bbcf-323fa80109ba
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team
// ID: 3921f0ca-20fb-4c66-bf7b-3d936e97d936
export interface TeamRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 28913ab4-ede0-4201-b070-b3e376729d0e
  text: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: e4c9c92f-23af-4cb0-bbcf-323fa80109ba
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team
// ID: 3921f0ca-20fb-4c66-bf7b-3d936e97d936
export const TeamTemplate = {
  templateId: '3921f0ca-20fb-4c66-bf7b-3d936e97d936',

  /// The Text field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 28913ab4-ede0-4201-b070-b3e376729d0e</para>
  textFieldId: '28913ab4-ede0-4201-b070-b3e376729d0e',
  textFieldName: 'Text',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: e4c9c92f-23af-4cb0-bbcf-323fa80109ba</para>
  titleFieldId: 'e4c9c92f-23af-4cb0-bbcf-323fa80109ba',
  titleFieldName: 'Title',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member
// ID: 6103e1a3-7ac0-4985-98ab-9238a3485df9
export interface TeamMemberDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Full Name field.
  /// Field Type: Single-Line Text
  /// Field ID: 715e62c9-ac4f-457c-9aac-34c7f6d91b37
  fullName: ReactJssModule.TextField;

  /// The Image field.
  /// Field Type: Image
  /// Field ID: 29de299b-340f-4dea-adf0-ec4ded7dd047
  image: ReactJssModule.ImageField;

  /// The Position field.
  /// Field Type: Single-Line Text
  /// Field ID: 9739e61c-f46d-4d19-a47e-86fd8aeb08fa
  position: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member
// ID: 6103e1a3-7ac0-4985-98ab-9238a3485df9
export interface TeamMemberRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Full Name field.
  /// Field Type: Single-Line Text
  /// Field ID: 715e62c9-ac4f-457c-9aac-34c7f6d91b37
  fullName: string;

  /// The Image field.
  /// Field Type: Image
  /// Field ID: 29de299b-340f-4dea-adf0-ec4ded7dd047
  image: string;

  /// The Position field.
  /// Field Type: Single-Line Text
  /// Field ID: 9739e61c-f46d-4d19-a47e-86fd8aeb08fa
  position: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member
// ID: 6103e1a3-7ac0-4985-98ab-9238a3485df9
export const TeamMemberTemplate = {
  templateId: '6103e1a3-7ac0-4985-98ab-9238a3485df9',

  /// The Full Name field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 715e62c9-ac4f-457c-9aac-34c7f6d91b37</para>
  fullNameFieldId: '715e62c9-ac4f-457c-9aac-34c7f6d91b37',
  fullNameFieldName: 'Full Name',

  /// The Image field.
  /// <para>Field Type: Image</para>
  /// <para>Field ID: 29de299b-340f-4dea-adf0-ec4ded7dd047</para>
  imageFieldId: '29de299b-340f-4dea-adf0-ec4ded7dd047',
  imageFieldName: 'Image',

  /// The Position field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 9739e61c-f46d-4d19-a47e-86fd8aeb08fa</para>
  positionFieldId: '9739e61c-f46d-4d19-a47e-86fd8aeb08fa',
  positionFieldName: 'Position',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member Social Link
// ID: db02cc04-e764-4ee5-b9be-62a9b7fb9ce1
export interface TeamMemberSocialLinkDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Icon Class field.
  /// Field Type: Single-Line Text
  /// Field ID: e2c7da05-4ca3-41c6-a19b-5b58e8aeffbc
  iconClass: ReactJssModule.TextField;

  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: bbe13012-d82b-4ad5-bf9f-f3f8c07eaf73
  uri: ReactJssModule.LinkField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member Social Link
// ID: db02cc04-e764-4ee5-b9be-62a9b7fb9ce1
export interface TeamMemberSocialLinkRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Icon Class field.
  /// Field Type: Single-Line Text
  /// Field ID: e2c7da05-4ca3-41c6-a19b-5b58e8aeffbc
  iconClass: string;

  /// The Uri field.
  /// Field Type: General Link
  /// Field ID: bbe13012-d82b-4ad5-bf9f-f3f8c07eaf73
  uri: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Team/Team Member Social Link
// ID: db02cc04-e764-4ee5-b9be-62a9b7fb9ce1
export const TeamMemberSocialLinkTemplate = {
  templateId: 'db02cc04-e764-4ee5-b9be-62a9b7fb9ce1',

  /// The Icon Class field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: e2c7da05-4ca3-41c6-a19b-5b58e8aeffbc</para>
  iconClassFieldId: 'e2c7da05-4ca3-41c6-a19b-5b58e8aeffbc',
  iconClassFieldName: 'Icon Class',

  /// The Uri field.
  /// <para>Field Type: General Link</para>
  /// <para>Field ID: bbe13012-d82b-4ad5-bf9f-f3f8c07eaf73</para>
  uriFieldId: 'bbe13012-d82b-4ad5-bf9f-f3f8c07eaf73',
  uriFieldName: 'Uri',
};

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Welcome/Welcome
// ID: bcc4e809-3687-4676-9542-061712345c7b
export interface WelcomeDataSource extends ReactJssModule.BaseDataSourceItem {
  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: 314819dc-f55d-4d42-a238-e8db57969ee5
  subtitle: ReactJssModule.TextField;

  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 0630149f-8a91-4715-91ee-bd2483e9e3a4
  text: ReactJssModule.TextField;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 214065bc-bcd8-49fc-ab7e-58a613017935
  title: ReactJssModule.TextField;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Welcome/Welcome
// ID: bcc4e809-3687-4676-9542-061712345c7b
export interface WelcomeRenderingParams extends ReactJssModule.BaseRenderingParam {
  /// The Subtitle field.
  /// Field Type: Single-Line Text
  /// Field ID: 314819dc-f55d-4d42-a238-e8db57969ee5
  subtitle: string;

  /// The Text field.
  /// Field Type: Multi-Line Text
  /// Field ID: 0630149f-8a91-4715-91ee-bd2483e9e3a4
  text: string;

  /// The Title field.
  /// Field Type: Single-Line Text
  /// Field ID: 214065bc-bcd8-49fc-ab7e-58a613017935
  title: string;
}

// Path: /sitecore/templates/HCA/Feature/PageContent/About Us/Welcome/Welcome
// ID: bcc4e809-3687-4676-9542-061712345c7b
export const WelcomeTemplate = {
  templateId: 'bcc4e809-3687-4676-9542-061712345c7b',

  /// The Subtitle field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 314819dc-f55d-4d42-a238-e8db57969ee5</para>
  subtitleFieldId: '314819dc-f55d-4d42-a238-e8db57969ee5',
  subtitleFieldName: 'Subtitle',

  /// The Text field.
  /// <para>Field Type: Multi-Line Text</para>
  /// <para>Field ID: 0630149f-8a91-4715-91ee-bd2483e9e3a4</para>
  textFieldId: '0630149f-8a91-4715-91ee-bd2483e9e3a4',
  textFieldName: 'Text',

  /// The Title field.
  /// <para>Field Type: Single-Line Text</para>
  /// <para>Field ID: 214065bc-bcd8-49fc-ab7e-58a613017935</para>
  titleFieldId: '214065bc-bcd8-49fc-ab7e-58a613017935',
  titleFieldName: 'Title',
};
