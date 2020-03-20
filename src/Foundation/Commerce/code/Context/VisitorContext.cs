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

namespace Wooli.Foundation.Commerce.Context
{
    using System.Collections;
    using System.Web;

    using DependencyInjection;

    using Models;
    using Models.Entities.Users;

    using Providers;

    [Service(typeof(IVisitorContext))]
    public class VisitorContext : IVisitorContext
    {
        private const string CurrentUserItemKey = "_CurrentCommerceUser";

        private const string ExperienceEditorVisitorTrackingId = "{74E29FDC-8523-4C4F-B422-23BBFF0A342A}";

        private const string StaticVisitorId = "{74E29FDC-8523-4C4F-B422-23BBFF0A342A}";

        private readonly ICustomerProvider customerProvider;

        public VisitorContext(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        private IDictionary Items => HttpContext.Current.Items;

        public string ContactId => this.CurrentUser?.ContactId;

        public User CurrentUser
        {
            get => this.Items[CurrentUserItemKey] as User;
            set => this.Items[CurrentUserItemKey] = value;
        }
    }
}