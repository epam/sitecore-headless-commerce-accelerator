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

namespace HCA.Foundation.Commerce
{
    public static class Constants
    {
        public enum ItemType
        {
            Unknown,

            Category,

            Product,

            Variant
        }

        public const string CommerceRoutePrefix = "apix/client/commerce";

        public const string CommerceCustomerIdPrefix = "Entity-Customer-";

        public static class Login
        {
            public const string CommerceUserSource = "CommerceUser";
        }

        public static class StorefrontSettings
        {
            public const string FederatedPaymentOptionTitle = "Federated";
        }

        public static class Promotion
        {
            public const string FreeShippingPromotion = "FreeShippingPromotion";
        }

        public static class Geolocation
        {
            public const double EarthRadiusInKilometers = 6371.0;
        }

        public static class PasswordRecovery
        {
            public const string PasswordRecoveryLink = "%passwordRecoveryLink%";

            public const string ResetPasswordUrl = "account/new-password";

            public const string ConfirmTokenKey = "PasswordToken";

            public const string TokenCreationDatePropertyKey = "Token Creation Date";

            public const string UsersDomain = "CommerceUsers";
        }

        public static class Pipelines
        {
            public const string Login = "hca.foundation.account.login";

            public const string Logout = "hca.foundation.account.logout";

            public const string ConfirmPasswordRecovery = "hca.foundation.account.confirmPasswordRecovery";
        }

        public static class ErrorMessages
        {
            public const string EmailInUse = "Email is in use.";

            public const string UserNotFoundEmail = "User with specified email was not found";

            public const string UserNotFoundName = "User was not found.";

            public const string IncorrectOldPassword = "Incorrect old password.";

            public const string UnableToChangePassword = "Unable to change password.";

            public const string TokenIsInvalid = "Token is invalid.";

            public const string PasswordRecoveryLinkTokenMissing = "Missing password recovery link token in password recovery email";
        }
    }
}