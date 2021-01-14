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

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Network Link
// ID: 656950e2-cdb1-4899-8106-067a6b76468b
export interface SocialNetworkLinkDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Css Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 5fb8c44b-7a28-4d89-a38e-738e35911baf
    cssClass: ReactJssModule.TextField;

    /// The Title field.
    /// Field Type: Single-Line Text
    /// Field ID: d3fc7572-4cc4-42b9-92c1-10cf8c83ef42
    title: ReactJssModule.TextField;

    /// The Uri field.
    /// Field Type: General Link
    /// Field ID: c99f9ed0-3626-48ef-96ab-f837ce37e6a1
    uri: ReactJssModule.LinkField;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Network Link
// ID: 656950e2-cdb1-4899-8106-067a6b76468b
export interface SocialNetworkLinkRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Css Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 5fb8c44b-7a28-4d89-a38e-738e35911baf
    cssClass: string;

    /// The Title field.
    /// Field Type: Single-Line Text
    /// Field ID: d3fc7572-4cc4-42b9-92c1-10cf8c83ef42
    title: string;

    /// The Uri field.
    /// Field Type: General Link
    /// Field ID: c99f9ed0-3626-48ef-96ab-f837ce37e6a1
    uri: string;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Network Link
// ID: 656950e2-cdb1-4899-8106-067a6b76468b
export const SocialNetworkLinkTemplate = {
    templateId: '656950e2-cdb1-4899-8106-067a6b76468b',

    /// The Css Class field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 5fb8c44b-7a28-4d89-a38e-738e35911baf</para>
    cssClassFieldId: '5fb8c44b-7a28-4d89-a38e-738e35911baf',
    cssClassFieldName: 'Css Class',

    /// The Title field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: d3fc7572-4cc4-42b9-92c1-10cf8c83ef42</para>
    titleFieldId: 'd3fc7572-4cc4-42b9-92c1-10cf8c83ef42',
    titleFieldName: 'Title',

    /// The Uri field.
    /// <para>Field Type: General Link</para>
    /// <para>Field ID: c99f9ed0-3626-48ef-96ab-f837ce37e6a1</para>
    uriFieldId: 'c99f9ed0-3626-48ef-96ab-f837ce37e6a1',
    uriFieldName: 'Uri',

};

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links
// ID: 94fb0bb4-f041-45f7-a0ce-1e3899fc40e0
export interface SocialNetworksLinksDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Links field.
    /// Field Type: Treelist
    /// Field ID: 69e30764-9342-4ade-9c1b-db74d087537d
    links: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

    /// The Section Title field.
    /// Field Type: Single-Line Text
    /// Field ID: ef68acf6-1155-4c4c-9e38-fe1dcccb7d1f
    sectionTitle: ReactJssModule.TextField;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links
// ID: 94fb0bb4-f041-45f7-a0ce-1e3899fc40e0
export interface SocialNetworksLinksRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Links field.
    /// Field Type: Treelist
    /// Field ID: 69e30764-9342-4ade-9c1b-db74d087537d
    links: string;

    /// The Section Title field.
    /// Field Type: Single-Line Text
    /// Field ID: ef68acf6-1155-4c4c-9e38-fe1dcccb7d1f
    sectionTitle: string;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links
// ID: 94fb0bb4-f041-45f7-a0ce-1e3899fc40e0
export const SocialNetworksLinksTemplate = {
    templateId: '94fb0bb4-f041-45f7-a0ce-1e3899fc40e0',

    /// The Links field.
    /// <para>Field Type: Treelist</para>
    /// <para>Field ID: 69e30764-9342-4ade-9c1b-db74d087537d</para>
    linksFieldId: '69e30764-9342-4ade-9c1b-db74d087537d',
    linksFieldName: 'Links',

    /// The Section Title field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: ef68acf6-1155-4c4c-9e38-fe1dcccb7d1f</para>
    sectionTitleFieldId: 'ef68acf6-1155-4c4c-9e38-fe1dcccb7d1f',
    sectionTitleFieldName: 'Section Title',

};

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Folder
// ID: 06baa321-f0c9-4e47-933e-3c0ae7ba1d61
export interface SocialNetworksLinksFolderDataSource extends ReactJssModule.BaseDataSourceItem {

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Folder
// ID: 06baa321-f0c9-4e47-933e-3c0ae7ba1d61
export interface SocialNetworksLinksFolderRenderingParams extends ReactJssModule.BaseRenderingParam {

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Folder
// ID: 06baa321-f0c9-4e47-933e-3c0ae7ba1d61
export const SocialNetworksLinksFolderTemplate = {
    templateId: '06baa321-f0c9-4e47-933e-3c0ae7ba1d61',

};

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Params
// ID: 49f79318-adc9-492b-82f2-e29fa8f5dfb1
export interface SocialNetworksLinksParamsDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Container Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 489896f2-c063-433a-aef0-980605450800
    containerClass: ReactJssModule.TextField;

    /// The Show Title field.
    /// Field Type: Checkbox
    /// Field ID: 8ca8968a-f2ba-48d4-b589-b528fb251cc1
    showTitle: ReactJssModule.Field<boolean>;

    /// The Title Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 45f1d33d-bebd-4f5f-9548-ad78cf2ada54
    titleClass: ReactJssModule.TextField;

    /// The Title Tag field.
    /// Field Type: Single-Line Text
    /// Field ID: cb67858c-b67c-422f-975d-aacb0e5f845d
    titleTag: ReactJssModule.TextField;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Params
// ID: 49f79318-adc9-492b-82f2-e29fa8f5dfb1
export interface SocialNetworksLinksParamsRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Container Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 489896f2-c063-433a-aef0-980605450800
    containerClass: string;

    /// The Show Title field.
    /// Field Type: Checkbox
    /// Field ID: 8ca8968a-f2ba-48d4-b589-b528fb251cc1
    showTitle: string;

    /// The Title Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 45f1d33d-bebd-4f5f-9548-ad78cf2ada54
    titleClass: string;

    /// The Title Tag field.
    /// Field Type: Single-Line Text
    /// Field ID: cb67858c-b67c-422f-975d-aacb0e5f845d
    titleTag: string;

}

// Path: /sitecore/templates/HCA/Feature/Navigation/Social Network Links/Social Networks Links Params
// ID: 49f79318-adc9-492b-82f2-e29fa8f5dfb1
export const SocialNetworksLinksParamsTemplate = {
    templateId: '49f79318-adc9-492b-82f2-e29fa8f5dfb1',

    /// The Container Class field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 489896f2-c063-433a-aef0-980605450800</para>
    containerClassFieldId: '489896f2-c063-433a-aef0-980605450800',
    containerClassFieldName: 'Container Class',

    /// The Show Title field.
    /// <para>Field Type: Checkbox</para>
    /// <para>Field ID: 8ca8968a-f2ba-48d4-b589-b528fb251cc1</para>
    showTitleFieldId: '8ca8968a-f2ba-48d4-b589-b528fb251cc1',
    showTitleFieldName: 'Show Title',

    /// The Title Class field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 45f1d33d-bebd-4f5f-9548-ad78cf2ada54</para>
    titleClassFieldId: '45f1d33d-bebd-4f5f-9548-ad78cf2ada54',
    titleClassFieldName: 'Title Class',

    /// The Title Tag field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: cb67858c-b67c-422f-975d-aacb0e5f845d</para>
    titleTagFieldId: 'cb67858c-b67c-422f-975d-aacb0e5f845d',
    titleTagFieldName: 'Title Tag',

};
