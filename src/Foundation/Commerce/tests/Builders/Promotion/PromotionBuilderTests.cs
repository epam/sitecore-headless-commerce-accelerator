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

namespace HCA.Foundation.Commerce.Tests.Builders.Promotion
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Base.Tests.Customization;

    using Commerce.Builders.Promotion;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Exceptions;

    using Xunit;

    public class PromotionBuilderTests
    {
        private readonly PromotionBuilder builder;

        private readonly IFixture fixture;

        public PromotionBuilderTests()
        {
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.builder = new PromotionBuilder();
        }

        [Fact]
        public void BuildBenefits_IfSourceIsInvalid_ShouldThrowInvalidValueException()
        {
            // act & assert
            Assert.Throws<InvalidValueException>(() => this.builder.BuildBenefits(this.fixture.Create<EntityView>()));
        }

        [Fact]
        public void BuildBenefits_IfSourceIsNull_ShouldReturnNull()
        {
            // act
            var result = this.builder.BuildBenefits(null);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void BuildBenefits_IfSourceIsValid_ShouldReturnValidBenefits()
        {
            // arrange
            var action = this.fixture.Create<string>();
            var amountOff = this.fixture.Create<string>();

            var source = this.CreateBenefitEntity(action, amountOff);

            // act
            var result = this.builder.BuildBenefits(source);

            // assert
            var actual = result.First();
            Assert.Equal(action, actual.Action);
            Assert.Equal(amountOff, actual.AmountOff);
        }

        [Fact]
        public void BuildPromotion_IfSourceIsInvalid_ShouldThrowInvalidValueException()
        {
            // act & assert
            Assert.Throws<InvalidValueException>(() => this.builder.BuildPromotion(this.fixture.Create<EntityView>()));
        }

        [Fact]
        public void BuildPromotion_IfSourceIsNull_ShouldReturnNull()
        {
            // act
            var result = this.builder.BuildPromotion(null);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void BuildPromotion_IfSourceIsValid_ShouldReturnValidBenefits()
        {
            // arrange
            var id = this.fixture.Create<string>();
            var name = this.fixture.Create<string>();
            var displayName = this.fixture.Create<string>();

            var description = this.fixture.Create<string>();

            var action = this.fixture.Create<string>();
            var amountOff = this.fixture.Create<string>();

            var conditionOperator = this.fixture.Create<string>();
            var condition = this.fixture.Create<string>();
            var @operator = this.fixture.Create<string>();
            var subtotal = this.fixture.Create<int>().ToString();

            var details = this.CreateDetailsEntity(description);
            var qualification = this.CreateQualificationEntity(conditionOperator, condition, @operator, subtotal);
            var benefit = this.CreateBenefitEntity(action, amountOff);

            var source = this.CreatePromotionEntity(id, name, displayName, details, qualification, benefit);

            // act
            var result = this.builder.BuildPromotion(source);

            // assert
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(displayName, result.DisplayName);
            Assert.Equal(description, result.Description);

            var actualQualification = result.Qualifications.First();
            Assert.Equal(conditionOperator, actualQualification.ConditionOperator);
            Assert.Equal(condition, actualQualification.Condition);
            Assert.Equal(@operator, actualQualification.Operator);
            Assert.Equal(subtotal, actualQualification.Subtotal);
            
            var actualBenefit = result.Benefits.First();
            Assert.Equal(action, actualBenefit.Action);
            Assert.Equal(amountOff, actualBenefit.AmountOff);
        }

        [Fact]
        public void BuildQualifications_IfSourceIsInvalid_ShouldThrowInvalidValueException()
        {
            // act & assert
            Assert.Throws<InvalidValueException>(
                () => this.builder.BuildQualifications(this.fixture.Create<EntityView>()));
        }

        [Fact]
        public void BuildQualifications_IfSourceIsNull_ShouldReturnNull()
        {
            // act
            var result = this.builder.BuildQualifications(null);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void BuildQualifications_IfSourceIsValid_ShouldReturnValidBenefits()
        {
            // arrange
            var conditionOperator = this.fixture.Create<string>();
            var condition = this.fixture.Create<string>();
            var @operator = this.fixture.Create<string>();
            var subtotal = this.fixture.Create<int>().ToString();

            var source = this.CreateQualificationEntity(conditionOperator, condition, @operator, subtotal);

            // act
            var result = this.builder.BuildQualifications(source);

            // assert
            var actual = result.First();
            Assert.Equal(conditionOperator, actual.ConditionOperator);
            Assert.Equal(condition, actual.Condition);
            Assert.Equal(@operator, actual.Operator);
            Assert.Equal(subtotal, actual.Subtotal);
        }

        private EntityView CreateBenefitEntity(string action, string amountOff)
        {
            return this.fixture
                .Build<EntityView>()
                .With(prop => prop.Name, "Benefits")
                .With(
                    s => s.ChildViews,
                    new ObservableCollection<Model>(
                        this.fixture
                            .Build<EntityView>()
                            .With(s => s.Name, "BenefitDetails")
                            .With(
                                s => s.Properties,
                                new ObservableCollection<ViewProperty>(
                                    new List<ViewProperty>
                                    {
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "Action")
                                            .With(prop => prop.Value, action)
                                            .Create(),
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "AmountOff")
                                            .With(prop => prop.Value, amountOff)
                                            .Create()
                                    }))
                            .CreateMany(1)))
                .Create();
        }

        private EntityView CreateDetailsEntity(string description)
        {
            return this.fixture
                .Build<EntityView>()
                .With(prop => prop.Name, "Details")
                .With(
                    s => s.Properties,
                    new ObservableCollection<ViewProperty>(
                        new List<ViewProperty>
                        {
                            this.fixture
                                .Build<ViewProperty>()
                                .With(prop => prop.Name, "Description")
                                .With(prop => prop.Value, description)
                                .Create()
                        }))
                .Create();
        }

        private EntityView CreatePromotionEntity(
            string id,
            string name,
            string displayName,
            EntityView details,
            EntityView qualification,
            EntityView benefit)
        {
            return this.fixture
                .Build<EntityView>()
                .With(prop => prop.Name, "Master")
                .With(
                    s => s.ChildViews,
                    new ObservableCollection<Model>(
                        new List<EntityView>
                        {
                            details,
                            qualification,
                            benefit
                        }))
                .With(
                    s => s.Properties,
                    new ObservableCollection<ViewProperty>(
                        new List<ViewProperty>
                        {
                            this.fixture
                                .Build<ViewProperty>()
                                .With(prop => prop.Name, "ItemId")
                                .With(prop => prop.Value, id)
                                .Create(),
                            this.fixture
                                .Build<ViewProperty>()
                                .With(prop => prop.Name, "Name")
                                .With(prop => prop.Value, name)
                                .Create(),
                            this.fixture
                                .Build<ViewProperty>()
                                .With(prop => prop.Name, "DisplayName")
                                .With(prop => prop.Value, displayName)
                                .Create()
                        }))
                .Create();
        }

        private EntityView CreateQualificationEntity(
            string conditionOperator,
            string condition,
            string @operator,
            string subtotal)
        {
            return this.fixture
                .Build<EntityView>()
                .With(prop => prop.Name, "Qualifications")
                .With(
                    s => s.ChildViews,
                    new ObservableCollection<Model>(
                        this.fixture
                            .Build<EntityView>()
                            .With(s => s.Name, "QualificationDetails")
                            .With(
                                s => s.Properties,
                                new ObservableCollection<ViewProperty>(
                                    new List<ViewProperty>
                                    {
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "ConditionOperator")
                                            .With(prop => prop.Value, conditionOperator)
                                            .Create(),
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "Condition")
                                            .With(prop => prop.Value, condition)
                                            .Create(),
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "Operator")
                                            .With(prop => prop.Value, @operator)
                                            .Create(),
                                        this.fixture
                                            .Build<ViewProperty>()
                                            .With(prop => prop.Name, "Subtotal")
                                            .With(prop => prop.Value, subtotal)
                                            .Create()
                                    }))
                            .CreateMany(1)))
                .Create();
        }
    }
}