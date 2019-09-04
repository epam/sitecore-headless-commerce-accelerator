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

#region GlassMapperSc generated code
/*************************************

DO NOT CHANGE THIS FILE - UPDATE GlassMapperScCustom.cs

**************************************/

using Glass.Mapper.Maps;
using Glass.Mapper.Sc.Configuration.Fluent;
using Glass.Mapper.Sc.IoC;
using Glass.Mapper.Sc.Pipelines.GetChromeData;
using Sitecore.Pipelines;

// WebActivator has been removed. If you wish to continue using WebActivator uncomment the line below
// and delete the Glass.Mapper.Sc.CastleWindsor.config file from the Sitecore Config Include folder.
// [assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Wooli.Foundation.GlassMapper.App_Start.GlassMapperSc), "Start")]

namespace Wooli.Foundation.GlassMapper.App_Start
{
	public class  GlassMapperSc
	{
		public void Process(PipelineArgs args){
			GlassMapperSc.Start();
		}

		public static void Start()
		{
			//install the custom services
			var resolver = GlassMapperScCustom.CreateResolver();

			//create a context
			var context = Glass.Mapper.Context.Create(resolver);

			LoadConfigurationMaps(resolver, context);

			context.Load(      
				GlassMapperScCustom.GlassLoaders()        				
				);

			GlassMapperScCustom.PostLoad(resolver);

			
			//EditFrameBuilder.EditFrameItemPrefix = "Glass-";

        }

        public static void LoadConfigurationMaps(IDependencyResolver resolver, Glass.Mapper.Context context)
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
            SitecoreFluentConfigurationLoader configurationLoader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();
            context.Load(configurationLoader);
        }
	}
}
#endregion
