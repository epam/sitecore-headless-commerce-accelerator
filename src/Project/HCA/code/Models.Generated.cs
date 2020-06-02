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

#pragma warning disable 1591
#pragma warning disable 0108
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Rainbow JS CodeGeneration.
//     (https://github.com/asmagin/rainbow-js-codegeneration)
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCA.Project.HCA.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Fields;
    using Sitecore.Globalization;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    using System.CodeDom.Compiler;
    using global::HCA.Foundation.GlassMapper.Models;


    /// <summary>
    /// ICart Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Cart</para>
    /// <para>ID: d0466420-eeca-46a4-93b6-b17cc198e87b</para>
    /// </summary>
    [SitecoreType(TemplateId="d0466420-eeca-46a4-93b6-b17cc198e87b")]
    public partial interface ICart: IPage, IGlassBase
    {

    }


    /// <summary>
    /// Cart Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Cart</para>
    /// <para>ID: d0466420-eeca-46a4-93b6-b17cc198e87b</para>
    /// </summary>
    [SitecoreType(TemplateId="d0466420-eeca-46a4-93b6-b17cc198e87b")]
    public partial class Cart: GlassBase, ICart
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Cart
        /// </summary>
        public const string TemplateId = "d0466420-eeca-46a4-93b6-b17cc198e87b";

    }


    /// <summary>
    /// ICategory Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Category</para>
    /// <para>ID: 0cbbb134-4025-4a51-9b46-1ab7021ccb0f</para>
    /// </summary>
    [SitecoreType(TemplateId="0cbbb134-4025-4a51-9b46-1ab7021ccb0f")]
    public partial interface ICategory: IPage, IGlassBase
    {

    }


    /// <summary>
    /// Category Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Category</para>
    /// <para>ID: 0cbbb134-4025-4a51-9b46-1ab7021ccb0f</para>
    /// </summary>
    [SitecoreType(TemplateId="0cbbb134-4025-4a51-9b46-1ab7021ccb0f")]
    public partial class Category: GlassBase, ICategory
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Category
        /// </summary>
        public const string TemplateId = "0cbbb134-4025-4a51-9b46-1ab7021ccb0f";

    }


    /// <summary>
    /// ICheckout Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Checkout</para>
    /// <para>ID: d5440345-d25e-4a9b-b8e4-5c2aef3c9178</para>
    /// </summary>
    [SitecoreType(TemplateId="d5440345-d25e-4a9b-b8e4-5c2aef3c9178")]
    public partial interface ICheckout: IPage, IGlassBase
    {

    }


    /// <summary>
    /// Checkout Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Checkout</para>
    /// <para>ID: d5440345-d25e-4a9b-b8e4-5c2aef3c9178</para>
    /// </summary>
    [SitecoreType(TemplateId="d5440345-d25e-4a9b-b8e4-5c2aef3c9178")]
    public partial class Checkout: GlassBase, ICheckout
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Checkout
        /// </summary>
        public const string TemplateId = "d5440345-d25e-4a9b-b8e4-5c2aef3c9178";

    }


    /// <summary>
    /// ICheckoutFolder Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Checkout Folder</para>
    /// <para>ID: 938f39ab-1217-4a06-9c0f-f53494433851</para>
    /// </summary>
    [SitecoreType(TemplateId="938f39ab-1217-4a06-9c0f-f53494433851")]
    public partial interface ICheckoutFolder: IGlassBase
    {

    }


    /// <summary>
    /// CheckoutFolder Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Checkout Folder</para>
    /// <para>ID: 938f39ab-1217-4a06-9c0f-f53494433851</para>
    /// </summary>
    [SitecoreType(TemplateId="938f39ab-1217-4a06-9c0f-f53494433851")]
    public partial class CheckoutFolder: GlassBase, ICheckoutFolder
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Folders/Checkout Folder
        /// </summary>
        public const string TemplateId = "938f39ab-1217-4a06-9c0f-f53494433851";

    }


    /// <summary>
    /// IDataFolder Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Data Folder</para>
    /// <para>ID: 5c07e9e8-6ca9-43ad-9cec-2988a77f6d70</para>
    /// </summary>
    [SitecoreType(TemplateId="5c07e9e8-6ca9-43ad-9cec-2988a77f6d70")]
    public partial interface IDataFolder: IGlassBase
    {

    }


    /// <summary>
    /// DataFolder Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Data Folder</para>
    /// <para>ID: 5c07e9e8-6ca9-43ad-9cec-2988a77f6d70</para>
    /// </summary>
    [SitecoreType(TemplateId="5c07e9e8-6ca9-43ad-9cec-2988a77f6d70")]
    public partial class DataFolder: GlassBase, IDataFolder
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Folders/Data Folder
        /// </summary>
        public const string TemplateId = "5c07e9e8-6ca9-43ad-9cec-2988a77f6d70";

    }


    /// <summary>
    /// IHome Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Home</para>
    /// <para>ID: 83883836-8f50-4bb8-bf62-369b7661e815</para>
    /// </summary>
    [SitecoreType(TemplateId="83883836-8f50-4bb8-bf62-369b7661e815")]
    public partial interface IHome: IPage, IGlassBase
    {

    }


    /// <summary>
    /// Home Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Home</para>
    /// <para>ID: 83883836-8f50-4bb8-bf62-369b7661e815</para>
    /// </summary>
    [SitecoreType(TemplateId="83883836-8f50-4bb8-bf62-369b7661e815")]
    public partial class Home: GlassBase, IHome
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Home
        /// </summary>
        public const string TemplateId = "83883836-8f50-4bb8-bf62-369b7661e815";

    }


    /// <summary>
    /// IPage Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Page</para>
    /// <para>ID: 9ca4a8c4-0295-49c1-a879-e008933d0a4f</para>
    /// </summary>
    [SitecoreType(TemplateId="9ca4a8c4-0295-49c1-a879-e008933d0a4f")]
    public partial interface IPage: IGlassBase
    {

    }


    /// <summary>
    /// Page Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Page</para>
    /// <para>ID: 9ca4a8c4-0295-49c1-a879-e008933d0a4f</para>
    /// </summary>
    [SitecoreType(TemplateId="9ca4a8c4-0295-49c1-a879-e008933d0a4f")]
    public partial class Page: GlassBase, IPage
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Page
        /// </summary>
        public const string TemplateId = "9ca4a8c4-0295-49c1-a879-e008933d0a4f";

    }


    /// <summary>
    /// IProduct Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Product</para>
    /// <para>ID: 1daeff25-b075-4c13-b41a-72b553b22542</para>
    /// </summary>
    [SitecoreType(TemplateId="1daeff25-b075-4c13-b41a-72b553b22542")]
    public partial interface IProduct: IPage, IGlassBase
    {

    }


    /// <summary>
    /// Product Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Pages/Product</para>
    /// <para>ID: 1daeff25-b075-4c13-b41a-72b553b22542</para>
    /// </summary>
    [SitecoreType(TemplateId="1daeff25-b075-4c13-b41a-72b553b22542")]
    public partial class Product: GlassBase, IProduct
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Pages/Product
        /// </summary>
        public const string TemplateId = "1daeff25-b075-4c13-b41a-72b553b22542";

    }


    /// <summary>
    /// ISettingsFolder Interface
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder</para>
    /// <para>ID: ff3f660a-3d15-4efc-9873-113aaf71b44e</para>
    /// </summary>
    [SitecoreType(TemplateId="ff3f660a-3d15-4efc-9873-113aaf71b44e")]
    public partial interface ISettingsFolder: IGlassBase
    {

    }


    /// <summary>
    /// SettingsFolder Class
    /// <para>Path: /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder</para>
    /// <para>ID: ff3f660a-3d15-4efc-9873-113aaf71b44e</para>
    /// </summary>
    [SitecoreType(TemplateId="ff3f660a-3d15-4efc-9873-113aaf71b44e")]
    public partial class SettingsFolder: GlassBase, ISettingsFolder
    {
        /// <summary>
        /// The TemplateId string for /sitecore/templates/HCA/Project/HCA/Folders/Settings Folder
        /// </summary>
        public const string TemplateId = "ff3f660a-3d15-4efc-9873-113aaf71b44e";

    }

}