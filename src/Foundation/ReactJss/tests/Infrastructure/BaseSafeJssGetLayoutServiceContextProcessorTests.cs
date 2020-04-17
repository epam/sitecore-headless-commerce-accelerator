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

namespace Wooli.Foundation.ReactJss.Tests.Infrastructure
{
    using System;

    using NSubstitute;

    using ReactJss.Infrastructure;

    using Sitecore.FakeDb;
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    using Xunit;

    public class BaseSafeJssGetLayoutServiceContextProcessorTests
    {
        [Fact]
        public void ResolveContents_ErrorableContentsResolver_ExceptionNotThrowed()
        {
            using (var db = new Db
            {
                new DbItem("RenderedItem")
            })
            {
                var renderedItem = db.GetItem("/sitecore/content/RenderedItem");
                var configurationResolver = Substitute.For<IConfigurationResolver>();

                var args = new GetLayoutServiceContextArgs
                {
                    RenderedItem = renderedItem,
                    RenderingConfiguration = new DefaultRenderingConfiguration(),
                    ContextData =
                    {
                        {
                            "_sign", 1
                        }
                    }
                };

                IGetLayoutServiceContextProcessor contextProcessor =
                    new ErrorableGetLayoutServiceContextProcessor(configurationResolver);

                contextProcessor.Process(args);

                var resultObject = args.ContextData["_sign"];
                var value = Assert.IsType<int>(resultObject);
                Assert.Equal(1, value);
            }
        }

        public class ErrorableGetLayoutServiceContextProcessor : BaseSafeJssGetLayoutServiceContextProcessor
        {
            public ErrorableGetLayoutServiceContextProcessor(IConfigurationResolver configurationResolver)
                : base(configurationResolver)
            {
            }

            protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
            {
                throw new Exception("The test shouldnot show this exception!");
            }
        }
    }
}