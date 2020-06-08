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

#region GlassMapperScCustom generated code

namespace HCA.Foundation.GlassMapper.App_Start
{
    using Glass.Mapper.Configuration;
    using Glass.Mapper.IoC;
    using Glass.Mapper.Maps;
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.IoC;

    using IDependencyResolver = Glass.Mapper.Sc.IoC.IDependencyResolver;

    public static class GlassMapperScCustom
    {
        public static IDependencyResolver CreateResolver()
        {
            var config = new Config();

            var dependencyResolver = new DependencyResolver(config);

            // add any changes to the standard resolver here
            return dependencyResolver;
        }

        public static IConfigurationLoader[] GlassLoaders()
        {
            /* USE THIS AREA TO ADD FLUENT CONFIGURATION LOADERS
             * 
             * If you are using Attribute Configuration or auto-mapping/on-demand mapping you don't need to do anything!
             * 
             */

            return new IConfigurationLoader[] { };
        }

        public static void PostLoad()
        {
            //Remove the comments to activate CodeFist
            /* CODE FIRST START
            var dbs = Sitecore.Configuration.Factory.GetDatabases();
            foreach (var db in dbs)
            {
                var provider = db.GetDataProviders().FirstOrDefault(x => x is GlassDataProvider) as GlassDataProvider;
                if (provider != null)
                {
                    using (new SecurityDisabler())
                    {
                        provider.Initialise(db);
                    }
                }
            }
             * CODE FIRST END
             */
        }

        public static void AddMaps(IConfigFactory<IGlassMap> mapsConfigFactory)
        {
            // Add maps here
            // mapsConfigFactory.Add(() => new SeoMap());
        }
    }
}

#endregion