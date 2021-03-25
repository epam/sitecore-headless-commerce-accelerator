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

namespace HCA.Foundation.Account.Tests.Services.Authentication
{
    using Base.Services.Pipeline;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Security;
    using Sitecore.Security.Accounts;

    using Xunit;

    using Assert = Sitecore.Diagnostics.Assert;
    using UserManager = Managers.User.UserManager;

    public class UserManagerTests
    {
        private readonly UserManager userManager;

        private readonly IFixture fixture;

        private readonly string key;
        private readonly string value;
        private readonly string username;

        public UserManagerTests()
        {
            this.fixture = new Fixture();

            this.userManager = new UserManager();

            this.key = this.fixture.Create<string>();
            this.value = this.fixture.Create<string>();
            this.username = this.fixture.Create<string>();
        }

        [Fact]
        public void AddCustomProperty_IfParametersAreNotNull_ShouldAddCustomPropertyToUserProfileAndSave()
        {
            // arrange
            var user = Substitute.For<User>(this.username, true);
            user.Profile.Returns(Substitute.For<UserProfile>());

            // act
            this.userManager.AddCustomProperty(user, this.key, this.value);

            // assert
            user.Profile.Received(1).SetCustomProperty(this.key, this.value);
            user.Profile.Received(1).Save();
        }
    }
}