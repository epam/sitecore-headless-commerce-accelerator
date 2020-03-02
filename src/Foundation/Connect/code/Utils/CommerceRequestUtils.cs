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

namespace Wooli.Foundation.Connect.Utils
{
    using System;
    using System.Linq;

    using Sitecore;
    using Sitecore.Data.Items;

    public class CommerceRequestUtils
    {
        public static string GetPaymentOptionId(string paymentType)
        {
            Item paymentOptions = Context.Database.GetItem(
                "/sitecore/Commerce/Commerce Control Panel/Shared Settings/Payment Options");
            if ((paymentOptions != null) && paymentOptions.Children.Any())
            {
                Item paymentOption = paymentOptions.Children.FirstOrDefault(
                    o => o.Name.Equals(paymentType, StringComparison.OrdinalIgnoreCase));
                if (paymentOption != null) return paymentOption.ID.ToGuid().ToString("D");
            }

            return string.Empty;
        }
    }
}