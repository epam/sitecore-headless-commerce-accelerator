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

#region GlassMapperSc generated code

/*************************************

DO NOT CHANGE THIS FILE - UPDATE GlassMapperScCustom.cs

**************************************/

namespace HCA.Foundation.GlassMapper.App_Start
{
    using System.Linq;

    using Glass.Mapper;
    using Glass.Mapper.Configuration;
    using Glass.Mapper.Maps;
    using Glass.Mapper.Sc.Configuration.Fluent;
    using Glass.Mapper.Sc.IoC;

    public class GlassMapperSc : Glass.Mapper.Sc.Pipelines.Initialize.GlassMapperSc
    {
        public override IDependencyResolver CreateResolver()
        {
            var resolver = GlassMapperScCustom.CreateResolver();
            base.CreateResolver(resolver);
            return resolver;
        }

        public override IConfigurationLoader[] GetGlassLoaders(Context context)
        {
            var loaders1 = GlassMapperScCustom.GlassLoaders();
            var loaders2 = base.GetGlassLoaders(context);

            return loaders1.Concat(loaders2).ToArray();
        }

        public override void LoadConfigurationMaps(IDependencyResolver resolver, Context context)
        {
            var dependencyResolver = resolver as DependencyResolver;
            if (dependencyResolver == null)
            {
                return;
            }

            if (dependencyResolver.ConfigurationMapFactory is ConfigurationMapConfigFactory)
            {
                GlassMapperScCustom.AddMaps(dependencyResolver.ConfigurationMapFactory);
            }

            IConfigurationMap configurationMap = new ConfigurationMap(dependencyResolver);
            var configurationLoader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();
            context.Load(configurationLoader);

            base.LoadConfigurationMaps(resolver, context);
        }

        public override void PostLoad(IDependencyResolver dependencyResolver)
        {
            GlassMapperScCustom.PostLoad();
            base.PostLoad(dependencyResolver);
        }
    }
}

#endregion