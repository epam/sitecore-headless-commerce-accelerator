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

namespace Wooli.Foundation.ReactJss.Tests.Serialization.RenderJsonRendering
{
    using System.Collections.Generic;

    using NSubstitute;

    using ReactJss.Serialization.RenderJsonRendering;

    using Sitecore.FakeDb;
    using Sitecore.LayoutService.Configuration;
    using Sitecore.LayoutService.ItemRendering;
    using Sitecore.LayoutService.Presentation.Pipelines.RenderJsonRendering;
    using Sitecore.Mvc.Presentation;

    using Xunit;

    public class FormatRenderingParamsTests
    {
        // [Fact]
        public void FormatRenderingParameters_Parameters_ProcessedLowerCaseParameterKeys()
        {
            var configuration = Substitute.For<IConfiguration>();
            var renderingConfiguration = Substitute.For<IRenderingConfiguration>();

            var renderingItem = new DbItem("renderingItem");
            using (var db = new Db
            {
                renderingItem
            })
            {
                var args = new RenderJsonRenderingArgs
                {
                    RenderingConfiguration = renderingConfiguration,
                    Rendering = new Rendering
                    {
                        RenderingItem = db.GetItem(renderingItem.ID)
                    },
                    Result = new RenderedJsonRendering
                    {
                        RenderingParams = new Dictionary<string, string>
                        {
                            { "Param1", "Value1" },
                            { "Param 2", "Value 2" },
                            { "notChanged", "1" }
                        }
                    }
                };

                var likesContextExtension = new FormatRenderingParams(configuration);
                likesContextExtension.Process(args);

                Assert.NotNull(args.Result);

                var renderingParams = args.Result.RenderingParams;
                Assert.NotNull(renderingParams);

                Assert.Equal(3, renderingParams.Count);
                Assert.Contains(renderingParams, x => (x.Key == "param1") && (x.Value == "Value1"));
                Assert.Contains(renderingParams, x => (x.Key == "param2") && (x.Value == "Value 2"));
                Assert.Contains(renderingParams, x => (x.Key == "notChanged") && (x.Value == "1"));
            }
        }
    }
}