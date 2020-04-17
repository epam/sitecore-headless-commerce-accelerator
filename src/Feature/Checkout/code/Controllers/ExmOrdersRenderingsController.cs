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

namespace HCA.Feature.Checkout.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;

    using Sitecore.Diagnostics;

    using Utils;

    public class ExmOrdersRenderingsController : Controller
    {
        private readonly IMvcContext mvcContext;

        private readonly IOrderEmailUtils orderEmailUtils;

        public ExmOrdersRenderingsController(IMvcContext mvcContext, IOrderEmailUtils orderEmailUtils)
        {
            Assert.ArgumentNotNull(mvcContext, nameof(mvcContext));
            Assert.ArgumentNotNull(orderEmailUtils, nameof(orderEmailUtils));

            this.mvcContext = mvcContext;
            this.orderEmailUtils = orderEmailUtils;
        }

        public ActionResult OrderConfirmation()
        {
            var dataSource = this.mvcContext.GetDataSourceItem<Models.OrderConfirmation>();

            if (dataSource?.MessageBody != null)
            {
                dataSource.MessageBody = this.orderEmailUtils.ResolveTokens(dataSource.MessageBody);
            }

            return this.View(dataSource);
        }
    }
}