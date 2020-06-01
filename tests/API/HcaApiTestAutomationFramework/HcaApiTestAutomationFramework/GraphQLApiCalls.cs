using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using HcaApiTestAutomationFramework.GraphQlDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HcaApiTestAutomationFramework
{
	public class GraphQLApiCalls
	{
		public string GraphQLSendboxUrl = ConfigurationManager.AppSettings["GraphQLSendboxUrl"];
		public dynamic ClientConfiguration()
		{
			var client = new GraphQlClient(GraphQLSendboxUrl);
			var operationName = "ClientConfiguration";
			object clientSdkMetadata = new
			{
				integration = "custom",
				sessionId =  "866d30d0-7342-4373-91e9-c3c57a823d67",
				source = "client"
			};
			var query =
				"query ClientConfiguration {   clientConfiguration {     analyticsUrl     environment     merchantId     assetsUrl     clientApiUrl     creditCard {       supportedCardBrands       challenges       threeDSecureEnabled     }     applePayWeb {       countryCode       currencyCode       merchantIdentifier       supportedCardBrands     }     googlePay {       displayName       supportedCardBrands       environment       googleAuthorization     }     ideal {       routeId       assetsUrl     }     kount {       merchantId     }     masterpass {       merchantCheckoutId       supportedCardBrands     }     paypal {       displayName       clientId       privacyUrl       userAgreementUrl       assetsUrl       environment       environmentNoNetwork       unvettedMerchant       braintreeClientId       billingAgreementsEnabled       merchantAccountId       currencyCode       payeeEmail     }     unionPay {       merchantAccountId     }     usBankAccount {       routeId       plaidPublicKey     }     venmo {       merchantId       accessToken       environment     }     visaCheckout {       apiKey       externalClientId       supportedCardBrands     }     braintreeApi {       accessToken       url     }     supportedFeatures   } }";
			var result = client.Execute(clientSdkMetadata, operationName, query);
			return result;
		}

		public dynamic TokenizeCreditCard(Input.Creditcard creditcard)
		{
			var client = new GraphQlClient(GraphQLSendboxUrl);
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
			var result = client.Execute(clientSdkMetadata, operationName, mutation, variableRequest);
			return result;
		}
	}
}
