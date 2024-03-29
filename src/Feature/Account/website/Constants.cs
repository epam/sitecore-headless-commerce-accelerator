﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Feature.Account
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public static class Redirects
        {
            public const string Login = "/login";

            public const string CurrentPage = "/";
        }

        public static class Account
        {
            public static class ErrorMessages
            {
                public const string TermsAndPolicyAgreementMessage =
                    "You have to agree to the Terms of Use and Customer Privacy Policy";
            }

            public static class PasswordValidation
            {
                public const int MinimumPasswordLength = 6;
            }
        }
    }
}