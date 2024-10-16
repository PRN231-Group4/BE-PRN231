using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WineManagementWeb.Models;

namespace WineManagementWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string SupplierURL;

        public BaseController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            var apiSettings = new ApiSettings();
            configuration.GetSection("ApiUrls").Bind(apiSettings);
            SupplierURL = apiSettings.SupplierURL;

        }
    }
}
