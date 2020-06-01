using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HcaApiTestAutomationFramework;
using HcaApiTestAutomationFramework.HcaDTO;
using NUnit.Framework;

namespace HcaApiTests
{
	[TestFixture, Description("Account scenario")]
	public class AccountScenarioTests
	{

		[Test, Description("create new account")]
		public void VerifyCreateAccountTest()
		{
			//Test Data for Creation Account
			var accountData = new AccountCreateAccountRequestDTO
			{
				email = "test4@email.com",
				firstName = "testFirstName1",
				lastName = "testLastName1",
				password = "testPassword1",
			};

			//creating Account
			var account = new HcaApiMethods<AccountCreateAccountResponseDTO>();
			var accountReq = account.CreateAccount(accountData);
			Assert.True("ok".Equals(accountReq.status.ToLower()), "The Create Account request is not passed");
			Assert.Multiple(() =>
			{
				Assert.AreEqual("test4@email.com", accountReq.data.email, "Email is not equal");
				Assert.AreEqual("testFirstName", accountReq.data.firstName, "First name is not equal");
				Assert.AreEqual("testLastName", accountReq.data.lastName, "Last Name is not equal");
			});
			
		}

		[Test, Description("check impossible to create new account with the same email")]
		public void VerifyImpossibleCreationAccountWithTheSameEmailTest()
		{
			//Test Data for Creation Account
			var accountData = new AccountCreateAccountRequestDTO
			{
				email = "test2@email.com",
				firstName = "testFirstName1",
				lastName = "testLastName1",
				password = "testPassword1",
			};

			//creating Account
			var account = new HcaApiMethods<AccountCreateAccountResponseDTO>();
			var accountReq = account.CreateAccount(accountData);
			Assert.True("ok".Equals(accountReq.status.ToLower()), "The Create Account request is not passed");

			var createAccountEmail = accountReq.data.email;

			//Test Data for Creation another Account with the same email
			accountData = new AccountCreateAccountRequestDTO
			{
				email = createAccountEmail,
				firstName = "testFirstName2",
				lastName = "testLastName2",
				password = "testPassword2",
			};

			//creating Account
			account = new HcaApiMethods<AccountCreateAccountResponseDTO>();
			accountReq = account.CreateAccount(accountData);
			Assert.True("error".Equals(accountReq.status.ToLower()), "The Account request with the same  email should not be created");
		}
	}

}
