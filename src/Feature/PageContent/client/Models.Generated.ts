// tslint:disable:no-empty-interface

import * as ReactJssModule from 'Foundation/ReactJss/client';

// Path: /sitecore/templates/HCA/Feature/PageContent/Rendering Parameters/Grid Layout
// ID: 61ed6a03-bbdb-4937-87fa-db5c53fc870c
export interface GridLayoutDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The First Column Class field.
    /// Field Type: Single-Line Text
    /// Field ID: a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88
    firstColumnClass: ReactJssModule.TextField;

    /// The Second Column Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 525bb442-fc9e-49dd-b557-52b833f0855d
    secondColumnClass: ReactJssModule.TextField;

    /// The Wrapper Class field.
    /// Field Type: Single-Line Text
    /// Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52
    wrapperClass: ReactJssModule.TextField;

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Rendering Parameters/Grid Layout
// ID: 61ed6a03-bbdb-4937-87fa-db5c53fc870c
export interface GridLayoutRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The First Column Class field.
    /// Field Type: Single-Line Text
    /// Field ID: a10a1cf0-25e7-4b9f-a9ac-6d40de3cbe88
    firstColumnClass: string;

    /// The Second Column Class field.
    /// Field Type: Single-Line Text
    /// Field ID: 525bb442-fc9e-49dd-b557-52b833f0855d
    secondColumnClass: string;

    /// The Wrapper Class field.
    /// Field Type: Single-Line Text
    /// Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52
    wrapperClass: string;

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Rendering Parameters/Grid Layout
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

    /// The Wrapper Class field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: b4386ac8-1f1b-49f3-adcb-264f5f595e52</para>
    wrapperClassFieldId: 'b4386ac8-1f1b-49f3-adcb-264f5f595e52',
    wrapperClassFieldName: 'Wrapper Class',

};

// Path: /sitecore/templates/HCA/Feature/PageContent/Home
// ID: 309e5f2f-cb6f-4e51-9a39-185be515f278
export interface HomeDataSource extends ReactJssModule.BaseDataSourceItem {

    /// The Main Promo Image field.
    /// Field Type: Image
    /// Field ID: e61322f9-d70b-4a4b-ba2c-9359584e3362
    mainPromoImage: ReactJssModule.ImageField;

    /// The Main Promo Text field.
    /// Field Type: Single-Line Text
    /// Field ID: 010272e8-caad-4046-9410-7d4c67e95bfb
    mainPromoText: ReactJssModule.TextField;

    /// The Promo A Image field.
    /// Field Type: Image
    /// Field ID: 0af1cec2-d257-44f8-a034-0c6624da9560
    promoAImage: ReactJssModule.ImageField;

    /// The Promo A Text field.
    /// Field Type: Single-Line Text
    /// Field ID: 0612860c-f8be-428b-a432-e2aed3cb5776
    promoAText: ReactJssModule.TextField;

    /// The Promo B Image field.
    /// Field Type: Image
    /// Field ID: d743c370-80dc-41d3-9c39-58b67dc9176c
    promoBImage: ReactJssModule.ImageField;

    /// The Promo C Image field.
    /// Field Type: Image
    /// Field ID: 4eeb0814-a262-45f1-beea-7956ed5e15a6
    promoCImage: ReactJssModule.ImageField;

    /// The Promo D Image field.
    /// Field Type: Image
    /// Field ID: 2c20de23-de06-4b1d-95f2-c39d911c1518
    promoDImage: ReactJssModule.ImageField;

    /// The Slider Header field.
    /// Field Type: Single-Line Text
    /// Field ID: 6a98f330-5d79-480b-a700-ce29bd09570f
    sliderHeader: ReactJssModule.TextField;

    /// The Wide Promo Image field.
    /// Field Type: Image
    /// Field ID: 5567973b-f2f0-445c-b9b8-76d1d41c23ae
    widePromoImage: ReactJssModule.ImageField;

    /// The Wide Promo Text field.
    /// Field Type: Single-Line Text
    /// Field ID: bbca79c1-1f9e-43b3-a5d4-882f1a6b1a32
    widePromoText: ReactJssModule.TextField;

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Home
// ID: 309e5f2f-cb6f-4e51-9a39-185be515f278
export interface HomeRenderingParams extends ReactJssModule.BaseRenderingParam {

    /// The Main Promo Image field.
    /// Field Type: Image
    /// Field ID: e61322f9-d70b-4a4b-ba2c-9359584e3362
    mainPromoImage: string;

    /// The Main Promo Text field.
    /// Field Type: Single-Line Text
    /// Field ID: 010272e8-caad-4046-9410-7d4c67e95bfb
    mainPromoText: string;

    /// The Promo A Image field.
    /// Field Type: Image
    /// Field ID: 0af1cec2-d257-44f8-a034-0c6624da9560
    promoAImage: string;

    /// The Promo A Text field.
    /// Field Type: Single-Line Text
    /// Field ID: 0612860c-f8be-428b-a432-e2aed3cb5776
    promoAText: string;

