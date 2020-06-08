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

namespace HCA.Foundation.GlassMapper.Infrastructure
{
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;

    using Microsoft.Extensions.DependencyInjection;

    using Sitecore;
    using Sitecore.DependencyInjection;

    public class GlassMapperConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISitecoreService>(provider => new SitecoreService(Context.Database));
            serviceCollection.AddTransient<IRequestContext>(
                provider =>
                {
                    var sitecoreService = provider.GetService<ISitecoreService>();
                    return new RequestContext(sitecoreService);
                });
            serviceCollection.AddTransient<IMvcContext>(
                provider =>
                {
                    var sitecoreService = provider.GetService<ISitecoreService>();
                    return new MvcContext(sitecoreService);
                });

            serviceCollection.AddTransient<IGlassHtml, GlassHtml>();
        }
    }
}