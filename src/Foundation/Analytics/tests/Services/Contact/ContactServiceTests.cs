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

namespace HCA.Foundation.Analytics.Tests.Services.Contact
{
    using System;

    using Analytics.Repositories.Contact;
    using Analytics.Services.Contact;

    using Base.Models.Logging;
    using Base.Services.Logging;
    using Base.Services.Tracking;

    using Models.Entities.Contact;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.XConnect;

    using Xunit;

    public class ContactServiceTests
    {
        private readonly Fixture fixture;

        private readonly ITrackingService trackingService;
        private readonly IContactRepository contactRepository;

        private readonly IContactService contactService;

        public ContactServiceTests()
        {
            this.fixture = new Fixture();

            ILogService<CommonLog> logService = Substitute.For<ILogService<CommonLog>>();

            this.trackingService = Substitute.For<ITrackingService>();
            this.contactRepository = Substitute.For<IContactRepository>();

            this.contactService = Substitute.For<ContactService>(this.trackingService, logService, this.contactRepository);
        }

        [Fact]
        public void SetEmail_ShouldCallGetCurrentContactReference()
        {
            // act
            this.contactService.SetEmail("Confirmation", "test@test.com");

            // assert
            this.trackingService.Received(1).GetCurrentContactReference();
        }

        [Fact]
        public void SetEmail_ShouldCallContactRepositorySetEmail()
        {
            // arrange
            var key = "Confirmation";
            var email = "test@test.com";

            // act
            this.contactService.SetEmail(key, email);

            // assert
            this.contactRepository.Received(1).SetEmail(key, email, Arg.Any<IdentifiedContactReference>());
        }

        [Fact]
        public void SetEmail_ContactRepositorySetEmailThrowsException_ResultsFalse()
        {
            // arrange
            var key = "Confirmation";
            var email = "test@test.com"; 
            this.contactRepository.When(x => x.SetEmail(key, email, Arg.Any<IdentifiedContactReference>())).Do(x => throw new Exception());

            // act & assert
            Assert.False(this.contactService.SetEmail(key, email));
        }

        [Fact]
        public void SetEmail_ContactRepositorySetEmailDoNotThrow_ResultsTrue()
        {
            // arrange
            var key = "Confirmation";
            var email = "test@test.com";
            this.contactRepository.SetEmail(key, email, Arg.Any<IdentifiedContactReference>());

            // act & assert
            Assert.True(this.contactService.SetEmail(key, email));
        }

        [Fact]
        public void SetPersonalInfo_ShouldCallGetCurrentContactReference()
        {
            // arrange
            var personalInfo = this.fixture.Create<PersonalInfo>();

            // act
            this.contactService.SetPersonalInfo(personalInfo);

            // assert
            this.trackingService.Received(1).GetCurrentContactReference();
        }

        [Fact]
        public void SetPersonalInfo_ShouldCallContactRepositorySetPersonalInfo()
        {
            // arrange
            var personalInfo = this.fixture.Create<PersonalInfo>();

            // act
            this.contactService.SetPersonalInfo(personalInfo);

            // assert
            this.contactRepository.Received(1).SetPersonalInfo(personalInfo, Arg.Any<IdentifiedContactReference>());
        }

        [Fact]
        public void SetPersonalInfo_ContactRepositorySetPersonalInfoDoNotThrow_ResultsTrue()
        {
            // arrange
            var personalInfo = this.fixture.Create<PersonalInfo>();
            this.contactRepository.SetPersonalInfo(personalInfo, Arg.Any<IdentifiedContactReference>());

            // act & assert
            Assert.True(this.contactService.SetPersonalInfo(personalInfo));
        }

        [Fact]
        public void SetPersonalInfo_ContactRepositorySetPersonalInfoThrowsException_ResultsFalse()
        {
            // arrange
            var personalInfo = this.fixture.Create<PersonalInfo>();
            this.contactRepository.When(x => x.SetPersonalInfo(personalInfo, Arg.Any<IdentifiedContactReference>())).Do(x => throw new Exception());

            // act & assert
            Assert.False(this.contactService.SetPersonalInfo(personalInfo));
        }
    }
}
