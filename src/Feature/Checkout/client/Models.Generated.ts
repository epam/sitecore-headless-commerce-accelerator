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

import * as ReactJssModule from 'Foundation/ReactJss/client';

// Path: /sitecore/templates/HCA/Feature/Checkout/Data Sources/Checkout Navigation
// ID: 57cfa14b-718e-43c4-a1e0-5553ec8000b8
export interface CheckoutNavigationDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Back To Link field.
    /// Field Type: General Link
    /// Field ID: cb7502d8-cf47-4919-a528-ac7dcc1b636a
    backToLink: ReactJssModule.LinkField;

    /// The Checkout Steps field.
    /// Field Type: Multilist
    /// Field ID: 55a6fd7a-07be-4fcd-ab3a-138b4ecbdc6b
    checkoutSteps: ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>;

    /// The Info Link field.
    /// Field Type: General Link
    /// Field ID: da2a953e-b11a-4be5-8dba-f2e2c45233b8
    infoLink: ReactJssModule.LinkField;

}

// Path: /sitecore/templates/HCA/Feature/Checkout/Data Sources/Checkout Navigation
// ID: 57cfa14b-718e-43c4-a1e0-5553ec8000b8
export interface CheckoutNavigationRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Back To Link field.
    /// Field Type: General Link
    /// Field ID: cb7502d8-cf47-4919-a528-ac7dcc1b636a
    backToLink: string;

    /// The Checkout Steps field.
    /// Field Type: Multilist
    /// Field ID: 55a6fd7a-07be-4fcd-ab3a-138b4ecbdc6b
    checkoutSteps: string;

    /// The Info Link field.
    /// Field Type: General Link
    /// Field ID: da2a953e-b11a-4be5-8dba-f2e2c45233b8
    infoLink: string;

}

// Path: /sitecore/templates/HCA/Feature/Checkout/Data Sources/Checkout Navigation
// ID: 57cfa14b-718e-43c4-a1e0-5553ec8000b8
export const CheckoutNavigationTemplate = {
    templateId: '57cfa14b-718e-43c4-a1e0-5553ec8000b8',

    /// The Back To Link field.
    /// <para>Field Type: General Link</para>
    /// <para>Field ID: cb7502d8-cf47-4919-a528-ac7dcc1b636a</para>
    backToLinkFieldId: 'cb7502d8-cf47-4919-a528-ac7dcc1b636a',
    backToLinkFieldName: 'Back To Link',

    /// The Checkout Steps field.
    /// <para>Field Type: Multilist</para>
    /// <para>Field ID: 55a6fd7a-07be-4fcd-ab3a-138b4ecbdc6b</para>
    checkoutStepsFieldId: '55a6fd7a-07be-4fcd-ab3a-138b4ecbdc6b',
    checkoutStepsFieldName: 'Checkout Steps',

    /// The Info Link field.
    /// <para>Field Type: General Link</para>
    /// <para>Field ID: da2a953e-b11a-4be5-8dba-f2e2c45233b8</para>
    infoLinkFieldId: 'da2a953e-b11a-4be5-8dba-f2e2c45233b8',
    infoLinkFieldName: 'Info Link',

};

// Path: /sitecore/templates/HCA/Feature/Checkout/Abstract/_Checkout Step
// ID: 41bec638-40fe-4a75-ace5-f3377299d8fa
export interface CheckoutStepDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Checkout Step Name field.
    /// Field Type: Single-Line Text
    /// Field ID: 69f49102-c8c0-4e55-8f8e-c9cb130071e4
    checkoutStepName: ReactJssModule.TextField;

}

// Path: /sitecore/templates/HCA/Feature/Checkout/Abstract/_Checkout Step
// ID: 41bec638-40fe-4a75-ace5-f3377299d8fa
export interface CheckoutStepRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Checkout Step Name field.
    /// Field Type: Single-Line Text
    /// Field ID: 69f49102-c8c0-4e55-8f8e-c9cb130071e4
    checkoutStepName: string;

}

// Path: /sitecore/templates/HCA/Feature/Checkout/Abstract/_Checkout Step
// ID: 41bec638-40fe-4a75-ace5-f3377299d8fa
export const CheckoutStepTemplate = {
    templateId: '41bec638-40fe-4a75-ace5-f3377299d8fa',

    /// The Checkout Step Name field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 69f49102-c8c0-4e55-8f8e-c9cb130071e4</para>
    checkoutStepNameFieldId: '69f49102-c8c0-4e55-8f8e-c9cb130071e4',
    checkoutStepNameFieldName: 'Checkout Step Name',

};
