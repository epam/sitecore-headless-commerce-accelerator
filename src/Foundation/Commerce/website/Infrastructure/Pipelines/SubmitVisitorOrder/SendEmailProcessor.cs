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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.SubmitVisitorOrder
{
    using System;
    using System.Net.Mail;

    using Base.Extensions;

    using Sitecore;
    using Sitecore.Commerce.Engine.Connect.Pipelines;
    using Sitecore.Commerce.Entities.Orders;
    using Sitecore.Commerce.Pipelines;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Links.UrlBuilders;

    [Obsolete("Use Automation Plan")]
    public class SendEmailProcessor : PipelineProcessor
    {
        private static readonly string SendConfirmationFrom =
            Settings.GetSetting("HCA.Foundation.Commerce.SendConfirmation.From");

        private static readonly string SendConfirmationSubject =
            Settings.GetSetting("HCA.Foundation.Commerce.SendConfirmation.Subject");

        public override void Process(ServicePipelineArgs args)
        {
            var order = this.GetOrderFromArgs(args);

            if (order == null || order.Total == null)
            {
                return;
            }

            try
            {
                var body = this.BuildBody(order);
                var emailMessage = new MailMessage(SendConfirmationFrom, order.Email, SendConfirmationSubject, body)
                {
                    IsBodyHtml = true
                };

                MainUtil.SendMail(emailMessage);
            }
            catch (Exception ex)
            {
                Log.Error("Send order confirmation email failed!", ex, this);
            }
        }

        private string BuildBody(Order order)
        {
            var options = new ItemUrlBuilderOptions
            {
                AlwaysIncludeServerUrl = true
            };

            var homeItemUrl = Context.Database.GetItem(Context.Site.StartPath).Url(options);
            var link = $"<a href=\"{homeItemUrl}Checkout/Confirmation?trackingNumber={order.TrackingNumber}\">here</a>";
            var result = $"<h2>Thank You For Your Order!</h2><p>Click {link} for more details.</p>";

            return result;
        }

        private Order GetOrderFromArgs(ServicePipelineArgs args)
        {
            if (args == null || !(args.Result is SubmitVisitorOrderResult))
            {
                return null;
            }

            return ((SubmitVisitorOrderResult)args.Result).Order;
        }
    }
}