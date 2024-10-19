using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FE_WineManagement.Services
{
    public class AuthServices
    {
        private readonly HttpClient _httpClient;

        public AuthServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("/odata/auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<BaseResponseForLogin<LoginResponseModel>> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("/odata/auth/login", model);
            var response2 = await _httpClient.PostAsync("/odata/auth/login", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var Result = JsonConvert.DeserializeObject<BaseResponseForLogin<LoginResponseModel>>(await response2.Content.ReadAsStringAsync());
            return Result;
        }
    }
}
