using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class AuthService : BaseHcaService
    {
        public const string AUTH_LOGIN_ENDPOINT = "auth/login";
        public const string AUTH_LOGOUT_ENDPOINT = "auth/logout";

        public AuthService(IHttpClientService httpClientService) : base(httpClientService)
        {
        }

        public HcaResponse<LoginResult> Login(LoginRequest loginData)
        {
            return ExecuteJsonRequest<LoginResult>(AUTH_LOGIN_ENDPOINT, Method.POST, loginData);
        }

        public HcaVoidResponse Logout()
        {
            return ExecuteJsonRequest(AUTH_LOGOUT_ENDPOINT, Method.POST);
        }
    }
}