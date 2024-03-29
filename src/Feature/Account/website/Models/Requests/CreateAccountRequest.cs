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

namespace HCA.Feature.Account.Models.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    using TypeLite;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class CreateAccountRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name is invalid")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name is invalid")]
        public string LastName { get; set; }

        [Required]
        [MinLength(Constants.Account.PasswordValidation.MinimumPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// User have to accept our terms of use and privacy policy agreement
        /// </summary>
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = Constants.Account.ErrorMessages.TermsAndPolicyAgreementMessage)]
        public bool TermsAndPolicyAgreement { get; set; }
    }
}