    /// The Promo B Image field.
    /// Field Type: Image
    /// Field ID: d743c370-80dc-41d3-9c39-58b67dc9176c
    promoBImage: string;

    /// The Promo C Image field.
    /// Field Type: Image
    /// Field ID: 4eeb0814-a262-45f1-beea-7956ed5e15a6
    promoCImage: string;

    /// The Promo D Image field.
    /// Field Type: Image
    /// Field ID: 2c20de23-de06-4b1d-95f2-c39d911c1518
    promoDImage: string;

    /// The Slider Header field.
    /// Field Type: Single-Line Text
    /// Field ID: 6a98f330-5d79-480b-a700-ce29bd09570f
    sliderHeader: string;

    /// The Wide Promo Image field.
    /// Field Type: Image
    /// Field ID: 5567973b-f2f0-445c-b9b8-76d1d41c23ae
    widePromoImage: string;

    /// The Wide Promo Text field.
    /// Field Type: Single-Line Text
    /// Field ID: bbca79c1-1f9e-43b3-a5d4-882f1a6b1a32
    widePromoText: string;

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Home
// ID: 309e5f2f-cb6f-4e51-9a39-185be515f278
export const HomeTemplate = {
    templateId: '309e5f2f-cb6f-4e51-9a39-185be515f278',

    /// The Main Promo Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: e61322f9-d70b-4a4b-ba2c-9359584e3362</para>
    mainPromoImageFieldId: 'e61322f9-d70b-4a4b-ba2c-9359584e3362',
    mainPromoImageFieldName: 'Main Promo Image',

    /// The Main Promo Text field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 010272e8-caad-4046-9410-7d4c67e95bfb</para>
    mainPromoTextFieldId: '010272e8-caad-4046-9410-7d4c67e95bfb',
    mainPromoTextFieldName: 'Main Promo Text',

    /// The Promo A Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: 0af1cec2-d257-44f8-a034-0c6624da9560</para>
    promoAImageFieldId: '0af1cec2-d257-44f8-a034-0c6624da9560',
    promoAImageFieldName: 'Promo A Image',

    /// The Promo A Text field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 0612860c-f8be-428b-a432-e2aed3cb5776</para>
    promoATextFieldId: '0612860c-f8be-428b-a432-e2aed3cb5776',
    promoATextFieldName: 'Promo A Text',

    /// The Promo B Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: d743c370-80dc-41d3-9c39-58b67dc9176c</para>
    promoBImageFieldId: 'd743c370-80dc-41d3-9c39-58b67dc9176c',
    promoBImageFieldName: 'Promo B Image',

    /// The Promo C Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: 4eeb0814-a262-45f1-beea-7956ed5e15a6</para>
    promoCImageFieldId: '4eeb0814-a262-45f1-beea-7956ed5e15a6',
    promoCImageFieldName: 'Promo C Image',

    /// The Promo D Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: 2c20de23-de06-4b1d-95f2-c39d911c1518</para>
    promoDImageFieldId: '2c20de23-de06-4b1d-95f2-c39d911c1518',
    promoDImageFieldName: 'Promo D Image',

    /// The Slider Header field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: 6a98f330-5d79-480b-a700-ce29bd09570f</para>
    sliderHeaderFieldId: '6a98f330-5d79-480b-a700-ce29bd09570f',
    sliderHeaderFieldName: 'Slider Header',

    /// The Wide Promo Image field.
    /// <para>Field Type: Image</para>
    /// <para>Field ID: 5567973b-f2f0-445c-b9b8-76d1d41c23ae</para>
    widePromoImageFieldId: '5567973b-f2f0-445c-b9b8-76d1d41c23ae',
    widePromoImageFieldName: 'Wide Promo Image',

    /// The Wide Promo Text field.
    /// <para>Field Type: Single-Line Text</para>
    /// <para>Field ID: bbca79c1-1f9e-43b3-a5d4-882f1a6b1a32</para>
    widePromoTextFieldId: 'bbca79c1-1f9e-43b3-a5d4-882f1a6b1a32',
    widePromoTextFieldName: 'Wide Promo Text',

};

// Path: /sitecore/templates/HCA/Feature/PageContent/Home Folder
// ID: 3c486a78-0beb-48d9-8eb3-9a8f87b5e7e2
export interface HomeFolderDataSource extends ReactJssModule.BaseDataSourceItem {

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Home Folder
// ID: 3c486a78-0beb-48d9-8eb3-9a8f87b5e7e2
export interface HomeFolderRenderingParams extends ReactJssModule.BaseRenderingParam {

}

// Path: /sitecore/templates/HCA/Feature/PageContent/Home Folder
// ID: 3c486a78-0beb-48d9-8eb3-9a8f87b5e7e2
export const HomeFolderTemplate = {
    templateId: '3c486a78-0beb-48d9-8eb3-9a8f87b5e7e2',

};
