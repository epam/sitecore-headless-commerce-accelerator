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

namespace Wooli.Feature.Checkout.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc;

    using Models.ViewModels;

    using Sitecore.Diagnostics;
    using Sitecore.Modules.EmailCampaign.Core;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Web.UI.WebControls;

    using Log = Sitecore.Diagnostics.Log;

    public class ExmOrdersController : Controller
    {
        private const string OrderTrackingNumberToken = "$orderTrackingNumber$";

        private readonly ISitecoreService sitecoreService;

        public ExmOrdersController(ISitecoreService sitecoreService)
        {
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));

            this.sitecoreService = sitecoreService;
        }

        public ActionResult OrderConfirmation()
        {
            var dataSource = RenderingContext.Current.Rendering.Item;

            OrderConfirmationViewModel model = null;
            var isRender = ExmContext.IsRenderRequest;

            if (ExmContext.QueryString != null && dataSource != null)
            {
                var orderId = ExmContext.QueryString["order_id"];

                model = new OrderConfirmationViewModel
                {
                    MessageTitle = FieldRenderer.Render(dataSource, Models.OrderConfirmation.MessageTitleFieldName),
                    MessageBody = FieldRenderer.Render(dataSource, Models.OrderConfirmation.MessageBodyFieldName)
                        .Replace(OrderTrackingNumberToken, orderId)
                };

                
                // This is just to see what values are available, can be removed
                foreach (var key in ExmContext.QueryString.AllKeys)
                {
                    Log.Info($"[EXM] key:{key}, value: {ExmContext.QueryString[key]}", this);
                }
            }

            return this.View(model);
        }
    }
}