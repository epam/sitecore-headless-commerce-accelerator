//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Feature.Catalog.Pipelines.GetLayoutServiceContext
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc;

    using Sitecore.Data;
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    using Wooli.Feature.Catalog.Models;
    using Wooli.Foundation.ReactJss.Infrastructure;

    public class ProductColorsContextExtension : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly ISitecoreContext sitecoreContext;

        public ProductColorsContextExtension(
            ISitecoreContext sitecoreContext,
            IConfigurationResolver configurationResolver) : base(configurationResolver)
        {
            this.sitecoreContext = sitecoreContext;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            string productColorMappingQuery = $"{application.SitecorePath.TrimEnd('/')}/Settings/*[@@templateid='{ID.Parse(ProductColorMappingFolder.TemplateId)}']";
            var productColorMapping = this.sitecoreContext.QuerySingle<IProductColorMappingFolder>(productColorMappingQuery);

            var dictionary = new Dictionary<string, string>();
            if (productColorMapping?.Mappings != null)
            {
                foreach (IProductColorMapping colorMapping in productColorMapping.Mappings)
                {
                    dictionary.Add(colorMapping.ColorName, colorMapping.ColorHEX);
                }
            }


            args.ContextData.Add("productColors", dictionary);
        }
    }
}
