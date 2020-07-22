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

namespace HCA.Foundation.Commerce.Builders.Promotion
{
    using System.Collections.Generic;
    using System.Linq;

    using DependencyInjection;

    using Models.Entities.Promotion;

    using Sitecore.Commerce.EntityViews;
    using Sitecore.Diagnostics;

    [Service(typeof(IPromotionBuilder), Lifetime = Lifetime.Singleton)]
    public class PromotionBuilder : IPromotionBuilder
    {
        public IEnumerable<Qualification> BuildQualifications(EntityView source)
        {
            if (source == null)
            {
                return null;
            }

            Assert.AreEqual(source.Name, "Qualifications", "It should be entit Qualifications view");

            return source.ChildViews
                .Cast<EntityView>()
                .Select(
                    child => new Qualification
                    {
                        ConditionOperator = this.GetProperty(child, "ConditionOperator"),
                        Condition = this.GetProperty(child, "Condition"),
                        Operator = this.GetProperty(child, "Operator"),
                        Subtotal = this.GetProperty(child, "Subtotal")
                    })
                .ToList();
        }

        public IEnumerable<Benefit> BuildBenefits(EntityView source)
        {
            if (source == null)
            {
                return null;
            }

            Assert.AreEqual(source.Name, "Benefits", "It should be entit Benefits view");

            return source.ChildViews
                .Cast<EntityView>()
                .Select(
                    child => new Benefit
                    {
                        Action = this.GetProperty(child, "Action"),
                        AmountOff = this.GetProperty(child, "AmountOff")
                    })
                .ToList();
        }

        public Promotion BuildPromotion(EntityView source)
        {
            if (source == null)
            {
                return null;
            }

            Assert.AreEqual(source.Name, "Master", "It should be entit Master view");

            var children = source.ChildViews.Cast<EntityView>();

            var details = children.First(cv => cv.Name == "Details");
            var qualifications = children.First(cv => cv.Name == "Qualifications");
            var benefits = children.First(cv => cv.Name == "Benefits");

            return new Promotion
            {
                Id = this.GetProperty(source, "ItemId"),
                Name = this.GetProperty(source, "Name"),
                DisplayName = this.GetProperty(source, "DisplayName"),
                Description = this.GetProperty(details, "Description"),
                Qualifications = this.BuildQualifications(qualifications),
                Benefits = this.BuildBenefits(benefits)
            };
        }

        private string GetProperty(EntityView entity, string name)
        {
            var property = entity.Properties.FirstOrDefault(prop => prop.Name == name);
            return property != null ? property.Value : string.Empty;
        }
    }
}