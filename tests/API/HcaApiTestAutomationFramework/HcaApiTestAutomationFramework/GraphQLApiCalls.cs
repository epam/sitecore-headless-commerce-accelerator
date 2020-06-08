using System.Configuration;
using HcaApiTestAutomationFramework.GraphQlDTO;

namespace HcaApiTestAutomationFramework
{
	public class GraphQLApiCalls
	{
		public string GraphQLSandboxUrl = ConfigurationManager.AppSettings["GraphQLSandboxUrl"];
		public dynamic ClientConfiguration()
		{
			var client = new GraphQlClient(GraphQLSandboxUrl);
			var operationName = "ClientConfiguration";
			object clientSdkMetadata = new
			{
				integration = "custom",
				sessionId =  "866d30d0-7342-4373-91e9-c3c57a823d67",
				source = "client"
			};
			var query =
				"query ClientConfiguration {   clientConfiguration {     analyticsUrl     environment     merchantId     assetsUrl     clientApiUrl     creditCard {       supportedCardBrands       challenges       threeDSecureEnabled     }     applePayWeb {       countryCode       currencyCode       merchantIdentifier       supportedCardBrands     }     googlePay {       displayName       supportedCardBrands       environment       googleAuthorization     }     ideal {       routeId       assetsUrl     }     kount {       merchantId     }     masterpass {       merchantCheckoutId       supportedCardBrands     }     paypal {       displayName       clientId       privacyUrl       userAgreementUrl       assetsUrl       environment       environmentNoNetwork       unvettedMerchant       braintreeClientId       billingAgreementsEnabled       merchantAccountId       currencyCode       payeeEmail     }     unionPay {       merchantAccountId     }     usBankAccount {       routeId       plaidPublicKey     }     venmo {       merchantId       accessToken       environment     }     visaCheckout {       apiKey       externalClientId       supportedCardBrands     }     braintreeApi {       accessToken       url     }     supportedFeatures   } }";
			return client.Execute(clientSdkMetadata, operationName, query);
		}

		public dynamic TokenizeCreditCard(Input.Creditcard creditcard)
		{
			var client = new GraphQlClient(GraphQLSandboxUrl);
			var operationName = "TokenizeCreditCard";
			object clientSdkMetadata = new
			{
				integration = "custom",
				sessionId = "866d30d0-7342-4373-91e9-c3c57a823d67",
				source = "client"
			};
			object variableRequest = new
			{
				input  = new
				{
					creditCard = creditcard,
					options = new
					{
						validate = true,
					}
				}
			};
			var mutation =
				"mutation TokenizeCreditCard($input: TokenizeCreditCardInput!) {   tokenizeCreditCard(input: $input) {     token     creditCard {       bin       brandCode       last4       binData {         prepaid         healthcare         debit         durbinRegulated         commercial         payroll         issuingBank         countryOfIssuance         productId       }     }   } }";
			return client.Execute(clientSdkMetadata, operationName, mutation, variableRequest);
		}
	}
}
