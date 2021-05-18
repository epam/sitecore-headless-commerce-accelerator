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

namespace HCA.Foundation.Base.Services.Tracking
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DependencyInjection;

    using Microsoft.Extensions.DependencyInjection;

    using Sitecore.Analytics;
    using Sitecore.XConnect;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ITrackingService), Lifetime = Lifetime.Transient)]
    public class TrackingService : ITrackingService
    {
        private const string IdentificationSource = "ContactRepository";

        public IdentifiedContactReference GetCurrentContactReference()
        {
            if (!Tracker.Current.Contact.Identifiers.Any())
            {
                var identifier = Guid.NewGuid().ToString();
                
                //IdentifyAs() Method is obsolete
                //Tracker.Current.Session.IdentifyAs(IdentificationSource, identifier);

                var identificationManager = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider
                    .GetRequiredService<Sitecore.Analytics.Tracking.Identification.IContactIdentificationManager>();
                var result = identificationManager.IdentifyAs(new Sitecore.Analytics.Tracking.Identification.KnownContactIdentifier(IdentificationSource, identifier));
                
                return new IdentifiedContactReference(IdentificationSource, identifier);
            }
            else
            {
                var identifier = Tracker.Current.Contact.Identifiers.First();
                return new IdentifiedContactReference(identifier.Source, identifier.Identifier);
            }
        }

        public void EnsureTracker()
        {
            if (!Tracker.IsActive)
            {
                Tracker.StartTracking();
            }
        }
    }
}