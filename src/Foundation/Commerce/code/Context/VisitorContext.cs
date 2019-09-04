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

namespace Wooli.Foundation.Commerce.Context
{
    using System.Collections;
    using System.Web;

    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Commerce.Providers;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(IVisitorContext))]
    public class VisitorContext : IVisitorContext
    {
        private const string StaticVisitorId = "{74E29FDC-8523-4C4F-B422-23BBFF0A342A}";
        private const string ExperienceEditorVisitorTrackingId = "{74E29FDC-8523-4C4F-B422-23BBFF0A342A}";

        private const string CurrentUserItemKey = "_CurrentCommerceUser";

        private readonly ICustomerProvider customerProvider;

        public VisitorContext(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        public string ContactId => this.CurrentUser?.ContactId;

        public CommerceUserModel CurrentUser
        {
            get => this.Items[CurrentUserItemKey] as CommerceUserModel;
            set => this.Items[CurrentUserItemKey] = value;
        }

        private IDictionary Items => HttpContext.Current.Items;
    }
}
